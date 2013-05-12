using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.AccTests
{
    class AdminAccTests : AccTestsForumGenerator
    {
        const string PROXY = "Proxy";
        const string REAL = "Real";

        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;

        public AdminAccTests(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            testCreateNewSubForum();
            testAddModerator();
            testRemoveModerator();
            testGetNumOfCommentsSubForum();
            testGetNumOfCommentsSingleUser();
            testGetResponsersForSingleUser();
        }

        private void testGetResponsersForSingleUser()
        {
            throw new NotImplementedException();
        }

        private void testGetNumOfCommentsSingleUser()
        {
            throw new NotImplementedException();
        }

        private void testGetNumOfCommentsSubForum()
        {
            throw new NotImplementedException();
        }

        private void testRemoveModerator()
        {
            throw new NotImplementedException();
        }

        private void testCreateNewSubForum()
        {
            testsLogger.logAction("testing createNewSubForum...  ");
            int testNum = 1;
            bool passed = true;

            string ADMIN_NAME = "mngr";
            string ADMIN_PSWD = "mngrPswd";

            SubForum res;
            this.bridge.superUserLogin(SU_NAME, SU_PSWD);
            Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", ADMIN_NAME, ADMIN_PSWD);
            this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);


            /* success tests */
            try
            {
                res = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "title1");
                AssertTrue(isSubForumExist(forum, res.subForumTitle));
                testNum++;

                int subForumsCountPre = forum.subForums.Count;
                res = this.bridge.createNewSubForum("mngr", "mngrPswd", forum.forumId, null);
                AssertEquals(subForumsCountPre + 1, forum.subForums.Count);
                testNum++;

            }
            catch
            {
                failMsg(testNum++);
            }
            /* failure tests */
            string newTitle = getUniqueSubForumTitle(forum);

            try
            {
                res = this.bridge.createNewSubForum("not mngr", "mngrPswd", forum.forumId, newTitle);
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            try
            {
                // invalid forumId
                res = this.bridge.createNewSubForum("mngr", "mngrPswd", -1, newTitle);
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            try
            {
                // case sensitive
                res = this.bridge.createNewSubForum("mngr", "MNGRPSWD", forum.forumId, newTitle);
                failMsg(testNum++);
                passed = false;
            }
            catch { testNum++; }

            // more tests here

            if (passed)
                testsLogger.logAction("createNewSubForum tests PASSED\n");

        }

        private void testAddModerator()
        {

        }


    }
}
