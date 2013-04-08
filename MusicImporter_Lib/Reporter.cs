using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Utility;

namespace MusicImporter_Lib
{
    /// <summary>
    /// collect and generate import reports
    /// </summary>
    public class Reporter
    {
        private List<string> corrupt_files = new List<string>();
        private DateTime timestamp = DateTime.Now;
        private string db_version = string.Empty;
        private uint scanned_count = 0;
        private uint insert_song_count = 0;
        private uint update_song_count = 0;
        private uint delete_song_count = 0;
        private uint insert_album_count = 0;
        private uint update_album_count = 0;
        private uint delete_album_count = 0;
        private uint insert_artist_count = 0;
        private uint delete_artist_count = 0;
        private uint insert_art_count = 0;
        private uint delete_art_count = 0;
        private uint delete_art_file_count = 0;
        private string db_prev_version = string.Empty;
        
        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
        public string DBPeviousVersion
        {
            get { return db_prev_version; }
            set { db_prev_version = value; }
        }
        public string DBVersion
        {
            get { return db_version; }
            set { db_version = value; }
        }
        public uint ScannedCount
        {
            get { return scanned_count; }
            set { scanned_count = value; }
        }
        public uint CorruptFileCount
        {
            get { return (uint)corrupt_files.Count; }
        }
        public string[] CorruptFiles
        {
            get { return corrupt_files.ToArray(); }
        }
        public uint InsertSongCount
        {
            get { return insert_song_count; }
            set { insert_song_count = value; }
        }
        public uint DeleteSongCount
        {
            get { return delete_song_count; }
            set { delete_song_count = value; }
        }
        public uint UpdateSongCount
        {
            get { return update_song_count; }
            set { update_song_count = value; }
        }
        public uint InsertAlbumCount
        {
            get { return insert_album_count; }
            set { insert_album_count = value; }
        }
        public uint DeleteAlbumCount
        {
            get { return delete_album_count; }
            set { delete_album_count = value; }
        }
        public uint InsertArtistCount
        {
            get { return insert_artist_count; }
            set { insert_artist_count = value; }
        }
        public uint UpdateAlbumCount
        {
            get { return update_album_count; }
            set { update_album_count = value; }
        }
        public uint DeleteArtistCount
        {
            get { return delete_artist_count; }
            set { delete_artist_count = value; }
        }
        public uint InsertArtCount
        {
            get { return insert_art_count; }
            set { insert_art_count = value; }
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
        /// helper function used by GetHTML
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string ListFragment(string name, string style)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<tr><td class=\"gray_text\" {1}>{0} INSERT total:</td><td {1}>{{0}}</tr></td>\r\n", name, style);
            sb.AppendFormat("<tr><td class=\"gray_text\" {1}>{0} DELETE total:</td><td {1}>{{1}}</tr></td>\r\n", name, style);
            return sb.ToString();
        }

        /// <summary>
        /// return report as HTML 
        /// </summary>
        /// <returns></returns>
        public string GetHTML()
        {
            timestamp = DateTime.Now;
            StringBuilder sb = new StringBuilder("<html>\r\n<head>\r\n");
            sb.AppendLine("<title>Report</title>");
            sb.AppendLine("<style type=\"text/css\">");
            sb.AppendLine("body\r\n{\r\n\t font-family: Arial;\r\n}");
            sb.AppendLine(".gray_text\r\n{\r\n\tcolor:#666666;\r\n}");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");

            string dt = timestamp.ToString(Utility.Globals.PrettyLogDateFormat);
            sb.AppendFormat("<h2><i>{0}</i></h2>\r\n", dt);
            sb.AppendLine("<br />");

            sb.AppendLine("<div style=\"text-align: center\">");
            string style = "style=\"font-size: 9pt\"";

            sb.AppendLine("<table>");
            sb.AppendFormat("<tr><td class=\"gray_text\" {1}>Total files scanned:</td><td {1}>{0}</td></tr>\r\n", scanned_count, style);
            sb.AppendFormat("<tr><td class=\"gray_text\" {1}>Corrupt files found:</td><td {1}>{0}</td></tr>\r\n", CorruptFileCount, style);
            sb.AppendFormat("<tr><td class=\"gray_text\" {1}>Song INSERT total:</td><td {1}>{0}</td></tr>\r\n", insert_song_count, style);
            sb.AppendFormat("<tr><td class=\"gray_text\" {1}>Song UPDATE total:</td><td {1}>{0}</td></tr>\r\n", update_song_count, style);
            sb.AppendFormat("<tr><td class=\"gray_text\" {1}>Song DELETE total:</td><td {1}>{0}</td></tr>\r\n", delete_song_count, style);
            sb.AppendFormat(ListFragment("Artist", style), insert_artist_count, delete_artist_count);
            sb.AppendFormat(ListFragment("Album", style), insert_album_count, delete_album_count);
            sb.AppendFormat(ListFragment("Art", style), insert_art_count, delete_art_count);
            sb.AppendFormat("<tr><td class=\"gray_text\" {1}>Art files deleted:</td><td {1}>{0}</tr></td>\r\n", delete_art_file_count, style);
            sb.AppendLine("</table>");

            sb.AppendLine("<br />");
            sb.AppendFormat("<div {0}>\r\n", style);
            sb.AppendFormat("<i><b>Database version:&nbsp;{0} previous version:&nbsp;{1}</b></i>\r\n", db_version, db_prev_version);
            sb.AppendLine("</div>");

            sb.AppendLine("</div>");

            sb.AppendLine("</body>\r\n</html>");
            return sb.ToString();
        }

        /// <summary>
        /// save an html report to process directory
        /// </summary>
        /// <returns></returns>
        public string SaveReport()
        {
            string proc_dir = Utility.Globals.ProcessDirectory();
            string file = timestamp.ToString(Utility.Globals.LogDateFormat);
            string path = string.Format(@"{0}/{1}.html", proc_dir, file);
            string html = GetHTML();
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(html);
            }
            return path;
        }

        /// <summary>
        /// delete all log files in process directory
        /// </summary>
        public void ClearReports()
        {
            // find or generate a log file
            string dir = Path.GetDirectoryName(Globals.ProcessPath());

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);
            FileInfo[] files = di.GetFiles("??????????????????.html");
            foreach (FileInfo file in files)
            {
                file.Delete();
            }
        }
    }
}
