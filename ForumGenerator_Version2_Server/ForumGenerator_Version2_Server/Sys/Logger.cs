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
        internal int eventID = 0;

        public Logger()
        {
            this.events = new List<LogItem>();
            this.events.Add(new LogItem(LogItem.ACTION, this.eventID, "Forum Generator has been started"));
            this.eventID++;
            try
            {
                String path;
                do
                {
                    path = getLogFileName();
                } while (File.Exists(path));
                setOutputFileStream(path);
                Console.WriteLine("Log file: " + path);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured during log file creation:\n" + e.Message);
            }
        }


        public Logger(string outFile)
        {
            this.events = new List<LogItem>();
            this.events.Add(new LogItem(LogItem.ACTION, 0, "Forum Generator has been started"));
            this.setOutputFileStream(outFile);
        }


        public void logAction(string description)
        {
            LogItem li = new LogItem(LogItem.ACTION, this.eventID, description);
            this.eventID++;
            this.outFile.WriteLine(li.toString());
           // this.events.Add(new LogItem(LogItem.ACTION, this.events.Count(), description));
        }


        public void logError(string description)
        {
            LogItem li = new LogItem(LogItem.ERROR, this.eventID, description);
            this.eventID++;
            this.outFile.WriteLine(li.toString());
          //  this.events.Add(new LogItem(LogItem.ERROR, this.events.Count(), description));
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


        public void closeFile()
        {
            this.outFile.Close();
        }

        private string getPath()
        {
            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(currentDir);
            string fullDirectory = directory.FullName;
            fullDirectory = fullDirectory.Replace('\\', '/');
            return fullDirectory;
        }


        private String getLogFileName()
        {
            String logFileName = "LogEvents_" + logFileID + ".txt";
            ++logFileID;
            if (logFileID > 100)
            {
                logFileID = 0;
            }
            return getPath() + logFileName;
        }

    }
}
