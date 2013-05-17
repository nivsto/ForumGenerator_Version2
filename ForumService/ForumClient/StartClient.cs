using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ForumClient.ServiceReference1;

namespace ForumClient
{
    class StartClient
    {
        static void Main(string[] args)
        {
            //this channel creates basic http binding with http://localhost:8888/methods/
            //ChannelFactory<IForumService> httpFactory = new ChannelFactory<IForumService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8888/methods"));
            ChannelFactory<IForumService> httpFactory = new ChannelFactory<IForumService>(new BasicHttpBinding(), new EndpointAddress("http://10.0.0.7:8888/methods"));

            IForumService httpProxy = httpFactory.CreateChannel();

            while (true)
            {
                SuperUser sres = httpProxy.superUserLogin("admin", "admin");
                Console.WriteLine("super user name: {0}, password: {1}, isloggedin?: {2}", sres.userName, sres.password, sres.isLoggedIn);
                Forum fres = httpProxy.createNewForum("admin", "admin", "forumG", "man", "manman");
                Console.WriteLine("Forum received");
                User suser = httpProxy.login(fres.forumId, fres.admin.userName, fres.admin.password);
                Console.WriteLine("suser received");
            }
        }
    }
}
