using MusicImporter_Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Console
{
    class Program
    {
        static void Main( string[] args )
        {
            Importer importer = new Importer();
            importer.Connect();
        }
    }
}
