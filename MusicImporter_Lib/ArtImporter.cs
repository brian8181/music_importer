using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using BKP.Online.Data;
using System.Security.Cryptography;
using System.IO;
using System.Data.Common;
using BKP.Online.Media;
using MusicImporter_Lib.Properties;
using BKP.Online.IO;

namespace MusicImporter_Lib
{
    class ArtImporter
    {
        private IDatabase db = null;
        private string art_path = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="art_path"></param>
        public ArtImporter(IDatabase db, string art_path)
        {
            this.db = db;
            this.art_path = art_path;
        }

        /// <summary>
        ///  insert album art
        /// </summary>
        /// <param name="tag">the id3 tag</param>
        /// <param name="current_dir">current directory</param>
        /// <returns>primary key (insert id)</returns>
        public string InsertArt(TagLib.File tag_file)
        {
            string art = null;
            string key = null;
            byte[] hash = null;
            byte[] data = null;
            string type = string.Empty;
            string mime_type = string.Empty;
            string description = string.Empty;
            string current_dir = Path.GetDirectoryName(tag_file.Name);
            TagLib.Tag tag = tag_file.Tag;

            // look for art in directory
            string[] files = DirectoryExt.GetFiles( current_dir, Settings.Default.art_mask );
            if (tag.Pictures.Length > 0 || files.Length > 0)
            {
                Guid guid = Guid.NewGuid();
                // find picture              
                if (tag.Pictures.Length > 0)
                {
                    //
                    TagLib.IPicture pic = FindBestPic(tag);
                    if (pic.MimeType.StartsWith("image/"))
                    {
                        art = guid.ToString("B") + pic.MimeType.Replace("image/", ".");
                        data = new byte[pic.Data.Count];
                        pic.Data.CopyTo(data, 0);
                        type = pic.Type.ToString();
                        mime_type = pic.MimeType;
                        description = pic.Description;
                    }
                    else { return null; }
                }
                else
                {
                    string ext = Path.GetExtension(files[0]);
                    art = guid.ToString("B") + ext;
                    data = File.ReadAllBytes(files[0]);
                    type = "Cover";
                    mime_type = ext;
                    description = "cover art";
                }

                // 
                long id = 0;
                if (isDuplicate(data, out id))
                {
                    string file = null;
                    if (isOrphaned(id, out file))
                    {
                        // write file
                        SaveArt(file, data);
                    }
                    key = id.ToString();
                }
                else
                {
                    // write file
                    SaveArt(art, data);
                    string sql = "INSERT INTO art VALUES(NULL, ?file, ?type, ?hash, ?description, ?mime_type, NULL, NOW())";
                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.AddWithValue("?file", art);
                    cmd.Parameters.AddWithValue("?type", type);
                    cmd.Parameters.AddWithValue("?hash", hash);
                    cmd.Parameters.AddWithValue("?description", description);
                    cmd.Parameters.AddWithValue("?mime_type", mime_type);
                    db.ExecuteNonQuery(cmd);
                    key = db.ExecuteScalar("SELECT LAST_INSERT_ID()").ToString();
                }

                return key;
            }
                       

            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public TagLib.IPicture FindBestPic(TagLib.Tag tag)
        {
            if (tag.Pictures.Length < 1)
                return null;

            TagLib.IPicture pic = tag.Pictures[0];
            // find the first front cover image, if not use first image
            foreach (TagLib.IPicture p in tag.Pictures)
            {
                if (p.Type == TagLib.PictureType.FrontCover)
                {
                    pic = p;
                    break;
                }
            }

            return pic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="data"></param>
        public void SaveArt(string file, byte[] data)
        {
            // write file to art location
            System.IO.File.WriteAllBytes(art_path + file, data);
            // gen & write thumbs
            GenerateThumbs(file);
        }
              
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file_name"></param>
        private void GenerateThumbs(string file_name)
        {
            string art = art_path + file_name;
            Thumb.Generate(
                art_path + "large\\" + file_name, art, Settings.Default.art_large, 0, true);
            Thumb.Generate(
                art_path + "small\\" + file_name, art, Settings.Default.art_small, 0, true);
            Thumb.Generate(
                art_path + "xsmall\\" + file_name, art, Settings.Default.art_xsmall, 0, true);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool isDuplicate(byte[] data, out long id)
        {
            byte[] hash = ComputeHash(data);
            string sql = "SELECT id FROM art WHERE hash=?hash";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Parameters.AddWithValue("?hash", hash);
            object obj = null;
            try
            {
                obj = db.ExecuteScalar(cmd);
            }
            catch
            {
                obj = null;
            }

            id = 0;
            if (obj != null)
            {
                id = (long)obj;
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// returns wheater a given art id has matching file on disk
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool isOrphaned(long id, out string file)
        {
            string sql = "SELECT file FROM art WHERE id=?id";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Parameters.AddWithValue("?id", id);
            DbDataReader reader = db.ExecuteReader(cmd);

            file = null;
            while (reader.Read())
            {
                file = reader.GetString(0);
            }

            return file != null && File.Exists( file );
        }

        /// <summary>
        /// compute a hash value
        /// </summary>
        /// <param name="data">data to hash</param>
        /// <returns>the hash value</returns>
        private byte[] ComputeHash(byte[] data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            return result;
        }
    }
}
