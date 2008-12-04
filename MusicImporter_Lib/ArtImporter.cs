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
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Utility.Data;
using System.Security.Cryptography;
using System.IO;
using System.Data.Common;
using Utility.Media;
using MusicImporter_Lib.Properties;
using Utility.IO;
using System.Data;
using System.Text.RegularExpressions;
using Utility;
using System.Diagnostics;

namespace MusicImporter_Lib
{
    /// <summary>
    /// importer for album art
    /// </summary>
    class ArtImporter
    {
        private IDatabase db = null;
        private string path = null;

        /// <summary>
        /// defualt ctor
        /// </summary>
        /// <param name="db">the db connection</param>
        /// <param name="art_path">path to art directory</param>
        public ArtImporter(IDatabase db, string art_path)
        {
            this.db = db;
            this.path = art_path.TrimEnd('\\');

            Directory.CreateDirectory(path + "\\large\\");
            Directory.CreateDirectory(path + "\\small\\");
            Directory.CreateDirectory(path + "\\xsmall\\");

            // place NA jpg
            string proc_path = System.IO.Path.GetDirectoryName(Globals.ProcessPath());
            string file = proc_path = proc_path.TrimEnd('\\') + "\\Resources\\NA.JPG";
            string dest = path + "\\NA.JPG";

            if (File.Exists(file) && !File.Exists(dest))
            {
                File.Copy(file, dest);
                GenerateThumbs(Path.GetFileName(dest));
            }
        }
        /// <summary>
        ///  insert album art
        /// </summary>
        /// <param name="tag">the id3 tag</param>
        /// <param name="current_dir">current directory</param>
        /// <returns>count</returns>
        public uint InsertArt(object song_id, TagLib.File tag_file)
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

            if (tag.Pictures.Length == 0)
            {
                return 0;
            }

            uint inserted = 0;
            DDLHelper helper = new DDLHelper(db);
            helper.DeleteArtLinks(song_id);

