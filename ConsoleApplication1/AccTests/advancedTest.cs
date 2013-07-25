using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.ForumData;

namespace ConsoleApplication1.AccTests
{
    class AdvancedTest : AccTestsForumGenerator
    {
        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;
        const string ADMIN_NAME = "mngr";
        const string ADMIN_PSWD = "mngrPswd";


        public AdvancedTest(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            this.testsLogger.logTestsSection("Advanced Tests");

            Console.WriteLine("testing advancedTest1:");
            test(advancedTest1);
            Console.WriteLine("Done \n");

            Console.WriteLine("testing advancedTest2:");
            test(advancedTest2);
            Console.WriteLine("Done \n");

            Console.WriteLine("testing LargeScalability:");
           // test(LargeScalability);
            Console.WriteLine("Done \n");
            
        }

        private int advancedTest1()
        {
            int testNum = 1;
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                Discussion d = this.bridge.createNewDiscussion(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");
                this.bridge.login(forum.forumId, "user1", "pswd1");

                for (int i = 1; i <= 10; i++)
                {
                    this.bridge.createNewComment("user1", "pswd1", forum.forumId, subForum.subForumId, d.discussionId, "no content");
                }
                Boolean logoutAns = this.bridge.logout(forum.forumId, user.userName, user.password);
                AssertFalse(user.isLoggedIn);
                AssertTrue(logoutAns);

                int num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 11);

                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");
                user2 = this.bridge.login(forum.forumId, "user2", "pswd2");

                for (int i = 1; i <= 100; i++)
                {
                    this.bridge.createNewComment(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId, d.discussionId, "no content");
                }
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 111);

                d = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, d.discussionId, ADMIN_NAME, ADMIN_PSWD, "brand new content");
                AssertEquals(d.content, "brand new content");
                

                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 111);

                Discussion[] discussions = new Discussion[100];
                for (int i = 0; i <= 99; i++)
                    discussions[i] = this.bridge.createNewDiscussion("user2", "pswd2", forum.forumId, subForum.subForumId, "discussion" + i, "no content");
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 211);

                for (int i = 1; i <= 10; i++)
                    d = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, discussions[i].discussionId, "user2", "pswd2", "brand new content" + i);
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 211);

            }
            catch { failMsg(testNum++); }

            this.bridge.reset();
            return testNum++;
        }


        private int advancedTest2()
        {
            int testNum = 1;
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");

                bool moderatorRes = this.bridge.addModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);
                AssertTrue(moderatorRes);
                this.bridge.login(forum.forumId, "user1", "pswd1");
                Discussion d = this.bridge.createNewDiscussion("user1", "pswd1", forum.forumId, subForum.subForumId, "discussion1", "no content");

                User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");
                user2 = this.bridge.login(forum.forumId, "user2", "pswd2");

                for (int i = 1; i <= 30; i++)
                {
                    this.bridge.createNewComment("user2", "pswd2", forum.forumId, subForum.subForumId, d.discussionId, "no content");
                }
                //moderator deletes discussion
                this.bridge.deleteDiscussion(forum.forumId, subForum.subForumId, d.discussionId, "user1", "pswd1");
                //remove moderator
                moderatorRes = this.bridge.removeModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);
                //AssertFalse(subForum.moderators.Contains(user));
                AssertTrue(moderatorRes);
                int num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 0);
                //add new moderator
                moderatorRes = this.bridge.addModerator("user2", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);
                //AssertTrue(subForum.moderators.Contains(user));
                AssertTrue(moderatorRes);

            }

            catch { failMsg(testNum); }

            this.bridge.reset();
            return testNum;

        }


    }
}
