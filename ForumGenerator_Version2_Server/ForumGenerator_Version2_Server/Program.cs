using System;
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
            ForumGenerator forumGenerator = initProgram("admin", "admin"); // initialize the system with a super-user (username,password)
            Console.WriteLine("testing HttpServer");

            startServer(forumGenerator);
            //stubClient();
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

            //int forum_id = 123;
            //string user_name = "gid";
            //string password = "456";

            //string post_data = cGenLoginXML(forum_id, user_name, password);
            //byte[] data = Encoding.ASCII.GetBytes(post_data);
            //post_request.ContentLength = data.Length;
            //Stream requestStream = post_request.GetRequestStream();
            //requestStream.Write(data, 0, data.Length);

            ASCIIEncoding encoding = new ASCIIEncoding();

            //int forum_id = 123;
            //int user_id = 456;
            //XmlHandler x_test = new XmlHandler();
            //XmlWriterSettings xsettings = x_test.cWritePrelogueXML();
            //string logout_res = x_test.cWriteLoginXML(forum_id, "gid", "456", xsettings);
            //byte[] data = Encoding.ASCII.GetBytes(logout_res);
            //post_request.ContentLength = data.Length;
            //Stream requestStream = post_request.GetRequestStream();
            //requestStream.Write(data, 0, data.Length);

            string username = "admin";
            string password = "admin";
            XmlHandler x_test = new XmlHandler();
            LinkedList<Tuple<string, string>> test_args_list = new LinkedList<Tuple<string, string>>();
            Tuple<string, string> t1 = new Tuple<string, string>("UserName", username);
            Tuple<string, string> t2 = new Tuple<string, string>("Password", password);
            test_args_list.AddLast(t1);
            test_args_list.AddLast(t2);
            string logout_res = x_test.cCreateXml("adminlogin", test_args_list);
            byte[] data = Encoding.ASCII.GetBytes(logout_res);
            post_request.ContentLength = data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            //string forum_id = "126";
            //string username = "gid";
            //string password = "123";
            //string email = "blabla@gmail.com";
            //string signature = "gideon";
            //XmlHandler x_test = new XmlHandler();
            //LinkedList<Tuple<string, string>> test_args_list = new LinkedList<Tuple<string, string>>();
            //Tuple<string, string> t1 = new Tuple<string, string>("ForumID", forum_id);
            //Tuple<string, string> t2 = new Tuple<string, string>("UserName", username);
            //Tuple<string, string> t3 = new Tuple<string, string>("Password", password);
            //Tuple<string, string> t4 = new Tuple<string, string>("Email", email);
            //Tuple<string, string> t5 = new Tuple<string, string>("Signature", signature);
            //test_args_list.AddLast(t1);
            //test_args_list.AddLast(t2);
            //test_args_list.AddLast(t3);
            //test_args_list.AddLast(t4);
            //test_args_list.AddLast(t5);
            //string logout_res = x_test.cCreateXml("register", test_args_list);
            //byte[] data = Encoding.ASCII.GetBytes(logout_res);
            //post_request.ContentLength = data.Length;
            //Stream requestStream = post_request.GetRequestStream();
            //requestStream.Write(data, 0, data.Length);
        }

        public static string cGenLoginXML(int forum_id, string user_name, string password)
        {
            //XmlHandler xml_converter = new XmlHandler();
            //XmlWriterSettings xsettings = xml_converter.cWritePrelogueXML();
            //string xml_str = xml_converter.cWriteLoginXML(forum_id, user_name, password, xsettings);
            return null;
        }

        public static void startServer(ForumGenerator fg)
        {
            HttpServer httpServer = new MyHttpServer(80, fg);
            Thread thread = new Thread(new ThreadStart(httpServer.listen));
            thread.Start();
        }

    }
}
