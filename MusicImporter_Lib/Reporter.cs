using System;
using System.Collections.Generic;
using System.Text;

namespace MusicImporter_Lib
{
    public class Reporter
    {
        private int scanned_count;

        public int ScannedCount
        {
            get { return scanned_count; }
            set { scanned_count = value; }
        }

        private int insert_song_count;

        public int InsertSongCount
        {
            get { return insert_song_count; }
            set { insert_song_count = value; }
        }
        private int update_song_count;

        public int UpdateSongCount
        {
            get { return update_song_count; }
            set { update_song_count = value; }
        }
        private int insert_album_count;
        private int insert_artist_count;
        private int insert_art_count;

        private int delete_artist_count;
        private int delete_album_count;
        private int delete_song_count;
        private int delete_art_count;

        public int DeleteArtCount
        {
            get { return delete_art_count; }
            set { delete_art_count = value; }
        }

        public int DeleteSongCount
        {
            get { return delete_song_count; }
            set { delete_song_count = value; }
        }

        public string GetReport()
        {
            return "";
        }
    }
}
