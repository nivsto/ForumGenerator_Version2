using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ForumGenerator_Version2_Server.Sys;

namespace ForumGenerator_Version2_Server.Communication
{
    public class MyHttpServer : HttpServer
    {
        ForumGenerator _forum_gen;

        
        public MyHttpServer(int port, ForumGenerator forum_gen)
            : base(port)
        {
            _forum_gen = forum_gen;
        }
        public override void handleGETRequest(HttpProcessor p)
        {

            string full_get_url = p.http_url; //http://localhost/requests?function=getforums --> /requests?function=getforums 
            int q_mark_loca = full_get_url.IndexOf('?'); //9
            string get_url = full_get_url.Substring(q_mark_loca, full_get_url.Length - q_mark_loca); //function=getforums 

            //#TBD - parse by & delimiter and invoke relevant methods
            
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

            //variables to be later used for switch (can not be declared in the switch)
            int forum_id = 0; //for login, logout
            LinkedList<Tuple<string, string>> xarguments_list = new LinkedList<Tuple<string, string>>(); 
            string username = null;
            string password = null;
            Tuple<string, string> succ_tuple = new Tuple<string, string>("", "");
            Tuple<string, string> msg_tuple = new Tuple<string, string>("", "");

            string response_xml = null;

            switch (method_name)
            {
                case "login":
                    forum_id = Convert.ToInt32(args_list.ElementAt(0));
                    username = args_list.ElementAt(1);
                    password = args_list.ElementAt(2);
                    Tuple<string, string> login_success_usertype  = _forum_gen.login(forum_id, username, password);
                    succ_tuple = new Tuple<string,string>("success", login_success_usertype.Item1);
                    if (login_success_usertype.Item1 == "1")
                        msg_tuple = new Tuple<string, string>("UserType", login_success_usertype.Item2); //success on login
                    else
                        msg_tuple = new Tuple<string, string>("ErrorMsg", login_success_usertype.Item2); //failure on login
                    xarguments_list.AddLast(succ_tuple);
                    xarguments_list.AddLast(msg_tuple);
                    response_xml = xparser.cCreateXml(method_name, xarguments_list);
                    break;

                case "logout":
                    forum_id = Convert.ToInt32(args_list.ElementAt(0));
                    int user_id = Convert.ToInt32(args_list.ElementAt(1));
                    Tuple<string, string> logout_success_usertype = _forum_gen.logout(forum_id, user_id);
                    succ_tuple = new Tuple<string, string>("success", logout_success_usertype.Item1);
                    if (logout_success_usertype.Item1 == "1")
                        msg_tuple = new Tuple<string, string>("UserType", logout_success_usertype.Item2); //success on login
                    else
                        msg_tuple = new Tuple<string, string>("ErrorMsg", logout_success_usertype.Item2); //failure on login
                    xarguments_list.AddLast(succ_tuple);
                    xarguments_list.AddLast(msg_tuple);
                    response_xml = xparser.cCreateXml(method_name, xarguments_list);
                    break;

                case "adminlogin":
                    username = args_list.ElementAt(0);
                    password = args_list.ElementAt(1);
                    Tuple<string, string> admin_login_success_usertype = _forum_gen.adminLogin(username, password);
                    succ_tuple = new Tuple<string, string>("success", admin_login_success_usertype.Item1);
                    if (admin_login_success_usertype.Item1 == "1")
                        msg_tuple = new Tuple<string, string>("UserType", admin_login_success_usertype.Item2); //success on login
                    else
                        msg_tuple = new Tuple<string, string>("ErrorMsg", admin_login_success_usertype.Item2); //failure on login
                    xarguments_list.AddLast(succ_tuple);
                    xarguments_list.AddLast(msg_tuple);
                    response_xml = xparser.cCreateXml(method_name, xarguments_list);
                    break;

                case "adminlogout":
                    Tuple<string, string> admin_logout_success_usertype = _forum_gen.adminLogout();
                    succ_tuple = new Tuple<string, string>("success", admin_logout_success_usertype.Item1);
                    if (admin_logout_success_usertype.Item1 == "1")
                        msg_tuple = new Tuple<string, string>("UserType", admin_logout_success_usertype.Item2); //success on login
                    else
                        msg_tuple = new Tuple<string, string>("ErrorMsg", admin_logout_success_usertype.Item2); //failure on login
                    xarguments_list.AddLast(succ_tuple);
                    xarguments_list.AddLast(msg_tuple);
                    response_xml = xparser.cCreateXml(method_name, xarguments_list);
                    break;
                    
                case "register":
                    forum_id = Convert.ToInt32(args_list.ElementAt(0));
                    username = args_list.ElementAt(1);
                    password = args_list.ElementAt(2);
                    string email = args_list.ElementAt(3);
                    string signature = args_list.ElementAt(4);
                    Tuple<string, string> register_success_usertype  = _forum_gen.register(forum_id, username, password, email, signature);
                    succ_tuple = new Tuple<string,string>("success", register_success_usertype.Item1);
                    if (register_success_usertype.Item1 == "1")
                        msg_tuple = new Tuple<string, string>("UserType", register_success_usertype.Item2); //success on login
                    else
                        msg_tuple = new Tuple<string, string>("ErrorMsg", register_success_usertype.Item2); //failure on login
                    xarguments_list.AddLast(succ_tuple);
                    xarguments_list.AddLast(msg_tuple);
                    response_xml = xparser.cCreateXml(method_name, xarguments_list);
                    break;

                case "createnewforum":
                    username = args_list.ElementAt(0);
                    password = args_list.ElementAt(1);
                    string forum_name = args_list.ElementAt(2);
                    string admin_username = args_list.ElementAt(3);
                    string admin_password = args_list.ElementAt(4);
                    Tuple<string, string> createnewforum_success_usertype = _forum_gen.createNewForum(username, password, forum_name, admin_username, admin_password);
                    succ_tuple = new Tuple<string, string>("success", createnewforum_success_usertype.Item1);
                    if (createnewforum_success_usertype.Item1 == "1")
                        msg_tuple = new Tuple<string, string>("UserType", createnewforum_success_usertype.Item2); //success on login
                    else
                        msg_tuple = new Tuple<string, string>("ErrorMsg", createnewforum_success_usertype.Item2); //failure on login
                    xarguments_list.AddLast(succ_tuple);
                    xarguments_list.AddLast(msg_tuple);
                    response_xml = xparser.cCreateXml(method_name, xarguments_list);
                    break;

                //case "getforums":
                //    Tuple<bool, string, string[], string[,]> getforums_success_result = _forum_gen.getForums();
                //    response_xml = xparser.writeXML("getForums", getforums_success_result.Item2, getforums_success_result.Item3, getforums_success_result.Item4);
                //    break;

                //case "getsubforums":
                //    forum_id = Convert.ToInt32(args_list.ElementAt(0));
                //    Tuple<bool, string, string[], string[,]> getsubforums_success_result = _forum_gen.getSubForums(forum_id);
                //    response_xml = xparser.writeXML("getSubForums", getsubforums_success_result.Item2, getsubforums_success_result.Item3, getsubforums_success_result.Item4);
                //    break;

                //case "getdiscussions":
                //    forum_id = Convert.ToInt32(args_list.ElementAt(0));
                //    int subforum_id = Convert.ToInt32(args_list.ElementAt(1));
                //    Tuple<bool, string, string[], string[,]> getdiscussions_success_result = _forum_gen.getDiscussions(forum_id, subforum_id);
                //    response_xml = xparser.writeXML("getDiscussions", getdiscussions_success_result.Item2, getdiscussions_success_result.Item3, getdiscussions_success_result.Item4);
                //    break;



                default:
                    break;
            }

            sendPostRequest("http://localhost/", response_xml);
        }

        /*
         * sends a http POST request/response to the given URI and attaches the xml_string if exists
         * returns 0 on success or -1 on failure
         */
        public int sendPostRequest(string uri, string xml_string)
        {
            if (uri == null || xml_string == null)
                return -1;
            
            HttpWebRequest post_request = (HttpWebRequest)WebRequest.Create(uri);
            post_request.Method = "POST";
            post_request.ContentType = "text/xml";
            
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] xml_data = Encoding.ASCII.GetBytes(xml_string);
            post_request.ContentLength = xml_data.Length;
            Stream requestStream = post_request.GetRequestStream();
            requestStream.Write(xml_data, 0, xml_data.Length);

            return 0;
        }
    }
}
