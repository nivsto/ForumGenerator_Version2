using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Collections;
using System.Net;

using ForumGenerator_Client.Communication;

namespace ForumGenerator_Client.Communication
{
    public class XmlHandler
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
         * defines global xml settings for xml creation
         */
        public XmlWriterSettings createXmlSettings()
        {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;
            xsettings.IndentChars = ("\t");
            xsettings.OmitXmlDeclaration = true;
            return xsettings;
        }

        /*
         * Receives a method name and a list of tuples where each tuple contains args attribute and args value.
         * Returns XML string with the relevant data
         */
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
            foreach (XmlNode current_arg in args_list)
            { //args_list is a list of all child nodes for params
                string current_arg_value = current_arg.InnerText;
                res_args_list.AddLast(current_arg_value);
            }

            Tuple<String, LinkedList<String>> res_method_args = new Tuple<string, LinkedList<string>>(method_name, res_args_list);
            return res_method_args;
        }

        /*
         * method_name - name of method - "getForums"/"getSubForums"/"getThreads" etc
         * startElement - "Forum"/"SubForum"/"Discussion"/"Comment"
         * properties - array of strings - for example "ForumID" and "ForumTitle"
         * data - first column is for "ForumID" values and 2nd column is for "ForumTitle"
         */
        public String writeXML(string method_name, string startElement, string[] properties, string[,] data)
        {
            XmlWriterSettings xsettings = createXmlSettings();
            XmlWriter xwriter = null;
            StringWriter swriter = null;

            // Root.
            try
            {
                //creating xml writer with xsettings that writes the result to a string
                swriter = new StringWriter();
                xwriter = XmlWriter.Create(swriter, xsettings);

                xwriter.WriteStartElement("XML");
                xwriter.WriteStartElement("HTTP");
                xwriter.WriteElementString("MessageType", "request");
                xwriter.WriteElementString("MethodName", method_name);

                // Loop over Tuples.
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    // Write Employee data.
                    xwriter.WriteStartElement(startElement);
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            xwriter.WriteElementString(properties[i], data[j, i]);
                        }
                    }

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



        public LinkedList<Tuple<int, string>> parseXmlGets(string xml_string)
        {
            XmlDocument xreader = new XmlDocument();
            xreader.LoadXml(xml_string);

            //getting the list of args
            //init
            LinkedList<Tuple<int, string>> res_args_list = new LinkedList<Tuple<int, string>>();

            XmlNodeList getSubArgs_list = xreader.GetElementsByTagName("getSubArgs");
            int number_of_subargs = 0;

            while (number_of_subargs < getSubArgs_list.Count)
            {
                int current_id = Convert.ToInt32(getSubArgs_list.Item(number_of_subargs).InnerText);
                number_of_subargs++;
                string current_title = getSubArgs_list.Item(number_of_subargs).InnerText.ToString();
                number_of_subargs++;
                Tuple<int, string> curr_tuple = new Tuple<int, string>(current_id, current_title);
                res_args_list.AddLast(curr_tuple);
            }

            return res_args_list;
        }
    }
}
