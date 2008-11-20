using MusicImporter_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagLib;
using BKP.Online.Data;
using System.Text.RegularExpressions;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for ArtImporterTest and is intended
    ///to contain all ArtImporterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ArtImporterTest
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
        ///A test for GenerateFileName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MusicImporter_Lib.dll")]
        public void GenerateFileNameTest()
        {
            IDatabase db = Globals.MySQL_DB;

            //PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ArtImporter_Accessor target = new ArtImporter_Accessor(db, ""); // TODO: Initialize to an appropriate value
            IPicture pic = new Picture();
            pic.MimeType = "IMAGE/JPG";

            string actual;
            actual = target.GenerateFileName(pic);
            StringAssert.EndsWith(actual, ".jpg");
            string exp = @"^\{[a-f0-9]{8}-([a-f0-9]{4}-){3}[a-f0-9]{12}\}.\w+$";
            Regex regex = new Regex(exp);
            StringAssert.Matches(actual, regex);
        }
    }
}
