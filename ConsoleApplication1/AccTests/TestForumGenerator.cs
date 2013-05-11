using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using ForumGenerator_Version2_Server;
using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;
using System.Threading;
using System.Net;
using System.Xml;
using System.IO;

namespace ConsoleApplication1
{
    public class TestForumGenerator : AccTestsForumGenerator
    {
        const string PROXY = "Proxy";
        const string REAL = "Real";

        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;

        private string mode;


        // constructor
        public TestForumGenerator()
        {
            this.bridge = new ProxyForumGeneratorImpl();
            this.mode = PROXY;
        }

        // constructor
        public TestForumGenerator(ForumGenerator forumGen, string outFile)
        {
            this.bridge = new RealForumGeneratorImpl(forumGen);
            this.testsLogger = new TestsLogger(outFile);
            this.mode = REAL;
        }


        // main function
        public override void runTests()
        {
            this.testsLogger.logAction("\n** ForumGenerator Tests **\n" +
                                        "Testin on " + this.mode + " mode\n");

            // Most important: basic tests comes first !!!
            testSuperUserLogin();
            testSuperUserLogout();
            testGetForums();
            testCreateNewForum();
            testRegister();
            testLogin();
            testLogout();
            testGetsubForums();
            testCreateNewSubForum();
            testGetDiscussions();
            testCreateNewDiscussion();
            testCreateNewComment();
            testGetComments();

            sumTests();
        }

        /************************************************************/
        /*                   T E S T I N G
        /************************************************************/

        // Server definitions
        const string GUEST = "guest";
        const string MEMBER = "member";
        const string MODERATOR = "moderator";
        const string SUPER_USER = "administrator";
        const string SUPER_USER = "superuser";
        const string OK = "ok";

        // Tester definitions
        const string DUMMY = "dummy";


