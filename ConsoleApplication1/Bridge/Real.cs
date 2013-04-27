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
    public class Real : Bridge
    {
        const string POST = "POST";
        const string GET = "GET";
        const string HOST = "http://localhost/";

        private ForumGenerator forumGen;
        private HttpServer httpServer;
        private HttpHandler httpHandler;

        public Real()
        {
            Console.Write("Creating ForumGenerator...  ");
            this.forumGen = new ForumGenerator("admin", "admin"); // initialize the system with a super-user (username, password)
            Console.WriteLine("done");
            Console.Write("Initializing server... ");
            httpServer = new MyHttpServer(80, this.forumGen);
            Thread thread = new Thread(new ThreadStart(httpServer.listen));
            thread.Start();
            Console.WriteLine("done");

            ASCIIEncoding encoding = new ASCIIEncoding();
            this.httpHandler = new HttpHandler(encoding);
        }


        public string login(string xmlRequest)
        {
            return this.httpHandler.sendRequest(HOST, POST, xmlRequest);
        }


        public string adminLogin(string xmlRequest)
        {
            return this.httpHandler.sendRequest(HOST, POST, xmlRequest);
        }


        public string adminLogout(string xmlRequest)
        {
            return this.httpHandler.sendRequest(HOST, POST, xmlRequest);
        }


        public string logout(string xmlRequest)
        {
            return this.httpHandler.sendRequest(HOST, POST, xmlRequest);
        }


        public string createNewForum(string xmlRequest)
        {
            return this.httpHandler.sendRequest(HOST, POST, xmlRequest);
        }


        public string createNewSubForum(string xmlRequest)
        {
            return this.httpHandler.sendRequest(HOST, POST, xmlRequest);
        }


        public string register(string xmlRequest)
        {
            throw new NotImplementedException();
        }


        public string createNewThead(string xmlRequest)
        {
            throw new NotImplementedException();
        }


        public string addNewReply(string xmlRequest)
        {
            throw new NotImplementedException();
        }


        public string getForums(string xmlRequest)
        {
            throw new NotImplementedException();
        }


        public string getSubForums(string xmlRequest)
        {
            throw new NotImplementedException();
        }


        public string getThreads(string xmlRequest)
        {
            throw new NotImplementedException();
        }


        public string getReplies(string xmlRequest)
        {
            throw new NotImplementedException();
        }
    }
}
