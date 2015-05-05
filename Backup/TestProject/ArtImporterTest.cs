using System.Text.RegularExpressions;
using DB = Utility.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicImporter_Lib;
using TagLib;
using System.IO;
using MySql.Data.MySqlClient;

namespace TestProject
{
    /// <summary>
    ///This is a test class for ArtImporterTest and is intended
    ///to contain all ArtImporterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ArtImporterTest
    {
        private DB.IDatabase db = Globals.MySQL_DB; 
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            DDLHelperTest helper = new DDLHelperTest();
            helper.CreateDatabaseTest();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            // set up db
            db.ChangeDatabase("music_test");
            db.ExecuteNonQuery("TRUNCATE song_art");
            db.ExecuteNonQuery("TRUNCATE art");
            db.ExecuteNonQuery("TRUNCATE song");

            // entry 1
            db.ExecuteNonQuery("INSERT INTO song (`file`) VALUES ('duh.jpg')");
            object sid = db.LastInsertID;
            db.ExecuteNonQuery("INSERT INTO art VALUES (NULL, 'duh.jpg', NULL, NULL, NULL, NULL, NULL, NULL)");
            object aid = db.LastInsertID;

            MySqlCommand cmd = new MySqlCommand("INSERT INTO song_art (`song_id`, `art_id`) VALUES (?sid, ?aid)");
            cmd.Parameters.AddWithValue("?sid", sid);
            cmd.Parameters.AddWithValue("?aid", aid);
            db.ExecuteNonQuery(cmd);

            // entry 2
            db.ExecuteNonQuery("INSERT INTO song (`file`) VALUES ('test1.jpg')");
            sid = db.LastInsertID;
            db.ExecuteNonQuery("INSERT INTO art VALUES (NULL, 'test1.jpg', NULL, NULL, NULL, NULL, NULL, NULL)");
            aid = db.LastInsertID;

            cmd = new MySqlCommand("INSERT INTO song_art (`song_id`, `art_id`) VALUES (?sid, ?aid)");
            cmd.Parameters.AddWithValue("?sid", sid);
            cmd.Parameters.AddWithValue("?aid", aid);
            db.ExecuteNonQuery(cmd);
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        
        /// <summary>
        ///A test for GenerateFileName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MusicImporter_Lib.dll")]
        public void GenerateFileNameTest()
        {
            ArtImporter_Accessor target = new ArtImporter_Accessor(db, ""); 
            
            // test strings
            string[] mime_test = new string[] { "IMAGE/JPG", "image/jpg", "image/jpeg", "JPG", 
                "image/png", "PNG", "image/bmp", "BMP", "" };
            
            string exp = @"^\{[a-f0-9]{8}-([a-f0-9]{4}-){3}[a-f0-9]{12}\}.(jpg)|(jpeg)|(png)|(bmp)$";
            Regex regex = new Regex(exp);

            foreach( string mime in mime_test )
            {
                IPicture pic = new Picture();
                pic.MimeType = mime;
                string actual;
                actual = target.GenerateFileName(pic);
                StringAssert.Matches(actual, regex);
            }
        }

        /// <summary>
        ///A test for DeleteOrphanedInserts
        ///</summary>
        [TestMethod()]
        public void DeleteOrphanedInsertsTest()
        {
            DirectoryInfo di = Directory.CreateDirectory("..\\..\\.album_art");
            string file = string.Format( "{0}\\test1.jpg", di.FullName.TrimEnd('\\') );
            FileInfo fi = new FileInfo(file);
            // create then close stream
            fi.Create().Close();
                   
            // Two songs with two arts links have been inserted, the following
            // attempts to delete the art and the links because its has 
            // no matching file. ( duh.jpg )
            ArtImporter target = new ArtImporter(db, di.FullName); 
            uint deleted = target.DeleteOrphanedInserts();
            Assert.IsTrue(deleted == 1);
            // do clean up
            System.IO.File.Delete(file);
        }

        /// <summary>
        ///A test for DeleteOrphanedFiles
        ///</summary>
        [TestMethod()]
        public void DeleteOrphanedFilesTest()
        {
            string file = "..\\..\\.album_art\\DeleteOrphanedFilesTest.jpg ";
            FileInfo fi = new FileInfo(file);
            // create then close stream
            fi.Create().Close();
           
            // The file DeleteOrphanedFilesTest.jpg just create should be deleted 
            // since it is not in database.
            ArtImporter target = new ArtImporter(db, fi.Directory.FullName);
            uint deleted = target.DeleteOrphanedFiles();
            Assert.IsTrue(deleted == 1);
        }
    }
}