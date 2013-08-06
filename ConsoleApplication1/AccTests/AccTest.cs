using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml;

namespace ConsoleApplication1
{
    public abstract class AccTest
    {

        protected TestsLogger testsLogger;
        protected bool passed = true;

        public abstract void runTests();


        public void AssertTrue(bool obj)
        {
            if (! obj)
                throw new Exception("failedAssertTrue");
        }


        public void AssertFalse(bool obj)
        {
            if (obj)
                throw new Exception("failedAssertFalse");
        }

        public void AssertEquals(Object o1, Object o2)
        {
            if (! o1.Equals(o2))
                throw new Exception("failedAssertEquals");
        }

        /* Checks if object is not null */
        public void AssertExist(Object obj)
        {
            if (obj==null)
                throw new Exception("failedAssertExist");
        }


        public void failMsg(int testNum)
        {
            testsLogger.logError(testNum);
        }

        public void failMsg(string testDesc)
        {
            testsLogger.logError(testDesc);
        }

        public void test(Func<int> methodName)
        {
            testsLogger.logMethodTest(methodName.Method.Name);
            int testNum = methodName();
            testsLogger.logMethodTestResults(methodName.Method.Name, testNum);
        }

        /*
         * Prints to log file "success" or "fail", according to fully pass all tests.
         * In case of success, a message "P A S S" is printed to console.
         */
        public void sumTests()
        {
            if (this.passed)
            {
                testsLogger.logAction("\nSUCCESS\n");
                Console.WriteLine("\n\n********* P A S S E D **********\n\n");
            }
            else
            {
                testsLogger.logAction("\nFAIL\n");
            }
            testsLogger.closeFile();
        }

       
    
    }

    
}


