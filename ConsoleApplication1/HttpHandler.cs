using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;


namespace ConsoleApplication1
{

    public class HttpHandler
    {

        private ASCIIEncoding encoding;
        const string HTTP_VER = "http1.0";

        public HttpHandler(ASCIIEncoding encoding)
        {
            this.encoding = encoding;
        }


        /* Create HTTP request with text/xml content
         * and send it to server
         */
        public string sendRequest(string host, string req, string content)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = req;
            request.ContentType = "text/xml";
            byte[] data = encoding.GetBytes(content);
            request.ContentLength = data.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Gets the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();
            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, encoding);

            Console.WriteLine("\r\nResponse stream received.");
            Char[] read = new Char[256];
            // Reads 256 characters at a time.     
            int count = readStream.Read(read, 0, 256);
            Console.WriteLine("HTML...\r\n");
            string str = null;
            while (count > 0)
            {
                // Dumps the 256 characters on a string and displays the string to the console.
                str = new String(read, 0, count);
                Console.Write(str);
                count = readStream.Read(read, 0, 256);
            }

            Console.WriteLine("\n\nthis is the str: {0}", str);
            // Releases the resources of the response.
            response.Close();
            // Releases the resources of the Stream.
            readStream.Close();

            return str;



        }


    }
}
