using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Xml;
using System.IO;


namespace ForumGenerator_Version2_Server.Sys
{
    public class Logger
    {
 
        public List<LogItem> events;
        StreamWriter outFile;
        internal int logFileID = 0;     // Used when creating a logFile name

        public Logger()
        {
            this.events = new List<LogItem>();
            this.events.Add(new LogItem(LogItem.ACTION, 0, "Forum Generator has been started"));
            outFile = null;
        }


        public Logger(string outFile)
        {
            this.events = new List<LogItem>();
            this.events.Add(new LogItem(LogItem.ACTION, 0, "Forum Generator has been started"));
            this.setOutputFileStream(outFile);
        }


        public void logAction(string description)
        {
            this.events.Add(new LogItem(LogItem.ACTION, this.events.Count(), description));
        }


        public void logError(string description)
        {
            this.events.Add(new LogItem(LogItem.ERROR, this.events.Count(), description));
        }
      

        public void setOutputFileStream(string fileName)
        {
            
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            this.outFile = new StreamWriter(fileName);
            this.outFile.AutoFlush = true;
            this.outFile.WriteLine(" -- " + fileName + " --");
        }


        private string getPath()
        {
            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(currentDir);
            string fullDirectory = directory.FullName;
          //  fullDirectory = fullDirectory.Substring(0, fullDirectory.Length - 10); // remove "/bin/Debug"
          //  string path = fullDirectory + "\\Logger";
            fullDirectory = fullDirectory.Replace('\\', '/');
            return fullDirectory;
        }


        public void collectLogs()
        {
            String logFileName = "LogEvents_" + logFileID + ".txt";
            ++logFileID;
            if (logFileID > 100)
            {
                logFileID = 0;
            }
            collectLogs(logFileName);
        }


        public void collectLogs(string logFileName)
        {
            Console.WriteLine("Creating a log file...");
            this.setOutputFileStream(logFileName);

            for (int i = 0; i < events.Count(); i++)
            {
                LogItem ev = events.ElementAt(i);
                this.outFile.WriteLine(ev.toString());
            }

            string path = this.getPath() + "/" + logFileName;
            Console.WriteLine("Log file created successfully as " + path);
            this.outFile.Close();

            this.events.Clear();
        }

    }
}
