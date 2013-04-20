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
                xwriter.WriteElementString("ForumID", s_forum_id);
                xwriter.WriteElementString("UserName", username);
                xwriter.WriteElementString("Password", password);

                xwriter.Flush();
            }
            finally
            {
                if (xwriter != null)
                    xwriter.Close();
            }

            return swriter.ToString(); //swriter contains full xml for login request
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
