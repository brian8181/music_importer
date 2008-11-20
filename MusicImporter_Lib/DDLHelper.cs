using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BKP.Online.Data;
using System.IO;
using BKP.Online;
using BKP.Online.IO;

namespace MusicImporter_Lib
{
    /// <summary>
    /// create / update music database
    /// </summary>
    class DDLHelper
    {
        private IDatabase db = null;

        private string CreateScript
        {
            get
            {
                return Properties.Resources.create;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="art_path"></param>
        public DDLHelper(IDatabase db)
        {
            this.db = db;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema_name"></param>
        public void CreateDatabase(string schema_name)
        {
            // create database
            db.CreateDatabase(schema_name);
            db.ChangeDatabase(schema_name);

            // todo make function
            string sql = String.Format
                ("GRANT SELECT,INSERT,UPDATE ON {0}.* TO 'web'@'localhost' IDENTIFIED BY 'sas*.0125'", schema_name);
            db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            Execute(CreateScript);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void Execute(string sql)
        {
            // run create scipt
            sql = sql.Replace("\n", "");
            sql = sql.Replace("\r", "");
            string[] sql_cmds = sql.Split(';');
            foreach (string cmd in sql_cmds)
            {
                db.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void ExecuteFile(string path)
        {
            if (File.Exists(path))
            {
                // run create scipt
                string sql = File.ReadAllText(path);
                Execute(sql);
            }
        }
        /// <summary>
        /// execute all files in directory
        /// </summary>
        public void ExecuteDirectory(string path)
        {
            string[] files = Directory.GetFiles(path, "*.sql");
            foreach (string file in files)
            {
                string sql = File.ReadAllText(file);
                db.ExecuteNonQuery(sql);
            }
        }
    }
}
