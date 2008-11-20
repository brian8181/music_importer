using MusicImporter_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB = BKP.Online.Data;
using System.Data;

namespace TestProject
{
    
    /// <summary>
    ///This is a test class for DDLHelperTest and is intended
    ///to contain all DDLHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DDLHelperTest
    {
        private static BKP.Online.Data.IDatabase db = new BKP.Online.Data.MySqlDatabase();
        private static string schema_name = "music_test";
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            db.Open("Data Source=localhost;Port=3306;User Id=root;Password=sas_0125");
            db.ExecuteNonQuery("DROP DATABASE IF EXISTS " + schema_name); // drop test db
           
            DataSet ds = db.ExecuteQuery("SELECT * FROM information_schema.SCHEMATA WHERE SCHEMA_NAME='" + schema_name + "'");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Assert.Inconclusive("drop database failed");
                return;
            }
        }
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            db.Close();
        }
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
        ///A test for CreateScript
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MusicImporter_Lib.dll")]
        public void CreateScriptTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            DDLHelper_Accessor target = new DDLHelper_Accessor(param0); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.CreateScript;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ExecuteFile
        ///</summary>
        [TestMethod()]
        public void ExecuteFileTest()
        {
            CreateDatabaseTest();

            DDLHelper target = new DDLHelper(db);
            db.ChangeDatabase("information_schema");
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
            ds = db.ExecuteQuery("SELECT * FROM information_schema.SCHEMATA WHERE SCHEMA_NAME='music_test'");
            dt = ds.Tables[0];

            Assert.IsTrue(dt.Rows.Count > 0);
        }
             
        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethod()]
        public void ExecuteTest()
        {
            DB.IDatabase db = null; // TODO: Initialize to an appropriate value
            DDLHelper target = new DDLHelper(db); // TODO: Initialize to an appropriate value
            target.Execute();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CreateDatabase
        ///</summary>
        [TestMethod()]
        public void CreateDatabaseTest()
        {

            DDLHelper target = new DDLHelper(db);
            target.CreateDatabase(schema_name);
            db.ChangeDatabase("information_schema");
            DataSet ds = db.ExecuteQuery("SELECT * FROM information_schema.SCHEMATA WHERE SCHEMA_NAME='music_test'");
            DataTable dt = ds.Tables[0];

            Assert.IsTrue(dt.Rows.Count > 0);
        }


        /// <summary>
        ///A test for ExecuteAll
        ///</summary>
        [TestMethod()]
        public void ExecuteCommandTest()
        {
            CreateDatabaseTest();
            ExecuteFileTest();
            db.ChangeDatabase("music_test");
            DDLHelper target = new DDLHelper(db); // TODO: Initialize to an appropriate value
            string path = @"C:\Documents and Settings\brian\dev\muisc_importer\GUI\sql\procedures"; // TODO: Initialize to an appropriate value
            target.ExecuteDirectory(path);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");

            db.ChangeDatabase("information_schema");
            //DataSet ds = db.ExecuteQuery(SELECT * FROM information_schema.ROUTINES WHERE ROUTINE_SCHEMA='music_test';);
            //DataTable dt = ds.Tables[0];
            //Assert.IsTrue(  dt.Rows.Count != 1 );
            
        }
    }
}
