using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.AccTests
{
    class GuestAccTests : AccTestsForumGenerator
    {
        const string PROXY = "Proxy";
        const string REAL = "Real";

        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;

        public GuestAccTests(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            testGetForums();
            testGetsubForums();
            testGetDiscussions();
            testGetComments();
        }

        private void testGetForums()
        {
            testsLogger.logAction("testing getForums...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
                List<Forum> res;
                Forum tmp;

                /* success tests */

                // At this point there is no existing forums
                res = this.bridge.getForums();
                AssertTrue(res.Count == 0);
                testNum++;

                tmp = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st Forum", "mngr", "mngrPswd");
                res = this.bridge.getForums();
                AssertTrue(res.Count == 1);
                testNum++;

                /* failure tests */

                // TODO add tests here

                if (passed)
                    testsLogger.logAction("getForums tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }
        }

        private void testGetDiscussions()
        {
            testsLogger.logAction("testing getDiscussions...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
                List<Discussion> res;
                List<Forum> forums = this.bridge.getForums();
                Forum f = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
                int fid = f.forumId;
                SubForum sf = this.bridge.createNewSubForum("mngr", "mngrPswd", f.forumId, "title");
                int sfid = sf.subForumId;

                /* success tests */

                Discussion d = this.bridge.createNewDiscussion("mngr", "mngrPswd", fid, sfid, "title", "content");
                res = this.bridge.getDiscussions(fid, sfid);
                AssertEquals(sf.discussions, res);
                testNum++;


                /* failure tests */

                // TODO add tests here

                if (passed)
                    testsLogger.logAction("getDiscussions tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }
        }

        private void testGetComments()
        {
            testsLogger.logAction("testing getComments...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
            //    List<Comment> res;
            //    List<Forum> forums = this.bridge.getForums();
            //    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
            //    int forumId = forum.forumId;
            //    SubForum sf = this.bridge.createNewSubForum("mngr", "mngrPswd", forumId, getUniqueSubForumTitle(forum));
            //    int sfId = sf.subForumId;
            //    Discussion disc = this.bridge.createNewDiscussion("mngr", "mngrPswd", forumId, sfId, "disc", "disc content");
            //    int discId = disc.discussionId;


            //    /* success tests */

            //    res = this.bridge.getComments(forumId, sfId, discId);
            //    AssertEquals(res, disc.comments);
            //    testNum++;

            //    /* failure tests */

            //    // TODO add tests here

            //    if (passed)
            //        testsLogger.logAction("getComments tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }
        }

        private void testGetsubForums()
        {
            testsLogger.logAction("testing getSubForums...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
                List<SubForum> res;
                List<Forum> forums = this.bridge.getForums();
                Forum f = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
                int fid = f.forumId;

                /* success tests */

                res = this.bridge.getSubForums(fid);
                AssertEquals(f.subForums, res);
                testNum++;

                /* failure tests */

                // TODO add tests here


                if (passed)
                    testsLogger.logAction("getSubForums tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }
        }


    }
}
