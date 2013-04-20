﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server.Communication;
using System.Threading;
using System.Net;
using System.Xml;
using System.IO;

namespace ForumGenerator_Version2_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //ForumGenerator forumGenerator = initProgram("admin", "admin"); // initialize the system with a super-user (username,password)
            Console.WriteLine("testing HttpServer");
            startServer();
            stubClient();
        }

        private static ForumGenerator initProgram(string superUserName, string superUserPass)
        {
            return new ForumGenerator(superUserName, superUserPass);
        }

        public static void stubClient()
        {
            //Uri post_uri = new Uri("127.0.0.1/upload"); //where to send the post request to
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create("http://localhost/");
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            
            //ASCIIEncoding encoding = new ASCIIEncoding();

            string forum_id = "123";
            string user_name = "gid";
            string password = "456";

            string post_data = generateLoginXML(forum_id, user_name, password);
            byte[] data = Encoding.ASCII.GetBytes(post_data);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            
            //Console.WriteLine(result);
            //startServer();
            //MyHttpServer testServer = new MyHttpServer(80);
            //testServer.listen();
            //startServer();  //starting the server and waiting for requests

        }

        public static string generateLoginXML(string forum_id, string user_name, string password)
        {
            XmlHandler xml_converter = new XmlHandler();
            String xml_str = xml_converter.XMLwrite(forum_id, user_name, password);
            return xml_str;
        }

        public static void startServer()
        {
            HttpServer httpServer = new MyHttpServer(80);
            Thread thread = new Thread(new ThreadStart(httpServer.listen));
            thread.Start();
        }

    }
}
