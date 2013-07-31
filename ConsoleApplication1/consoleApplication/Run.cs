using System;
using System.IO;
using System.Windows.Forms;
using ConsoleApplication1.AccTests;

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
