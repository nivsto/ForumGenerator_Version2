using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ConsoleApplication1
{
    public class TestsLogger
    {
        System.IO.StreamWriter logFile;
        

        public TestsLogger(string fileName)
        {
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            this.logFile = new System.IO.StreamWriter(fileName);
            this.logFile.WriteLine(" -- T E S T S   L O G G E R --");
        }

        public void logAction(string description)
        {
            this.logFile.WriteLine(description);
        }

        public void logError(string description)
        {
            logFile.WriteLine(description);
        }

        public void closeFile()
        {
            this.logFile.Close();
        }

    }
}
