using System;
using System.Collections.Generic;
using System.Text;

namespace MusicImporter_Lib
{
    public class Reporter
    {
        private int scanned_count = 0;
        private List<string> corrupt_files = new List<string>();
        private int insert_song_count = 0;
        private int update_song_count = 0;
        private int delete_song_count = 0;
        private int insert_album_count = 0;
        private uint delete_album_count = 0;
        private int insert_artist_count = 0;
        private int delete_artist_count = 0;
        private int insert_art_count = 0;
        private uint delete_art_count = 0;
        private uint delete_art_file_count = 0;

        public int ScannedCount
        {
            get { return scanned_count; }
            set { scanned_count = value; }
        }
        public int CorruptFileCount
        {
            get { return corrupt_files.Count; }
        }
        public string[] CorruptFiles
        {
            get { return corrupt_files.ToArray(); }
        }
        public int InsertSongCount
        {
            get { return insert_song_count; }
            set { insert_song_count = value; }
        }
        public int DeleteSongCount
        {
            get { return delete_song_count; }
            set { delete_song_count = value; }
        }
        public int UpdateSongCount
        {
            get { return update_song_count; }
            set { update_song_count = value; }
        }
        public int InsertAlbumCount
        {
            get { return insert_album_count; }
            set { insert_album_count = value; }
        }
        public uint DeleteAlbumCount
        {
            get { return delete_album_count; }
            set { delete_album_count = value; }
        }
        public int InsertArtistCount
        {
            get { return insert_artist_count; }
            set { insert_artist_count = value; }
        }
        public int DeleteArtistCount
        {
            get { return delete_artist_count; }
            set { delete_artist_count = value; }
        }
        public uint DeleteArtCount
        {
            get { return delete_art_count; }
            set { delete_art_count = value; }
        }
        public uint DeleteArtFileCount
        {
            get { return delete_art_file_count; }
            set { delete_art_file_count = value; }
        }
        /// <summary>
        /// add a corrupt file to list
        /// </summary>
        /// <param name="file_name">the file name</param>
        public void AddCorruptFile(string file_name)
        {
            corrupt_files.Add(file_name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string ListFragment(string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            sb.AppendFormat("<li>{0} INSERT Count: {{0}}</li>", name);
            sb.AppendFormat("<li>{0} DELETE Count: {{1}}</li>", name);
            sb.Append("</ul>");
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetHTML()
        {
            StringBuilder sb = new StringBuilder("<html><head>");
            sb.AppendLine( "<style type=\"text/css\">" );
            sb.AppendLine("body{ font-family: Arial; }");
            sb.AppendLine( "</style>" );
		
            sb.AppendLine("<h2>Report:</h2>");
            sb.AppendLine("<div style=\"text-align: center; font-size: 9pt;\">");
   
            sb.AppendLine("<ul>");
            sb.AppendFormat("<li>Scanned Count: {0}</li>", scanned_count);
            sb.AppendFormat("<li>Corrupt Files Count: {0}</li>", CorruptFileCount);
            sb.AppendFormat("<li>Song INSERT Count: {0}</li>", insert_song_count);
            sb.AppendFormat("<li>Song UPDATE Count: {0}</li>", update_song_count);
            sb.AppendFormat("<li>Song DELETE Count: {0}</li>", update_song_count);
            sb.AppendLine("</ul>");

            sb.AppendLine("<br />");
            sb.AppendFormat( ListFragment("Artist"), 
                insert_artist_count, delete_artist_count );

            sb.AppendLine("<br />");
            sb.AppendFormat(ListFragment("Album"),
                insert_album_count, delete_album_count);

            sb.AppendLine("<br />");
            sb.AppendFormat(ListFragment("Art"),
                insert_art_count, delete_art_count);
            sb.AppendLine("<ul>");
            sb.AppendFormat("<li>Delete Art File Count: {0}</li>", delete_art_file_count);
            sb.AppendLine("</ul>");


            sb.AppendLine("</div></div></body></html>");
            return sb.ToString();
        }
    }
}
