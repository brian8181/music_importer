using System;
using System.Collections.Generic;
using System.Text;
using Utility.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace MusicImporter_Lib
{
    public class DataView
    {
        private IDatabase connection = null;
        private ISql sql = null;

        public void InsertSong(TagLib.Tag tag, TagLib.File tag_file, string art_id, object artist_id, object album_id)
        {
            // format the timespane (H:M:SS) 
            TimeSpan ts = tag_file.Properties.Duration;
            string h = ts.Hours != 0 ? ts.Hours.ToString() + ":" : "";
            string m = ts.Minutes != 0 ? ts.Minutes.ToString() : "0";
            string s = ts.Seconds < 10 ? "0" + ts.Seconds.ToString() : ts.Seconds.ToString();
            StringBuilder length = new StringBuilder(h + m + ":" + s);
            StringBuilder file = new StringBuilder(tag_file.Name);
            StringBuilder lyrics = ((tag.Lyrics != null) && (tag.Lyrics != string.Empty)) ?
                new StringBuilder(tag.Lyrics) : null;
            // change path to unix style
            file.Remove(0, 2);
            file.Replace('\\', '/');
            object song_id = GetKey("song", "file", file.ToString());
            DbCommand cmd = connection.Connection.CreateCommand();
            cmd.Parameters.Add("?artist_id", artist_id);
            cmd.Parameters.AddWithValue("?album_id", album_id);
            cmd.Parameters.AddWithValue("?track", tag.Track);
            cmd.Parameters.AddWithValue("?title", tag.Title);
            cmd.Parameters.AddWithValue("?file", file);
            cmd.Parameters.AddWithValue("?genre", tag.FirstGenre);
            cmd.Parameters.AddWithValue("?bitrate", tag_file.Properties.AudioBitrate.ToString());
            cmd.Parameters.AddWithValue("?length", length);
            // (MySql Year) the allowable values are 1901 to 2155, and 0000
            string year = (tag.Year == 0000 || (tag.Year > 1900 && tag.Year < 2155)) ? tag.Year.ToString() : "0000";
            cmd.Parameters.AddWithValue("?year", year);
            cmd.Parameters.AddWithValue("?comments", tag.Comment);

            cmd.CommandText = sql.INSERT_song;
            connection.ExecuteNonQuery(cmd);

        }

        public void InsertAlbum()
        {
            throw new System.NotImplementedException();
        }

        public void InsertArtist()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///  get primary key for a given column value 
        /// </summary>
        /// <param name="table">the table</param>
        /// <param name="column">the column</param>
        /// <param name="value">value to match</param>
        /// <returns>primary key</returns>
        private uint? GetKey(string table, string column, string value)
        {
            MySqlCommand command = new MySqlCommand("SELECT_last_update id FROM " + table + " WHERE " + column + "=?value LIMIT 1");
            command.Parameters.AddWithValue("?value", value);
            object obj = connection.ExecuteScalar(command);
            uint? result = (uint?)obj;
            // see if we have a result
            return result;
        }
    }
}
