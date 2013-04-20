using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Collections;


namespace ForumGenerator_Version2_Server.Communication
{
    class XmlHandler
    {
        StringWriter str;
        XmlTextWriter xml;

        //c'tor
        public XmlHandler()
        {
            this.str = new StringWriter();
            this.xml = new XmlTextWriter(str);
        }

        /*
         * retrieves the logout info (forum id, user id) from the xml_string
         * returns a string containing forum id and user id seperated by whitespace
         */
        public string sGetLogoutInfo(string xml_string)
        {
            if (xml_string == null)
                return null;

            int forum_id = 0;
            int user_id = 0;
            XmlReader xreader = XmlReader.Create(new StringReader(xml_string.ToLower()));

            xreader.ReadToFollowing("params");
            xreader.Read(); //read the following whitespace
            xreader.Read(); //read the forumid
            forum_id = Convert.ToInt32(xreader.ReadInnerXml()); //save forumid's value
            xreader.Read(); //read the following whitespace
            user_id = Convert.ToInt32(xreader.ReadInnerXml());

            return forum_id + " " + user_id; //returns all logout data seperated by whitespaces
        }
        
        /*
         * returns the forum id, username and password from the xml_string
         * returns null on failure
         */
        public string sGetLoginInfo(string xml_string)
        {
            if (xml_string == null)
                return null;

            //init
            int forum_id = 0;
            string user_name = null;
            string password = null;
            XmlReader xreader = XmlReader.Create(new StringReader(xml_string.ToLower()));
            
            //reading info from xml
            xreader.ReadToFollowing("params");
            xreader.Read(); //read the following whitespace
            xreader.Read(); //read the forumid
            forum_id = Convert.ToInt32(xreader.ReadInnerXml()); //save forumid's value
            xreader.Read(); //read the following whitespace
            user_name = xreader.ReadInnerXml();
            xreader.Read(); //read the following whitespace
            password = xreader.ReadInnerXml();

            return forum_id + " " + user_name + " " + password; //returns all login data seperated by whitespaces
        }

     

        /*
         * this method receives xml string and parses the type of method invocation.
         * returns: method name string on success or "UnknownMethod" otherwise
         */
        public string sParseXMLMethod(string xml_string)
        {
            XmlReader xreader = XmlReader.Create(new StringReader(xml_string));
            xreader.ReadToFollowing("MessageType");
            
            if (xreader.ReadInnerXml() != "request")
            {
                Console.WriteLine("error in sParseXMLMethod - given xml is not a request xml");
                return "UnknownMethod";
            }

            //add call to xmlParse here
            string str_method_name = null;
            xreader.ReadToFollowing("MethodName");
            switch (xreader.ReadInnerXml())
            {
                case "login":
                    Console.WriteLine("LOGIN");
                    str_method_name = "login";
                    break;
                case "logout":
                    Console.WriteLine("LOGOUT");
                    str_method_name = "logout";
                    break;
                default:
                    Console.WriteLine("Error in MethodType in sParseXMLMethod");
                    break;
            }

            return str_method_name;
        }

        
        public XmlWriterSettings cWritePrelogueXML() {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;
            xsettings.IndentChars = ("\t");
            xsettings.OmitXmlDeclaration = true;
            return xsettings;
        }

        public string cWriteLogoutXML(int forum_id, int user_id, XmlWriterSettings xsettings)
        {
            //init
            string s_forum_id = forum_id.ToString();
            string s_user_id = user_id.ToString();
            XmlWriter xwriter = null;
            StringWriter swriter = null;

            try
            {
                //creating xml writer with xsettings that writes the result to a string
                swriter = new StringWriter();
                xwriter = XmlWriter.Create(swriter, xsettings);

                //creating prelogue
                xwriter.WriteStartElement("XML");
                xwriter.WriteStartElement("HTTP");
                xwriter.WriteElementString("MessageType", "request");
                xwriter.WriteElementString("MethodName", "logout");

                            ////inserting relevant info
                xwriter.WriteStartElement("params");
                xwriter.WriteElementString("ForumID", s_forum_id);
                xwriter.WriteElementString("UserName", s_user_id);

                xwriter.Flush();
            }
            finally
            {
                if (xwriter != null)
                    xwriter.Close();
            }

            return swriter.ToString(); //swriter contains full xml for logout request

            }

