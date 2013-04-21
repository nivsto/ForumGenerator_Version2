using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
//using System.Threading.Tasks;
using System.Threading;
using System.Xml;
using System.IO;

namespace ForumGenerator_Client.Communication
{
    class Communicator
    {
        public void sendLoginReq(int forumId, string userName, string password)
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();
            
            XmlHandler x_test = new XmlHandler();
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("ForumId", forumId.ToString());
            Tuple<string, string> t2 = new Tuple<string, string>("UserName", userName);
            Tuple<string, string> t3 = new Tuple<string, string>("Password", password);
            args_list.AddLast(t1);
            args_list.AddLast(t2);
            args_list.AddLast(t3);
            string logout_res = x_test.cCreateXml("login", args_list);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
        }


        public Tuple<int,string> sendLogoutReq(int forumId, string userName)
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("ForumId", forumId.ToString());
            Tuple<string, string> t2 = new Tuple<string, string>("UserName", userName);
            args_list.AddLast(t1);
            args_list.AddLast(t2);
            string logout_res = x_test.cCreateXml("logout", args_list);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);



            return null;
        }

        public Tuple<int,string> sendAdminLoginReq(string userName, string password)
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("UserName", userName);
            Tuple<string, string> t2 = new Tuple<string, string>("Password", password);
            args_list.AddLast(t1);
            args_list.AddLast(t2);
            string logout_res = x_test.cCreateXml("adminlogin", args_list);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return null;
        }


        public Tuple<int,string> sendAdminLogoutReq(string userName)
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("UserName", userName);
            args_list.AddLast(t1);
            string logout_res = x_test.cCreateXml("adminlogout", args_list);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);


            return null;
        }

        public Tuple<int, string> sendRegisterReq(int forumId, string userName, string password, string email, string signature)
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();
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

            string logout_res = x_test.cCreateXml("register", args_list);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return null;
        }

        public LinkedList<Tuple<int, string>> sendGetForumsReq()
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "GET";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();

            string logout_res = x_test.cCreateXml("getForums", null);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return null;
        }


        public LinkedList<Tuple<int, string>> sendGetSubForumsReq()
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "GET";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();

            string logout_res = x_test.cCreateXml("getSubForums", null);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);


            return null;
        }


        public LinkedList<Tuple<int, string>> sendGetDiscussionsReq()
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "GET";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();

            string logout_res = x_test.cCreateXml("getDiscussions", null);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return null;
        }

        public LinkedList<Tuple<int, string>>> sendGetCommentsReq()
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "GET";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();

            string logout_res = x_test.cCreateXml("getComments", null);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return null;
        }

        public LinkedList<Tuple<int, string>> sendGetUsersReq()
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "GET";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();

            string logout_res = x_test.cCreateXml("getUsers", null);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return null;

        }

        public Tuple<int, string> sendCreateNewForumReq(string userName, string password, string forumName, 
            string adminUserName, string adminPassword)
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();
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

            string logout_res = x_test.cCreateXml("createNewForum", args_list);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return null;
        }

        public Tuple<int, string> sendCreateNewSubForumReq(string userName, string password, int forumId, string subForumTitle)
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();
            LinkedList<Tuple<string, string>> args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("UserName", userName);
            Tuple<string, string> t2 = new Tuple<string, string>("Password", password);
            Tuple<string, string> t3 = new Tuple<string, string>("ForumId", forumId.ToString());
            Tuple<string, string> t4 = new Tuple<string, string>("SubForumTitle", subForumTitle);

            args_list.AddLast(t1);
            args_list.AddLast(t2);
            args_list.AddLast(t3);
            args_list.AddLast(t4);

            string logout_res = x_test.cCreateXml("createNewSubForum", args_list);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return null;
        }



        public Tuple<int, string> sendCreateNewDiscussionReq(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();
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


            string logout_res = x_test.cCreateXml("createNewDiscussion", args_list);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return null;
        }


        public Tuple<int, string> sendCreateNewCommentReq(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            ASCIIEncoding encoding = new ASCIIEncoding();

            XmlHandler x_test = new XmlHandler();
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

            string logout_res = x_test.cCreateXml("createNewComment", args_list);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            return null;
        }


    }
}
