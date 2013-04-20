using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

/*
 * taken from http://www.codeproject.com/Articles/137979/Simple-HTTP-Server-in-C
 */
namespace ForumGenerator_Version2_Server.Communication
{
    public abstract class HttpServer
    {

        protected int port; 
        IPAddress ip_address; 
        TcpListener listener;
        bool is_active = true;

        /*
         * c'tor - if constructed with port only will create localhost ip address
         */
        public HttpServer(int port)
        {
            this.port = port;
            ip_address = IPAddress.Parse("127.0.0.1");
        }

        /*
         * c'tor - creates a server with port and new_ip
         */
        public HttpServer(int port, IPAddress new_ip)
        {
            this.port = port;
            this.ip_address = new_ip;
        }

        /*
         * enables the http server to listen on ip_address and port.
         * each new client receives a thread to work with. After allocating a thread for the new connection the server goes
         * back to listening on ip_address and port
         */
        public void listen()
        {
            listener = new TcpListener(ip_address, port); //creates a tcp accept socket
            listener.Start();
            while (is_active)
            {
                TcpClient client_socket = listener.AcceptTcpClient();
                HttpProcessor processor = new HttpProcessor(client_socket, this);
                Thread thread = new Thread(new ThreadStart(processor.process));
                thread.Start();
                Thread.Sleep(1);
            }
        }

        public abstract void handleGETRequest(HttpProcessor p);
        public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);
    }
}
