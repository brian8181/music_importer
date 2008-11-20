using MusicImporter_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Data;
using DB = BKP.Online.Data;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for DatabaseManagerTest and is intended
    ///to contain all DatabaseManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DatabaseManagerTest
    {


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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
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
        ///A test for ExecuteFile
        ///</summary>
        [TestMethod()]
        public void ExecuteFileTest()
        {
            BKP.Online.Data.IDatabase db = new BKP.Online.Data.MySqlDatabase(); // TODO: Initialize to an appropriate value
            db.Open("Data Source=localhost;Port=3306;User Id=root;Password=sas_0125");
            DatabaseManager target = new DatabaseManager(db);
            db.ChangeDatabase("information_schema");
            string schema_name = "music_test";
            DataSet ds = db.ExecuteQuery("SELECT * FROM information_schema.SCHEMATA WHERE SCHEMA_NAME='" + schema_name + "'");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 1)
            {
                Assert.Inconclusive("database does not exist");
                return;
            }

            db.ChangeDatabase(schema_name);
            // needs to be realtive 
            string path = @"C:\Documents and Settings\brian\dev\muisc_importer\GUI\sql\create_music_complete.sql"; 
            target.ExecuteFile(path);
            db.ChangeDatabase("information_schema");
            ds  = db.ExecuteQuery("SELECT * FROM information_schema.SCHEMATA WHERE SCHEMA_NAME='music_test'");
            dt = ds.Tables[0];
            Assert.IsTrue(dt.Rows.Count > 0);
        }

        /// <summary>
        ///A test for CreateDatabase
        ///</summary>
        [TestMethod()]
        public void CreateDatabaseTest()
        {
            BKP.Online.Data.IDatabase db = new BKP.Online.Data.MySqlDatabase();
            db.Open("Data Source=localhost;Port=3306;User Id=root;Password=sas_0125");
            DatabaseManager target = new DatabaseManager(db);
            string schema_name = "music_test";

            db.ExecuteNonQuery("DROP DATABASE IF EXISTS " + schema_name);
            DataSet ds = db.ExecuteQuery("SELECT * FROM information_schema.SCHEMATA WHERE SCHEMA_NAME='" + schema_name + "'");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Assert.Inconclusive("drop database failed");
                return;
            }

            target.CreateDatabase(schema_name);
            db.ChangeDatabase("information_schema");
            ds = db.ExecuteQuery("SELECT * FROM information_schema.SCHEMATA WHERE SCHEMA_NAME='music_test'");
            dt = ds.Tables[0];
            Assert.IsTrue(dt.Rows.Count > 0);
        }
    }
}
