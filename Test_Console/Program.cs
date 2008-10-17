using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Console
{
    class Program
    {
        static void Main( string[] args )
        {
            MusicImporter_Lib.Importer importer = new MusicImporter_Lib.Importer( "0" );
            importer.Connect();
        }
    }
}
