using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
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

        public String writeXML(String startElement, String[] properties, String[][] data)
        {
            // Root.
            xml.WriteStartDocument();
            xml.WriteStartElement("List");
            xml.WriteWhitespace("\n");

            // Loop over Tuples.
            foreach (String[] element in data)
            {
                // Write Employee data.
                xml.WriteStartElement(startElement);
                {
                    for(int i=0; i<properties.Length; i++)
                    {
                        xml.WriteElementString(properties[i], element[i]);
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
