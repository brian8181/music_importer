using System;
using System.Collections.Generic;
using System.Text;

namespace MusicImporter_Lib
{
    
    public class MySql_Strings : ISql
    {
        public string INSERT_artist
        {
            get
            {
                return "INSERT INTO artist (artist) Values(?artist)";
            }
           
        }

        public string INSERT_album
        {
            get
            {
                return "INSERT INTO album (album, artist) Values(?album, ?artist)";
            }
           
        }

        public string INSERT_song
        {
            get
            {
                return "INSERT INTO song (artist_id, album_id, track, title, file, genre, bitrate, length, year, comments, " +
                      "encoder, file_size, file_type, art_id, lyrics, composer, conductor, copyright, " +
                      "disc, disc_count, performer, tag_types, track_count, beats_per_minute, sha1, file_sha1) VALUES(" +
                      "?artist_id, ?album_id, ?track, ?title, ?file, ?genre, ?bitrate, ?length, ?year, ?comments, " +
                      "?encoder, ?file_size, ?file_type, ?art_id, ?lyrics, ?composer, ?conductor, ?copyright, " +
                      "?disc, ?disc_count, ?performer, ?tag_types, ?track_count, ?beats_per_minute, ?sha1, ?file_sha1)";
            }
        }

        public string INSERT_art
        {
            get
            {
                return "INSERT INTO art VALUES(NULL, ?file, ?type, ?hash, ?description, ?mime_type, NULL, NOW())";
            }
        }

        public string SELECT_art_link
        {
            get
            {
                return "SELECT_last_update song_id FROM song_art WHERE song_id=?song_id AND art_id=?art_id LIMIT 1";
            }
        }

        public string INSERT_art_link
        {
            get
            {
                return "INSERT INTO song_art VALUES(NULL, ?song_id, ?art_id, NULL, NOW())";
            }
        }
               
        public string UPDATE_song
        {
            get
            {
                return "UPDATE song SET artist_id=?artist_id, album_id=?album_id, track=?track, title=?title, file=?file, genre=?genre, " +
                      "bitrate=?bitrate, length=?length, year=?year, comments=?comments, encoder=?encoder, file_size=?file_size, file_type=?file_type, " +
                      "art_id=?art_id, lyrics=?lyrics, composer=?composer, conductor=?conductor, copyright=?copyright, disc=?disc, disc_count=?disc_count, " +
                      "performer=?performer, tag_types=?tag_types, track_count=?track_count, beats_per_minute=?beats_per_minute, sha1=?sha1, file_sha1=?file_sha1 " +
                      "WHERE id = ?song_id";
            }
        }

        public string UPDATE_album
        {
            get
            {
                return "UPDATE album SET album=?album, artist=?artist WHERE id=?album_id";
            }
        }

        public string SELECT_last_update
        {
            get
            {
                return "SELECT `update` FROM `update` WHERE `version` = (SELECT MAX(version) FROM `update`)";
            }
        }

        public string GRANT_default_user
        {
            get
            {
                return "GRANT SELECT_last_update,INSERT,UPDATE ON {0}.* TO 'web'@'localhost' IDENTIFIED BY 'sas*.0125'";
            }
        }
    }
}
