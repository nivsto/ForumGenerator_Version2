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

        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;

        public SuperUserAccTests(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            this.testsLogger.logTestsSection("SuperUser");
            test(testSuperUserLogin);
            test(testSuperUserLogout);
            test(testCreateNewForum);
            test(testGetMutualUsers);
        }

        private int testSuperUserLogin()
        {
            int testNum = 0;
            SuperUser res;

            /* success tests */
            try
            {
                res = this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                AssertEquals(SU_NAME, res.userName);
                AssertEquals(SU_PSWD, res.password);
                testNum++;
            }
            catch { failMsg(testNum); }

            this.bridge.reset();

            /* failure tests */

            try
            {
                res = this.bridge.superUserLogin("wrong user name", SU_PSWD);
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            try
            {
                res = this.bridge.superUserLogin(SU_NAME, "wrong password");
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            try
            {
                res = this.bridge.superUserLogin(SU_NAME, null);
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            return testNum;
        }

        private int testSuperUserLogout()
        {
            int testNum = 0;

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
                failMsg(testNum);
            }

            this.bridge.reset();

            /* failure tests */

            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.superUserLogout("wrong username", SU_PSWD);
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.superUserLogout(SU_NAME, "wrong password");
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            try
            {
                res = this.bridge.superUserLogout(SU_NAME, "wrong password");
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            return testNum;
        }

        private int testCreateNewForum()
        {
            int testNum = 0;

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
                failMsg(1);
            }

            /* failure tests */
            this.bridge.reset();

            //wrong superUserName
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum("wrong user", SU_PSWD, "unique Forum", "forum mngr", "pswd");
                failMsg(2);
            }
            catch { testNum++;}

            this.bridge.reset();

            //wrong superUserPassword
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, "wrong pswd", "unique Forum", "forum mngr", "pswd");
                failMsg(3);
            }
            catch { testNum++; }

            this.bridge.reset();

            //SuperUser wasn't logged in
            try
            {
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr", "pswd");
                failMsg(4);
            }
            catch { testNum++; }

            this.bridge.reset();

            //Forum name already exist
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr", "pswd");
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr2", "pswd2");
                failMsg(5);
            }
            catch { testNum++; }

            this.bridge.reset();

            //Forum name with wrong characters (not A-Z,a-z,0-9)
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd~;forum", "mngr", "pswd");
                failMsg(6);
            }
            catch { testNum++; }

            this.bridge.reset();

            //Forum Admin UserName with wrong characters (not A-Z,a-z,0-9)
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr;*$#", "pswd");
                failMsg(7);
            }
            catch { testNum++; }

            this.bridge.reset();

            //Forum Admin Password with wrong characters (not A-Z,a-z,0-9)
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr", "pswd;(*%^)");
                failMsg(8);
            }
            catch { testNum++; }

            this.bridge.reset();

            return testNum;
        }

        private int testGetMutualUsers()
        {
            int testNum = 0;

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
                failMsg(1);
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
                failMsg(2);
            }
            catch { testNum++; }

            this.bridge.reset();

            //wrong superUserPassword
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum1 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
                Forum forum2 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr2", "mngrPswd2");

                res = this.bridge.getMutualUsers(SU_NAME, "wrong pass", forum1.forumId, forum2.forumId);
                failMsg(3);
            }
            catch { testNum++; }

            this.bridge.reset();

            //Invalid ForumId
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum1 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
                Forum forum2 = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr2", "mngrPswd2");

                res = this.bridge.getMutualUsers(SU_NAME, "wrong pass", -5, forum2.forumId);
                failMsg(4);
            }
            catch { testNum++; }

            this.bridge.reset();

            return testNum;
        }

    }
}
