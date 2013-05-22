using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys.Exceptions
{
    class SubForumNotFoundException : Exception
    {

        public SubForumNotFoundException()
            : base()
        {

        }


        public SubForumNotFoundException(string msg)
            : base(msg)
        {

        }

    }
}
