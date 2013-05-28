using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys
{
    public class LogItem
    {
        public const string ACTION = "ACTION";
        public const string ERROR = "ERROR";

        internal string type;
        internal int logItemId;
        internal string description;
        internal DateTime logDate;

        public LogItem(string type, int errorItemId, string description)
        {
            this.type = type;
            this.logItemId = errorItemId;
            this.description = description;
            this.logDate = DateTime.Now;
        }


        virtual public string toString()
        {
            string l_type = type == ERROR ? ERROR+":" : "";
            string res = logItemDateFormat() + "\t" + l_type + " " + description;
            return res;
        }

        private string logItemDateFormat()
        {
            int month = logDate.Month,      day = logDate.Day;
            int h = logDate.Hour,   m = logDate.Minute,     s = logDate.Second;

            return day + "-" + month + "|" + h + ":" + m + ":" + s;
        }
    }
}
