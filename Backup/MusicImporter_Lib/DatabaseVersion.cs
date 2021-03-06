using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MusicImporter_Lib
{
    public class DatabaseVersion
    {
        private bool valid = false;
        /// <summary>
        /// 
        /// </summary>
        public bool Valid
        {
            get { return valid; }
        }
        private string filename = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Filename
        {
            get { return filename; }
        }
        private string major = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Major
        {
            get { return major; }
        }
        private string minor = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Minor
        {
            get { return minor; }
        }

        private string revison = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Revison
        {
            get { return revison; }
        }

        private int version = 0;
        /// <summary>
        /// 
        /// </summary>
        public int Version
        {
            get { return version; }
        }
        /// <summary>
        /// create blank string, use ParseVersion to intialize
        /// </summary>
        /// <param name="filename"></param>
        public DatabaseVersion()
        {
        }
        /// <summary>
        /// create with a string from database
        /// </summary>
        /// <param name="filename"></param>
        public DatabaseVersion( string filename )
        {
            try
            {
                this.filename = filename;
                Regex exp =
                    new Regex("^update.(?<ver_str>[0-9].[0-9].[0-9]).sql$");

                MatchCollection mc = exp.Matches( System.IO.Path.GetFileName(filename) );
                if(mc.Count != 1)
                {
                    valid = false;
                    return;
                }
                Match m = mc[0];
                string ver_str = m.Groups["ver_str"].Value;
                ParseVersion(ver_str);
            }
            catch
            {
                valid = false;
            }
        }
        /// <summary>
        /// parses a version string, can be used to create a version fomr db string
        /// </summary>
        /// <param name="value"></param>
        public void ParseVersion(string value)
        {
            Regex exp =
                   new Regex("^(?<major>[0-9]).(?<minor>[0-9]).(?<revison>[0-9])$");

            MatchCollection mc = exp.Matches(value);
            if (mc.Count != 1)
            {
                valid = false;
                return;
            }
            Match m = mc[0];
            major = m.Groups["major"].Value;
            minor = m.Groups["minor"].Value;
            revison = m.Groups["revison"].Value;
            version = int.Parse(major + minor + revison);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", major, minor, revison);
        }
    }
}
