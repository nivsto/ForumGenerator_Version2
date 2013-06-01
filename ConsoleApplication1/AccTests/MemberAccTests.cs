using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.AccTests
{
    class MemberAccTests : AccTestsForumGenerator
    {
        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;
        const string ADMIN_NAME = "mngr";
        const string ADMIN_PSWD = "mngrPswd";


        public MemberAccTests(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            this.testsLogger.logTestsSection("Member");
            Console.WriteLine("testing Login:");
            test(testLogin);
            Console.WriteLine("Done \n");

            Console.WriteLine("testing Logout:");
            test(testLogout);
            Console.WriteLine("Done \n");

            Console.WriteLine("testing CreateNewDiscussion:");
            test(testCreateNewDiscussion);
            Console.WriteLine("Done \n");

            Console.WriteLine("testing CreateNewComment:");
            test(testCreateNewComment);
            Console.WriteLine("Done\n");

            Console.WriteLine("testing DeleteDiscussion:");
            test(testDeleteDiscussion);
            Console.WriteLine("Done\n");

            Console.WriteLine("testing EditDiscussion:");
            test(testEditDiscussion);
            Console.WriteLine("Done \n");

            Console.WriteLine("testing Register:");
            test(testRegister);
            Console.WriteLine("Done \n");
        }

        private int testLogin()
        {
            {
                int testNum = 0;

                User res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");

                    res = this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    AssertTrue(res.isLoggedIn);

                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    res = this.bridge.login(forum.forumId, user.userName, user.password);
                    AssertTrue(res.isLoggedIn);

                    testNum++;

                    res = this.bridge.login(forum.forumId, "mngr", "mngrPswd");
                    AssertTrue(res.isLoggedIn);
                    testNum++;

                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                /* failure tests */

                // wrong userName
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");

                    res = this.bridge.login(forum.forumId, "wrong user", ADMIN_PSWD);
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong password
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");

                    res = this.bridge.login(forum.forumId, ADMIN_NAME, "wrong pass"); 
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong forumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");

                    res = this.bridge.login(-2, ADMIN_NAME, ADMIN_PSWD); 
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                return testNum;
            }
        }

        private int testLogout()
        {
            {
                int testNum = 0;

                Boolean res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    User user = this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                    res = this.bridge.logout(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    AssertFalse(user.isLoggedIn);
                    AssertTrue(res);

                    user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    res = this.bridge.logout(forum.forumId, user.userName, user.password);
                    AssertFalse(user.isLoggedIn);
                    AssertTrue(res);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                /* failure tests */

                // wrong userName
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    User user = this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                    res = this.bridge.logout(forum.forumId, "wrong name", ADMIN_PSWD);
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong password
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    User user = this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                    res = this.bridge.logout(forum.forumId, ADMIN_NAME, "wrong pass");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong forumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    User user = this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                    res = this.bridge.logout(-2, ADMIN_NAME, ADMIN_PSWD);
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                return testNum;
            }
        }

        private int testCreateNewDiscussion()
        {
            {
                int testNum = 0;

                Discussion res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);

                    res = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");
                    AssertTrue(subForum.discussions.Contains(res));

                    res = this.bridge.createNewDiscussion(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId, "discussion2", "no content");
                    AssertTrue(subForum.discussions.Contains(res));

                    res = this.bridge.createNewDiscussion(user.userName, user.password, forum.forumId, subForum.subForumId, "discussion3", "no content");
                    AssertTrue(subForum.discussions.Contains(res));

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                /* failure tests */

                // wrong userName
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);

                    res = this.bridge.createNewDiscussion("wrong user", SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong password
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);

                    res = this.bridge.createNewDiscussion(SU_NAME, "wrong pass", forum.forumId, subForum.subForumId, "discussion1", "no content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong forumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);

                    res = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, -2, subForum.subForumId, "discussion1", "no content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong subForumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);

                    res = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, -1, "discussion1", "no content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                return testNum;
            }
        }

        private int testCreateNewComment()
        {
            {
                int testNum = 0;

                Comment res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                  //  res = this.bridge.createNewComment(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, discussion.discussionId, "no content");
                  //  AssertTrue(discussion.comments.Contains(res));

                    res = this.bridge.createNewComment(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId, discussion.discussionId, "no content");
                    AssertTrue(discussion.comments.Contains(res));

                    res = this.bridge.createNewComment(user.userName, user.password, forum.forumId, subForum.subForumId, discussion.discussionId, "no content");
                    AssertTrue(discussion.comments.Contains(res));

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                /* failure tests */

                // wrong userName
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.createNewComment("wrong user", SU_PSWD, forum.forumId, subForum.subForumId, discussion.discussionId, "no content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong password
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.createNewComment(SU_NAME, "wrong pass", forum.forumId, subForum.subForumId, discussion.discussionId, "no content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong forumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.createNewComment(SU_NAME, SU_PSWD, -2, subForum.subForumId, discussion.discussionId, "no content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong subForumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.createNewComment(SU_NAME, SU_PSWD, forum.forumId, -1, discussion.discussionId, "no content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong discussionId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.createNewComment(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, -1, "no content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                return testNum;
            }
        }

        private int testDeleteDiscussion()
        {
            {
                int testNum = 0;

                Boolean res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

               //    res = this.bridge.deleteDiscussion(forum.forumId, subForum.subForumId, discussion.discussionId, SU_NAME, SU_PSWD);
               //    AssertFalse(subForum.discussions.Contains(discussion));
               //    AssertTrue(res);

                    res = this.bridge.deleteDiscussion(forum.forumId, subForum.subForumId, discussion.discussionId, ADMIN_NAME, ADMIN_PSWD);
                    AssertFalse(subForum.discussions.Contains(discussion));
                    AssertTrue(res);

                 //   res = this.bridge.deleteDiscussion(forum.forumId, subForum.subForumId, discussion.discussionId, user.userName, user.password);
                 //   AssertFalse(subForum.discussions.Contains(discussion));
                 //   AssertTrue(res);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                /* failure tests */

                // wrong userName
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.deleteDiscussion(forum.forumId, subForum.subForumId, discussion.discussionId, "wrong user", SU_PSWD);
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong password
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.deleteDiscussion(forum.forumId, subForum.subForumId, discussion.discussionId, SU_NAME, "wrong pass");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong forumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.deleteDiscussion(forum.forumId, -2, discussion.discussionId, SU_NAME, SU_PSWD);
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong subForumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.deleteDiscussion(forum.forumId, -2, discussion.discussionId, SU_NAME, SU_PSWD);
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong discussionId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.deleteDiscussion(forum.forumId, subForum.subForumId, -2, SU_NAME, SU_PSWD);
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                return testNum;
            }
        }

        private int testEditDiscussion()
        {
            {
                int testNum = 0;

                Boolean res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");
                    //Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

              //    res = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, discussion.discussionId, SU_NAME, SU_PSWD, "new content");
              //    AssertEquals(discussion.content, "new content");
              //    AssertTrue(res);

                    res = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, discussion.discussionId, ADMIN_NAME, ADMIN_PSWD, "brand new content");
                    AssertEquals(discussion.content, "brand new content");
                    AssertTrue(res);

                    // (Asa) This test should not pass since publisher is ADMIN. A regular member is not authorized to edit this discussion.
                 //res = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, discussion.discussionId, user.userName, user.password, "new content");
                 //AssertEquals(discussion.content, "new content");
                 //AssertTrue(res);

                    testNum++;
                }
                catch { failMsg(testNum); }

                this.bridge.reset();

                /* failure tests */

                // wrong userName
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, discussion.discussionId, "wrong user", SU_PSWD, "new content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong password
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, discussion.discussionId, SU_NAME, "wrong pass", "new content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong forumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.editDiscussion(-1, subForum.subForumId, discussion.discussionId, SU_NAME, SU_PSWD, "new content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong subForumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.editDiscussion(forum.forumId, -1, discussion.discussionId, SU_NAME, SU_PSWD, "new content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong discussionId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    this.bridge.login(forum.forumId, user.userName, user.password);
                    Discussion discussion = this.bridge.createNewDiscussion(SU_NAME, SU_PSWD, forum.forumId, subForum.subForumId, "discussion1", "no content");

                    res = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, -1, SU_NAME, SU_PSWD, "new content");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                return testNum;
            }
        }

        private int testRegister()
        {
            {
                int testNum = 0;

                User res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                    res = this.bridge.register(forum.forumId, "user1", "pswd1", "a@a.com", "");
                    AssertTrue(forum.members.Contains(res));
                    AssertTrue(res.userName == "user1");
                    AssertTrue(res.email == "a@a.com");

                    testNum++;
                }
                catch { failMsg(testNum++); }

                this.bridge.reset();


                /* failure tests */

                // userName with wrong characters
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                    res = this.bridge.register(forum.forumId, "fs^&-!~", "pswd1", "a@a.com", "");
                    failMsg(testNum++);
                }
                catch { testNum++; }

                this.bridge.reset();

                // password with wrong characters
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                    res = this.bridge.register(forum.forumId, "user1", "1~-#~!", "a@a.com", "");
                    failMsg(testNum++);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong forumId
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                    res = this.bridge.register(-1, "user1", "pswd1", "a@a.com", "");
                    failMsg(testNum++);
                }
                catch { testNum++; }

                this.bridge.reset();

                // userName already exists
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                    res = this.bridge.register(forum.forumId, "user1", "pswd1", "a@a.com", "");
                    res = this.bridge.register(forum.forumId, SU_NAME, "pswd1", "a2@a.com", "");
                    res = this.bridge.register(forum.forumId, ADMIN_NAME, "pswd1", "a3@a.com", "");
                    res = this.bridge.register(forum.forumId, "user1", "pswd1", "a4@a.com", "");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                return testNum;
            }
        }

    }
}
