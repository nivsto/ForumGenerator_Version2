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
        const string SU_NAME = "admin";//ForumGenerator_Version2_Server.Sys.ForumGeneratorDefs.SU_USERNAME; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator_Version2_Server.Sys.ForumGeneratorDefs.SU_PSWD;

        public GuestAccTests(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
            this.bridge.reset();
        }

        public override void runTests()
        {
            this.testsLogger.logTestsSection("Guest");
            Console.WriteLine("testing GetForums:");
            test(testGetForums);
            Console.WriteLine("Done \n");

            Console.WriteLine("testing GetsubForums:");
            test(testGetsubForums);
            Console.WriteLine("Done \n");

            Console.WriteLine("testing GetDiscussions:");
            test(testGetDiscussions);
            Console.WriteLine("Done\n");
            
            Console.WriteLine("testing GetComments:");
            test(testGetComments);
            Console.WriteLine("Done \n");
        }

        private int testGetForums()
        {
            {
                int testNum = 0;
                this.bridge.reset();

                List<Forum> res;

                /* success tests */
                try
                {
                    this.bridge.reset();
                    res = this.bridge.getForums();
                    AssertTrue(res.Count == 0);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd", Forum.RegPolicy.NONE);

                    res = this.bridge.getForums();
                    AssertTrue(res.Count == 1);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    for (int i=0; i<100; i++)
                        this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum" + i, "mngr" + i, "mngrPswd" + i, Forum.RegPolicy.NONE);

                    res = this.bridge.getForums();
                    AssertTrue(res.Count == 100);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                /* failure tests */

                return testNum;
            }
        }

        private int testGetsubForums()
        {
            {
                int testNum = 0;

                List<SubForum> res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd", Forum.RegPolicy.NONE);
                    res = this.bridge.getSubForums(forum.forumId);
                    AssertTrue(res.Count == 0);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd", Forum.RegPolicy.NONE);
                    this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    for(int i=0; i<100; i++)
                        this.bridge.createNewSubForum("mngr", "mngrPswd", forum.forumId, "subForum" + i);

                    res = this.bridge.getSubForums(forum.forumId);
                    AssertTrue(res.Count == 100);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                /* failure tests */

                return testNum;
            }
        }

        private int testGetDiscussions()
        {
            {
                int testNum = 0;

                List<Discussion> res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd", Forum.RegPolicy.NONE);
                    this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    SubForum subForum = this.bridge.createNewSubForum("mngr", "mngrPswd", forum.forumId, "subForum");
                    res = this.bridge.getDiscussions(forum.forumId, subForum.subForumId);
                    AssertTrue(res.Count == 0);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd", Forum.RegPolicy.NONE);
                    this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    SubForum subForum = this.bridge.createNewSubForum("mngr", "mngrPswd", forum.forumId, "subForum");
                    for (int i = 0; i < 100; i++)
                        this.bridge.createNewDiscussion("mngr", "mngrPswd", forum.forumId, subForum.subForumId, "discussion" + i, "no content");

                    res = this.bridge.getDiscussions(forum.forumId, subForum.subForumId);
                    AssertTrue(res.Count == 100);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                /* failure tests */

                return testNum;
            }
        }

        private int testGetComments()
        {
            {
                int testNum = 0;

                List<Comment> res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd", Forum.RegPolicy.NONE);
                    this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    SubForum subForum = this.bridge.createNewSubForum("mngr", "mngrPswd", forum.forumId, "subForum");
                    Discussion discussion = this.bridge.createNewDiscussion("mngr", "mngrPswd", forum.forumId, subForum.subForumId, "discussion", "no content");
                    res = this.bridge.getComments(forum.forumId, subForum.subForumId, discussion.discussionId);
                    AssertTrue(res.Count == 0);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd", Forum.RegPolicy.NONE);
                    this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    SubForum subForum = this.bridge.createNewSubForum("mngr", "mngrPswd", forum.forumId, "subForum");
                    Discussion discussion = this.bridge.createNewDiscussion("mngr", "mngrPswd", forum.forumId, subForum.subForumId, "discussion", "no content");
 
                    for (int i = 0; i < 100; i++)
                        this.bridge.createNewComment("mngr", "mngrPswd", forum.forumId, subForum.subForumId, discussion.discussionId, "no content");

                    res = this.bridge.getComments(forum.forumId, subForum.subForumId, discussion.discussionId);
                    AssertTrue(res.Count == 100);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                /* failure tests */

                return testNum;
            }
        }
    }
}
