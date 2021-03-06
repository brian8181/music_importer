// Music Importer imports ID3 tags to a MySql database.
// Copyright (C) 2008  Brian Preston

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
using Utility.Data;
using Utility.IO;
using Utility;
using MusicImporter_Lib.Properties;

namespace MusicImporter_Lib
{
    public delegate void StateDelegate(Importer.State state);
    public delegate void StateChangedDelegate(Importer.State new_state, Importer.State old_sate);
    public delegate void FileProcessingDelegate(string file, int total_files);
    /// <summary>
    /// imports music tag information into database 
    /// </summary>
    public class Importer
    {
        public enum State
        {
            Idle,
            Starting,
            PrepareStep, // not idle 
            Paused,
            CreateDB,
            CreatePlaylists,
            Scanning,
            Optimizing,
            Cleaning,
            Stopping
        }
        
        #region Events
        /// <summary>
        /// state change notification
        /// </summary>
        public event StateChangedDelegate StateChanged;
        /// <summary>
        /// message
        /// </summary>
        public event Utility.StringDelegate Message;
        /// file scanned specific
        /// <summary>
        /// status message
        /// </summary>
        public event FileProcessingDelegate FileProcessing;
        /// <summary>
        /// processing directory
        /// </summary>
        public event Utility.StringDelegate DirectoryProcessing;
        /// <summary>
        ///  error message
        /// </summary>
        public event Utility.StringDelegate Error;
        /// <summary>
        /// out sync error, occurs if scan is called while another scan in progress
        /// </summary>
        public event Utility.VoidDelegate SyncError;
        #endregion

        #region Construction
        //private SQLiteDatabase mm_connection = new SQLiteDatabase();
        private MySqlDatabase mysql_connection = new MySqlDatabase();
        private string[] files = null;
        private ImporterOptions options = null;
        private volatile bool running = false;
        private Thread thread = null;
        private string connect_string = string.Empty;
        private string mm_conn_str = string.Empty;
        private string art_path = string.Empty;
        private ThreadPriority priority = ThreadPriority.BelowNormal;
        private ManualResetEvent pause = new ManualResetEvent( true );
        private int file_count;
        private DDLHelper db_mgr = null;
        private Reporter reporter = new Reporter();
        private State current_state = State.Idle;
        private State last_state = State.Idle; 
        private ArtImporter art_importer = null;

