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
        const string PROXY = "Proxy";
        const string REAL = "Real";

        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;

        public MemberAccTests(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            testLogin();
            testLogout();
            testCreateNewDiscussion();
            testCreateNewComment();
            testDeleteDiscussion();
            testEditDiscussion();
            testRegister();
        }

        private void testRegister()
        {
            testsLogger.logAction("testing register...  ");
            int testNum = 1;
            bool passed = true;

            //try
            //{
            //    User res;
            //    List<Forum> forums = this.bridge.getForums();
            //    Forum forum;
            //    int forumId;
            //    LinkedList<User> members;
            //    User member;

            //    forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
            //    forumId = forum.forumId;
            //    string memberName = getUniqueUserName(forum);

            //    /* success tests */

            //    AssertFalse(isUserNameExist(forum, memberName));
            //    member = this.bridge.register(forumId, memberName, "newPSWD", "newMem@gmail.com", "The newMEM");
            //    AssertTrue(isUserNameExist(forum, memberName));
            //    testNum++;


            //    /* failure tests */
            //    memberName = getUniqueUserName(forum);

            //    try
            //    {
            //        // illegal chars in password
            //        res = this.bridge.register(forumId, memberName, "~pswd~", "newMEM@gmail.com", "The newMEM");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        res = this.bridge.register(forumId, null, "pswd", "newMEM@gmail.com", "The newMEM");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // illegal char in userName
            //        res = this.bridge.register(forumId, memberName + "~", "legalPSWD", "legal@gmail.com", "legal");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // invalid forumId
            //        res = this.bridge.register(-1, memberName, "pswd", "newMEM@gmail.com", "The newMEM");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // invalid email form
            //        res = this.bridge.register(forumId, memberName, "pswd", "newMEM.gmail.com", "The newMEM");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // invalid signature
            //        res = this.bridge.register(forumId, memberName, "pswd", "newMEM@gmail.com", "לא_חוקי");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    if (passed)
            //        testsLogger.logAction("register tests PASSED\n");
            //}
            //catch
            //{
            //    failMsg(testNum++);
            //}
        }


        private void testEditDiscussion()
        {
            //throw new NotImplementedException();
        }

        private void testDeleteDiscussion()
        {
            //throw new NotImplementedException();
        }

        private void testLogin()
        {
            testsLogger.logAction("testing login...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
                User res;
                List<Forum> forums = this.bridge.getForums();
                Forum forum;
                int forumId;
                List<User> members;
                User member;

                forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
                forumId = forum.forumId;
                string memberName = getUniqueUserName(forum);
                member = this.bridge.register(forumId, memberName, "newPSWD", "mem@gmail.com", "The MEM");

                /* success tests */

                res = this.bridge.login(forumId, member.userName, member.password);
                AssertTrue(member.isLoggedIn);
                testNum++;


                /* failure tests */

                // verify that member is not logged in. An error here will cause
                // a failure in test no. 1 and exit from this test method
                this.bridge.logout(forumId, member.memberID);

                try
                {
                    res = this.bridge.login(forumId, "", member.password);
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    res = this.bridge.login(forumId, member.userName, member.password + "a");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // password is "newPSWD" - checking case sensitive.
                    res = this.bridge.login(forumId, member.userName, member.password.ToUpper());
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    res = this.bridge.login(forumId, getUniqueUserName(forum), member.password);
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // invalid forumID.
                    res = this.bridge.login(-1, member.userName, member.password);
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                if (passed)
                    testsLogger.logAction("login tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }
        }

        private void testLogout()
        {
            testsLogger.logAction("testing logout...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
                bool res;
                List<Forum> forums = this.bridge.getForums();
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
                // first member in forum
                User member = this.bridge.register(forum.forumId, getUniqueUserName(forum), "pswd", "mem@gmail.com", "The MEM");
                member = this.bridge.login(forum.forumId, member.userName, member.password);

                /* success tests */

                AssertTrue(member.isLoggedIn);
                res = this.bridge.logout(forum.forumId, member.memberID);
                AssertFalse(member.isLoggedIn);
                testNum++;

                res = this.bridge.logout(forum.forumId, member.memberID); // already logged out
                AssertFalse(res);
                AssertFalse(member.isLoggedIn);

                /* failure tests */

                if (passed)
                    testsLogger.logAction("logout tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }

        }

        private void testCreateNewDiscussion()
        {
            //testsLogger.logAction("testing createNewDiscussion...  ");
            //int testNum = 1;
            //bool passed = true;

            //try
            //{
            //    Discussion res;
            //    List<Forum> forums = this.bridge.getForums();
            //    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
            //    int forumId = forum.forumId;
            //    SubForum sf = this.bridge.createNewSubForum("mngr", "mngrPswd", forumId, getUniqueSubForumTitle(forum));
            //    int sfId = sf.subForumId;
            //    int discCountPre;

            //    /* success tests */

            //    discCountPre = sf.discussions.Count;
            //    string memberName = getUniqueUserName(forum);
            //    User member = this.bridge.register(forumId, memberName, "pswd", "mem@gmail.com", "the Mem");
            //    res = this.bridge.createNewDiscussion(memberName, "pswd", forumId,
            //                                          sfId, "disc1", "This test should success!");
            //    AssertEquals(discCountPre + 1, sf.discussions.Count);
            //    testNum++;

            //    // test no title
            //    discCountPre = sf.discussions.Count;
            //    res = this.bridge.createNewDiscussion(memberName, "pswd", forumId,
            //                                          sfId, "", "This test should also success!");
            //    AssertEquals(discCountPre + 1, sf.discussions.Count);
            //    AssertTrue(res.title.startWith("no subject", true));
            //    testNum++;


            //    /* failure tests */

            //    try
            //    {
            //        // wrong password
            //        res = this.bridge.createNewDiscussion(memberName, "wrongPSWD", forumId,
            //                                          sfId, "disc1", "Content is ok");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // wrong user name
            //        res = this.bridge.createNewDiscussion(getUniqueUserName(forum), "pswd", forumId,
            //                                          sfId, "disc1", "Content is ok");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // invalid forumId
            //        res = this.bridge.createNewDiscussion(memberName, "pswd", -1,
            //                                          sfId, "disc1", "Content is ok");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // invalid subForumId
            //        res = this.bridge.createNewDiscussion(memberName, "pswd", forumId,
            //                                          -1, "disc1", "Content is ok");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // invalid title - ilegal chars
            //        res = this.bridge.createNewDiscussion(memberName, "pswd", forumId,
            //                                          sfId, "~disc1~", "Content is ok");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // invalid title - too long
            //        string title = "This title contains legal chars as described in User Stories doc," +
            //                       "but is also too long - over 80 chars. Thus, this test should fail";
            //        res = this.bridge.createNewDiscussion(memberName, "pswd", forumId,
            //                                          sfId, title, "Content is ok");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }


            //    if (passed)
            //        testsLogger.logAction("createNewDiscussion tests PASSED\n");
            //}
            //catch
            //{
            //    failMsg(testNum++);
            //}
        }

        private void testCreateNewComment()
        {
            //testsLogger.logAction("testing createNewComment... ");
            //int testNum = 1;
            //bool passed = true;

            //try
            //{
            //    Comment res;
            //    List<Forum> forums = this.bridge.getForums();
            //    Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, "1st forum", "mngr", "mngrPswd");
            //    int forumId = forum.forumId;
            //    SubForum sf = this.bridge.createNewSubForum("mngr", "mngrPswd", forumId, getUniqueSubForumTitle(forum));
            //    int sfId = sf.subForumId;
            //    Discussion disc = this.bridge.createNewDiscussion("mngr", "mngrPswd", forumId, sfId, "disc", "disc content");
            //    int discId = disc.discussionId;

            //    string memberName = getUniqueUserName(forum);

            //    /* success tests */

            //    int commentCountPre = disc.comments.Count;
            //    User member = this.bridge.register(forumId, memberName, "pswd", "mem@gmail.com", "the Mem");
            //    res = this.bridge.createNewComment(memberName, "pswd", forumId,
            //                                          sfId, discId, "This test should success!");
            //    AssertEquals(commentCountPre + 1, disc.comments.Count);
            //    testNum++;

            //    /* unlimited content length - test on 8192 chars */
            //    commentCountPre = disc.comments.Count;
            //    res = this.bridge.createNewComment(memberName, "pswd", forumId,
            //                                          sfId, discId, new String('a', 8192));
            //    AssertEquals(commentCountPre + 1, disc.comments.Count);
            //    testNum++;


            //    /* failure tests */

            //    try
            //    {
            //        // wrong password
            //        res = this.bridge.createNewComment(memberName, "wrongPSWD", forumId,
            //                                          sfId, discId, "Content is ok");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // wrong user name (unregistered user)
            //        res = this.bridge.createNewComment(getUniqueUserName(forum), "pswd", forumId,
            //                                          sfId, discId, "Content is ok");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // invalid forumId
            //        res = this.bridge.createNewComment(memberName, "pswd", -1,
            //                                          sfId, discId, "Content is ok");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // invalid subForumId
            //        res = this.bridge.createNewComment(memberName, "pswd", forumId,
            //                                          -1, discId, "Content is ok");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }

            //    try
            //    {
            //        // invalid content - ilegal chars
            //        res = this.bridge.createNewComment(memberName, "pswd", forumId,
            //                                          sfId, discId, "Content is ~not~ valid מכיל עברית");
            //        failMsg(testNum++);
            //        passed = false;
            //    }
            //    catch { testNum++; }


            //    if (passed)
            //        testsLogger.logAction("createNewComment tests PASSED\n");
            //}
            //catch
            //{
            //    failMsg(testNum++);
            //}

        }

    }
}
