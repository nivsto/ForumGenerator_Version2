using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;
using System.Windows.Forms;
using System.Drawing;
using ConsoleApplication1.AccTests;


namespace ConsoleApplication1.AccTests
{
    class ThreadTest : AccTestsForumGenerator
    {
        const string SU_NAME = "admin"; // ForumGenerator.SU_NAME;
        const string SU_PSWD = "admin"; //ForumGenerator.SU_PSWD;
        const int min = 0;
        const int max = 1000000;
        static Random random = new Random();
        int testNum = 0;

        static int numOfThreads1 = 20;
        static int numOfThreads2 = 20;
        static ManualResetEvent[] events = new ManualResetEvent[numOfThreads1];
        static ManualResetEvent[] events2 = new ManualResetEvent[numOfThreads2];
        Forum Unitedforum;
        bool failed1 = false;
        bool failed2 = false;
        // const string ADMIN_NAME = "mngr";
        // const string ADMIN_PSWD = "mngrPswd";

        public ThreadTest(TestsLogger testsLogger, BridgeForumGenerator bridge)
        {
            this.bridge = bridge;
            this.testsLogger = testsLogger;
            this.bridge.reset();
        }

        public override void runTests()
        {
            this.testsLogger.logTestsSection("Thread");
            this.bridge.superUserLogin(SU_NAME, SU_PSWD);
            testsLogger.logMethodTest("ThreadTest1");
            Console.WriteLine("testing ThreadTest1:");
            ThreadTest1();
            testNum++;
            testsLogger.logMethodTestResults("ThreadTest1", testNum);
            Console.WriteLine("Done \n");

            this.bridge.reset();

            this.bridge.superUserLogin(SU_NAME, SU_PSWD);
            Console.WriteLine("testing ThreadTest2:");
            ThreadTest2();
            testNum++;
            testsLogger.logMethodTestResults("ThreadTest2", testNum);
            Console.WriteLine("Done \n");
            Console.WriteLine("Press Enter to exit");
            do
            {
                while (!Console.KeyAvailable)
                { }
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }


    
        private void ThreadTest1()
        {
            var list = new List<int>();           
            for (int i = 0; i < numOfThreads1; i++)
            {
                list.Add(i);
                events[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback(runAndSetScenario1),i);
            }
            WaitHandle.WaitAll(events);
            if(!failed1)
                testsLogger.logMethodTestResults("ThreadTest1", testNum);
        }

        private void runAndSetScenario1(object index)
        {
            int j = (int)index;
            scenario1();
            events[j].Set(); 
        }

        /*
         * Scenario 1 and 2:
            1. create constants
            2. create Forum, SubForum, User, Discussion.
            3. create 10 comments in Discussion
            4. User loging out, validate User is logout.
            5. register User2.
            6. create 10 comments in Discussion
            7. edit Discussion message
            8. create 10 Discussions
            9. edit all 10 Discussions' message

        */
        private void scenario1()
        {
            /* Initialization of uniqe string names*/
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
                Console.WriteLine("begining scenario1 - each thread has his own forum \n");
                Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, FORUM_NAME1, ADMIN_NAME, ADMIN_PSWD);
                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, SUB_FORUM1);
                User user = this.bridge.register(forum.forumId, USER_1, PASSWORD_1, "", "");
                Discussion d = this.bridge.createNewDiscussion(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId, DISCUSSION_1, NO_CONTENT);

                this.bridge.login(forum.forumId, USER_1, PASSWORD_1);
                for (int i = 1; i <= 10; i++)
                {
                    this.bridge.createNewComment(USER_1, PASSWORD_1, forum.forumId, subForum.subForumId, d.discussionId, NO_CONTENT);
                }
                Boolean logoutAns = this.bridge.logout(forum.forumId, user.userName, user.password);
                AssertFalse(user.isLoggedIn);
                AssertTrue(logoutAns);

                int num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
           

                this.bridge.login(forum.forumId, ADMIN_NAME, ADMIN_PSWD);
                User user2 = this.bridge.register(forum.forumId, USER_2, PASSWORD_2, "", "");
                user2 = this.bridge.login(forum.forumId, USER_2, PASSWORD_2);

                for (int i = 1; i <= 10; i++)
                {
                    this.bridge.createNewComment(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId, d.discussionId, NO_CONTENT);
                }
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
              

                d = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, d.discussionId, ADMIN_NAME, ADMIN_PSWD, "brand new content");
                AssertEquals(d.content, "brand new content");
               
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
              

                Discussion[] discussions = new Discussion[100];
                for (int i = 1; i <= 10; i++)
                    discussions[i] = this.bridge.createNewDiscussion(USER_2, PASSWORD_2, forum.forumId, subForum.subForumId, "discussion" + i, NO_CONTENT);
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
             

