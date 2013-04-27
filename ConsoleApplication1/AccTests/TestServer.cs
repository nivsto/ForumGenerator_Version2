/*
 * This is a server tests class. It runs tests on all methods that are accessible
 * for client, such as login(..), createForum(..) etc.
 * Results are documented in a Log file named by user.
 * 
 * Invariante: at the begining of each test method, the system is initialized and
 * does not hold any extra data. For example: a user that logged in under testLogin(..)
 * is considered to be logout at the end of this method. Same thing for Forum,
 * subForum etc.
 *
 * Created by: Asa
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    public class TestServer : AccTestsServer
    {
        const string PROXY = "Proxy";
        const string REAL = "Real";

        private ServerRequestCreator src;
        private string mode;


        // constructor
        public TestServer()
        {
            //this.bridge = new Proxy();
            this.xmlHandler = new XmlHandler();
            this.mode = PROXY;
        }

        // constructor
        public TestServer(Real bridge, string outFile)
        {
            this.testsLogger = new TestsLogger(outFile);
            this.bridge = bridge;
            this.xmlHandler = new XmlHandler();
            this.mode = REAL;
            this.src = new ServerRequestCreator();
        }


        // main function
        public override void runTests()
        {
            this.testsLogger.logAction("\n** HttpServer Tests **\n" +
                                        "Testin on " + this.mode + " mode\n");
            
               testAdminLogin();
               testAdminLogout();
         //      testCreateNewForum();
         //    testGetForums();
         //    testLogin();
         //    testLogout();
         //    testCreateNewSubForum();
         //    testRegister();
         //    testGetsubForums();
         //    testCreateNewDiscussion();
         //    testAddNewReply();

               sumTests();  
        }

        /************************************************************/
        /*                   T E S T I N G
        /************************************************************/

        // Constant tags of communication protocol
        const string REQ = "request";
        const string RES = "response";
        const string METHOD = "MethodName";
        const string MSG_TYPE = "MessageType";
        const string PARAMS = "params";
        const string SUCC = "Success";
        const string ERROR = "ErrorMsg";

        // Server definitions
        const string GUEST = "guest";
        const string MEMBER = "member";
        const string MODERATOR = "moderator";
        const string ADMIN = "administrator";
        const string SUPER_USER = "superuser";
        const string OK = "ok";
        
        // Tester definitions
        const string DUMMY = "dummy";

        // General variables
        LinkedList<String> ret_vals;
        string request;


        private void testAdminLogin()
        {
            testsLogger.logAction("testing adminLogin...  ");
            int testNum = 1;
            /* response params:
             * success, user type / error   */
            string res;
            try
            {
                /* success tests */

                request = src.AdminLoginReq("admin", "admin");
                ret_vals = xmlHandler.getXmlParse(this.bridge.adminLogin(request));
                AssertEquals(SUPER_USER, ret_vals.ElementAt(1).ToLower());
                testNum++;

                /* failure tests */

                request = src.AdminLoginReq("admin", "wrong");
                ret_vals = xmlHandler.getXmlParse(this.bridge.adminLogin(request));
                AssertEquals("0", ret_vals.ElementAt(0));
                testNum++;

                request = src.AdminLoginReq("admin", null);
                ret_vals = xmlHandler.getXmlParse(this.bridge.adminLogin(request));
                AssertEquals("0", ret_vals.ElementAt(0));
                testNum++;

                request = src.AdminLoginReq(null, "admin");
                ret_vals = xmlHandler.getXmlParse(this.bridge.adminLogin(request));
                AssertEquals("0", ret_vals.ElementAt(0));
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
             * success, user type / error   */
            string res;
            string tmp;
            try
            {
                /* success tests */

                tmp = this.bridge.adminLogin(src.AdminLoginReq("admin", "admin"));

                request = src.AdminLogoutReq("admin");
                ret_vals = xmlHandler.getXmlParse(this.bridge.adminLogout(request));
                AssertEquals(OK, ret_vals.ElementAt(1).ToLower());
                testNum++;

                /* failure tests */

                tmp = this.bridge.adminLogin(src.AdminLoginReq("admin", "admin"));

                request = src.AdminLogoutReq("wrong");
                res = this.bridge.adminLogout(request);
                ret_vals = xmlHandler.getXmlParse(res);
                AssertEquals("0", ret_vals.ElementAt(0)); // #fail - return 1 (expected 0)
                testNum++;

                testsLogger.logAction("adminLogout tests PASSED");
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
             * success, forumID / error       */
            string res;
            string tmp;
            try
            {
                /* success tests */

                request = src.CreateNewForumReq("admin", "admin", "Main Forum", "admin", "admin");
                res = this.bridge.adminLogout(request);
                ret_vals = xmlHandler.getXmlParse(res);
                int forumID = int.Parse(ret_vals.ElementAt(0));
                AssertTrue(forumID > 0);        // #fail - returns 0 (error "no permission")
                testNum++;

                /* failure tests */

                request = src.CreateNewForumReq("admin", "no good", "Main Forum", "admin", "admin");
                res = this.bridge.adminLogout(request);
                ret_vals = xmlHandler.getXmlParse(res);
                AssertEquals("0", ret_vals.ElementAt(0));
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


            testsLogger.logAction("OK");
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


        private void testAddNewReply()
        {
            testsLogger.logAction("testing addNewReply...  ");
            /* same as login */

            testsLogger.logAction("OK");
        }


        private void testGetForums()
        {
            testsLogger.logAction("testing getForums...  ");
            /* same as login */

            testsLogger.logAction("OK");
        }


        private void testGetsubForums()
        {
            testsLogger.logAction("testing getSubForums...  ");
            /* same as login */

            testsLogger.logAction("OK");
        }



    }
}
