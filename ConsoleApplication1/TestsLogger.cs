using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ConsoleApplication1
{
    public class TestsLogger
    {
        StreamWriter logFile;
        

        public TestsLogger(string fileName)
        {
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            this.logFile = new StreamWriter(fileName);
            this.logFile.AutoFlush = true;
            this.logFile.WriteLine(" -- T E S T S   L O G G E R --");
        }

        public void logMethodTest(string methodName)
        {
            this.logFile.WriteLine("test method: " + methodName);
        }

        public void logMethodTestResults(string methodName, int numOfTestsPassed)
        {
            this.logFile.WriteLine("test summary for " + methodName + ": " + numOfTestsPassed + " tests passed");
            this.logFile.WriteLine();
        }

        public void logTestsSection(string description)
        {
            this.logFile.WriteLine("*testing " + description + " functions*");
            this.logFile.WriteLine();
        }

        public void logAction(string description)
        {
            this.logFile.WriteLine(description);
        }

        public void logError(string description)
        {
            logFile.WriteLine("-- FAILED --  failed to " + description);
        }

        public void logError(int testNum)
        {
            logFile.WriteLine("-- FAILED --  failed test number " + testNum);
        }

        public void closeFile()
        {
            this.logFile.Close();
        }

    }
}
