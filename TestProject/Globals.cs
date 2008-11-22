using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    public static class Globals
    {
        private static BKP.Online.Data.MySqlDatabase mysql_db = null;

        public static BKP.Online.Data.MySqlDatabase MySQL_DB
        {
            get
            {
                if (mysql_db == null)
                {
                    mysql_db = new BKP.Online.Data.MySqlDatabase(); // TODO: Initialize to an appropriate value
                    mysql_db.Open("Data Source=localhost;Port=3306;User Id=root;Password=sas_0125");
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
