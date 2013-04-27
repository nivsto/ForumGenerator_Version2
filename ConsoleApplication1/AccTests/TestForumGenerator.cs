using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using ForumGenerator_Version2_Server;
using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server.Communication;
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

        const string ADMIN_USR = "admin"; // ForumGenerator.ADMIN_PSWD;
        const string ADMIN_PSWD = "admin"; //ForumGenerator.ADMIN_PSWD;

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
            testAdminLogin();
            testAdminLogout();
            testGetForums();
            testCreateNewForum();
            
            //    testLogin();
            //    testLogout();
            //    testCreateNewSubForum();
            //    testRegister();
            //    testGetsubForums();

                testCreateNewDiscussion();
                testCreateNewComment();
                //testGetDiscussions();
                //testGetComments();
                
            sumTests();
        }

        /************************************************************/
        /*                   T E S T I N G
        /************************************************************/

        // Server definitions
        const string GUEST = "guest";
        const string MEMBER = "member";
        const string MODERATOR = "moderator";
        const string ADMIN = "administrator";
        const string SUPER_USER = "superuser";
        const string OK = "ok";

        // Tester definitions
        const string DUMMY = "dummy";


        private void testAdminLogin()
        {
            testsLogger.logAction("testing adminLogin...  ");
            int testNum = 1;
            /* response params:
             * <success, userType/error>   */
            try
            {
                Tuple<string, string> res;

                /* success tests */

                res = this.bridge.adminLogin(ADMIN_USR, ADMIN_PSWD);
                AssertEquals(SUPER_USER, res.Item2.ToLower());
                testNum++;

                /* failure tests */

                res = this.bridge.adminLogin("wrong user name", ADMIN_PSWD);
                AssertEquals("0", res.Item1.ToLower());
                testNum++;

                res = this.bridge.adminLogin(ADMIN_USR, "");
                AssertEquals("0", res.Item1.ToLower());
                testNum++;

                res = this.bridge.adminLogin(ADMIN_USR, null);
                AssertEquals("0", res.Item1.ToLower());
                testNum++;
                // more test here

                testsLogger.logAction("adminLogin tests PASSED");
            }
            catch (Exception e)
            {
                failMsg(testNum);
            }
        }



        private void testAdminLogout()
        {
            testsLogger.logAction("testing adminLogout...  ");
            int testNum = 1;
            /* response params:
             * <success, OK/error>   */
            try
            {
                Tuple<string, string> res;
                Tuple<string, string> tmp;

                /* success tests */
                tmp = this.bridge.adminLogin(ADMIN_USR, ADMIN_PSWD);

                res = this.bridge.adminLogout();
                AssertEquals(OK, res.Item2.ToLower());
                testNum++;

                /* failure tests */

                res = this.bridge.adminLogout();
                AssertEquals("0", res.Item1);
                testNum++;

                testsLogger.logAction("adminLogout tests PASSED");
            }
            catch (Exception e)
            {
                failMsg(testNum);
            }
        }


        private void testGetForums()
        {
            testsLogger.logAction("testing getForums...  ");
            int testNum = 1;
            /* res:  <bool, string, string[], string[,]>
             *    bool - true on success, false on error
             *    string - "forum"                     (const value)
             *    string[] - "ID", "Name", "AdminName" (const values)
             *    string[,] - <forumID, forumName, forumAdminName>  for each forum     */
            try
            {
                Tuple<bool, string, string[], string[,]> res;

                /* success tests */

                res = this.bridge.getForums();
                AssertTrue(res.Item4.Length > 0);  // there is at least 1 forum
                testNum++;

                /* failure tests */

                // TODO add tests here


                testsLogger.logAction("getForums tests PASSED");
            }
            catch (Exception e)
            {
                failMsg(testNum);
            }
        }



        private void testCreateNewForum()
        {
            testsLogger.logAction("testing createNewForum...  ");
            int testNum = 1;
            /* response params:
             * <success, forumID/error>       */
            try
            {
                Tuple<string, string> res;

                /* success tests */

                res = this.bridge.createNewForum("admin", "admin", "asa", "admin", "admin");
                int forumId = int.Parse(res.Item1);
                AssertTrue(forumId > 0);        // #fail - returns 0 (error "no permission")
                testNum++;

                /* failure tests */

                res = this.bridge.createNewForum("admin", "no good", "unique Forum", "admin", "admin");
                forumId = int.Parse(res.Item1);
                AssertTrue(forumId == 0);
                testNum++;

                testsLogger.logAction("createNewForum tests PASSED");
            }
            catch (Exception e)
            {
                failMsg(testNum);
            }

        }



        private void testLogin()
        {
            testsLogger.logAction("testing login...  ");
            int testNum = 1;
            /* response params:
             * <success, userType/error>       */
            try
            {
                Tuple<string, string> res;

                /* success tests */
                Tuple<bool, string, string[], string[,]> allForums = this.bridge.getForums();

                res = this.bridge.createNewForum("admin", "admin", "asa", "admin", "admin");
                int forumId = int.Parse(res.Item1);
                AssertTrue(forumId > 0);        // #fail - returns 0 (error "no permission")
                testNum++;

                /* failure tests */

                res = this.bridge.createNewForum("admin", "no good", "unique Forum", "admin", "admin");
                forumId = int.Parse(res.Item1);
                AssertTrue(forumId == 0);
                testNum++;

                testsLogger.logAction("login tests PASSED");
            }
            catch (Exception e)
            {
                failMsg(testNum);
            }
        }


        private void testLogout()
        {
            testsLogger.logAction("testing logout...  ");
            /* same as login */

            testsLogger.logAction("OK");
        }


        private void testCreateNewSubForum()
        {
            testsLogger.logAction("testing createNewSubForum...  ");
            /* same as login */

            testsLogger.logAction("OK");
        }


        private void testRegister()
        {
            testsLogger.logAction("testing register...  ");
            /* same as login */

            testsLogger.logAction("OK");
        }


        private void testCreateNewDiscussion()
        {
            testsLogger.logAction("testing createNewDiscussion...  ");
            int testNum = 1;
            //success:
            try
            {
                bridge.adminLogin(ADMIN_USR, ADMIN_PSWD);
                int forumId = int.Parse(bridge.createNewForum(ADMIN_USR, ADMIN_PSWD, "forumName2", ADMIN_USR, ADMIN_PSWD).Item2);
                bridge.login(forumId, ADMIN_USR, ADMIN_PSWD);
                int subForumId = int.Parse(bridge.createNewSubForum(ADMIN_USR, ADMIN_PSWD, forumId, "subForumTitle2").Item2);
                bridge.register(forumId, "u_name2", "u_password2", "e@mail.com2", "sign2");
                bridge.login(forumId, "u_name2", "u_password2");
                int discussionId = int.Parse(bridge.createNewDiscussion("u_name2", "u_password2", forumId, subForumId,
                                                "discussion_title2", "discussion_content2").Item2);

                AssertTrue(discussionId >= 0);        // #fail - returns 0 (error "no permission")             
            }

            catch (Exception e)
            {
                failMsg(testNum);
            }

            testNum++;

            //failure:
            try
            {
                bridge.adminLogin(ADMIN_USR, ADMIN_PSWD);
                int forumId = int.Parse(bridge.createNewForum(ADMIN_USR, ADMIN_PSWD, "forumName52", ADMIN_USR, ADMIN_PSWD).Item2);
                bridge.login(forumId, ADMIN_USR, ADMIN_PSWD);
                int subForumId = int.Parse(bridge.createNewSubForum(ADMIN_USR, ADMIN_PSWD, forumId, "subForumTitle52").Item2);
                bridge.register(forumId, "u_name52", "u_password52", "e@mail.com2", "sign2");
                bridge.login(forumId, "u_name52", "u_password52");
                int discussionId = int.Parse(bridge.createNewDiscussion("u_name52", "u_password52", forumId, subForumId,
                                "", "discussion_content").Item2);
                AssertFalse(discussionId >= 0);        // #fail - returns 0 --> title's length = 0!
            }

            catch (Exception e)
            {
                failMsg(testNum);
            }

            testNum++;

            try
            {
                bridge.adminLogin(ADMIN_USR, ADMIN_PSWD);
                int forumId = int.Parse(bridge.createNewForum(ADMIN_USR, ADMIN_PSWD, "forumName12", ADMIN_USR, ADMIN_PSWD).Item2);
                bridge.login(forumId, ADMIN_USR, ADMIN_PSWD);
                int subForumId = int.Parse(bridge.createNewSubForum(ADMIN_USR, ADMIN_PSWD, forumId, "subForumTitle23").Item2);
                bridge.register(forumId, "u_name22", "u_password22", "e@mail.com22", "sign2");
                bridge.login(forumId, "u_name22", "u_password22");
                int discussionId = int.Parse(bridge.createNewDiscussion("u_name22", "u_password22", forumId, subForumId,
                                                "disc_title", "תווים לא חוקיים").Item2);
                AssertFalse(discussionId >= 0);        // #fail - returns 0 --> hebrew characters is not legal!

            }

            catch (Exception e)
            {
                failMsg(testNum);
            }

            testsLogger.logAction("OK");
        }


        private void testCreateNewComment()
        {
            testsLogger.logAction("testing createNewComment...  ");
            int testNum = 1;
                        
            try
            {
                bridge.adminLogin(ADMIN_USR, ADMIN_PSWD);
                int forumId = int.Parse(bridge.createNewForum(ADMIN_USR, ADMIN_PSWD, "forumBla", ADMIN_USR, ADMIN_PSWD).Item2);
                bridge.login(forumId, ADMIN_USR, ADMIN_PSWD);
                int subForumId = int.Parse(bridge.createNewSubForum(ADMIN_USR, ADMIN_PSWD, forumId, "subForumTitle2").Item2);
                bridge.register(forumId, "u_name", "u_password", "e@mail.com2", "sign2");
                bridge.login(forumId, "u_name", "u_password");
                int discussionId = int.Parse(bridge.createNewDiscussion("u_name", "u_password", forumId, subForumId,
                                                "discussion_title", "discussion_content").Item2);
                int commentId = int.Parse(bridge.createNewComment("u_name", "u_password", forumId, subForumId, discussionId,
                                                "comment_content").Item2);
                AssertTrue(commentId >= 0);        // #fail - returns 0 (error "no permission")
                
            }
            catch (Exception e)
            {
                failMsg(testNum);
            }
             //failure:
            testNum++;
             try 
             {
               bridge.adminLogin(ADMIN_USR, ADMIN_PSWD);
               int forumId = int.Parse(bridge.createNewForum(ADMIN_USR, ADMIN_PSWD, "forumBla2", ADMIN_USR, ADMIN_PSWD).Item2);
               bridge.login(forumId, ADMIN_USR, ADMIN_PSWD);
               int subForumId = int.Parse(bridge.createNewSubForum(ADMIN_USR, ADMIN_PSWD, forumId, "subForumTitle3").Item2);
               bridge.register(forumId, "u_name1", "u_password1", "e@mail.com1", "sign1");
               bridge.login(forumId, "u_name1", "u_password1");
               int discussionId = int.Parse(bridge.createNewDiscussion("u_name1", "u_password1", forumId, subForumId,
                                                "discussion_title1", "discussion_content1").Item2);
               int commentId = int.Parse(bridge.createNewComment("u_name1", "u_password1", forumId, subForumId, discussionId,
                                                "לא קורא עברית").Item2);
               AssertFalse(commentId >= 0);        // #fail - returns 0 (error "no permission") --> can't read hebrew!

            }
            catch (Exception e)
            {
                failMsg(testNum);
            }

            testsLogger.logAction("OK");
        }


        


        private void testGetsubForums()
        {
            testsLogger.logAction("testing getSubForums...  ");
            int testNum = 1;
            /* res:  <bool, string, string[], string[,]>
             *    bool - true on success, false on error
             *    string - "forum"                     (const value)
             *    string[] - "ID", "Name", "AdminName" (const values)
             *    string[,] - <forumID, forumName, forumAdminName>  for each forum     */
            try
            {
                Tuple<bool, string, string[], string[,]> res;

                string[,] forums = this.bridge.getForums().Item4;
                int numOfForums = forums.Length;

                /* success tests */
              //  int forumID = forums[numOfForums-1]
                //res = this.bridge.getSubForums();
                //AssertTrue(res.Item4.Length > 0);  // there is at least 1 forum
                testNum++;

                /* failure tests */

                // TODO add tests here


                testsLogger.logAction("getSubForums tests PASSED");
            }
            catch (Exception e)
            {
                failMsg(testNum);
            }
        }



    }
}

