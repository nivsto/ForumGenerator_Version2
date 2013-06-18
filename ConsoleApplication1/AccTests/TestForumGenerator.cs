using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumGenerator_Version2_Server;
using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;
using System.Threading;
using System.Net;
using System.Xml;
using System.IO;
using ConsoleApplication1.AccTests;

namespace ConsoleApplication1
{
    public class TestForumGenerator
    {
        const string PROXY = "Proxy";
        const string REAL = "Real";

        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;

        protected TestsLogger testsLogger;
        protected BridgeForumGenerator bridge;
        protected string mode;

        // constructor
        public TestForumGenerator()
        {
            this.bridge = new ProxyForumGeneratorImpl();
            this.mode = PROXY;
        }

        // constructor
        public TestForumGenerator(ForumGenerator forumGen, string outFile)
        {
            this.bridge = new RealForumGeneratorImpl(forumGen);
            this.testsLogger = new TestsLogger(outFile);
            this.mode = REAL;
        }


        // main function
        public void runTests()
        {
            this.testsLogger.logAction("\n** ForumGenerator Tests **\n" +
                                        "Testing on " + this.mode + " mode\n");

            SuperUserAccTests superUserAccTests = new SuperUserAccTests(this.testsLogger, this.bridge);
            AdminAccTests adminAccTests = new AdminAccTests(this.testsLogger, this.bridge);
            MemberAccTests memberAccTests = new MemberAccTests(this.testsLogger, this.bridge);
            GuestAccTests guestAccTests = new GuestAccTests(this.testsLogger, this.bridge);
            AdvancedTest advancedTest = new AdvancedTest(this.testsLogger, this.bridge);
            ThreadTest threadTest = new ThreadTest(this.testsLogger, this.bridge);

            //superUserAccTests.runTests();
            //adminAccTests.runTests();
            //memberAccTests.runTests();
            //guestAccTests.runTests();
            //advancedTest.runTests();
            threadTest.runTests();
            //sumTests();
        }

        /************************************************************/
        /*                   T E S T I N G
        /************************************************************/

        // Server definitions
        const string GUEST = "guest";
        const string MEMBER = "member";
        const string MODERATOR = "moderator";
        const string ADMIN = "administrator";
        const string SUPER_USER = "superuser";
        const string OK = "ok";

        // Tester definitions
        const string DUMMY = "dummy";




    }
}

