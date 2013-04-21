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
        public override string handleGETRequest(HttpProcessor p)
        {

            string full_get_url = p.http_url; //http://localhost/requests?function=getforums --> /requests?function=getforums 
            int q_mark_loca = full_get_url.IndexOf('?'); //9
            string get_url = full_get_url.Substring(q_mark_loca, full_get_url.Length - q_mark_loca); //function=getforums 
            get_url.ToLower(); //verifying everything is in lowercase
            XmlHandler xparser = new XmlHandler();

            //case variables
            string[] get_params = get_url.Split('&');
            string[] function_param = get_params[0].Split('=');
            string[] get_forum_id; //forumid=123
            string[] get_subforum_id; //subforumid=456
            string[] get_discussion_id; //discussionid=789

            int discussion_id;
            int subforum_id;
            int forum_id;
            string xml_string;

            switch (function_param[1])
            {
                case "getforums":
                    Tuple<bool, string, string[], string[,]> getforums_result = _forum_gen.getForums();
                    xml_string = xparser.writeXML("getforums", getforums_result.Item2, getforums_result.Item3, getforums_result.Item4);
                    break;

                case "getsubforums":
                    if (get_params.Length != 2)
                    {
                        Console.WriteLine("error with getsubforums - wrong number of params");
                        return "error get forums";
                    }
                    get_forum_id = get_params[1].Split('=');
                    forum_id = Convert.ToInt32(get_forum_id[1]);     
                    Tuple<bool, string, string[], string[,]> getsubforums_result = _forum_gen.getSubForums(forum_id);
                    xml_string = xparser.writeXML("getsubforums", getsubforums_result.Item2, getsubforums_result.Item3, getsubforums_result.Item4);
                    break;

                case "getdiscussions":
                    if (get_params.Length != 3)
                    {
                        Console.WriteLine("error with getdiscussions - wrong number of params");
                        return "erro get discussions";
                    }
                    get_forum_id = get_params[1].Split('=');
                    get_subforum_id = get_params[2].Split('=');
                    forum_id = Convert.ToInt32(get_forum_id[1]);     
                    subforum_id = Convert.ToInt32(get_subforum_id[1]);
                    Tuple<bool, string, string[], string[,]> getdiscussions_result = _forum_gen.getDiscussions(forum_id, subforum_id);
                    xml_string = xparser.writeXML("getdiscussions", getdiscussions_result.Item2, getdiscussions_result.Item3, getdiscussions_result.Item4);
                    break;

                case "getcomments":
                    if (get_params.Length != 4)
                    {
                        Console.WriteLine("error with getcomments - wrong number of params");
                        return "error get comments";
                    }
                    get_forum_id = get_params[1].Split('=');
                    get_subforum_id = get_params[2].Split('=');
                    get_discussion_id = get_params[3].Split('=');
                    forum_id = Convert.ToInt32(get_forum_id[1]);     
                    subforum_id = Convert.ToInt32(get_subforum_id[1]);
                    discussion_id = Convert.ToInt32(get_discussion_id[1]);
                    Tuple<bool, string, string[], string[,]> getcomments_result = _forum_gen.getComments(forum_id, subforum_id, discussion_id);
                    xml_string = xparser.writeXML("getcomments", getcomments_result.Item2, getcomments_result.Item3, getcomments_result.Item4);
                    break;

                case "getusers":
                    if (get_params.Length != 2)
                    {
                        Console.WriteLine("error with getcomments - wrong number of params");
                        return "error get users";
                    }
                    get_forum_id = get_params[1].Split('=');
                    forum_id = Convert.ToInt32(get_forum_id[1]);     
                    Tuple<bool, string, string[], string[,]> getusers_result = _forum_gen.getUsers(forum_id);
                    xml_string = xparser.writeXML("getusers", getusers_result.Item2, getusers_result.Item3, getusers_result.Item4);
                    break;

                default:
                    Console.WriteLine("error with get - unrecognized method");
                    xml_string = "error general";
                    break;
            }
            
            //xml_string now contains xml_response to client
            return xml_string;
            //sendPostRequest(p, "http://localhost/", xml_string);
        }

        public override string handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {

            Console.WriteLine("POST request: {0}", p.http_url);
            string xml_data = inputData.ReadToEnd(); //xml_data now contains XML from application

            XmlHandler xparser = new XmlHandler();
            Tuple<String, LinkedList<String>> parsed_info = xparser.getXmlParse(xml_data); //parsed_info contains method name and values of args
            string method_name = parsed_info.Item1;            
            LinkedList<string> args_list = parsed_info.Item2;

            //switch declarations
            int forum_id = 0; 
            int sub_forum_id = 0;
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

                case "createnewsubforum":
                    username = args_list.ElementAt(0);
                    password = args_list.ElementAt(1);
                    forum_id = Convert.ToInt32(args_list.ElementAt(2));
                    string sub_forum_title = args_list.ElementAt(3);
                    Tuple<string, string> createnewsubforum_success_usertype = _forum_gen.createNewSubForum(username, password, forum_id, sub_forum_title);
                    succ_tuple = new Tuple<string, string>("success", createnewsubforum_success_usertype.Item1);
                    if (createnewsubforum_success_usertype.Item1 == "1")
                        msg_tuple = new Tuple<string, string>("UserType", createnewsubforum_success_usertype.Item2); //success on login
                    else
                        msg_tuple = new Tuple<string, string>("ErrorMsg", createnewsubforum_success_usertype.Item2); //failure on login
                    xarguments_list.AddLast(succ_tuple);
                    xarguments_list.AddLast(msg_tuple);
                    response_xml = xparser.cCreateXml(method_name, xarguments_list);
                    break;

                case "createnewdiscussion":
                    username = args_list.ElementAt(0);
                    password = args_list.ElementAt(1);
                    forum_id = Convert.ToInt32(args_list.ElementAt(2));
                    sub_forum_id = Convert.ToInt32(args_list.ElementAt(3));
                    string discussion_title = args_list.ElementAt(4);
                    string discussion_content = args_list.ElementAt(5);
                    Tuple<string, string> createnewdiscussion_success_usertype = _forum_gen.createNewDiscussion(username, password, forum_id, sub_forum_id, discussion_title, discussion_content);
                    succ_tuple = new Tuple<string, string>("success", createnewdiscussion_success_usertype.Item1);
                    if (createnewdiscussion_success_usertype.Item1 == "1")
                        msg_tuple = new Tuple<string, string>("UserType", createnewdiscussion_success_usertype.Item2); //success on login
                    else
                        msg_tuple = new Tuple<string, string>("ErrorMsg", createnewdiscussion_success_usertype.Item2); //failure on login
                    xarguments_list.AddLast(succ_tuple);
                    xarguments_list.AddLast(msg_tuple);
                    response_xml = xparser.cCreateXml(method_name, xarguments_list);
                    break;

                case "createnewcomment":
                    username = args_list.ElementAt(0);
                    password = args_list.ElementAt(1);
                    forum_id = Convert.ToInt32(args_list.ElementAt(2));
                    sub_forum_id = Convert.ToInt32(args_list.ElementAt(3));
                    int discussion_id = Convert.ToInt32(args_list.ElementAt(4));
                    string comment_content = args_list.ElementAt(5);
                    Tuple<string, string> createnewcomment_success_usertype = _forum_gen.createNewComment(username, password, forum_id, sub_forum_id, discussion_id, comment_content);
                    succ_tuple = new Tuple<string, string>("success", createnewcomment_success_usertype.Item1);
                    if (createnewcomment_success_usertype.Item1 == "1")
                        msg_tuple = new Tuple<string, string>("UserType", createnewcomment_success_usertype.Item2); //success on login
                    else
                        msg_tuple = new Tuple<string, string>("ErrorMsg", createnewcomment_success_usertype.Item2); //failure on login
                    xarguments_list.AddLast(succ_tuple);
                    xarguments_list.AddLast(msg_tuple);
                    response_xml = xparser.cCreateXml(method_name, xarguments_list);
                    break;


                default:
                    break;
            }

            return response_xml;
        }

        /*
         * sends a http POST request/response to the given URI and attaches the xml_string if exists
         * returns 0 on success or -1 on failure
         */
        public int sendPostRequest(HttpProcessor p,string uri, string xml_string)
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
