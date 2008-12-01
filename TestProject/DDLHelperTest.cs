using MusicImporter_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB = Utility.Data;
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
        private static Utility.Data.IDatabase db = null;
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
            db = Globals.MySQL_DB;
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
        ///A test for ExecuteCreateScript
        ///</summary>
        [TestMethod()]
        public void ExecuteCreateScriptTest()
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
            // todo could be a DE
            string file = @"..\..\..\GUI\bin\Debug\sql\create.sql";
            // needs to be realtive 
            target.ExecuteCreateScript(file);
            db.ChangeDatabase("information_schema");
            ds = db.ExecuteQuery("SELECT * FROM information_schema.SCHEMATA WHERE SCHEMA_NAME='" + schema_name + "'");
            dt = ds.Tables[0];

            Assert.IsTrue(dt.Rows.Count > 0);
        }

        /// <summary>
        ///A test for ExecuteCreateScript
        ///</summary>
        [TestMethod()]
        [DeploymentItem(@"create.sql")]
        public void TestTest()
        {
            Assert.IsTrue( System.IO.File.Exists( @"create.sql"  ) );
        }

   }
}
