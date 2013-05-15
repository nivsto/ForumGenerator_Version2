using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ForumGenerator_Version2_Server.Sys
{
    public class Logger
    {
        public List<LogItem> actionLog;
        public List<LogItem> errorLog;
        StreamWriter outFile;

        public Logger()
        {
            this.actionLog = new List<LogItem>();
            this.errorLog = new List<LogItem>();
            this.actionLog.Add(new LogItem(0, "Forum Generator has been started"));
            outFile = null;
        }


        public Logger(string outFile)
        {
            this.actionLog = new List<LogItem>();
            this.errorLog = new List<LogItem>();
            this.actionLog.Add(new LogItem(0, "Forum Generator has been started"));
            this.setOutputFileStream(outFile);
        }


        public void logAction(string description)
        {
            this.actionLog.Add(new LogItem(this.actionLog.Count(), description));
        }

        public void logError(string description)
        {
            this.errorLog.Add(new LogItem(this.errorLog.Count(), description));
        }


        public void setOutputFileStream(string fileName)
        {
            string path = this.getPath() + "/" + fileName;
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
            fullDirectory = fullDirectory.Substring(0, fullDirectory.Length - 10); // remove "/bin/Debug"
            string path = fullDirectory + "\\Logger";
            path = path.Replace('\\', '/');
            return path;
        }
    }
}
