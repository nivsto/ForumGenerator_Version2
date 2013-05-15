using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.ForumData;

namespace ConsoleApplication1.AccTests
{
    class AdminAccTests : AccTestsForumGenerator
    {
        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;
        const string ADMIN_NAME = "mngr";
        const string ADMIN_PSWD = "mngrPswd";

        public AdminAccTests(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            this.testsLogger.logTestsSection("Admin");
            test(testCreateNewSubForum);
            test(testAddModerator);
            test(testRemoveModerator);
            test(testGetNumOfCommentsSubForum);
            test(testGetNumOfCommentsSingleUser);
            test(testGetResponsersForSingleUser);
        }

        private int testCreateNewSubForum()
        {
            int testNum = 0;

            SubForum res;

            /* success tests */
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                
                res = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "title1");
                AssertTrue(isSubForumExist(forum, res.subForumTitle));
                testNum++;

                int subForumsCountPre = forum.subForums.Count;
                res = this.bridge.createNewSubForum("mngr", "mngrPswd", forum.forumId, null);
                AssertEquals(subForumsCountPre + 1, forum.subForums.Count);
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

                res = this.bridge.createNewSubForum("wrong user", "mngrPswd", forum.forumId, "title1");
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

                res = this.bridge.createNewSubForum("mngr", "wrong pass", forum.forumId, "title1");
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

                res = this.bridge.createNewSubForum("mngr", "mngrPswd", -5, "title1");
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            // subForum title already exists
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                res = this.bridge.createNewSubForum("mngr", "mngrPswd", forum.forumId, "title1");
                res = this.bridge.createNewSubForum("mngr", "mngrPswd", forum.forumId, "title1");
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            // subForum title woth wrong characters
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);

