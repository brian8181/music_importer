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

namespace MusicImporter.TagLibV
{
    /// <summary>
    /// imports music tag information into database 
    /// </summary>
    public class Importer
    {
        /// <summary>
        /// out sync error, occurs if scan is called while another scan in progress
        /// </summary>
        public event BKP.Online.VoidDelegate SyncError;
        /// <summary>
        /// tag scan started
        /// </summary>
        public event BKP.Online.VoidDelegate TagScanStarted;
        /// <summary>
        /// tag scan completed
        /// </summary>
        public event BKP.Online.VoidDelegate TagScanStopped;
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
        public event BKP.Online.StringDelegate ProcessDirectory;
        private SQLiteDatabase mm_connection = new SQLiteDatabase();
        private MySqlDatabase mysql_connection = new MySqlDatabase();
        //private string root_path = null;
        private string[] files = null;
        private ImporterOptions options = null;
        private volatile bool running = false;
        private Thread thread = null;
        private string connect_string = string.Empty;
        private string mm_conn_str = string.Empty;

        /// <summary>
        /// default ctor intitialize from default Setting file
        /// </summary>
        public Importer() : this( Settings.Default ) 
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
            BKP.Online.Logger.Init();
            Initialize( settings );
        }
        /// <summary>
        /// (Re)Initialize 
        /// </summary>
        public void Initialize( MusicImporter_Lib.Properties.Settings settings )
        {
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
        }
        /// <summary>
        ///  connect to MySQL
        /// </summary>
        public void Connect()
        {
            try
            {
                mysql_connection.Open( connect_string );  
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
                    thread.Priority = ThreadPriority.BelowNormal;
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
                    OnTagScanStarted();
                   
                    OnMessage( "creating datsbase ..." );
                    // CRATE DATABASE
                    if(Settings.Default.create_db)
                    {
                        if(!File.Exists( "recreate_music.sql" ))
                        {
                            LogError( "create database failed could not find file \"recreate_music.sql\" " );
                            return;
                        }
                        string sql = File.ReadAllText( "recreate_music.sql" );
                        mysql_connection.ExecuteNonQuery( "CREATE DATABASE IF NOT EXISTS " + Settings.Default.schema );
                        mysql_connection.ChangeDatabase( Settings.Default.schema );
                        mysql_connection.ExecuteNonQuery( sql );
                    }
                    // check for stop signal
                    if(!running) return;

                    // make sure database set
                    mysql_connection.ChangeDatabase( Settings.Default.schema );

                    Status( "scanning tags ..." );
                    // SCAN TGAGS
                    if(Settings.Default.ScanTags)
                    {
                        int len = Settings.Default.Dirs.Count;
                        for(int i = 0; i < len && running; ++i)
                        {
                            Thread( Settings.Default.Dirs[i] );
                        }
                    }
                    OnProcessDirectory( "None." );

                    // check for stop signal
                    if(!running) return;

                    Status( "scanning playlist ..." );
                    // SCAN PLAYLIST
                    if(Settings.Default.ScanPlaylist)
                    {
                        ImportPlaylist();
                    }
                    // check for stop signal
                    if(!running) return;
                   
                    Status( "cleaning ..." );
                    // CLEAN
                    if(Settings.Default.Clean)
                    {
                        Clean();
                    }
                    
                    // check for stop signal
                    if(!running) return;
                    Status( "scanning art directory ..." );
                    // RESCAN ART
                    if(Settings.Default.RecanArt)
                    {
                        ScanArt();
                    }

                    // check for stop signal
                    if(!running) return;

                    Status( "optimizing ..." );
                    // OPITIMIZE
                    if(Settings.Default.Optimize)
                    {
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
        /// 
        /// </summary>
        public void StopScan()
        {
            running = false;
            //thread.Join();
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
                TagLib.Tag tag = null;
                TagLib.File tag_file = null;
                try
                {
                    tag_file = TagLib.File.Create( files[i] );
                    tag = tag_file.Tag;
                    //OnMessage( "processing " + files[i] );
                } 
                //catch(TagLib.CorruptFileException)
                //{
                //}
                //catch( TagLib.UnsupportedFormatException )
                //{
                //}
                catch(Exception e)
                {
                    OnError( "Exception: " + e.GetType().ToString() + " : " + e.Message );
                    continue;
                }
                //insert tag data
                object artist_id = InsertArtist( tag );
                object album_id = InsertAlbum( tag );
                string art_id = InsertArt( tag, dir );
                InsertSong( tag, tag_file, art_id, artist_id, album_id );
            }
            string[] dirs = System.IO.Directory.GetDirectories( dir );
            for(int i = 0; i < dirs.Length && running; ++i)
            {
                Thread( dirs[i] );
            }
        }
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
                    artist_id = mysql_connection.ExecuteScalar( "SELECT LAST_INSERT_ID()" );
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
                    album_id = mysql_connection.ExecuteScalar( "SELECT LAST_INSERT_ID()" );
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
        ///  insert album art
        /// </summary>
        /// <param name="tag">the id3 tag</param>
        /// <param name="current_dir">current directory</param>
        /// <returns>primary key (insert id)</returns>
        private string InsertArt( TagLib.Tag tag, string current_dir )
        {
            string art = null;
            string key = null;
            byte[] hash = null;
            string[] files = DirectoryExt.GetFiles( current_dir, Settings.Default.art_mask );
            if(tag.Pictures.Length > 0 || files.Length > 0)
            {
                Guid guid = Guid.NewGuid();
                byte[] data = null;
                string type = string.Empty;
                string mime_type = string.Empty;
                string description = string.Empty;
                if(tag.Pictures.Length > 0)
                {
                    TagLib.IPicture pic = tag.Pictures[0];
                    if(pic.MimeType.StartsWith( "image/" ))
                    {
                        art = guid.ToString( "B" ) + pic.MimeType.Replace( "image/", "." );
                        data = new byte[pic.Data.Count];
                        pic.Data.CopyTo( data, 0 );
                        type = pic.Type.ToString();
                        mime_type = pic.MimeType;
                        description = pic.Description;
                    }
                    else { return null; }
                }
                else
                {
                    string ext = Path.GetExtension( files[0] );
                    art = guid.ToString( "B" ) + ext;
                    data = File.ReadAllBytes( files[0] );
                    type = "Cover";
                    mime_type = ext;
                    description = "cover art";
                }
                //string path = System.IO.Path.GetPathRoot( root_path ) + ".album_art\\in\\" + art;
                string path = Settings.Default.art_location;
                path = ( path.EndsWith( "\\" ) ? path : path + "\\" ) + art;
                hash = ComputeHash( data );
                string sql = "SELECT id FROM art WHERE hash=?hash";
                MySqlCommand cmd = new MySqlCommand( sql );
                cmd.Parameters.AddWithValue( "?hash", hash );
                //BKP TODO: make a try / catch for overall function 
                object obj = null;
                try
                {
                    obj = mysql_connection.ExecuteScalar( cmd );
                } catch
                {
                    return null;
                }
                if(obj == null)
                {
                    System.IO.File.WriteAllBytes( path, data );
                    sql = "INSERT INTO art VALUES(NULL, ?file, ?type, ?hash, ?description, ?mime_type, NULL, NOW())";
                    cmd = new MySqlCommand( sql );
                    cmd.Parameters.AddWithValue( "?file", art );
                    cmd.Parameters.AddWithValue( "?type", type );
                    cmd.Parameters.AddWithValue( "?hash", hash );
                    cmd.Parameters.AddWithValue( "?description", description );
                    cmd.Parameters.AddWithValue( "?mime_type", mime_type );
                    mysql_connection.ExecuteNonQuery( cmd );
                    key = mysql_connection.ExecuteScalar( "SELECT LAST_INSERT_ID()" ).ToString();
                }
                else
                {
                    key = ( (uint)obj ).ToString();
                }
            }
            return key;
        }
        /// <summary>
        /// (Re)Scan and insert art from art directory
        /// </summary>
        private void ScanArt()
        {
            byte[] hash = null;
            string[] files = DirectoryExt.GetFiles( Settings.Default.art_location, "*.jpg;*.jpeg;*.png;*.bmp;*.gif" );
            for(int i = 0; i < files.Length; ++i)
            {
                OnMessage( "Processing Art: " + files[i] );
                string filename = Path.GetFileName( files[i] );
                string ext = Path.GetExtension( files[i] );
                byte[] data = null;
                string type = string.Empty;
                string mime_type = string.Empty;
                string description = string.Empty;
                data = File.ReadAllBytes( files[i] );
                type = "Cover";
                mime_type = ext;
                description = "cover art";
                hash = ComputeHash( data );
                string sql = "SELECT id FROM art WHERE hash=?hash";
                MySqlCommand cmd = new MySqlCommand( sql );
                cmd.Parameters.AddWithValue( "?hash", hash );
                object obj = null;
                obj = mysql_connection.ExecuteScalar( cmd );
                try
                {
                    obj = mysql_connection.ExecuteScalar( cmd );
                } catch
                {
                    //return;
                }
                if(obj == null)
                {
                    sql = "INSERT INTO art VALUES(NULL, ?file, ?type, ?hash, ?description, ?mime_type, NULL, NOW())";
                    cmd = new MySqlCommand( sql );
                    cmd.Parameters.AddWithValue( "?file", filename );
                    cmd.Parameters.AddWithValue( "?type", type );
                    cmd.Parameters.AddWithValue( "?hash", hash );
                    cmd.Parameters.AddWithValue( "?description", description );
                    cmd.Parameters.AddWithValue( "?mime_type", mime_type );
                    mysql_connection.ExecuteNonQuery( cmd );
                }
            }
        }
        /// <summary>
        /// todo
        /// </summary>
        private void Prepare()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Prepare();
        }
        /// <summary>
        ///  insert / update a song in database
        /// </summary>
        /// <param name="tag">id3 tag</param>
        /// <param name="tag_file">id3 tag file</param>
        /// <param name="art_id">sql art id</param>
        /// <param name="artist_id">sql artist id</param>
        /// <param name="album_id">sql album id</param>
        private void InsertSong(
            TagLib.Tag tag, TagLib.File tag_file, string art_id, object artist_id, object album_id )
        {
            // format the timespane (H:M:SS) 
            TimeSpan ts = tag_file.Properties.Duration;
            string h = ts.Hours != 0 ? ts.Hours.ToString() + ":" : "";
            string m = ts.Minutes != 0 ? ts.Minutes.ToString() : "0";
            string s = ts.Seconds < 10 ? "0" + ts.Seconds.ToString() : ts.Seconds.ToString();
            StringBuilder length = new StringBuilder( h + m + ":" + s );
            StringBuilder file = new StringBuilder( tag_file.Name );
            //StringBuilder mode = new StringBuilder(tag_file.Mode.ToString());
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
            cmd.Parameters.AddWithValue( "?encoder", "NA" );
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
            cmd.Parameters.AddWithValue( "?tag_types", "Not Implemented" );
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
            }
            else
            {
                sql = "UPDATE song SET artist_id=?artist_id, album_id=?album_id, track=?track, title=?title, file=?file, genre=?genre, " +
                      "bitrate=?bitrate, length=?length, year=?year, comments=?comments, encoder=?encoder, file_size=?file_size, file_type=?file_type, " +
                      "art_id=?art_id, lyrics=?lyrics, composer=?composer, conductor=?conductor, copyright=?copyright, disc=?disc, disc_count=?disc_count, " +
                      "performer=?performer, tag_types=?tag_types, track_count=?track_count, beats_per_minute=?beats_per_minute " +
                      "WHERE id = ?song_id";
                OnMessage( "UPDATED: " + Path.GetFileName( tag_file.Name ) );
            }
            cmd.CommandText = sql;
            mysql_connection.ExecuteNonQuery( cmd );
        }
        /// <summary>
        /// delete oprhaned songs (file no longer exsists)
        /// </summary>
        private void Clean()
        {
            DataSet ds = mysql_connection.ExecuteQuery( "SELECT file FROM song" );
            DataTable dt = ds.Tables[0];
            StringBuilder file = null;
            MySqlCommand cmd = new MySqlCommand();
            // Why dosen't this work?
            //cmd.Parameters.AddWithValue("@file", file);
            //cmd.CommandText = "DELETE FROM song WHERE file=@file";
            int c = 0;
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
                    ++c;
                }
            }
            //Clean Art By File
            ds = mysql_connection.ExecuteQuery( "SELECT file FROM art" );
            dt = ds.Tables[0];
            file = null;
            cmd = new MySqlCommand();
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
                }
            }
        }
        /// <summary>
        /// import playlist
        /// </summary>
        private void ImportPlaylist()
        {
            // get all playlist from mediamonkey
            DataSet ds = mm_connection.ExecuteQuery( "SELECT * FROM Playlists WHERE ParentPlaylist=0 AND IsAutoPlaylist=0" );
            // just truncate tables and recreate
            mysql_connection.ExecuteNonQuery( "TRUNCATE playlist_songs" );
            mysql_connection.ExecuteNonQuery( "TRUNCATE playlists" );
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                // insert all playlist
                long id = (long)row[0];
                string name = (string)row[1];
                name = name.Replace( "'", "''" );
                //BKP:TODO use ExecuteScalar
                string sql = "INSERT INTO playlists Values( NULL, '" + name + "', NULL, 0 )";
                mysql_connection.ExecuteNonQuery( sql );
                string playlist_id = mysql_connection.ExecuteScalar( "SELECT LAST_INSERT_ID()" ).ToString();
                //Log( sql );
                OnMessage( "Creating playlist: " + name + " ..." );
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
                        sql = "INSERT INTO playlist_songs Values( NULL, '" + playlist_id + "', '" + song_id + "', '" + order.ToString() + "' )";
                        mysql_connection.ExecuteNonQuery( sql );
                        //Log( sql );
                    }
                }
            }
        }
        /// <summary>
        ///  get primary key for a given column value 
        /// </summary>
        /// <param name="table">the table</param>
        /// <param name="column">the column</param>
        /// <param name="value">value to match</param>
        /// <returns>primary key</returns>
        public uint? GetKey( string table, string column, string value )
        {
            MySqlCommand command = new MySqlCommand( "SELECT id FROM " + table + " WHERE " + column + "=?value LIMIT 1" );
            //command.Parameters.AddWithValue("?table", table);
            //command.Parameters.AddWithValue("?column", column);
            command.Parameters.AddWithValue( "?value", value );
            object obj = mysql_connection.ExecuteScalar( command );
            uint? result = (uint?)obj;
            // see if we have a result
            return result;
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
        /// <summary>
        /// compute a hash value
        /// </summary>
        /// <param name="data">data to hash</param>
        /// <returns>the hash value</returns>
        private byte[] ComputeHash( byte[] data )
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash( data );
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
                strs[i].Replace( "�", "''" );
                strs[i].Replace( "`", "''" );
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
        ///// <summary>
        ///// send msg to stdout & log
        ///// </summary>
        ///// <param name="str"></param>
        //protected virtual void Out( string str )
        //{
        //    //if(Settings.Default.Log)
        //    //    Trace.WriteLine( str );
        //    //OnStatus( str );
        //}
        ///// <summary>
        ///// send msg to log
        ///// </summary>
        ///// <param name="str"></param>
        //protected virtual void Log( string str )
        //{
        //    if(Settings.Default.Log)
        //        Trace.WriteLine( str );
        //    //OnStatus( str );
        //}
        /// <summary>
        /// send msg to log
        /// </summary>
        /// <param name="str"></param>
        protected virtual void LogError( string str )
        {
            Trace.WriteLine( Logger.Level.Error, str );
            OnError( str );
        }
        ///// <summary>
        ///// call status update
        ///// </summary>
        ///// <param name="msg">status message</param>
        //protected virtual void OnStatus( string msg )
        //{
        //    if(Status != null) Status( msg );
        //}
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
        protected virtual void OnTagScanStarted()
        {
            if(TagScanStarted != null) TagScanStarted();
        }
        /// <summary>
        /// call TagScanStopped
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnTagScanStopped()
        {
            if(TagScanStopped != null) TagScanStopped();
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnSyncError()
        {
            if(SyncError != null) SyncError();
        }
    }
}
