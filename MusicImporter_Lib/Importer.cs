// Music Importer imports ID3 tags to a MySql database.
// Copyright (C) 2008  Brian Preston

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Security.Cryptography;
using BKP.Online.Data;
using BKP.Online.IO;
using BKP.Online;
using MusicImporter_Lib.Properties;

namespace MusicImporter_Lib
{
    public delegate void Int32Delegate(int value);
    /// <summary>
    /// imports music tag information into database 
    /// </summary>
    public class Importer
    {
        #region Events
        /// <summary>
        /// out sync error, occurs if scan is called while another scan in progress
        /// </summary>
        public event BKP.Online.VoidDelegate SyncError;
        /// <summary>
        /// tag scan started
        /// </summary>
        public event BKP.Online.VoidDelegate CreateDatabaseStarted;
        /// <summary>
        /// tag scan completed
        /// </summary>
        public event BKP.Online.VoidDelegate CreateDatabaseCompleted;
        /// <summary>
        /// tag scan started
        /// </summary>
        public event BKP.Online.VoidDelegate ScanStarted;
        /// <summary>
        /// tag scan completed
        /// </summary>
        public event BKP.Online.VoidDelegate ScanStopped;
        /// <summary>
        /// status message
        /// </summary>
        public event Int32Delegate Count;
        /// <summary>
        /// status message
        /// </summary>
        public event BKP.Online.StringDelegate Status;
        /// <summary>
        /// message
        /// </summary>
        public event BKP.Online.StringDelegate Message;
        /// <summary>
        ///  error message
        /// </summary>
        public event BKP.Online.StringDelegate Error;
        /// <summary>
        /// processing directory
        /// </summary>
        /// 
        public event BKP.Online.StringDelegate ProcessDirectory;
        #endregion

        #region Construction
        private SQLiteDatabase mm_connection = new SQLiteDatabase();
        private MySqlDatabase mysql_connection = new MySqlDatabase();
        private string[] files = null;
        private ImporterOptions options = null;
        private volatile bool running = false;
        private Thread thread = null;
        private string connect_string = string.Empty;
        private string mm_conn_str = string.Empty;
        private string art_path = string.Empty;
        private string version = "Auto";
        private ThreadPriority priority = ThreadPriority.BelowNormal;
        private ManualResetEvent pause = new ManualResetEvent( true );
        private int file_count;

        private Reporter reporter = new Reporter();
        private ArtImporter art_importer = null;

        /// <summary>
        /// default ctor intitialize from default Setting file
        /// </summary>
        public Importer()
            : this( "Auto", Settings.Default )
        {
        }
        /// <summary>
        /// default ctor intitialize from default Setting file
        /// </summary>
        public Importer(string version) : this( version, Settings.Default ) 
        {
        }
        /// <summary>
        ///  intialize from command line options
        /// </summary>
        /// <param name="options">command line options</param>
        public Importer( ImporterOptions options ) : this( (Settings)options )
        {
            this.options = options;
        }
        /// <summary>
        /// intitialize from Setting file
        /// </summary>
        public Importer( MusicImporter_Lib.Properties.Settings settings )
        {    
            Initialize( settings );
        }
        /// <summary>
        /// intitialize from Setting file
        /// </summary>
        public Importer( string version, MusicImporter_Lib.Properties.Settings settings )
        {
            this.version = version;
            Initialize( settings );
        }
        /// <summary>
        /// (Re)Initialize 
        /// </summary>
        public void Initialize( MusicImporter_Lib.Properties.Settings settings )
        {
            //logger throws exceptions!!
            //BKP.Online.Logger.Init();
            if(settings.use_conn_str)
            {
                connect_string =  settings.mysql_conn_str;
            }
            else
            {
                string cn_str = String.Format
                     ( "Persist Security Info=False;Data Source={0};Port={1};User Id={2};Password={3};Logging={4}",
                     settings.Address,
                     settings.Port,
                     settings.User,
                     settings.Pass,
                     settings.Log.ToString() );
                connect_string = cn_str;
            }
            mm_conn_str = settings.mm_conn_str;

            if(settings.insert_art)
            {
                art_path = Settings.Default.art_location;
                art_path = ( art_path.EndsWith( "\\" ) ? art_path : art_path + "\\" ) + ".album_art\\";
                Directory.CreateDirectory( art_path + "large\\" );
                Directory.CreateDirectory( art_path + "small\\" );
                Directory.CreateDirectory( art_path + "xsmall\\" );
            }
        }
        #endregion