                res = this.bridge.createNewSubForum("mngr", "mngrPswd", forum.forumId, "%%df#*(@1`");
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            return testNum;
        }

        private int testAddModerator()
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

                res = this.bridge.addModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);
                AssertTrue(subForum.moderators.Contains(user));
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
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");

                res = this.bridge.addModerator("user1", forum.forumId, subForum.subForumId, "wrong user", ADMIN_PSWD);
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

                res = this.bridge.addModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, "wrong pass");
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

                res = this.bridge.addModerator("user1", -3, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);
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

                res = this.bridge.addModerator("user1", forum.forumId, -2, ADMIN_NAME, ADMIN_PSWD);
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            // wrong moderator username
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");

                res = this.bridge.addModerator("wrong user", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            return testNum;
        }

        private int testRemoveModerator()
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
                this.bridge.addModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);

                res = this.bridge.removeModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);
                AssertFalse(subForum.moderators.Contains(user));
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
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                this.bridge.addModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);

                res = this.bridge.removeModerator("user1", forum.forumId, subForum.subForumId, "wrong user", ADMIN_PSWD);
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
                this.bridge.addModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);

                res = this.bridge.removeModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, "wrong pass");
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
                this.bridge.addModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);

                res = this.bridge.removeModerator("user1", -3, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);
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
                this.bridge.addModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);

                res = this.bridge.removeModerator("user1", forum.forumId, -3, ADMIN_NAME, ADMIN_PSWD);
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            // wrong moderator username
            try
            {
                this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                User user = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                this.bridge.addModerator("user1", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);

                res = this.bridge.removeModerator("wrong user", forum.forumId, subForum.subForumId, ADMIN_NAME, ADMIN_PSWD);
                failMsg(testNum);
            }
            catch { testNum++; }

            this.bridge.reset();

            return testNum;
        }


        private int testGetNumOfCommentsSubForum()
        {
            {
                int testNum = 0;

                int res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                    res = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                    AssertTrue(res==0);

                    user1 = this.bridge.login(forum.forumId, "user1", "pswd1");
                    user2 = this.bridge.login(forum.forumId, "user2", "pswd2");
                    Discussion[] discussions = new Discussion[100];
                    for (int i=1; i<=100; i++)
                        discussions[i] = this.bridge.createNewDiscussion("user1", "pswd1", forum.forumId, subForum.subForumId, "discussion" + i, "no content");
                    res = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                    AssertTrue(res == 100);

                    for (int i = 1; i <= 50; i++)
                    {
                        this.bridge.createNewComment("user1", "pswd1", forum.forumId, subForum.subForumId, discussions[35].discussionId, "no content");
                        this.bridge.createNewComment("user2", "pswd2", forum.forumId, subForum.subForumId, discussions[55].discussionId, "no content");
                        this.bridge.createNewComment("user2", "pswd2", forum.forumId, subForum.subForumId, discussions[75].discussionId, "no content");
                    }
                    res = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                    AssertTrue(res == 250);

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
                    User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                    res = this.bridge.getNumOfCommentsSubForum("wrong user", ADMIN_PSWD, forum.forumId, subForum.subForumId);
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
                    User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                    res = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, "wrong pass", forum.forumId, subForum.subForumId);
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
                    User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                    res = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, -2, subForum.subForumId);
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
                    User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                    res = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, -5);
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                return testNum;
            }
        }

        private int testGetNumOfCommentsSingleUser()
        {
            {
                int testNum = 0;

                int res;

                /* success tests */
                try
                {
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
                    Discussion[] discussions1 = new Discussion[100];
                    Discussion[] discussions2 = new Discussion[100];
                    for (int i = 0; i < 100; i++)
                    {
                        discussions1[i] = this.bridge.createNewDiscussion(user1.userName, user1.password, forum.forumId, subForum.subForumId, "discussion" + i, "no content");
                        discussions2[i] = this.bridge.createNewDiscussion(user2.userName, user2.password, forum.forumId, subForum.subForumId, "discussion" + i, "no content");
                    }
                    res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, user1.userName);
                    AssertTrue(res == 100);
                    res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, user2.userName);
                    AssertTrue(res == 100);

                    for (int i = 1; i <= 50; i++)
                    {
                        this.bridge.createNewComment(user1.userName, user1.password, forum.forumId, subForum.subForumId, discussions1[35].discussionId, "no content");
                        this.bridge.createNewComment(user2.userName, user2.password, forum.forumId, subForum.subForumId, discussions2[55].discussionId, "no content");
                        this.bridge.createNewComment(user2.userName, user2.password, forum.forumId, subForum.subForumId, discussions2[75].discussionId, "no content");
                    }
                    res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, user1.userName);
                    AssertTrue(res == 150);
                    res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, user2.userName);
                    AssertTrue(res == 200);

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
                    User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                    res = this.bridge.getNumOfCommentsSingleUser("wrong user", ADMIN_PSWD, forum.forumId, user1.userName);
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
                    User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                    res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, "wrong pass", forum.forumId, user1.userName);
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
                    User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                    res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, -3, user1.userName);
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong username of required user
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User user1 = this.bridge.register(forum.forumId, "user1", "pswd1", "", "");
                    User user2 = this.bridge.register(forum.forumId, "user2", "pswd2", "", "");

                    res = this.bridge.getNumOfCommentsSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "wrong user");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                return testNum;
            }
        }

        private int testGetResponsersForSingleUser()
        {
            {
                int testNum = 0;

                List<User> res;

                /* success tests */
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User[] users = new User[10];
                    for(int i=0; i<10; i++)
                        users[i] = this.bridge.register(forum.forumId, "user"+i, "pswd"+i, "", "");

                    res = this.bridge.getResponsersForSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, users[5].userName);
                    AssertTrue(res.Count == 0);

                    for (int i = 0; i < 10; i++)
                        this.bridge.login(forum.forumId, users[i].userName, users[i].password);
                    Discussion[] discussions = new Discussion[30];
                    for (int i = 0; i < 30; i++)
                        discussions[i] = this.bridge.createNewDiscussion(users[i % 10].userName, users[i % 10].password, forum.forumId, subForum.subForumId, "discussion" + i, "no content");
                    res = this.bridge.getResponsersForSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, users[5].userName);
                    AssertTrue(res.Count == 0);

                    for (int i = 0; i < 10; i++)
                        this.bridge.createNewComment(users[i].userName, users[i].password, forum.forumId, subForum.subForumId, discussions[0].discussionId, "no content");


                    res = this.bridge.getResponsersForSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, users[0].userName);
                    AssertTrue(res.Count == 10);
                    for(int i=0; i<10; i++)
                        AssertTrue(res.Contains(users[i]));

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
                    User[] users = new User[10];
                    for (int i = 0; i < 10; i++)
                        users[i] = this.bridge.register(forum.forumId, "user" + i, "pswd" + i, "", "");

                    res = this.bridge.getResponsersForSingleUser("wrong user", ADMIN_PSWD, forum.forumId, users[5].userName);
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
                    User[] users = new User[10];
                    for (int i = 0; i < 10; i++)
                        users[i] = this.bridge.register(forum.forumId, "user" + i, "pswd" + i, "", "");

                    res = this.bridge.getResponsersForSingleUser(ADMIN_NAME, "wrong pass", forum.forumId, users[5].userName);
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
                    User[] users = new User[10];
                    for (int i = 0; i < 10; i++)
                        users[i] = this.bridge.register(forum.forumId, "user" + i, "pswd" + i, "", "");

                    res = this.bridge.getResponsersForSingleUser(ADMIN_NAME, ADMIN_PSWD, 300000000, users[5].userName);
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                // wrong username of required user
                try
                {
                    this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "forum1", "mngr", "mngrPswd");
                    this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                    SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "subForum1");
                    User[] users = new User[10];
                    for (int i = 0; i < 10; i++)
                        users[i] = this.bridge.register(forum.forumId, "user" + i, "pswd" + i, "", "");

                    res = this.bridge.getResponsersForSingleUser(ADMIN_NAME, ADMIN_PSWD, forum.forumId, "wrong user");
                    failMsg(testNum);
                }
                catch { testNum++; }

                this.bridge.reset();

                return testNum;
            }
        }

    }
}
