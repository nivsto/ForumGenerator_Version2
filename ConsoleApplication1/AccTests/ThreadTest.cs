using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;

namespace ConsoleApplication1.AccTests
{
    class ThreadTest : AccTestsForumGenerator
    {
        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;
        static Random random = new Random();
        int testNum = 0;
        
        //const string ADMIN_NAME = "mngr";
       // const string ADMIN_PSWD = "mngrPswd";

        public ThreadTest(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
        }

        public override void runTests()
        {
            this.testsLogger.logTestsSection("Thread");
            this.bridge.superUserLogin(SU_NAME, SU_PSWD);
            testsLogger.logMethodTest("ThreadTest1");
            Console.WriteLine("testing ThreadTest1:");
            ThreadTest1();
            //scenario1();
            testNum++;
            testsLogger.logMethodTestResults("ThreadTest1", testNum);
            
            Console.WriteLine("Done \n");
            Console.WriteLine("Press Enter to exit");
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
            //System.Threading.Thread t1;
            //System.Threading.Thread t2;
            //System.Threading.Thread t3;
            //System.Threading.Thread t4;
            
            //t1 = new System.Threading.Thread(new SuperUserAccTests(this.testsLogger, this.bridge).runTests);
            //t2 = new System.Threading.Thread(new AdminAccTests(this.testsLogger, this.bridge).runTests);
            //t3 = new System.Threading.Thread(new GuestAccTests(this.testsLogger, this.bridge).runTests);
            //t4 = new System.Threading.Thread(new MemberAccTests(this.testsLogger, this.bridge).runTests);

            //t1.Start();
            //t2.Start();
            //t3.Start();
            //t4.Start();
        
        }

        private void ThreadTest1()
        {

            Console.WriteLine("Creating 2 threads \n");

            System.Threading.Thread t1;
            System.Threading.Thread t2;
            t1 = new System.Threading.Thread(this.scenario1);
            t2 = new System.Threading.Thread(this.scenario1);
            t1.Start();
            t2.Start();
        
        }

       private void scenario1()
        {
           /* Initialization of uniqe string names*/
          ///////////////////////////////////////////////////////////// problem with initialize same names
           int min = 0;
           int max =  10000;
          // int num = random.Next(min, max);
           string ADMIN_NAME = "mngr" + random.Next(min, max);
           string ADMIN_PSWD = "mngrPswd" + random.Next(min, max);
           string FORUM_NAME1 = "forum" + random.Next(min, max);
           string SUB_FORUM1 = "subForum" + random.Next(min, max);
           string USER_1 = "user" + random.Next(min, max);
           string PASSWORD_1 = "pswd" + random.Next(min, max);
           string DISCUSSION_1 = "discussion" + random.Next(min, max);
           string NO_CONTENT = "no content";

          // num = 2;//random.Next(min, max);
           string USER_2 = "user" + random.Next(min, max);
           string PASSWORD_2 = "pswd" + random.Next(min, max);
           
      
           
            try
            {
                Console.WriteLine("begining scenario1 \n");
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, FORUM_NAME1, ADMIN_NAME, ADMIN_PSWD);
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, SUB_FORUM1);
                User user = this.bridge.register(forum.forumId, USER_1, PASSWORD_1, "", "");
                Discussion d = this.bridge.createNewDiscussion(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId, DISCUSSION_1, NO_CONTENT);

                for (int i = 1; i <= 10; i++)
                {
                    this.bridge.createNewComment(USER_1, PASSWORD_1, forum.forumId, subForum.subForumId, d.discussionId, NO_CONTENT);
                }
                Boolean logoutAns = this.bridge.logout(forum.forumId, user.userName, user.password);
                AssertFalse(user.isLoggedIn);
                AssertTrue(logoutAns);

                int num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 10);

                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                User user2 = this.bridge.register(forum.forumId, USER_2, PASSWORD_2, "", "");
                user2 = this.bridge.login(forum.forumId, USER_2, PASSWORD_2);

                for (int i = 1; i <= 100; i++)
                {
                    this.bridge.createNewComment(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId, d.discussionId, NO_CONTENT);
                }
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 110);

                Boolean editAns = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, d.discussionId, ADMIN_NAME, ADMIN_PSWD, "brand new content");
                AssertEquals(d.content, "brand new content");
                AssertTrue(editAns);

                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 110);

                Discussion[] discussions = new Discussion[100];
                for (int i = 1; i <= 100; i++)
                    discussions[i] = this.bridge.createNewDiscussion(USER_2, PASSWORD_2, forum.forumId, subForum.subForumId, "discussion" + i, NO_CONTENT);
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 210);

                for (int i = 1; i <= 10; i++)
                    editAns = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, discussions[i].discussionId, USER_2, PASSWORD_2, "brand new content" + i);
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
                AssertTrue(num_of_comments == 210);

                testsLogger.logMethodTestResults("ThreadTest1", testNum);
                Console.WriteLine("finished scenario1 \n");

            }
            catch { 
                failMsg(testNum);
            }

            //this.bridge.reset();
          
        }
    }
}
