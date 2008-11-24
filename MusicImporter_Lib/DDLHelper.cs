﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BKP.Online.Data;
using System.IO;
using BKP.Online;
using BKP.Online.IO;
using System.Resources;
using System.Globalization;

namespace MusicImporter_Lib
{
    /// <summary>
    /// create / update music database
    /// </summary>
    class DDLHelper
    {
        private IDatabase db = null;
        private SortedList<int, DatabaseVersion> versions = null;
        private DatabaseVersion current_version = null;
        private DatabaseVersion update_version = null;
        private string schema_name = string.Empty;
        
        /// <summary>
        /// the current version
        /// </summary>
        public DatabaseVersion CurrentVersion
        {
            get { return current_version; }
            set { current_version = value; }
        }
        /// <summary>
        /// the update version
        /// </summary>
        public DatabaseVersion UpdateVersion
        {
            get { return update_version; }
        }
        /// <summary>
        /// gets the create script
        /// </summary>
        private string CreateScript
        {
            get
            {
                string path = System.IO.Path.GetDirectoryName( Globals.ProcessPath() );
                return path.TrimEnd('\\') + "\\sql\\create.sql";
            }
        }
        /// <summary>
        /// default ctor
        /// </summary>
        /// <param name="db">the database connection</param>
        public DDLHelper(IDatabase db)
        {
            this.db = db;
        }
        /// <summary>
        /// 
        /// </summary>
        public void InitializeVersionInfo()
        {
            //db check for installed version
            string version = (string)db.ExecuteScalar(
                "SELECT `update` FROM `update` WHERE `version` = (SELECT MAX(version) FROM `update`)");
            current_version = new DatabaseVersion();
            current_version.ParseVersion(version);
            string proc_path = Path.GetDirectoryName(Globals.ProcessPath());
            proc_path.TrimEnd('\\');
            string update_path = string.Format("{0}\\sql\\updates", proc_path);
            string[] files = Directory.GetFiles(update_path, "update.?.?.?.sql");
            versions = new SortedList<int, DatabaseVersion>();
            foreach (string f in files)
            {
                DatabaseVersion v = new DatabaseVersion(f);
                versions.Add(v.Version, v);
            }

            if (versions.Keys.Count > 0)
            {
                // apply in order
                update_version = versions.Values[versions.Values.Count - 1];
            }
        }
        /// <summary>
        ///  creates an empty database 
        /// </summary>
        /// <param name="schema_name">the name to give the database</param>
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
        /// executes the default create script file
        /// </summary>
        public void ExecuteCreateScript()
        {
            ExecuteCreateScript(CreateScript);
        }
        /// <summary>
        /// executes the default create script file
        /// </summary>
        public void ExecuteCreateScript(string file)
        {
            if (File.Exists(file))
            {
                string sql = File.ReadAllText(file);
                Execute(sql);
            }
        }
        /// <summary>
        /// parses then executes a sql script one command at a time
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
        /// execute all files in directory as single sql commands
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
        /// <summary>
        /// update the database
        /// </summary>
        public void UpdateDatabase()
        {
            // apply in order
            if (update_version.Version > current_version.Version)
            {
                foreach (DatabaseVersion ver in versions.Values)
                {
                    if (ver.Version <= current_version.Version)
                        continue;  // skip previous upgrades 

                    string sql = File.ReadAllText(ver.Filename);
                    db.ExecuteNonQuery(sql);
                    db.ExecuteNonQuery("INSERT INTO `update` (`update`, `version`, `release_date`) VALUES( '"
                        + ver.ToString() + "', " + ver.Version + ", '2008-11-11' )");
                }
            }
        }
    }
}
