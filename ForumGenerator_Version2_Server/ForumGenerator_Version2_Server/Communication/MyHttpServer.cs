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
            string xml_data = inputData.ReadToEnd(); //data now contains XML from application

            XmlHandler xparser = new XmlHandler();
            string method_name = xparser.sParseXMLMethod(xml_data); //getting method name from XML
            switch (method_name)
            {
                case "login":
                    string login_info = xparser.sGetLoginInfo(xml_data); //retrieve login info
                    string[] login_words = login_info.Split(' '); 
                    _forum_gen.login(Convert.ToInt32(login_words[0]), login_words[1], login_words[2]);
                    break;
                case "logout":
                    string logout_info = xparser.sGetLogoutInfo(xml_data); //retrieve logout info
                    string[] logout_words = logout_info.Split(' ');
                    _forum_gen.logout(Convert.ToInt32(logout_words[0]), Convert.ToInt32(logout_words[1]));
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
