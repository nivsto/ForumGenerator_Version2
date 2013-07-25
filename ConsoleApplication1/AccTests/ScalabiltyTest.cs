using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.ForumData;

namespace ConsoleApplication1.AccTests
{
    class ScalabiltyTest : AccTestsForumGenerator
    {
        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;
        const string ADMIN_NAME = "mngr";
        const string ADMIN_PSWD = "mngrPswd";


        public ScalabiltyTest(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            this.testsLogger.logTestsSection("Scalabilty Tests");

            Console.WriteLine("testing LargeScalability:");
            test(LargeScalability);
            Console.WriteLine("Done \n");
            
        }



        /* Large Scalabiliy - lots of comments and discussions */

        private int LargeScalability()
        {
            int testNum = 0;

            /* testGetNumOfCommentsSubForum */
            try
            {
                int res;

                const int DISCUSSIONS_NUMBER = 100;
                const int COMMENTS_NUMBER = 20;
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                res = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(res == 0);

                user1 = this.bridge.login(forum.forumId, "user1", "pswd1");
                user2 = this.bridge.login(forum.forumId, "user2", "pswd2");
                Discussion[] discussions = new Discussion[DISCUSSIONS_NUMBER];
                for (int i = 0; i < DISCUSSIONS_NUMBER; i++)
                    discussions[i] = this.bridge.createNewDiscussion("user1", "pswd1", forum.forumId, subForum.subForumId, "discussion" + i, "no content");
                res = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(res == DISCUSSIONS_NUMBER);

                for (int i = 1; i <= COMMENTS_NUMBER; i++)
                {
                    this.bridge.createNewComment("user1", "pswd1", forum.forumId, subForum.subForumId, discussions[35].discussionId, "no content");
                    this.bridge.createNewComment("user2", "pswd2", forum.forumId, subForum.subForumId, discussions[55].discussionId, "no content");
                    this.bridge.createNewComment("user2", "pswd2", forum.forumId, subForum.subForumId, discussions[75].discussionId, "no content");
                }
                res = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(res == COMMENTS_NUMBER * 3 + DISCUSSIONS_NUMBER);

                testNum++;
            }
            catch { failMsg(testNum++); }

            this.bridge.reset();




            ///* getNumOfCommentsSingleUser */
            try
            {
                int res;

                const int DISCUSSIONS_NUMBER = 10;
                const int COMMENTS_NUMBER = 25;
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, user1.userName);
                AssertTrue(res == 0);

                user1 = this.bridge.login(forum.forumId, "user1", "pswd1");
                user2 = this.bridge.login(forum.forumId, "user2", "pswd2");
                Discussion[] discussions1 = new Discussion[DISCUSSIONS_NUMBER];
                Discussion[] discussions2 = new Discussion[DISCUSSIONS_NUMBER];
                for (int i = 0; i < DISCUSSIONS_NUMBER; i++)
                {
                    discussions1[i] = this.bridge.createNewDiscussion(user1.userName, user1.password, forum.forumId, subForum.subForumId, "discussion" + i, "no content");
                    discussions2[i] = this.bridge.createNewDiscussion(user2.userName, user2.password, forum.forumId, subForum.subForumId, "discussion" + i, "no content");
                }
                res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, user1.userName);
                AssertTrue(res == DISCUSSIONS_NUMBER);
                res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, user2.userName);
                AssertTrue(res == DISCUSSIONS_NUMBER);

                for (int i = 1; i <= COMMENTS_NUMBER; i++)
                {
                    this.bridge.createNewComment(user1.userName, user1.password, forum.forumId, subForum.subForumId, discussions1[DISCUSSIONS_NUMBER- 3].discussionId, "no content");
                    this.bridge.createNewComment(user2.userName, user2.password, forum.forumId, subForum.subForumId, discussions2[DISCUSSIONS_NUMBER - 5].discussionId, "no content");
                    this.bridge.createNewComment(user2.userName, user2.password, forum.forumId, subForum.subForumId, discussions2[DISCUSSIONS_NUMBER - 9].discussionId, "no content");
                }
                res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, user1.userName);
                AssertTrue(res == DISCUSSIONS_NUMBER + COMMENTS_NUMBER);
                res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, user2.userName);
                AssertTrue(res == DISCUSSIONS_NUMBER + 2 * COMMENTS_NUMBER);

                testNum++;
            }
            catch { failMsg(testNum++); }

            this.bridge.reset();


            ///* getResponsersForSingleUser */
            try
            {
                List<User> res;

                const int USERS_NUMBER = 10;
                const int DISCUSSSION_NEMBER = 25;
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                User[] users = new User[USERS_NUMBER];
                for (int i = 0; i < USERS_NUMBER; i++)
                    users[i] = this.bridge.register(forum.forumId, "user" + i, "pswd" + i, "", "");

                res = this.bridge.getResponsersForSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, users[5].userName);
                AssertTrue(res.Count == 0);

                for (int i = 0; i < USERS_NUMBER; i++)
                    this.bridge.login(forum.forumId, users[i].userName, users[i].password);
                Discussion[] discussions = new Discussion[DISCUSSSION_NEMBER];
                for (int i = 0; i < DISCUSSSION_NEMBER; i++)
                    discussions[i] = this.bridge.createNewDiscussion(users[i % USERS_NUMBER].userName, users[i % USERS_NUMBER].password, forum.forumId, subForum.subForumId, "discussion" + i, "no content");
                res = this.bridge.getResponsersForSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, users[5].userName);
                AssertTrue(res.Count == 0);

                for (int i = 0; i < USERS_NUMBER; i++)
                    this.bridge.createNewComment(users[i].userName, users[i].password, forum.forumId, subForum.subForumId, discussions[0].discussionId, "no content");


                res = this.bridge.getResponsersForSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, users[0].userName);
                AssertTrue(res.Count == USERS_NUMBER); 


                testNum++;
            }
            catch { failMsg(testNum++); }

            this.bridge.reset();

            return testNum;

        }
    }
}