            foreach (TagLib.IPicture pic in tag.Pictures)
            {
                art = GenerateFileName(pic);
                data = new byte[pic.Data.Count];
                pic.Data.CopyTo(data, 0);
                type = pic.Type.ToString();
                mime_type = pic.MimeType;
                description = pic.Description;

                if (pic.MimeType != "-->") // no support for linked art
                {
                    hash = ComputeHash(data);
                    uint id = 0;
                    key = string.Empty;
                    if (isDuplicateInsert(hash, out id))
                    {
                        string file = null;
                        if (isOrphanedInsert(id, out file))
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
                        key = Insert(hash, art, type, description, mime_type);
                        ++inserted;
                    }
                    CreateLink(song_id, key);
                }
            }
            return inserted;
        }
        /// <summary>
        /// generate file name for picture
        /// </summary>
        /// <param name="pic"></param>
        /// <returns></returns>
        private string GenerateFileName(TagLib.IPicture pic)
        {
            Guid guid = Guid.NewGuid();
            string exp = @"[\w\\]*(?<ext>(jpg)|(jpeg)|(png)|(gif)|(bmp))";
            Regex regex = new Regex(exp, RegexOptions.IgnoreCase);
            Match m = regex.Match(pic.MimeType);
            string ext = m.Groups["ext"].Value.ToLower();
            // name blank mime_type jpg extension
            ext = ext == string.Empty ? "jpg" : ext;
            return guid.ToString("B") + "." + ext;
        }
        /// <summary>
        /// create link between song and art
        /// </summary>
        /// <param name="song_id"></param>
        /// <param name="art_id"></param>
        public void CreateLink(object song_id, object art_id)
        {
            Trace.WriteLine(string.Format("Creating link song_id={0} -> art_id={1}: ", song_id, art_id)
               , Logger.Level.Information.ToString());
            string sql = "SELECT song_id FROM song_art WHERE song_id=?song_id AND art_id=?art_id LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Parameters.AddWithValue("?song_id", song_id);
            cmd.Parameters.AddWithValue("?art_id", art_id);

            if (db.Exists(cmd))
                return; // already in db

            sql = "INSERT INTO song_art VALUES(NULL, ?song_id, ?art_id, NULL, NOW())";
            cmd = new MySqlCommand(sql);
            cmd.Parameters.AddWithValue("?song_id", song_id);
            cmd.Parameters.AddWithValue("?art_id", art_id);
            db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// do the database insert
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="art"></param>
        /// <param name="type"></param>
        /// <param name="description"></param>
        /// <param name="mime_type"></param>
        /// <returns></returns>
        public string Insert(byte[] hash, string art, string type, string description, string mime_type)
        {
            Trace.WriteLine("Inserting art: " + art, Logger.Level.Information.ToString());
            string sql = "INSERT INTO art VALUES(NULL, ?file, ?type, ?hash, ?description, ?mime_type, NULL, NOW())";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Parameters.AddWithValue("?file", art);
            cmd.Parameters.AddWithValue("?type", type);
            cmd.Parameters.AddWithValue("?hash", hash);
            cmd.Parameters.AddWithValue("?description", description);
            cmd.Parameters.AddWithValue("?mime_type", mime_type);
            db.ExecuteNonQuery(cmd);
            string key = db.ExecuteScalar("SELECT LAST_INSERT_ID()").ToString();

            return key;
        }
        /// <summary>
        /// get the first front cover or the first picture
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public TagLib.IPicture FindDefaultPicture(TagLib.Tag tag)
        {
            if (tag.Pictures.Length < 1)
                return null;

            TagLib.IPicture pic = tag.Pictures[0];
            // find the first front cover image, if not use first image
            foreach (TagLib.IPicture p in tag.Pictures)
            {
                if (p.Type == TagLib.PictureType.FrontCover && p.MimeType != "-->")
                {
                    pic = p;
                    break;
                }
            }
            return pic;
        }
        /// <summary>
        /// saves art to art location, this also generates thumbs
        /// </summary>
        /// <param name="file"></param>
        /// <param name="data"></param>
        public void SaveArt(string file, byte[] data)
        {
            Trace.WriteLine("Saved art: " + file, Logger.Level.Information.ToString());
            // write file to art location
            System.IO.File.WriteAllBytes(path + "\\" + file, data);
            // gen & write thumbs
            GenerateThumbs(file);
        }
        /// <summary>
        /// generate art thumbnails
        /// </summary>
        /// <param name="file_name"></param>
        private void GenerateThumbs(string file_name)
        {
            Trace.WriteLine("Generated thumbnails for: " + file_name, Logger.Level.Information.ToString());
            string art = path + "\\" + file_name;
            Thumb.Generate(
                path + "\\large\\" + file_name, art, Settings.Default.art_large, 0, true);
            Thumb.Generate(
                path + "\\small\\" + file_name, art, Settings.Default.art_small, 0, true);
            Thumb.Generate(
                path + "\\xsmall\\" + file_name, art, Settings.Default.art_xsmall, 0, true);

        }
        /// <summary>
        /// returns true if art is a duplicate
        /// </summary>
        /// <param name="data"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool isDuplicateInsert(byte[] hash, out uint id)
        {
            string sql = "SELECT id FROM art WHERE hash=?hash LIMIT 1";
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
                id = (uint)obj;
                return true;
            }

            return false;
        }
        /// <summary>
        /// returns true if a given art id has no matching file on disk
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool isOrphanedInsert(long id, out string file)
        {
            string sql = "SELECT file FROM art WHERE id=?id LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Parameters.AddWithValue("?id", id);
            using (DbDataReader reader = db.ExecuteReader(cmd))
            {
                file = null;
                while (reader.Read())
                {
                    file = reader.GetString(0);
                }
            }
            return file != null && File.Exists(file);
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

        #region Maintenance
        /// <summary>
        /// delete inserts if file does not exists
        /// </summary>
        /// <param name="path">art path</param>
        public uint DeleteOrphanedInserts()
        {
            DataSet ds = db.ExecuteQuery("SELECT file FROM art LIMIT 1");

            if (ds.Tables.Count != 1)
                return 0;

            DataTable dt = ds.Tables[0];
            MySqlCommand cmd = new MySqlCommand();
            string file = string.Empty;
            uint deleted = 0;
            foreach (DataRow row in dt.Rows)
            {
                file = row[0].ToString();
                path = path.Trim('\\');
                string full_path = String.Format("{0}\\{1}", path, file);
                if (!File.Exists(full_path))
                {
                    //// get id for file
                    //cmd = new MySqlCommand("SELECT id FROM art WHERE file=?file LIMIT 1");
                    //cmd.Parameters.AddWithValue("?file", file);
                    //object obj = db.ExecuteScalar(cmd);
                    //// delete all links from song_art table
                    //if (obj != null)
                    //{
                    //    cmd = new MySqlCommand("DELETE FROM song_art WHERE art_id=?art_id");
                    //    cmd.Parameters.AddWithValue("?art_id", obj);
                    //    db.ExecuteNonQuery(cmd);
                    //}
                    //// delete art
                    //cmd = new MySqlCommand("DELETE FROM art WHERE file=?file");
                    //cmd.Parameters.AddWithValue("?file", file);
                    //db.ExecuteNonQuery(cmd);

                    DDLHelper helper = new DDLHelper(db);
                    helper.DeleteArt(file);
                    ++deleted;
                }
            }
            return deleted;
        }
        /// <summary>
        /// check each file in path to see if it's in the database, if not delete it
        /// </summary>
        /// <param name="path">path to check</param>
        public uint DeleteOrphanedFiles()
        {
            uint deleted = 0;
            path = path.TrimEnd('\\');
            string[] files = DirectoryExt.GetFiles(path, "*.jpg;*.jpeg;*.png;*.bmp;*.gif");
            for (int i = 0; i < files.Length; ++i)
            {
                byte[] data = File.ReadAllBytes(files[i]);
                byte[] hash = ComputeHash(data);
                string file = Path.GetFileName(files[i]);

                if (file == "NA.JPG")
                {
                    continue; // do delete NA.JPG files
                }

                // file exist but hash doesn't match
                string sql = "SELECT id FROM art WHERE file=?file AND NOT hash=?hash LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("?hash", hash);
                cmd.Parameters.AddWithValue("?file", file);
                object obj = db.ExecuteScalar(cmd);
                if (obj != null)
                {
                    // todo corrupt picture
                }

                // file doesn't exist
                sql = "SELECT id FROM art WHERE file=?file LIMIT 1";
                cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("?hash", hash);
                cmd.Parameters.AddWithValue("?file", file);
                obj = db.ExecuteScalar(cmd);
                if (obj == null)
                {
                    File.Delete(files[i]);

                    string large = string.Format("{0}\\large\\{1}", path, file);
                    if (File.Exists(large))
                    {
                        File.Delete(large);
                    }
                    string small = string.Format("{0}\\small\\{1}", path, file);
                    if (File.Exists(small))
                    {
                        File.Delete(small);
                    }
                    string xsmall = string.Format("{0}\\xsmall\\{1}", path, file);
                    if (File.Exists(xsmall))
                    {
                        File.Delete(xsmall);
                    }
                    ++deleted;
                }
            }
            return deleted;
        }
        /// <summary>
        /// delete any thumb with out parent
        /// </summary>
        private void DeleteOrphanedThumbs()
        {
            // todo
        }
        /// <summary>
        /// (Re)Scan and insert art from art directory. 
        /// This is a utility function that typically should not be used. This function 
        /// preforms the opposite of the function DeleteOrphanedFiles.  
        /// </summary>
        private void RescanArt()
        {
            byte[] hash = null;
            string[] files = DirectoryExt.GetFiles(Settings.Default.art_location, "*.jpg;*.jpeg;*.png;*.bmp;*.gif");
            for (int i = 0; i < files.Length; ++i)
            {
                string filename = Path.GetFileName(files[i]);
                string ext = Path.GetExtension(files[i]);
                byte[] data = null;
                string type = string.Empty;
                string mime_type = string.Empty;
                string description = string.Empty;
                data = File.ReadAllBytes(files[i]);
                type = "Cover";
                mime_type = ext;
                description = "cover art";
                hash = ComputeHash(data);
                string sql = "SELECT id FROM art WHERE hash=?hash LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.Parameters.AddWithValue("?hash", hash);
                object obj = null;
                obj = db.ExecuteScalar(cmd);
                try
                {
                    obj = db.ExecuteScalar(cmd);
                }
                catch
                {
                    continue;
                }
                if (obj == null)
                {
                    sql = "INSERT INTO art VALUES(NULL, ?file, ?type, ?hash, ?description, ?mime_type, NULL, NOW())";
                    cmd = new MySqlCommand(sql);
                    cmd.Parameters.AddWithValue("?file", filename);
                    cmd.Parameters.AddWithValue("?type", type);
                    cmd.Parameters.AddWithValue("?hash", hash);
                    cmd.Parameters.AddWithValue("?description", description);
                    cmd.Parameters.AddWithValue("?mime_type", mime_type);
                    db.ExecuteNonQuery(cmd);
                }
            }
        }
        #endregion
    }
}