using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys
{
    class Logger
    {
        List<LogItem> actionLog;
        List<LogItem> errorLog;

        public Logger()
        {
            this.actionLog = new List<LogItem>();
            this.errorLog = new List<LogItem>();
            this.actionLog.Add(new LogItem(0, "Forum Generator has been started"));
        }

        public void logAction(string description)
        {
            this.actionLog.Add(new LogItem(this.actionLog.Count(), description));
        }

        public void logError(string description)
        {
            this.errorLog.Add(new LogItem(this.errorLog.Count(), description));
        }
    }
}