        #region Control Functions
        /// <summary>
        /// get / set thread priority (ignored if not threaded) 
        /// </summary>
        public ThreadPriority Priority
        {
            get { return priority; }
            set 
            {
                if( thread != null )
                    this.thread.Priority = priority; 
                priority = value; 
            }
        }
        /// <summary>
        ///  connect to MySQL
        /// </summary>
        public void Connect()
        {
            try
            {
                mysql_connection.Open( connect_string );
                art_importer = new ArtImporter(mysql_connection, art_path);
            }
            catch(MySql.Data.MySqlClient.MySqlException e)
            {
                Close();
                throw e;
            }
            if(Settings.Default.ScanPlaylist)
            {
                mm_connection.Open( mm_conn_str );
            }
        }
        /// <summary>
        ///  begins file scan
        /// </summary>
        /// <param name="fork">false  single thread, true for thread to fork</param>
        public void Scan( bool fork )
        {
            if(running)
            {
                OnSyncError();
                return;
            }
            else
            {
                if(fork)
                {
                    thread = new Thread( new ThreadStart( Scan ) );
                    thread.Name = "Impoter Thread";
                    thread.IsBackground = true;
                    thread.Priority = priority;
                    thread.Start();
                    return;
                }
                else
                {
                    Scan();
                }
            }
        }
        /// <summary>
        /// thread function may or may not be an actual thread
        /// </summary>
        /// <param name="fork"></param>
        public void Scan()
        {
            if(running)
            {
                OnSyncError();
                return;
            }
            else
            {
                running = true;
                DateTime start = DateTime.Now;
                try
                {
                    OnCreateDatabaseStarted();

                    // Create DATABASE
                    string proc_path = Path.GetDirectoryName( Globals.ProcessPath() );
                    if(Settings.Default.create_db)
                    {
                        OnMessage( "creating database ..." );
                        string path = proc_path + "/create.sql";
                        if(!File.Exists( path ))
                        {
                            LogError( "create database failed could not find file \"create_music.sql\" " );
                            return;
                        }
                        DDLHelper db_mgr = new DDLHelper(mysql_connection);
                        string schema_name = Settings.Default.schema;

                        mysql_connection.ExecuteNonQuery("DROP DATABASE IF EXISTS " + schema_name);
                        db_mgr.CreateDatabase( schema_name );
                        db_mgr.ExecuteFile( path );
                       
                        // insert initial version 
                        mysql_connection.ExecuteNonQuery( 
                             "INSERT INTO `update` (`update`, `version`) VALUES( '1.0.0', 1 )" );
                    }

                    OnCreateDatabaseCompleted();
                    // check for stop signal
                    pause.WaitOne();
                    if(!running) return;

                    OnTagScanStarted();

                    try
                    {
                        // make sure database set
                        mysql_connection.ChangeDatabase( Settings.Default.schema );
                        UpdateDatabase(); // always update!
                                          
                        // SCAN TAGS
                        if(Settings.Default.ScanTags)
                        {
                            Status( "scanning tags ..." );
                            int len = Settings.Default.Dirs.Count;
                            // scan just root or all in list
                            if(len > 0)
                            {
                                for(int i = 0; i < len && running; ++i)
                                {
                                    Thread( Settings.Default.Dirs[i] );
                                }
                            }
                            else
                            {
                                Thread( Settings.Default.music_root );
                            }
                        }
                    }
                    catch(MySqlException exp)
                    {
                       OnError( exp.Message );
                       return;
                    }
                    OnProcessDirectory( "None." );

                    // check for stop signal
                    pause.WaitOne();
                    if(!running) return;

                    // SCAN PLAYLIST
                    if(Settings.Default.ScanPlaylist)
                    {
                        Status( "scanning playlist ..." );
                        ImportPlaylist();
                    }
                    
                    // check for stop signal
                    pause.WaitOne();
                    if(!running) return;

                    // CLEAN
                    if(Settings.Default.Clean)
                    {
                        Status( "cleaning ..." );
                        Clean();
                    }
                    
                    // check for stop signal
                    pause.WaitOne();
                    if(!running) return;

                    // RESCAN ART
                    //if(Settings.Default.RecanArt)
                    //{
                    //    Status( "scanning art directory ..." );
                    //    RescanArt();
                    //}

                    // check for stop signal
                    pause.WaitOne();
                    if(!running) return;

                    // OPITIMIZE
                    if(Settings.Default.Optimize)
                    {
                        Status( "optimizing ..." );
                        Optimize();
                    }
                }
                finally
                {
                    DateTime end = DateTime.Now;

                    TimeSpan ts =  (TimeSpan)( end - start );
                    string elapsed = ts.Hours.ToString("D") + ":"  + ts.Minutes.ToString("D")
                        + ":" + ts.Seconds.ToString( "D2" ); // +"::" + ts.Milliseconds.ToString( "D4" );

                    OnMessage( "Completed at " + end.ToShortTimeString() +
                                " elapsed time " + elapsed + ".");
                    Close();
                    OnTagScanStopped();
                    running = false;
                }
            }
        }
        /// <summary>
        ///  stops running scan
        /// </summary>
        public void StopScan()
        {
            running = false;
            pause.Set();    // unpase
        }
        /// <summary>
        /// continue after pause
        /// </summary>
        public void ContiueScan()
        {
            pause.Set();
        }
        /// <summary>
        /// pauses a running scan
        /// </summary>
        public void PauseScan()
        {
            pause.Reset();
        }
        /// <summary>
        /// thread function may or may not be an actual thread
        /// </summary>
        /// <param name="dir">base directory</param>
        private void Thread( string dir )
        {
            OnProcessDirectory( dir );
            files = DirectoryExt.GetFiles( dir, Settings.Default.file_mask );
            for(int i = 0; i < files.Length && running; ++i)
            {
                ++file_count;
                reporter.ScannedCount++;     
                OnCount( file_count );
                pause.WaitOne();
                if(!running) return;

                TagLib.Tag tag = null;
                TagLib.File tag_file = null;
                try
                {
                    tag_file = TagLib.File.Create( files[i] );
                    tag = tag_file.Tag;
                } 
                catch( TagLib.CorruptFileException e )
                {
                    LogError( "Exception: " + e.GetType().ToString() + " : " + e.Message );
                    continue;
                }
                catch( TagLib.UnsupportedFormatException e )
                {
                    LogError( "Exception: " + e.GetType().ToString() + " : " + e.Message );
                    continue;
                }
                catch(Exception e)
                {
                    LogError( "Exception: " + e.GetType().ToString() + " : " + e.Message );
                    continue;
                }
                //insert tag data
                object artist_id = InsertArtist( tag );
                object album_id = InsertAlbum( tag );
                //string art_id = InsertArt( tag, dir );
                object song_id = InsertSong(tag, tag_file, null, artist_id, album_id);
                string[] art_ids = art_importer.InsertArt(song_id, tag_file); // do not need ret val
            }
            string[] dirs = System.IO.Directory.GetDirectories( dir );
            for(int i = 0; i < dirs.Length && running; ++i)
            {
                pause.WaitOne();
                Thread( dirs[i] );
            }
        }
        /// <summary>
        /// close database
        /// </summary>
        public void Close()
        {
            mysql_connection.Close();
            if(mm_connection.connection.State == ConnectionState.Open)
                mm_connection.Close();
            Logger.DisposeLogger();
        }
        #endregion

