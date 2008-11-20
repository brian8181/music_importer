using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BKP.Online.Data;
using System.IO;
using BKP.Online;

namespace MusicImporter_Lib
{
    /// <summary>
    /// create / update music database
    /// </summary>
    class DatabaseManager
    {
        private IDatabase db = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="art_path"></param>
        public DatabaseManager(IDatabase db)
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
        /// <param name="path"></param>
        public void ExecuteFile(string path)
        {
            if (File.Exists(path))
            {
                // run create scipt
                string sql = File.ReadAllText(path);
                sql = sql.Replace("\n", "");
                sql = sql.Replace("\r", "");
                string[] sql_cmds = sql.Split( ';' );
                foreach( string cmd in sql_cmds )
                {
                    db.ExecuteNonQuery(cmd);
                }
            }
        }
    }
}