        /// <summary>
        /// default ctor intitialize from default Setting file
        /// </summary>
        public Importer()
            : this( Settings.Default )
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
        /// 
        /// </summary>
        public Reporter Reporter
        {
            get { return reporter; }
            set { reporter = value; }
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
                     ( "Persist Security Info=False;Data Source={0};Port={1};User Id={2};Password={3};Logging=false",
                     settings.Address,
                     settings.Port,
                     settings.User_UTF8,
                     settings.Pass_UTF8,
                     settings.Log.ToString() );
                connect_string = cn_str;
            }
            mm_conn_str = settings.mm_conn_str;
            art_path = string.Format("{0}\\.album_art", Settings.Default.art_location.TrimEnd('\\'));
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
                if (Settings.Default.ScanPlaylist)
                {
                    //mm_connection.Open(mm_conn_str);
                }
                if (Settings.Default.insert_art)
                {
                    art_importer = new ArtImporter(mysql_connection, art_path);
                }
            }
            catch(Exception e)
            {
                Close();
                OnError(e.Message);
                throw e;
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
            if (fork)
            {
                thread = new Thread(new ThreadStart(Scan));
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

            OnStateChanged(State.Starting);
            OnStateChanged(State.PrepareStep);
            running = true;
            DateTime start = DateTime.Now;
            try
            {
                try
                {
                    // remove
                    OnStateChanged(State.CreateDB);
                    db_mgr = new DDLHelper(mysql_connection);

                    // Create DATABASE
                    if (Settings.Default.create_db)
                    {
                        OnMessage("Executing create scripts...");
                        string schema_name = Settings.Default.schema;
                        string sql = "DROP DATABASE IF EXISTS " + schema_name;
                        mysql_connection.ExecuteNonQuery(sql);
                        db_mgr.CreateDatabase(schema_name); 
                        db_mgr.ExecuteCreateScript();
                    }

                    OnMessage("Executing update scripts...");
                    // change to database
                    mysql_connection.ChangeDatabase(Settings.Default.schema);
                    // get version info
                    db_mgr.InitializeVersionInfo();
                    reporter.DBPeviousVersion = db_mgr.CurrentVersion.ToString();
                    reporter.DBVersion = db_mgr.UpdateVersion.ToString();
                    db_mgr.UpdateDatabase(); // update
                    OnMessage("Database has been updated");

                    // check for stop signal
                    pause.WaitOne();
                    if (!running) return;

                    OnStateChanged(State.PrepareStep);

                    // make sure database set
                    mysql_connection.ChangeDatabase(Settings.Default.schema);

                    // SCAN TAGS
                    if (Settings.Default.ScanTags)
                    {
                        OnStateChanged(State.Scanning);
                        int len = Settings.Default.Dirs.Count;
                        // scan just root or all in list
                        if (len > 0)
                        {
                            for (int i = 0; i < len && running; ++i)
                            {
                                Thread(Settings.Default.Dirs[i]);
                            }
                        }
                        else
                        {
                            Thread(Settings.Default.music_root);
                        }
                        OnStateChanged(State.PrepareStep);
                    }
                }
                catch (MySqlException exp)
                {
                    OnError(exp.Message);
                    return;
                }

                OnDirectoryProcessing("None.");

                // check for stop signal
                pause.WaitOne();
                if (!running) return;

                // check for stop signal
                pause.WaitOne();
                if (!running) return;

                // CLEAN
                if (Settings.Default.Clean)
                {
                    OnStateChanged(State.Cleaning);
                    if (Settings.Default.insert_art)
                    {
                        reporter.DeleteArtFileCount = art_importer.DeleteOrphanedFiles();
                        reporter.DeleteArtCount = art_importer.DeleteOrphanedInserts();
                    }
                    //reporter.DeleteSongCount = Clean();
                    OnStateChanged(State.PrepareStep);
                }

                // check for stop signal
                pause.WaitOne();
                if (!running) return;

                // OPITIMIZE
                if (Settings.Default.Optimize)
                {
                    OnStateChanged(State.Optimizing);
                    Optimize();
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                DateTime end = DateTime.Now;
                TimeSpan ts = (TimeSpan)(end - start);
                string elapsed = ts.Hours.ToString("D") + ":" + ts.Minutes.ToString("D")
                    + ":" + ts.Seconds.ToString("D2"); // +"::" + ts.Milliseconds.ToString( "D4" );

                OnMessage("Completed at " + end.ToShortTimeString() +
                            " elapsed time " + elapsed + ".");
                Close();
                OnStateChanged(State.Stopping);
                OnStateChanged(State.Idle);
                running = false;
            }
        }

        /// <summary>
        ///  stops running scan
        /// </summary>
        public void StopScan()
        {
            OnStateChanged(State.Idle);
            running = false;
            pause.Set();    // unpase
        }

        /// <summary>
        /// continue after pause
        /// </summary>
        public void ContiueScan()
        {
            OnStateChanged(last_state);
            pause.Set();
        }

        /// <summary>
        /// pauses a running scan
        /// </summary>
        public void PauseScan()
        {
            OnStateChanged(State.Paused);
            pause.Reset();
        }

        /// <summary>
        /// thread function may or may not be an actual thread
        /// </summary>
        /// <param name="dir">base directory</param>
        private void Thread( string dir )
        {
            OnDirectoryProcessing( dir );
            files = DirectoryExt.GetFiles( dir, Settings.Default.file_mask );
            for(int i = 0; i < files.Length && running; ++i)
            {
                ++file_count;
                reporter.ScannedCount++;     
                OnFileProcessing( files[i], file_count );
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
                    OnError( "Exception: " + e.GetType().ToString() + " : " + e.Message );
                    reporter.AddCorruptFile(files[i]);
                    continue;
                }
                catch( TagLib.UnsupportedFormatException e )
                {
                    OnError( "Exception: " + e.GetType().ToString() + " : " + e.Message );
                    continue;
                }
                catch(Exception e)
                {
                    // unknown error should I stop ?
                    OnError( "Exception: " + e.GetType().ToString() + " : " + e.Message );
                    continue;
                }

                try
                {
                    //insert tag data
                    object artist_id = InsertArtist(tag);
                    object album_id = InsertAlbum(tag);
                    //string art_id = InsertArt( tag, dir );
                    object song_id = InsertSong(tag, tag_file, null, artist_id, album_id);
                    if (Settings.Default.insert_art)
                    {
                        uint c = art_importer.InsertArt(song_id, tag_file); // do not need ret val
                        reporter.InsertArtCount += c;
                    }
                }
                catch (Exception e)
                {
                }
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
            //if(mm_connection.Connection.State == ConnectionState.Open)
            //    mm_connection.Close();
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
                    reporter.InsertArtistCount++;
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
                    reporter.InsertAlbumCount++;
                }
                else
                {
                    cmd.Parameters.AddWithValue( "?album_id", album_id );
                    cmd.CommandText = "UPDATE album SET album=?album, artist=?artist WHERE id=?album_id";
                    mysql_connection.ExecuteNonQuery( cmd );
                    reporter.UpdateAlbumCount++;
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
            catch(Exception e)
            {
                // taglib throws an exception on some file types? 
                OnError(e.Message);
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
            byte[] sha1 = null;
            if (Settings.Default.compute_sha1)
            {
                sha1 = TagLibExt.MediaSHA1(tag_file);
                string hex = Utility.Functions.Bytes2HexString(sha1);
                Trace.WriteLine("SHA1 - " + hex, Logger.Level.Information.ToString());
            }
            cmd.Parameters.AddWithValue("?sha1", sha1);
            string sql = string.Empty;
            if(song_id == null)
            {
                sql = "INSERT INTO song (artist_id, album_id, track, title, file, genre, bitrate, length, year, comments, " +
                      "encoder, file_size, file_type, art_id, lyrics, composer, conductor, copyright, " +
                      "disc, disc_count, performer, tag_types, track_count, beats_per_minute, sha1) VALUES(" +
                      "?artist_id, ?album_id, ?track, ?title, ?file, ?genre, ?bitrate, ?length, ?year, ?comments, " +
                      "?encoder, ?file_size, ?file_type, ?art_id, ?lyrics, ?composer, ?conductor, ?copyright, " +
                      "?disc, ?disc_count, ?performer, ?tag_types, ?track_count, ?beats_per_minute, ?sha1)";
                OnMessage( "INSERTED SONG: " + Path.GetFileName( tag_file.Name ) );
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
                      "performer=?performer, tag_types=?tag_types, track_count=?track_count, beats_per_minute=?beats_per_minute, sha1=?sha1 " +
                      "WHERE id = ?song_id";
                OnMessage( "UPDATED SONG: " + Path.GetFileName( tag_file.Name ) );
                cmd.CommandText = sql;
                mysql_connection.ExecuteNonQuery(cmd);
                reporter.UpdateSongCount++;
            }
            return song_id;
        }
        #endregion

        #region Maintainence
        /// <summary>
        /// delete orphaned songs inserts when file no longer exists
        /// </summary>
        private int Clean()
        {
            int deleted = 0;
            DataSet ds = mysql_connection.ExecuteQuery( "SELECT file FROM song" );
            if(ds.Tables.Count != 1)
                return 0;
            DataTable dt = ds.Tables[0];
            foreach(DataRow row in dt.Rows)
            {
                string file = row[0].ToString();
                string root = Properties.Settings.Default.music_root.TrimEnd('\\', '/');
                string path = string.Format("{0}//{1}", root, file);
                if(!File.Exists( path ))
                {
                    OnMessage( "Deleting: " + file );
                    deleted += db_mgr.DeleteSong(file);
                    reporter.DeleteSongCount++;
                }
            }
            return deleted;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int DeleteOrphanedArtist()
        {
            string sql = "DELETE FROM artist LEFT JOIN song ON artist.id=artist_id WHERE artist_id IS NULL";
            return mysql_connection.ExecuteNonQuery( sql );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int DeleteOrphanedAlbums()
        {
            string sql = "DELETE FROM artist LEFT JOIN song ON artist.id=artist_id WHERE artist_id IS NULL";
            return mysql_connection.ExecuteNonQuery( sql );
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
            mysql_connection.ExecuteNonQuery("OPTIMIZE TABLE song_art");
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
        #endregion

        #region Status \ Logging
        /// <summary>
        /// call status update
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnStateChanged(State state)
        {
            lock (this)
            {
                last_state = current_state;
                current_state = state;
            }
            Trace.WriteLine("State=" + state.ToString(), Logger.Level.Information.ToString());
            if (StateChanged != null) StateChanged(current_state, last_state);
        }

        /// <summary>
        /// call status update
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnMessage(string msg)
        {
            Trace.WriteLine(msg, Logger.Level.Information.ToString());
            if(Message != null) Message( msg );
        }

        /// <summary>
        /// call Error
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnError( string msg )
        {
            Trace.WriteLine(msg, Logger.Level.Error.ToString());
            if(Error != null) Error( msg );
        }

        /// <summary>
        /// call ProcessDirectory
        /// </summary>
        /// <param name="msg">status message</param>
        protected virtual void OnDirectoryProcessing( string dir )
        {
            Trace.WriteLine("Processing directory - " + dir, Logger.Level.Information.ToString());
            if(DirectoryProcessing != null) DirectoryProcessing( dir );
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnSyncError()
        {
            if(SyncError != null) SyncError();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnFileProcessing( string file, int value )
        {
            Trace.WriteLine("Processing file - " + file, Logger.Level.Information.ToString());
            if(FileProcessing != null) FileProcessing(file, value);
        }
        #endregion
    }
}