                for (int i = 1; i <= 10; i++)
                    d = this.bridge.editDiscussion(forum.forumId, subForum.subForumId, discussions[i].discussionId, USER_2, PASSWORD_2, "brand new content" + i);
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, forum.forumId, subForum.subForumId);
      
                Console.WriteLine("finished scenario1 \n");
            }
            catch
            {
                failMsg(testNum);
                failed1 = true;
            }
        }//scenario1


        string ADMIN_NAME = "mngr" + random.Next(min, max);
        string ADMIN_PSWD = "mngrPswd" + random.Next(min, max);
        string FORUM_NAME1 = "forum" + random.Next(min, max);


        private void ThreadTest2()
        {
            Console.WriteLine("creating one forum for all threads \n");

            /* creating one forum for all threads */
            Console.WriteLine("creating one forum for all threads \n");
          Unitedforum= this.bridge.createNewForum(SU_NAME, SU_PSWD, FORUM_NAME1, ADMIN_NAME, ADMIN_PSWD);
            this.bridge.login(Unitedforum.forumId, ADMIN_NAME, ADMIN_PSWD);

            var list = new List<int>();
            for (int i = 0; i < numOfThreads2; i++)
            {
                list.Add(i);
                events2[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback(runAndSetScenario2), i);
            }
            WaitHandle.WaitAll(events2);
            if(!failed2)
                testsLogger.logMethodTestResults("ThreadTest2", testNum);
        }


        private void runAndSetScenario2(object index)
        {
            int j = (int)index;
            scenario2();
            events2[j].Set();
        }
        private void scenario2()
        {
            /* Initialization of uniqe string names*/

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
             //   Forum forum = this.bridge.createNewForum(SU_NAME, SU_PSWD, FORUM_NAME1, ADMIN_NAME, ADMIN_PSWD);
                this.bridge.login(Unitedforum.forumId, ADMIN_NAME, ADMIN_PSWD);
                SubForum subForum = this.bridge.createNewSubForum(ADMIN_NAME, ADMIN_PSWD, Unitedforum.forumId, SUB_FORUM1);
                User user = this.bridge.register(Unitedforum.forumId, USER_1, PASSWORD_1, "", "");
                Discussion d = this.bridge.createNewDiscussion(ADMIN_NAME, ADMIN_PSWD, Unitedforum.forumId, subForum.subForumId, DISCUSSION_1, NO_CONTENT);

                this.bridge.login(Unitedforum.forumId, USER_1, PASSWORD_1);
                for (int i = 1; i <= 10; i++)
                {
                    this.bridge.createNewComment(USER_1, PASSWORD_1, Unitedforum.forumId, subForum.subForumId, d.discussionId, NO_CONTENT);
                }
                Boolean logoutAns = this.bridge.logout(Unitedforum.forumId, user.userName, user.password);
                AssertFalse(user.isLoggedIn);
                AssertTrue(logoutAns);

                int num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, Unitedforum.forumId, subForum.subForumId);


                this.bridge.login(Unitedforum.forumId, ADMIN_NAME, ADMIN_PSWD);
                User user2 = this.bridge.register(Unitedforum.forumId, USER_2, PASSWORD_2, "", "");
                user2 = this.bridge.login(Unitedforum.forumId, USER_2, PASSWORD_2);

                for (int i = 1; i <= 10; i++)
                {
                    this.bridge.createNewComment(ADMIN_NAME, ADMIN_PSWD, Unitedforum.forumId, subForum.subForumId, d.discussionId, NO_CONTENT);
                }
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, Unitedforum.forumId, subForum.subForumId);


                d = this.bridge.editDiscussion(Unitedforum.forumId, subForum.subForumId, d.discussionId, ADMIN_NAME, ADMIN_PSWD, "brand new content");
                AssertEquals(d.content, "brand new content");

                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, Unitedforum.forumId, subForum.subForumId);


                Discussion[] discussions = new Discussion[100];
                for (int i = 1; i <= 10; i++)
                    discussions[i] = this.bridge.createNewDiscussion(USER_2, PASSWORD_2, Unitedforum.forumId, subForum.subForumId, "discussion" + i, NO_CONTENT);
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, Unitedforum.forumId, subForum.subForumId);


                for (int i = 1; i <= 10; i++)
                    d = this.bridge.editDiscussion(Unitedforum.forumId, subForum.subForumId, discussions[i].discussionId, USER_2, PASSWORD_2, "brand new content" + i);
                num_of_comments = this.bridge.getNumOfCommentsSubForum(ADMIN_NAME, ADMIN_PSWD, Unitedforum.forumId, subForum.subForumId);
              

               
                Console.WriteLine("finished scenario2 \n");

            }
            catch
            {
                failMsg(testNum);
                failed2 = true;
            }

            //this.bridge.reset();

        }
    }
}
