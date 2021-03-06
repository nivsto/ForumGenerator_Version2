﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ForumGenerator_Version2_Server;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;

namespace ForumService
{
    class StartHttpServer
    {
        //1 base address is used for basicHttp
        private const int _numOfBaseAddress = 1;

        static void Main(string[] args)
        {
            //this is the base address for communicating with the server
            Uri[] baseAddresses = new Uri[_numOfBaseAddress]{ new Uri("http://192.168.1.105:8888") };
            //Uri[] baseAddresses = new Uri[_numOfBaseAddress] { new Uri("http://10.0.0.7:8888") };

            HttpServer server = new HttpServer();

            using (ServiceHost host = new ServiceHost(server, baseAddresses))
            {
                host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
                //adding endpoint for all methods without endpoints
                host.AddServiceEndpoint(typeof(IForumService), new BasicHttpBinding(), "methods");
                ////adding web browser compatability
                //var endpoint = host.AddServiceEndpoint(typeof(BrowserService), new WebHttpBinding(), "");
                //endpoint.Behaviors.Add(new WebHttpBehavior { AutomaticFormatSelectionEnabled = true });
                //#TBD - add callback endpoint to host later

                host.Open();

                Console.WriteLine("Service is available. Press <ENTER> to exit");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
