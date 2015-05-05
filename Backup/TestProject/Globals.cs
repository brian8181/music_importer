using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    public static class Globals
    {
        private static Utility.Data.MySqlDatabase mysql_db = null;
        private static object LOCK = new object();

        public static Utility.Data.MySqlDatabase MySQL_DB
        {
            get
            {
                if( mysql_db == null )
                {
                    lock (LOCK)
                    {
                        if (mysql_db == null)
                        {
                            mysql_db = new Utility.Data.MySqlDatabase();
                            mysql_db.Open("Data Source=localhost;Port=3306;User Id=root;Password=sas_0125");
                        }
                    }
                }
                return mysql_db;
            }
        }

        public static string SchemaName
        {
            get
            {
                return "music_test";
            }
        }
    }
}
