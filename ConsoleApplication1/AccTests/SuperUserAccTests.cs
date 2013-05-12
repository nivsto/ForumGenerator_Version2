using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.AccTests
{
    class SuperUserAccTests : AccTestsForumGenerator
    {
        const string PROXY = "Proxy";
        const string REAL = "Real";

        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;

        public SuperUserAccTests(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            testSuperUserLogin();
            testSuperUserLogout();
            testCreateNewForum();
            testGetMutualUsers();
        }

        private void testSuperUserLogin()
        {
            testsLogger.logAction("testing superUserLogin...  ");
            bool passed = true;
            int testNum = 1;

            SuperUser res;

            /* success tests */
            try
            {
                res = this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                AssertEquals(SU_NAME, res.userName);
                AssertEquals(SU_PSWD, res.password);
                testNum++;
            }
            catch
            {
                failMsg(testNum++);
            }

            this.bridge.reset();

            /* failure tests */

            try
            {
                res = this.bridge.superUserLogin("wrong user name", SU_PSWD);
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            this.bridge.reset();

            try
            {
                res = this.bridge.superUserLogin(SU_NAME, "wrong password");
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            this.bridge.reset();

            try
            {
                res = this.bridge.superUserLogin(SU_NAME, null);
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            this.bridge.reset();

            // more tests here

            if (passed)
                testsLogger.logAction("superUserLogin tests PASSED\n");
        }

        private void testSuperUserLogout()
        {
            testsLogger.logAction("testing superUserLogout...  ");
            int testNum = 1;
            bool passed = true;

            bool res;
            SuperUser tmp;

            /* success tests */
            try
            {
                tmp = this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.superUserLogout(SU_NAME, SU_PSWD);
                AssertTrue(res);
                testNum++;
            }
            catch
            {
                failMsg(testNum++);
            }

            this.bridge.reset();

            /* failure tests */

            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.superUserLogout("wrong username", SU_PSWD);
                passed = false;
                failMsg(testNum++);
            }
            catch { testNum++; }

            this.bridge.reset();

            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.superUserLogout(SU_NAME, "wrong password");
                passed = false;
                failMsg(testNum++);
            }
            catch { testNum++; }

            this.bridge.reset();

            try
            {
                res = this.bridge.superUserLogout(SU_NAME, "wrong password");
                passed = false;
                failMsg(testNum++);
            }
            catch { testNum++; }


            if (passed)
                testsLogger.logAction("superUserLogout tests PASSED\n");

            this.bridge.reset();
        }

        private void testCreateNewForum()
        {
            testsLogger.logAction("testing createNewForum...  ");
            int testNum = 1;
            bool passed = true;

            Forum res;
            List<Forum> forums = this.bridge.getForums();

            try
            {
                /* success tests */
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr", "mngrPswd");
                AssertTrue(res.forumId > 0);
                forums = this.bridge.getForums();
                AssertTrue(isForumExist(forums, "2nd forum"));
                testNum++;
            }
            catch
            {
                failMsg(testNum++);
            }

            /* failure tests */
            this.bridge.reset();

            //wrong superUserName
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum("wrong user", SU_PSWD, "unique Forum", "forum mngr", "pswd");
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++;}

            this.bridge.reset();

            //wrong superUserPassword
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, "wrong pswd", "unique Forum", "forum mngr", "pswd");
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            this.bridge.reset();

            //SuperUser wasn't logged in
            try
            {
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr", "pswd");
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            this.bridge.reset();

            //Forum name already exist
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr", "pswd");
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr2", "pswd2");
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            this.bridge.reset();

            //Forum name with wrong characters (not A-Z,a-z,0-9)
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd~;forum", "mngr", "pswd");
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            this.bridge.reset();

            //Forum Admin UserName with wrong characters (not A-Z,a-z,0-9)
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr;*$#", "pswd");
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            this.bridge.reset();

            //Forum Admin Password with wrong characters (not A-Z,a-z,0-9)
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr", "pswd;(*%^)");
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            if (passed)
                testsLogger.logAction("createNewForum tests PASSED\n");
        }

        private void testGetMutualUsers()
        {
            testsLogger.logAction("testing getMutualUsers...  ");
            int testNum = 1;
            bool passed = true;

            List<User> res;

            try
            {
                /* success tests */
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum1 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
                Forum forum2 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr2", "mngrPswd2");

                res = this.bridge.getMutualUsers(SU_NAME, SU_PSWD, forum1.forumId, forum2.forumId);
                AssertTrue(res.Count() == 0);

                this.bridge.register(forum1.forumId, "usr1", "pswd1", "", "");
                this.bridge.register(forum2.forumId, "usr1", "pswd2", "", "");

                res = this.bridge.getMutualUsers(SU_NAME, SU_PSWD, forum1.forumId, forum2.forumId);
                AssertTrue(res.Count() == 1);

                testNum++;
            }
            catch
            {
                failMsg(testNum++);
            }

            /* failure tests */
            this.bridge.reset();

            //wrong superUserName
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum1 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
                Forum forum2 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr2", "mngrPswd2");

                res = this.bridge.getMutualUsers("wrong user", SU_PSWD, forum1.forumId, forum2.forumId);
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            //wrong superUserPassword
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum1 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
                Forum forum2 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr2", "mngrPswd2");

                res = this.bridge.getMutualUsers(SU_NAME, "wrong pass", forum1.forumId, forum2.forumId);
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            //Invalid ForumId
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum1 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
                Forum forum2 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr2", "mngrPswd2");

                res = this.bridge.getMutualUsers(SU_NAME, "wrong pass", -5, forum2.forumId);
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            if (passed)
                testsLogger.logAction("getMutualUsers tests PASSED\n");
        }

    }
}
