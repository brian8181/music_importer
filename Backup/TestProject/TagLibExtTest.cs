using MusicImporter_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagLib;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for TagLibExtTest and is intended
    ///to contain all TagLibExtTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TagLibExtTest
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
        ///A test for MediaSHA1
        ///</summary>
        [TestMethod()]
        public void MediaSHA1Test()
        {
            File file1 = TagLib.File.Create( @"..\..\..\TestProject\test_files\test1.mp3" );
            byte[] b1 = TagLibExt.MediaSHA1(file1);

            File file3 = TagLib.File.Create(@"..\..\..\TestProject\test_files\test3.mp3");
            byte[] b3 = TagLibExt.MediaSHA1(file3);

            bool is_equal = Utility.Functions.CompareBytes( b1, b3 );
            Assert.IsTrue(is_equal);
    
            File file2 = TagLib.File.Create(@"..\..\..\TestProject\test_files\test2.mp3");
            byte[] b2 = TagLibExt.MediaSHA1(file2);
            
            is_equal = Utility.Functions.CompareBytes( b1, b2 );
            Assert.IsFalse(is_equal);

        }
    }
}
