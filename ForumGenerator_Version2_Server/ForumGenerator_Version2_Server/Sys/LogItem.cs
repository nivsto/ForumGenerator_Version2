using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys
{
    class LogItem
    {
        internal int logItemId;
        internal string description;
        internal DateTime logDate;

        public LogItem(int errorItemId, string description)
        {
            this.logItemId = errorItemId;
            this.description = description;
            this.logDate = DateTime.Now;
        }
    }
}
