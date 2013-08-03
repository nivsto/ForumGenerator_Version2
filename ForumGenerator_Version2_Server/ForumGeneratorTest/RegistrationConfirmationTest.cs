using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server.ForumData;

namespace ForumGeneratorTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class RegistrationConfirmationTest
    {
        public RegistrationConfirmationTest()
        {
            //
            // TODO: Add constructor logic here
            //
            this.fg = new ForumGenerator("admin", "admin");
        }

        private TestContext testContextInstance;
        private ForumGenerator fg;
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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
            try
            {
                this.fg.superUserLogin("admin", "admin");
                Forum f = this.fg.createNewForum("admin", "admin", "activation_forum", "admin1", "admin1", ForumGenerator_Version2_Server.ForumData.Forum.RegPolicy.MAIL_ACTIVATION);
                this.fg.login(f.forumId, "admin1", "admin1");
            }
            catch (Exception)
            {

            }

        }
    }
}