        private void testSuperUserLogin()
        {
            testsLogger.logAction("testing superUserLogin...  ");
            bool passed = true;
            int testNum = 1;
            try
            {
                SuperUser res;

                /* success tests */

                res = this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                AssertEquals(SU_NAME, res.userName);
                AssertEquals(SU_PSWD, res.password);
                testNum++;

                /* failure tests */

                bool tmp = this.bridge.superUserLogout();

                try {
                    res = this.bridge.superUserLogin("wrong user name", SU_PSWD);
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try {
                    res = this.bridge.superUserLogin(SU_NAME, "");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try {
                    res = this.bridge.superUserLogin(SU_NAME, null);
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }
                
                // more tests here

                // invariante
                res = this.bridge.superUserLogin(SU_NAME, SU_PSWD);

                if(passed)
                    testsLogger.logAction("superUserLogin tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }
        }



        private void testSuperUserLogout()
        {
            testsLogger.logAction("testing superUserLogout...  ");
            int testNum = 1;
            bool passed = true;
            try
            {
                bool res;
                SuperUser tmp;

                /* success tests */

                tmp = this.bridge.superUserLogin(SU_NAME, SU_PSWD);
                res = this.bridge.superUserLogout();
                AssertTrue(res);
                testNum++;

                /* failure tests */

                // At this point, the super user has already logged out.
                try {
                    res = this.bridge.superUserLogout();
                    passed = false;
                    failMsg(testNum++);
                }
                catch { testNum++; }

                if(passed)
                    testsLogger.logAction("superUserLogout tests PASSED\n");

                // invariante
                tmp = this.bridge.superUserLogin(SU_NAME, SU_PSWD); 
            }
            catch
            {
                failMsg(testNum++);
            }
        }


        private void testGetForums()
        {
            testsLogger.logAction("testing getForums...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
                LinkedList<Forum> res;
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

                if(passed)
                    testsLogger.logAction("getForums tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }
        }



        private void testCreateNewForum()
        {
            testsLogger.logAction("testing createNewForum...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
                Forum res;
                LinkedList<Forum> forums = this.bridge.getForums();

                /* success tests */
                AssertFalse(isForumExist(forums, "2nd forum"));
                res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr", "mngrPswd");
                AssertTrue(res.forumId > 0);
                forums = this.bridge.getForums();
                AssertTrue(isForumExist(forums, "2nd forum"));
                testNum++;

                /* failure tests */
                forums = this.bridge.getForums();

                try
                {
                    res = this.bridge.createNewForum(SU_NAME, "wrong pswd", "unique Forum", "forum mngr", "pswd");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { failMsg(testNum++); }

                try
                {
                    res = this.bridge.createNewForum(SU_NAME, SU_PSWD, "2nd forum", "mngr", "pswd");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { failMsg(testNum++); }

                try
                {
                    res = this.bridge.createNewForum(SU_NAME, SU_PSWD, getUniqueForumName(forums), null, "pswd");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { failMsg(testNum++); }


                if(passed)
                    testsLogger.logAction("createNewForum tests PASSED\n");

            }
            catch
            {
                failMsg(testNum++);
            }

        }



        private void testLogin()
        {
            testsLogger.logAction("testing login...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
                User res;
                LinkedList<Forum> forums = this.bridge.getForums();
                Forum forum;
                int forumId;
                LinkedList<User> members;
                User member;

                forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, getUniqueForumName(forums), "mngr", "mngrPswd");
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

                if(passed)
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
                LinkedList<Forum> forums = this.bridge.getForums();
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, getUniqueForumName(forums), "mngr", "mngrPswd");
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

                if(passed)
                    testsLogger.logAction("logout tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }

        }


        private void testCreateNewSubForum()
        {
            testsLogger.logAction("testing createNewSubForum...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
                SubForum res;
                LinkedList<Forum> forums = this.bridge.getForums();
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, getUniqueForumName(forums), "mngr", "mngrPswd");
                int forumId = forum.forumId;
                int subForumsCountPre;

                /* success tests */
  
                string title = getUniqueSubForumTitle(forum);
                AssertFalse(isSubForumExist(forum, title));
                res = this.bridge.createNewSubForum("mngr", "mngrPswd", forumId, title);
                AssertTrue(isSubForumExist(forum, res.subForumTitle));
                testNum++;

                subForumsCountPre = forum.subForums.Count;
                res = this.bridge.createNewSubForum("mngr", "mngrPswd", forumId, null);
                AssertEquals(subForumsCountPre + 1, forum.subForums.Count); 
                testNum++;
  
                /* failure tests */
                string newTitle = getUniqueSubForumTitle(forum);

                try
                {
                    res = this.bridge.createNewSubForum("not mngr", "mngrPswd", forumId, newTitle);
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
                    res = this.bridge.createNewSubForum("mngr", "MNGRPSWD", forumId, newTitle);
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

               // more tests here
  
                if(passed)
                    testsLogger.logAction("createNewSubForum tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }
        }


        private void testRegister()
        {
          testsLogger.logAction("testing register...  ");
          int testNum = 1;
          bool passed = true;

          try
          {
              User res;
              LinkedList<Forum> forums = this.bridge.getForums();
              Forum forum;
              int forumId;
              LinkedList<User> members;
              User member;

              forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, getUniqueForumName(forums), "mngr", "mngrPswd");
              forumId = forum.forumId;
              string memberName = getUniqueUserName(forum);

              /* success tests */

              AssertFalse(isUserNameExist(forum, memberName));
              member = this.bridge.register(forumId, memberName, "newPSWD", "newMem@gmail.com", "The newMEM");
              AssertTrue(isUserNameExist(forum, memberName));
              testNum++;


              /* failure tests */
              memberName = getUniqueUserName(forum);

              try
              {
                  // illegal chars in password
                  res = this.bridge.register(forumId, memberName, "~pswd~", "newMEM@gmail.com", "The newMEM");
                  failMsg(testNum++);
                  passed = false;
              }
              catch { testNum++; }

              try
              {
                  res = this.bridge.register(forumId, null, "pswd", "newMEM@gmail.com", "The newMEM");
                  failMsg(testNum++);
                  passed = false;
              }
              catch { testNum++; }

              try
              {
                  // illegal char in userName
                  res = this.bridge.register(forumId, memberName + "~", "legalPSWD", "legal@gmail.com", "legal");
                  failMsg(testNum++);
                  passed = false;
              }
              catch { testNum++; }

              try
              {
                  // invalid forumId
                  res = this.bridge.register(-1, memberName, "pswd", "newMEM@gmail.com", "The newMEM");
                  failMsg(testNum++);
                  passed = false;
              }
              catch { testNum++; }

              try
              {
                  // invalid email form
                  res = this.bridge.register(forumId, memberName, "pswd", "newMEM.gmail.com", "The newMEM");
                  failMsg(testNum++);
                  passed = false;
              }
              catch { testNum++; }

              try
              {
                  // invalid signature
                  res = this.bridge.register(forumId, memberName, "pswd", "newMEM@gmail.com", "לא_חוקי");
                  failMsg(testNum++);
                  passed = false;
              }
              catch { testNum++; }

              if(passed)
                testsLogger.logAction("register tests PASSED\n");
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
                LinkedList<Discussion> res;
                LinkedList<Forum> forums = this.bridge.getForums();
                Forum f = this.bridge.createNewForum(SU_NAME, SU_PSWD, getUniqueForumName(forums), "mngr", "mngrPswd");
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


        private void testCreateNewDiscussion()
        {
            testsLogger.logAction("testing createNewDiscussion...  ");
            int testNum = 1;
            bool passed = true;

            try
            {
                Discussion res;
                LinkedList<Forum> forums = this.bridge.getForums();
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, getUniqueForumName(forums), "mngr", "mngrPswd");
                int forumId = forum.forumId;
                SubForum sf = this.bridge.createNewSubForum("mngr", "mngrPswd", forumId, getUniqueSubForumTitle(forum));
                int sfId = sf.subForumId;
                int discCountPre;

                /* success tests */
  
                discCountPre = sf.discussions.Count;
                string memberName = getUniqueUserName(forum);
                User member = this.bridge.register(forumId, memberName, "pswd", "mem@gmail.com", "the Mem");
                res = this.bridge.createNewDiscussion(memberName, "pswd", forumId,
                                                      sfId, "disc1", "This test should success!");
                AssertEquals(discCountPre + 1, sf.discussions.Count);
                testNum++;

                // test no title
                discCountPre = sf.discussions.Count;
                res = this.bridge.createNewDiscussion(memberName, "pswd", forumId,
                                                      sfId, "", "This test should also success!");
                AssertEquals(discCountPre + 1, sf.discussions.Count);
                AssertTrue(res.title.startWith("no subject", true));
                testNum++;


                /* failure tests */

                try
                {
                    // wrong password
                    res = this.bridge.createNewDiscussion(memberName, "wrongPSWD", forumId,
                                                      sfId, "disc1", "Content is ok");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // wrong user name
                    res = this.bridge.createNewDiscussion(getUniqueUserName(forum), "pswd", forumId,
                                                      sfId, "disc1", "Content is ok");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // invalid forumId
                    res = this.bridge.createNewDiscussion(memberName, "pswd", -1,
                                                      sfId, "disc1", "Content is ok");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // invalid subForumId
                    res = this.bridge.createNewDiscussion(memberName, "pswd", forumId,
                                                      -1, "disc1", "Content is ok");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // invalid title - ilegal chars
                    res = this.bridge.createNewDiscussion(memberName, "pswd", forumId,
                                                      sfId, "~disc1~", "Content is ok");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // invalid title - too long
                    string title = "This title contains legal chars as described in User Stories doc," +
                                   "but is also too long - over 80 chars. Thus, this test should fail";
                    res = this.bridge.createNewDiscussion(memberName, "pswd", forumId,
                                                      sfId, title, "Content is ok");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }


                if(passed)
                    testsLogger.logAction("createNewDiscussion tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }
        }


        
        private void testCreateNewComment()
        {
            testsLogger.logAction("testing createNewComment... ");
            int testNum = 1;
            bool passed = true;

            try
            {
                Comment res;
                LinkedList<Forum> forums = this.bridge.getForums();
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, getUniqueForumName(forums), "mngr", "mngrPswd");
                int forumId = forum.forumId;
                SubForum sf = this.bridge.createNewSubForum("mngr", "mngrPswd", forumId, getUniqueSubForumTitle(forum));
                int sfId = sf.subForumId;
                Discussion disc = this.bridge.createNewDiscussion("mngr", "mngrPswd", forumId, sfId, "disc", "disc content");
                int discId = disc.discussionId;

                string memberName = getUniqueUserName(forum);

                /* success tests */

                int commentCountPre = disc.comments.Count;
                User member = this.bridge.register(forumId, memberName, "pswd", "mem@gmail.com", "the Mem");
                res = this.bridge.createNewComment(memberName, "pswd", forumId,
                                                      sfId, discId, "This test should success!");
                AssertEquals(commentCountPre + 1, disc.comments.Count);
                testNum++;

                /* unlimited content length - test on 8192 chars */
                commentCountPre = disc.comments.Count;
                res = this.bridge.createNewComment(memberName, "pswd", forumId,
                                                      sfId, discId, new String('a', 8192));
                AssertEquals(commentCountPre + 1, disc.comments.Count);
                testNum++;


                /* failure tests */

                try
                {
                    // wrong password
                    res = this.bridge.createNewComment(memberName, "wrongPSWD", forumId,
                                                      sfId, discId, "Content is ok");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // wrong user name (unregistered user)
                    res = this.bridge.createNewComment(getUniqueUserName(forum), "pswd", forumId,
                                                      sfId, discId, "Content is ok");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // invalid forumId
                    res = this.bridge.createNewComment(memberName, "pswd", -1,
                                                      sfId, discId, "Content is ok");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // invalid subForumId
                    res = this.bridge.createNewComment(memberName, "pswd", forumId,
                                                      -1, discId, "Content is ok");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }

                try
                {
                    // invalid content - ilegal chars
                    res = this.bridge.createNewComment(memberName, "pswd", forumId,
                                                      sfId, discId, "Content is ~not~ valid מכיל עברית");
                    failMsg(testNum++);
                    passed = false;
                }
                catch { testNum++; }


                if (passed)
                    testsLogger.logAction("createNewComment tests PASSED\n");
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
              LinkedList<Comment> res;
              LinkedList<Forum> forums = this.bridge.getForums();
              Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, getUniqueForumName(forums), "mngr", "mngrPswd");
              int forumId = forum.forumId;
              SubForum sf = this.bridge.createNewSubForum("mngr", "mngrPswd", forumId, getUniqueSubForumTitle(forum));
              int sfId = sf.subForumId;
              Discussion disc = this.bridge.createNewDiscussion("mngr", "mngrPswd", forumId, sfId, "disc", "disc content");
              int discId = disc.discussionId;

           
              /* success tests */

              res = this.bridge.getComments(forumId, sfId, discId);
              AssertEquals(res, disc.comments);
              testNum++;
  
              /* failure tests */
  
              // TODO add tests here
  
              if (passed)
                  testsLogger.logAction("getComments tests PASSED\n");
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
                LinkedList<SubForum> res;
                LinkedList<Forum> forums = this.bridge.getForums();
                Forum f = this.bridge.createNewForum(SU_NAME, SU_PSWD, getUniqueForumName(forums), "mngr", "mngrPswd");
                int fid = f.forumId;

                /* success tests */

                res = this.bridge.getSubForums(fid);
                AssertEquals(f.subForums, res);
                testNum++;

                /* failure tests */

                // TODO add tests here


                if(passed)
                    testsLogger.logAction("getSubForums tests PASSED\n");
            }
            catch
            {
                failMsg(testNum++);
            }
        }



    }
}

