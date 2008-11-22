using System;
using System.Collections.Generic;
using System.Text;

namespace MusicImporter_Lib
{
    public class Reporter
    {
        private int scanned_count = 0;
        private int insert_song_count = 0;
        private int update_song_count = 0;
        private int insert_album_count = 0;
        private int insert_artist_count = 0;
        private int insert_art_count = 0;
        private int delete_artist_count = 0;
        private int delete_album_count = 0;
        private int delete_song_count = 0;
        private uint delete_art_count = 0;
        private uint delete_art_file_count = 0;

        public int ScannedCount
        {
            get { return scanned_count; }
            set { scanned_count = value; }
        }
        public int InsertSongCount
        {
            get { return insert_song_count; }
            set { insert_song_count = value; }
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
        public int InsertArtistCount
        {
            get { return insert_artist_count; }
            set { insert_artist_count = value; }
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
