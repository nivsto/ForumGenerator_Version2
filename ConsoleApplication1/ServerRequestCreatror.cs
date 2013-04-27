using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Xml;
using System.IO;

namespace ConsoleApplication1
{
    class ServerRequestCreator
    {

        private XmlHandler xmlHandler;

        public ServerRequestCreator()
        {
            this.xmlHandler = new XmlHandler();
        }


        public string LoginReq(int forumId, string userName, string password)
        {
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("ForumId", forumId.ToString());
            Tuple<string, string> t2 = new Tuple<string, string>("UserName", userName);
            Tuple<string, string> t3 = new Tuple<string, string>("Password", password);
            args_list.AddLast(t1);
            args_list.AddLast(t2);
            args_list.AddLast(t3);
            string result = xmlHandler.cCreateXml("login", args_list);
            
            return result;
        }


        public string LogoutReq(int forumId, string userName)
        {
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("ForumId", forumId.ToString());
            Tuple<string, string> t2 = new Tuple<string, string>("UserName", userName);
            args_list.AddLast(t1);
            args_list.AddLast(t2);
            string result = xmlHandler.cCreateXml("logout", args_list);

            return result;
        }

        public string AdminLoginReq(string userName, string password)
        {
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("UserName", userName);
            Tuple<string, string> t2 = new Tuple<string, string>("Password", password);
            args_list.AddLast(t1);
            args_list.AddLast(t2);
            string result = xmlHandler.cCreateXml("adminLogin", args_list);
           
            return result;
        }


        public string AdminLogoutReq(string userName)
        {
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("UserName", userName);
            args_list.AddLast(t1);
            string result = xmlHandler.cCreateXml("adminLogout", args_list);

            return result;
        }

        public string RegisterReq(int forumId, string userName, string password, string email, string signature)
        {
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("ForumId", forumId.ToString());
            Tuple<string, string> t2 = new Tuple<string, string>("UserName", userName);
            Tuple<string, string> t3 = new Tuple<string, string>("Password", password);
            Tuple<string, string> t4 = new Tuple<string, string>("Email", email);
            Tuple<string, string> t5 = new Tuple<string, string>("Signature", signature);

            args_list.AddLast(t1);
            args_list.AddLast(t2);
            args_list.AddLast(t3);
            args_list.AddLast(t4);
            args_list.AddLast(t5);

            string result = xmlHandler.cCreateXml("register", args_list);
            
            return result;
        }


        public string GetForumsReq()
        {
            string result = xmlHandler.cCreateXml("getForums", null);

            return result;
        }


        public string GetSubForumsReq()
        {
            string result = xmlHandler.cCreateXml("getSubForums", null);

            return result;
        }


        public string GetDiscussionsReq()
        {
            string result = xmlHandler.cCreateXml("getDiscussions", null);

            return result;
        }

        public string GetCommentsReq()
        {
            string result = xmlHandler.cCreateXml("getComments", null);

            return result;
        }

        public string GetUsersReq()
        {
            string result = xmlHandler.cCreateXml("getUsers", null);

            return result;
        }

        public string CreateNewForumReq(string userName, string password, string forumName, 
            string adminUserName, string adminPassword)
        {
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("UserName", userName);
            Tuple<string, string> t2 = new Tuple<string, string>("Password", password);
            Tuple<string, string> t3 = new Tuple<string, string>("ForumName", forumName);
            Tuple<string, string> t4 = new Tuple<string, string>("AdminUserName", adminUserName);
            Tuple<string, string> t5 = new Tuple<string, string>("AdminPassword", adminPassword);

            args_list.AddLast(t1);
            args_list.AddLast(t2);
            args_list.AddLast(t3);
            args_list.AddLast(t4);
            args_list.AddLast(t5);

            string result = xmlHandler.cCreateXml("createnewforum", args_list);
            return result;
        }

        public string CreateNewSubForumReq(string userName, string password, int forumId, string subForumTitle)
        {
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("UserName", userName);
            Tuple<string, string> t2 = new Tuple<string, string>("Password", password);
            Tuple<string, string> t3 = new Tuple<string, string>("ForumId", forumId.ToString());
            Tuple<string, string> t4 = new Tuple<string, string>("SubForumTitle", subForumTitle);

            args_list.AddLast(t1);
            args_list.AddLast(t2);
            args_list.AddLast(t3);
            args_list.AddLast(t4);

            string result = xmlHandler.cCreateXml("createnewsubforum", args_list);

            return result;
        }



        public string CreateNewDiscussionReq(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("UserName", userName);
            Tuple<string, string> t2 = new Tuple<string, string>("Password", password);
            Tuple<string, string> t3 = new Tuple<string, string>("ForumName", forumId.ToString());
            Tuple<string, string> t4 = new Tuple<string, string>("SubForumId", subForumId.ToString());
            Tuple<string, string> t5 = new Tuple<string, string>("Title", title);
            Tuple<string, string> t6 = new Tuple<string, string>("Content", content);

            args_list.AddLast(t1);
            args_list.AddLast(t2);
            args_list.AddLast(t3);
            args_list.AddLast(t4);
            args_list.AddLast(t5);
            args_list.AddLast(t6);

            string result = xmlHandler.cCreateXml("createNewDiscussion", args_list);

            return result;
        }


        public string CreateNewCommentReq(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("UserName", userName);
            Tuple<string, string> t2 = new Tuple<string, string>("Password", password);
            Tuple<string, string> t3 = new Tuple<string, string>("ForumName", forumId.ToString());
            Tuple<string, string> t4 = new Tuple<string, string>("SubForumId", subForumId.ToString());
            Tuple<string, string> t5 = new Tuple<string, string>("DiscussionId", discussionId.ToString());
            Tuple<string, string> t6 = new Tuple<string, string>("Content", content);

            args_list.AddLast(t1);
            args_list.AddLast(t2);
            args_list.AddLast(t3);
            args_list.AddLast(t4);
            args_list.AddLast(t5);
            args_list.AddLast(t6);

            string result = xmlHandler.cCreateXml("createNewComment", args_list);

            return result;
        }


    }//class
}
