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
        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;

        public GuestAccTests(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            this.testsLogger.logTestsSection("Member");
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

                List<Forum> res;

                /* success tests */
                try
                {
                    res = this.bridge.getForums();
                    AssertTrue(res.Count == 0);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");

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
                        this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum"+i, "mngr"+i, "mngrPswd"+i);

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
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    res = this.bridge.getSubForums(forum.forumId);
                    AssertTrue(res.Count == 0);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    for(int i=0; i<100; i++)
                        this.bridge.createNewSubForum(SU_NAME, SU_PSWD, forum.forumId, "subForum" + i);

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
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    SubForum subForum = this.bridge.createNewSubForum(SU_NAME, SU_PSWD, forum.forumId, "subForum");
                    res = this.bridge.getDiscussions(forum.forumId, subForum.subForumId);
                    AssertTrue(res.Count == 0);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    SubForum subForum = this.bridge.createNewSubForum(SU_NAME, SU_PSWD, forum.forumId, "subForum");
                    for (int i = 0; i < 100; i++)
                        this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion" + i, "no content");

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
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    SubForum subForum = this.bridge.createNewSubForum(SU_NAME, SU_PSWD, forum.forumId, "subForum");
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion", "no content");
                    res = this.bridge.getComments(forum.forumId, subForum.subForumId, discussion.discussionId);
                    AssertTrue(res.Count == 0);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    SubForum subForum = this.bridge.createNewSubForum(SU_NAME, SU_PSWD, forum.forumId, "subForum");
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion", "no content");
 
                    for (int i = 0; i < 100; i++)
                        this.bridge.createNewComment(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, discussion.discussionId, "no content");

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
