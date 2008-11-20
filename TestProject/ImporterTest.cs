using MusicImporter_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using TagLib;
using System.Threading;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for ImporterTest and is intended
    ///to contain all ImporterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImporterTest
    {

        private TestContext testContextInstance;
        private static MusicImporter_Lib.Properties.Settings settings = new MusicImporter_Lib.Properties.Settings();

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
        public static void MyClassInitialize( TestContext testContext )
        {
            // default settings
            settings.Address = "192.168.81.50";
            settings.User = "brian";
            settings.Pass = "sas*0125";
            settings.music_root = "Z:\\";
            settings.Log = false;
            //settings.Dirs = ""; //todo
            //settings.mm_conn_str = ""; //todo
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Connect
        ///</summary>
        [TestMethod()]
        public void ConnectTest()
        {
            Importer target = new Importer( settings ); // TODO: Initialize to an appropriate value
            target.Connect();
        }
        /// <summary>
        ///A test for EscapeInvalidChars
        ///</summary>
        [TestMethod()]
        [DeploymentItem( "MusicImporter_Lib.dll" )]
        public void EscapeInvalidCharsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Importer_Accessor target = new Importer_Accessor( param0 ); // TODO: Initialize to an appropriate value
            StringBuilder[] strs = null; // TODO: Initialize to an appropriate value
            target.EscapeInvalidChars( strs );
            Assert.Inconclusive( "A method that does not return a value cannot be verified." );
        }

        /// <summary>
        ///A test for ComputeHash
        ///</summary>
        [TestMethod()]
        [DeploymentItem( "MusicImporter_Lib.dll" )]
        public void ComputeHashTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Importer_Accessor target = new Importer_Accessor( param0 ); // TODO: Initialize to an appropriate value
            byte[] data = new byte[5] { 0, 1, 2, 3, 4 };
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            //actual = target.ComputeHash( data );
            //Assert.AreEqual( expected, actual );
            Assert.Inconclusive( "Verify the correctness of this test method." );
        }

        /// <summary>
        ///A test for GetKey
        ///</summary>
        [TestMethod()]
        [DeploymentItem( "MusicImporter_Lib.dll" )]
        public void GetKeyTest()
        {
            Importer_Accessor target = new Importer_Accessor( settings ); // TODO: Initialize to an appropriate value
            string table = "art"; // TODO: Initialize to an appropriate value
            string column = "file"; // TODO: Initialize to an appropriate value
            string value = "{36ba30a6-618e-45f9-b2f1-1a0cf505eae9}.jpeg"; // TODO: Initialize to an appropriate value
            Nullable<uint> expected = new Nullable<uint>(2271); // TODO: Initialize to an appropriate value
            Nullable<uint> actual;
            target.Connect();
            actual = target.GetKey( table, column, value );
            //Assert.AreEqual( expected, actual );
            Assert.Inconclusive( "Verify the correctness of this test method." );
        }

        /// <summary>
        ///A test for InsertSong_Art
        ///</summary>
        [TestMethod()]
        [DeploymentItem( "MusicImporter_Lib.dll" )]
        public void InsertSong_ArtTest()
        {
            Importer_Accessor target = new Importer_Accessor( settings ); 
            long song_id = 0; // TODO: Initialize to an appropriate value
            long art_id = 0; // TODO: Initialize to an appropriate value
            //bool result = target.InsertSong_Art( song_id, art_id );
            //Assert.IsTrue( result );
        }


        /// <summary>
        ///A test for InsertArt
        ///</summary>
        [TestMethod()]
        [DeploymentItem( "MusicImporter_Lib.dll" )]
        public void InsertArtTest()
        {
            Importer_Accessor target = new Importer_Accessor(); // TODO: Initialize to an appropriate value
            Tag tag = null; // TODO: Initialize to an appropriate value
            uint song_id = 0; // TODO: Initialize to an appropriate value
            string current_dir = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = false;
            //actual = target.InsertArt( tag, song_id, current_dir );
            Assert.AreEqual( expected, actual );
            Assert.Inconclusive( "Verify the correctness of this test method." );
        }

        /// <summary>
        ///A test for RescanArt
        ///</summary>
        [TestMethod()]
        [DeploymentItem( "MusicImporter_Lib.dll" )]
        public void RescanArtTest()
        {
            Importer_Accessor target = new Importer_Accessor(); // TODO: Initialize to an appropriate value
            //target.RescanArt();
            Assert.Inconclusive( "A method that does not return a value cannot be verified." );
        }

        /// <summary>
        ///A test for Prepare
        ///</summary>
        [TestMethod()]
        [DeploymentItem( "MusicImporter_Lib.dll" )]
        public void PrepareTest()
        {
            Importer_Accessor target = new Importer_Accessor(); // TODO: Initialize to an appropriate value
            //target.Prepare();
            Assert.Inconclusive( "A method that does not return a value cannot be verified." );
        }

        /// <summary>
        ///A test for Scan
        ///</summary>
        [TestMethod()]
        public void ScanTest1()
        {
            Importer target = new Importer(); // TODO: Initialize to an appropriate value
            bool fork = false; // TODO: Initialize to an appropriate value
            target.Scan( fork );
            Assert.Inconclusive( "A method that does not return a value cannot be verified." );
        }

        /// <summary>
        ///A test for Scan
        ///</summary>
        [TestMethod()]
        public void ScanTest()
        {
            Importer target = new Importer(); // TODO: Initialize to an appropriate value
            target.Scan();
            Assert.Inconclusive( "A method that does not return a value cannot be verified." );
        }

        /// <summary>
        ///A test for InsertArt
        ///</summary>
        [TestMethod()]
        [DeploymentItem( "MusicImporter_Lib.dll" )]
        public void InsertArtTest1()
        {
            Importer_Accessor target = new Importer_Accessor(); // TODO: Initialize to an appropriate value
            Tag tag = null; // TODO: Initialize to an appropriate value
            string current_dir = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            //actual = target.InsertArt( tag, current_dir );
            //Assert.AreEqual( expected, actual );
            Assert.Inconclusive( "Verify the correctness of this test method." );
        }

        /// <summary>
        ///A test for Priority
        ///</summary>
        [TestMethod()]
        public void PriorityTest()
        {
            Importer target = new Importer(); // TODO: Initialize to an appropriate value
            ThreadPriority expected = new ThreadPriority(); // TODO: Initialize to an appropriate value
            ThreadPriority actual;
            target.Priority = expected;
            actual = target.Priority;
            Assert.AreEqual( expected, actual );
            Assert.Inconclusive( "Verify the correctness of this test method." );
        }

        /// <summary>
        ///A test for InsertArt
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MusicImporter_Lib.dll")]
        public void InsertArtTest2()
        {
            Importer_Accessor target = new Importer_Accessor(); // TODO: Initialize to an appropriate value
            Tag tag = null; // TODO: Initialize to an appropriate value
            string current_dir = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual = string.Empty;
            //actual = target.InsertArt(tag, current_dir);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
