using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumGenerator_Version2_Server.Communication
{
    public class MyHttpServer : HttpServer
    {
        ForumGenerator_Version2_Server.Sys.ForumGenerator _forum_gen;

        
        public MyHttpServer(int port)
            : base(port)
        {
            _forum_gen = new ForumGenerator_Version2_Server.Sys.ForumGenerator("admin", "admin");
        }
        public override void handleGETRequest(HttpProcessor p)
        {

            if (p.http_url.Equals("/Test.png"))
            {
                Stream fs = File.Open("../../Test.png", FileMode.Open);

                p.writeSuccess("image/png");
                fs.CopyTo(p.outputStream.BaseStream);
                p.outputStream.BaseStream.Flush();
            }

            //#TBD - deal with GET request
            Console.WriteLine("request: {0}", p.http_url);
            p.writeSuccess();
            p.outputStream.WriteLine("<html><body><h1>test server</h1>");
            p.outputStream.WriteLine("Current Time: " + DateTime.Now.ToString());
            p.outputStream.WriteLine("url : {0}", p.http_url);

            p.outputStream.WriteLine("<form method=post action=/form>");
            p.outputStream.WriteLine("<input type=text name=foo value=foovalue>");
            p.outputStream.WriteLine("<input type=submit name=bar value=barvalue>");
            p.outputStream.WriteLine("</form>");
        }

        public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {

            Console.WriteLine("POST request: {0}", p.http_url);
            string xml_data = inputData.ReadToEnd(); //xml_data now contains XML from application

            XmlHandler xparser = new XmlHandler();
            Tuple<String, LinkedList<String>> parsed_info = xparser.getXmlParse(xml_data); //parsed_info contains method name and values of args
            string method_name = parsed_info.Item1;            
            LinkedList<string> args_list = parsed_info.Item2;
            int forum_id = 0;

            switch (method_name)
            {
                case "login":
                    forum_id = Convert.ToInt32(args_list.ElementAt(0));
                    string username = args_list.ElementAt(1);
                    string password = args_list.ElementAt(2);
                    Tuple<string, string> login_success_usertype  = _forum_gen.login(forum_id, username, password);
                    xparser.cCreateXml(method_name, login_success_usertype);
                    break;

                case "logout":
                    forum_id = Convert.ToInt32(args_list.ElementAt(0));
                    int user_id = Convert.ToInt32(args_list.ElementAt(1));
                    Tuple<string, string> logout_sucess_usertype = _forum_gen.logout(forum_id, user_id);
                    xparser.cCreateXml(method_name, logout_sucess_usertype);
                    break;

                default:
                    break;
            }



            //this is what the server returns to the client
            p.writeSuccess(); //200 ok
            p.outputStream.WriteLine("<html><body><h1>test server</h1>");
            p.outputStream.WriteLine("<a href=/test>return</a><p>");
            p.outputStream.WriteLine("postbody: <pre>{0}</pre>", xml_data);


        }
    }
}
