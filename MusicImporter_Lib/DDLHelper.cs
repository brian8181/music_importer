using System;
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
        private int current_version = -1;
        private int update_version = -1;
        
        /// <summary>
        /// the current version
        /// </summary>
        public int CurrentVersion
        {
            get { return current_version; }
            set { current_version = value; }
        }
        /// <summary>
        /// the update version
        /// </summary>
        public int UpdateVersion
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

            //db check for installed version
            current_version = (int)db.ExecuteScalar("SELECT MAX(version) FROM `music`.`update`");
            string proc_path = Path.GetDirectoryName(Globals.ProcessPath());
            string[] files = Directory.GetFiles(proc_path, "update.?.?.?.sql");
            versions = new SortedList<int, DatabaseVersion>();
            foreach (string f in files)
            {
                DatabaseVersion v = new DatabaseVersion(f);
                versions.Add(v.Version, v);
            }
            // apply in order
            update_version = versions.Keys[versions.Keys.Count - 1];
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
            if (File.Exists(CreateScript))
            {
                string sql = File.ReadAllText(CreateScript);
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
            if (update_version > current_version)
            {
                foreach (DatabaseVersion ver in versions.Values)
                {
                    if (ver.Version <= (int)current_version)
                        continue;  // skip previous upgrades 

                    string sql = File.ReadAllText(ver.Filename);
                    db.ExecuteNonQuery(sql);
                    db.ExecuteNonQuery("INSERT INTO `update` (`update`, `version`) VALUES( '"
                        + ver.ToString() + "', " + ver.Version + " )");
                }
            }
        }
    }
}
