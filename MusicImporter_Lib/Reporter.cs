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
        private string ListFragment(string name, string style)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<tr><td {1}>{0} INSERT Count:</td><td {1}>{{0}}</tr></td>", name, style);
            sb.AppendFormat("<tr><td {1}>{0} DELETE Count:</td><td {1}>{{1}}</tr></td>", name, style);
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
            
            string dt = DateTime.Now.ToString(BKP.Online.Globals.PrettyLogDateFormat);   
            sb.AppendFormat("<h2>{0}</h2>\r\n", dt);
            sb.AppendLine("<div style=\"text-align: center\">");

            string style = "style=\"font-size: 9pt\"";

            sb.AppendLine("<table>");
            sb.AppendFormat("<tr><td {1}>Scanned Count:</td><td {1}>{0}</td></tr>", scanned_count, style);
            sb.AppendFormat("<tr><td {1}>Corrupt Files Count:</td><td {1}>{0}</td></tr>", CorruptFileCount, style);
            sb.AppendFormat("<tr><td {1}>Song INSERT Count:</td><td {1}>{0}</td></tr>", insert_song_count, style);
            sb.AppendFormat("<tr><td {1}>Song UPDATE Count:</td><td {1}>{0}</td></tr>", update_song_count, style);
            sb.AppendFormat("<tr><td {1}>Song DELETE Count:</td><td {1}>{0}</td></tr>", update_song_count, style);
         
            sb.AppendLine("<br />");
            sb.AppendFormat(ListFragment("Artist", style),
                insert_artist_count, delete_artist_count);

            sb.AppendLine("<br />");
            sb.AppendFormat(ListFragment("Album", style),
                insert_album_count, delete_album_count);

            sb.AppendLine("<br />");
            sb.AppendFormat(ListFragment("Art", style),
                insert_art_count, delete_art_count);
            
            sb.AppendLine("<br />");
            sb.AppendFormat("<tr><td {1}>Delete Art File Count:</td><td {1}>{0}</tr></td>", delete_art_file_count, style);
            sb.AppendLine("</table>");
            
            sb.AppendLine("</div></div></body></html>");
            return sb.ToString();
        }
    }
}
