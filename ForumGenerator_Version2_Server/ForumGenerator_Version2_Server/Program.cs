using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumGenerator_Version2_Server.System;
using ForumGenerator_Version2_Server.Communication;
using System.Threading;

namespace ForumGenerator_Version2_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ForumGenerator forumGenerator = initProgram("admin", "admin"); // initialize the system with a super-user (username,password)
            startServer();  //starting the server and waiting for requests
        }

        private static ForumGenerator initProgram(string superUserName, string superUserPass)
        {
            return new ForumGenerator(superUserName, superUserPass);
        }


        public static void startServer()
        {
            HttpServer httpServer = new MyHttpServer(8080);
            Thread thread = new Thread(new ThreadStart(httpServer.listen));
            thread.Start();
        }

    }
}
