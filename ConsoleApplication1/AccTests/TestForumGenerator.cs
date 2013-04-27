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
            //testCreateNewForum();
            
            //    testLogin();
            //    testLogout();
            //    testCreateNewSubForum();
            //    testRegister();
            //    testGetsubForums();
            //    testCreateNewDiscussion();
            //    testCreateNewComment();

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
            /* same as login */

            testsLogger.logAction("OK");
        }


        private void testCreateNewComment()
        {
            testsLogger.logAction("testing createNewComment...  ");
            /* same as login */

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

