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
        ///A test for GetKey
        ///</summary>
        [TestMethod()]
        [DeploymentItem( "MusicImporter_Lib.dll" )]
        public void GetKeyTest()
        {
            Utility.Data.MySqlDatabase db = Globals.MySQL_DB;

            //DDLHelper helper = new DDLHelper(db);
            //helper.CreateDatabase(music_test);

            DDLHelperTest helper = new DDLHelperTest();
            helper.CreateDatabaseTest();


            Importer_Accessor target = new Importer_Accessor( settings ); // TODO: Initialize to an appropriate value
            string table = "art"; // TODO: Initialize to an appropriate value
            string column = "file"; // TODO: Initialize to an appropriate value
            string value = "{36ba30a6-618e-45f9-b2f1-1a0cf505eae9}.jpeg"; // TODO: Initialize to an appropriate value
            //Nullable<uint> expected = new Nullable<uint>(2271); // TODO: Initialize to an appropriate value
            Nullable<uint> actual;
            target.Connect();
            actual = target.GetKey( table, column, value );
            //Assert.IsNotNull( expected );
            Assert.Inconclusive("TODO");
        }
    }
}