        #region Tag Inserts
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private object InsertArtist( TagLib.Tag tag )
        {
            string artist = tag.FirstPerformer;
            object artist_id = null;
            if(artist != null && artist != string.Empty)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.AddWithValue( "?artist", artist );
                artist_id = GetKey( "artist", "artist", artist );
                if(artist_id == null) // not found
                {
                    cmd.CommandText = "INSERT INTO artist (artist) Values(?artist)";
                    mysql_connection.ExecuteNonQuery( cmd );
                    artist_id = mysql_connection.LastInsertID;
                }
            }
            return artist_id;
        }
        /// <summary>
        ///insert album
        /// </summary>
        /// <param name="tag">the id3 tag</param>
        /// <returns>primary key (insert id)</returns>
        private object InsertAlbum( TagLib.Tag tag )
        {
            string album = tag.Album;
            object album_id = null;
            if(album != null && album != string.Empty)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.AddWithValue( "?album", album );
                cmd.Parameters.AddWithValue( "?artist", tag.FirstAlbumArtist );
                album_id = GetKey( "album", "album", album );
                if(album_id == null) // not found
                {
                    cmd.CommandText = "INSERT INTO album (album, artist) Values(?album, ?artist)";
                    mysql_connection.ExecuteNonQuery( cmd );
                    album_id = mysql_connection.LastInsertID;
                }
                else
                {
                    cmd.Parameters.AddWithValue( "?album_id", album_id );
                    cmd.CommandText = "UPDATE album SET album=?album, artist=?artist WHERE id=?album_id";
                    mysql_connection.ExecuteNonQuery( cmd );
                }
            }
            return album_id;
        }
        /// <summary>
        ///  insert / update a song in database
        /// </summary>
        /// <param name="tag">id3 tag</param>
        /// <param name="tag_file">id3 tag file</param>
        /// <param name="art_id">sql art id</param>
        /// <param name="artist_id">sql artist id</param>
        /// <param name="album_id">sql album id</param>
        private object InsertSong(
            TagLib.Tag tag, TagLib.File tag_file, string art_id, object artist_id, object album_id )
        {
            // format the timespane (H:M:SS) 
            TimeSpan ts = tag_file.Properties.Duration;
            string h = ts.Hours != 0 ? ts.Hours.ToString() + ":" : "";
            string m = ts.Minutes != 0 ? ts.Minutes.ToString() : "0";
            string s = ts.Seconds < 10 ? "0" + ts.Seconds.ToString() : ts.Seconds.ToString();
            StringBuilder length = new StringBuilder( h + m + ":" + s );
            StringBuilder file = new StringBuilder( tag_file.Name );
            StringBuilder lyrics = ( ( tag.Lyrics != null ) && ( tag.Lyrics != string.Empty ) ) ?
                new StringBuilder( tag.Lyrics ) : null;
            // change path to unix style
            file.Remove( 0, 2 );
            file.Replace( '\\', '/' );
             object song_id = GetKey( "song", "file", file.ToString() );
            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.AddWithValue( "?artist_id", artist_id );
            cmd.Parameters.AddWithValue( "?album_id", album_id );
            cmd.Parameters.AddWithValue( "?track", tag.Track );
            cmd.Parameters.AddWithValue( "?title", tag.Title );
            cmd.Parameters.AddWithValue( "?file", file );
            cmd.Parameters.AddWithValue( "?genre", tag.FirstGenre );
            cmd.Parameters.AddWithValue( "?bitrate", tag_file.Properties.AudioBitrate.ToString() );
            cmd.Parameters.AddWithValue( "?length", length );
            // (MySql Year) the allowable values are 1901 to 2155, and 0000
            string year = (tag.Year == 0000 || (tag.Year > 1900 && tag.Year < 2155)) ? tag.Year.ToString() : "0000";    
            cmd.Parameters.AddWithValue( "?year", year );
            cmd.Parameters.AddWithValue( "?comments", tag.Comment );
            TagLib.Id3v2.Tag idv2 = null;
            try
            {
                idv2 = tag_file.GetTag(TagLib.TagTypes.Id3v2) as TagLib.Id3v2.Tag;
            }
            catch
            {
                // taglib throws an exception on some file types? 
            }
            string encoder = "NA";
            if(idv2 != null)
            {
                TagLib.Id3v2.TextInformationFrame frame =
                    TagLib.Id3v2.TextInformationFrame.Get( (TagLib.Id3v2.Tag)idv2, "TSSE", false );
                encoder = frame != null && frame.Text.Length > 0 ? frame.Text[0] : "Unknown";
            }
            cmd.Parameters.AddWithValue( "?encoder", encoder );
            cmd.Parameters.AddWithValue( "?file_size", tag_file.Length.ToString() );
            cmd.Parameters.AddWithValue( "?file_type", tag_file.MimeType );
            cmd.Parameters.AddWithValue( "?art_id", art_id );
            cmd.Parameters.AddWithValue( "?lyrics", lyrics );
            cmd.Parameters.AddWithValue( "?composer", tag.FirstComposer );
            cmd.Parameters.AddWithValue( "?conductor", tag.Conductor );
            cmd.Parameters.AddWithValue( "?copyright", tag.Copyright );
            cmd.Parameters.AddWithValue( "?disc", tag.Disc );
            cmd.Parameters.AddWithValue( "?disc_count", tag.DiscCount );
            cmd.Parameters.AddWithValue( "?performer", tag.FirstPerformer );
            cmd.Parameters.AddWithValue( "?tag_types", tag.TagTypes.ToString() );
            cmd.Parameters.AddWithValue( "?track_count", tag.TrackCount );
            cmd.Parameters.AddWithValue( "?beats_per_minute", tag.BeatsPerMinute );
            cmd.Parameters.AddWithValue( "?song_id", song_id );
            string sql = string.Empty;
            if(song_id == null)
            {
                sql = "INSERT INTO song (artist_id, album_id, track, title, file, genre, bitrate, length, year, comments, " +
                      "encoder, file_size, file_type, art_id, lyrics, composer, conductor, copyright, " +
                      "disc, disc_count, performer, tag_types, track_count, beats_per_minute) VALUES(" +
                      "?artist_id, ?album_id, ?track, ?title, ?file, ?genre, ?bitrate, ?length, ?year, ?comments, " +
                      "?encoder, ?file_size, ?file_type, ?art_id, ?lyrics, ?composer, ?conductor, ?copyright, " +
                      "?disc, ?disc_count, ?performer, ?tag_types, ?track_count, ?beats_per_minute)";
                OnMessage( "INSERTED: " + Path.GetFileName( tag_file.Name ) );
                cmd.CommandText = sql;
                mysql_connection.ExecuteNonQuery(cmd);
                song_id = mysql_connection.LastInsertID;
                reporter.InsertSongCount++;
            }
            else
            {
                sql = "UPDATE song SET artist_id=?artist_id, album_id=?album_id, track=?track, title=?title, file=?file, genre=?genre, " +
                      "bitrate=?bitrate, length=?length, year=?year, comments=?comments, encoder=?encoder, file_size=?file_size, file_type=?file_type, " +
                      "art_id=?art_id, lyrics=?lyrics, composer=?composer, conductor=?conductor, copyright=?copyright, disc=?disc, disc_count=?disc_count, " +
                      "performer=?performer, tag_types=?tag_types, track_count=?track_count, beats_per_minute=?beats_per_minute " +
                      "WHERE id = ?song_id";
                OnMessage( "UPDATED: " + Path.GetFileName( tag_file.Name ) );
                cmd.CommandText = sql;
                mysql_connection.ExecuteNonQuery(cmd);
                reporter.UpdateSongCount++;
            }
            return song_id;
        }
        /// <summary>
        /// import playlist
        /// </summary>
        private void ImportPlaylist()
        {
            // get all playlist from mediamonkey
            DataSet ds = mm_connection.ExecuteQuery( "SELECT * FROM Playlists WHERE ParentPlaylist=0 AND (IsAutoPlaylist<>1 OR IsAutoPlaylist IS NULL)" );
            // just truncate tables and recreate
            mysql_connection.ExecuteNonQuery( "TRUNCATE playlist_songs" );
            mysql_connection.ExecuteNonQuery( "TRUNCATE playlists" );
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                // insert all playlist
                long id = (long)row[0];
                string name = (string)row[1];
                name = name.Replace( "'", "''" );
                string sql = "INSERT INTO playlists Values( NULL, '" + name + "', NULL, 0, NULL, NULL )";
                mysql_connection.ExecuteNonQuery( sql );
                string playlist_id = mysql_connection.LastInsertID.ToString();
                string msg = "Creating playlist: " + name + " ..."; 
                OnMessage( msg );
                Log( msg );
                //  get the playlist id
                DataSet ds2 = mm_connection.ExecuteQuery( "SELECT * FROM PlaylistSongs WHERE IDPlaylist=" + id.ToString() );
                foreach(DataRow pl_row in ds2.Tables[0].Rows)
                {
                    DataSet ds3 = mm_connection.ExecuteQuery( "SELECT * FROM Songs WHERE ID=" + pl_row[2].ToString() );
                    if(ds3.Tables[0].Rows.Count > 0)
                    {
                        string path = (string)ds3.Tables[0].Rows[0][8];
                        long order = (long)pl_row[3];
                        path = path.Remove( 0, 16 );
                        path = path.Replace( "\\", "/" );
                        object song_id = GetKey( "song", "file", path );
                        if(song_id == null)
                            continue;
                        sql = "INSERT INTO playlist_songs Values( NULL, '" + playlist_id + "', '" + song_id + "', '" + order.ToString() + "', NULL, NULL )";
                        mysql_connection.ExecuteNonQuery( sql );
                        //Log( sql );
                    }
                }
            }
        }
        #endregion

        #region Maintainence
        /// <summary>
        /// 
        /// </summary>
        private void UpdateDatabase()
        {
            //db check for installed version
            int current_version = (int)mysql_connection.ExecuteScalar("SELECT MAX(version) FROM `music`.`update`");
            string proc_path = Path.GetDirectoryName( Globals.ProcessPath() );
            string[] files = Directory.GetFiles( proc_path, "update.?.?.?.sql" );
            SortedList<int, DatabaseVersion> versions = new SortedList<int, DatabaseVersion>();
            foreach(string f in files)
            {
                DatabaseVersion v = new DatabaseVersion( f );
                versions.Add( v.Version, v );
            }
            // apply in order
            DatabaseVersion update_2_ver = new DatabaseVersion( version );
            // version 0 = auto (upgrade to latest) 
            if( update_2_ver.Version == 0 || update_2_ver.Version > current_version )
            {
                foreach(DatabaseVersion ver in versions.Values)
                {
                    // version 0 = auto (upgrade to latest)
                    if(ver.Version > update_2_ver.Version && update_2_ver.Version != 0)
                        break;    
                    if(ver.Version <= (long)current_version)
                        continue;  // skip previous upgrades 

                    string sql = File.ReadAllText( ver.Filename );
                    mysql_connection.ExecuteNonQuery( sql );
                    mysql_connection.ExecuteNonQuery( "INSERT INTO `update` (`update`, `version`) VALUES( '"
                        + ver.ToString() + "', " + ver.Version + " )" );
                }
            }
        }
        /// <summary>
        /// delete orphaned songs (file no longer exsists)
        /// </summary>
        private void Clean()
        {
            DataSet ds = mysql_connection.ExecuteQuery( "SELECT file FROM song" );
            if(ds.Tables.Count != 1)
                return;
            DataTable dt = ds.Tables[0];
            StringBuilder file = null;
            MySqlCommand cmd = new MySqlCommand();
            // Why dosen't this work?
            //cmd.Parameters.AddWithValue("@file", file);
            //cmd.CommandText = "DELETE FROM song WHERE file=@file";
            foreach(DataRow row in dt.Rows)
            {
                file = new StringBuilder( row[0].ToString() );
                string path = "z:" + file.ToString();
                if(!File.Exists( path ))
                {
                    EscapeInvalidChars( file );
                    cmd.CommandText = "DELETE FROM song WHERE file='" + file + "'";
                    OnMessage( "Deleting: " + file );
                    mysql_connection.ExecuteNonQuery( cmd );
                    reporter.DeleteSongCount++;
                }
            }
        }
        /// <summary>
        ///  deletes any records from art table where file no longer exsits
        /// </summary>
        public void DeleteMissingArt()
        {
            //Clean Art By File
            DataSet ds = mysql_connection.ExecuteQuery( "SELECT file FROM art" );
            if(ds.Tables.Count != 1)
                return;
            DataTable dt = ds.Tables[0];
            StringBuilder file = null;
            MySqlCommand cmd = new MySqlCommand();
            foreach(DataRow row in dt.Rows)
            {
                file = new StringBuilder( row[0].ToString() );
                string path = Settings.Default.art_location;
                path = ( path.EndsWith( "\\" ) ? path : path + "\\" ) + file.ToString();
                if(!File.Exists( path ))
                {
                    EscapeInvalidChars( file );
                    cmd.CommandText = "DELETE FROM art WHERE file='" + file + "'";
                    //mysql_connection.ExecuteNonQuery(cmd);
                    reporter.DeleteArtCount++;

                }
            }
        }
       
        /// <summary>
        /// optimize tables (MySql)
        /// </summary>
        public void Optimize()
        {
            mysql_connection.ExecuteNonQuery( "OPTIMIZE TABLE artist" );
            mysql_connection.ExecuteNonQuery( "OPTIMIZE TABLE album" );
            mysql_connection.ExecuteNonQuery( "OPTIMIZE TABLE art" );
            mysql_connection.ExecuteNonQuery( "OPTIMIZE TABLE song" );
        }
        #endregion
        
        #region Utility Functions
        /// <summary>
        ///  get primary key for a given column value 
        /// </summary>
        /// <param name="table">the table</param>
        /// <param name="column">the column</param>
        /// <param name="value">value to match</param>
        /// <returns>primary key</returns>
        private uint? GetKey( string table, string column, string value )
        {
            MySqlCommand command = new MySqlCommand( "SELECT id FROM " + table + " WHERE " + column + "=?value LIMIT 1" );
            command.Parameters.AddWithValue( "?value", value );
            object obj = mysql_connection.ExecuteScalar( command );
            uint? result = (uint?)obj;
            // see if we have a result
            return result;
        }
        /// <summary>
        /// escape \ change invalid characters
        /// </summary>
        /// <param name="strs"></param>
        private void EscapeInvalidChars( params StringBuilder[] strs )
        {
            for(int i = 0; i < strs.Length; ++i)
            {
                strs[i].Replace( "'", "''" );
                strs[i].Replace( "’", "''" );
                strs[i].Replace( "`", "''" );
            }
        }
        #endregion

        #region Status \ Logging
        /// <summary>
        /// send msg to log
        /// </summary>
        /// <param name="str"></param>
        protected virtual void LogError( string msg )
        {
            Log( Logger.Level.Error, msg );
        }
        /// <summary>
        /// send msg to log
        /// </summary>
        /// <param name="str"></param>
        protected virtual void Log( string msg )
        {
            Log( Logger.Level.Information, msg );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        protected virtual void Log( Logger.Level level, string msg )
        {
            Trace.WriteLine( level, msg );
        }
        /// <summary>
        /// call status update
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnStatus( string msg )
        {
            if(Status != null) Status( msg );
        }
        /// <summary>
        /// call status update
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnMessage(string msg)
        {
            if(Message != null) Message( msg );
        }
        /// <summary>
        /// call Error
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnError( string msg )
        {
            LogError( msg ); 
            if(Error != null) Error( msg );
        }
        /// <summary>
        /// call ProcessDirectory
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnProcessDirectory( string msg )
        {
            if(ProcessDirectory != null) ProcessDirectory( msg );
        }
        /// <summary>
        /// call TagScanStarted
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnCreateDatabaseStarted()
        {
            if (CreateDatabaseStarted != null) CreateDatabaseStarted();
        }
        /// <summary>
        /// call TagScanStopped
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnCreateDatabaseCompleted()
        {
            if (CreateDatabaseCompleted != null) CreateDatabaseCompleted();
        }
        /// <summary>
        /// call TagScanStarted
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnTagScanStarted()
        {
            if(ScanStarted != null) ScanStarted();
        }
        /// <summary>
        /// call TagScanStopped
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnTagScanStopped()
        {
            if(ScanStopped != null) ScanStopped();
        }
        /// <summary>
        /// scans can not be started twice
        /// </summary>
        protected virtual void OnSyncError()
        {
            if(SyncError != null) SyncError();
        }
        /// <summary>
        /// scans can not be started twice
        /// </summary>
        protected virtual void OnCount( int value )
        {
            if(Count != null) Count(value);
        }
        #endregion
    }
}