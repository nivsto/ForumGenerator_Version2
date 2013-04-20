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

        public XmlHandler()
        {
            this.str = new StringWriter();
            this.xml = new XmlTextWriter(str);
        }

        public String XMLwrite(String startElement, String property, String property_value)
        {
            XmlWriter writer = null;

            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("\t");
                settings.OmitXmlDeclaration = true;
                writer = XmlWriter.Create("temp_xml.xml", settings);

                writer.WriteStartElement("XML");
                writer.WriteStartElement("HTTP");
                writer.WriteStartElement("Login");
                writer.WriteElementString("ForumId", startElement);
                writer.WriteElementString("UserName", property);
                writer.WriteElementString("Password", property_value);

                writer.Flush();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

            string res = File.ReadAllText(@"temp_xml.xml");
            return res;
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
                        xml.WriteWhitespace("\n");
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
