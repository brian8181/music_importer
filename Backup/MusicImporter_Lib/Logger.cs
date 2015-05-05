using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicImporter_Lib
{
    class ImpLogger
    {
        public static class Policy
        {
            // related to all
            public static bool STARTS = true;
            public static bool STOPS = true;
            public static bool ERRORS = true;
            // related to file scan
            public static bool SCAN_ENTER_DIRECTORY = false;
            public static bool SCAN_FILE = true;
            public static bool SCAN_SQL = false;
            // related to clean
            public static bool CLEAN_ENTER_DIRECTORY = false;
            public static bool CLEAN_SQL = false;
            public static bool CLEAN_DELETE_FILE = true;
        }
       
    }
}
