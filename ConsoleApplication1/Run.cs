using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Threading.Tasks;
using ForumGenerator_Version2_Server;
using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;
using System.Threading;
using System.Net;
using System.Xml;
using ConsoleApplication1.AccTests;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    class Run
    {

         static void Main(string[] args)
        {
       
            string logFileName = getPath() + "/TestForumGenerator.Log.txt";
            //TestForumGenerator tests = new TestForumGenerator(new ForumGenerator("admin", "admin",true), logFileName);
            //tests.runTests();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new testGui(logFileName));
            //Console.ReadKey();

        }

         static string getPath()
         {
             string currentDir = Environment.CurrentDirectory;
             DirectoryInfo directory = new DirectoryInfo(currentDir);
             string fullDirectory = directory.FullName;
             fullDirectory = fullDirectory.Substring(0, fullDirectory.Length - 10); // remove "/bin/Debug"
             string path = fullDirectory + "\\Logger";
             path = path.Replace('\\', '/');
             return path;
         }

    }// class run
}