        public string cWriteLoginXML(int forum_id, string username, string password, XmlWriterSettings xsettings)
        {
            //init
            string s_forum_id = forum_id.ToString();
            XmlWriter xwriter = null;
            StringWriter swriter = null;

            try
            {
                //creating xml writer with xsettings that writes the result to a string
                swriter = new StringWriter();
                xwriter = XmlWriter.Create(swriter, xsettings);

                //creating prelogue
                xwriter.WriteStartElement("XML");
                xwriter.WriteStartElement("HTTP");
                xwriter.WriteElementString("MessageType", "request");
                xwriter.WriteElementString("MethodName", "login");

                //inserting relevant info
                xwriter.WriteStartElement("params");
                xwriter.WriteStartElement("arg");
                xwriter.WriteAttributeString("Name", "ForumID");
                xwriter.WriteString("123");
                xwriter.WriteEndElement();

                xwriter.WriteStartElement("arg");
                xwriter.WriteAttributeString("Name", "UserName");
                xwriter.WriteString("gid");
                xwriter.WriteEndElement();

                xwriter.WriteStartElement("arg");
                xwriter.WriteAttributeString("Name", "Password");
                xwriter.WriteString("456");
                xwriter.WriteEndElement();

                //xwriter.WriteElementString("ForumID", s_forum_id);
                //xwriter.WriteElementString("UserName", username);
                //xwriter.WriteElementString("Password", password);

                xwriter.Flush();
            }
            finally
            {
                if (xwriter != null)
                    xwriter.Close();
            }

            return swriter.ToString(); //swriter contains full xml for login request
        }


        public XmlWriterSettings createXmlSettings()
        {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;
            xsettings.IndentChars = ("\t");
            xsettings.OmitXmlDeclaration = true;
            return xsettings;
        }

        public string cCreateXml(string method_name, LinkedList<Tuple<string, string>> args_list)
        {
            XmlWriterSettings xsettings = createXmlSettings();
            XmlWriter xwriter = null;
            StringWriter swriter = null;

            try
            {
                //creating xml writer with xsettings that writes the result to a string
                swriter = new StringWriter();
                xwriter = XmlWriter.Create(swriter, xsettings);

                xwriter.WriteStartElement("XML");
                xwriter.WriteStartElement("HTTP");
                xwriter.WriteElementString("MessageType", "request");
                xwriter.WriteElementString("MethodName", method_name);
                xwriter.WriteStartElement("params");

                foreach (Tuple<string, string> curr_arg in args_list)
                {
                    xwriter.WriteStartElement("arg");
                    xwriter.WriteAttributeString("Name", curr_arg.Item1);
                    xwriter.WriteString(curr_arg.Item2);
                    xwriter.WriteEndElement();
                }

                xwriter.Flush();
            }
            finally
            {
                if (xwriter != null)
                    xwriter.Close();
            }

            return swriter.ToString();
        }

        /*
         * Receives XML string parses it and returns a tuple containing the method name(str) and a list of params to that method
         */
        public Tuple<string, LinkedList<String>> getXmlParse(string xml_string)
        {
            XmlDocument xreader = new XmlDocument();
            xreader.LoadXml(xml_string);

            //getting the method name
            XmlNodeList param_list = xreader.GetElementsByTagName("MethodName"); //jumping to the MethodName element
            string method_name = param_list.Item(0).InnerText; //receiving the value of the MethodName element

            //getting the list of args
            //init
            LinkedList<String> res_args_list = new LinkedList<String>();

            param_list = xreader.GetElementsByTagName("params");
            XmlNodeList args_list = param_list.Item(0).ChildNodes;
            foreach (XmlNode current_arg in args_list) { //args_list is a list of all child nodes for params
                string current_arg_value = current_arg.InnerText;
                res_args_list.AddLast(current_arg_value);
            }

            Tuple<String, LinkedList<String>> res_method_args = new Tuple<string, LinkedList<string>>(method_name, res_args_list);
            return res_method_args;
        }

        public String writeXML(string startElement, string[] properties, string[,] data)
        {
            // Root.
            xml.WriteStartDocument();
            xml.WriteStartElement("List");
            xml.WriteWhitespace("\n");

            // Loop over Tuples.
            for (int j = 0; j < data.GetLength(0); j++)
            {
                // Write Employee data.
                xml.WriteStartElement(startElement);
                {
                    for (int i = 0; i < properties.Length; i++)
                    {
                        xml.WriteElementString(properties[i], data[j, i]);
                    }
                }
                xml.WriteEndElement();
                xml.WriteWhitespace("\n");
            }

            // End.
            xml.WriteEndElement();
            xml.WriteEndDocument();

            // Result is a string.
            return str.ToString();
        }
    }
}
