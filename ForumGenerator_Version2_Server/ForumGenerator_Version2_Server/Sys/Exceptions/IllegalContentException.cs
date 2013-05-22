using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys.Exceptions
{
    class IllegalContentException : Exception
    {

        public IllegalContentException()
            : base()
        {

        }


        public IllegalContentException(string msg)
            : base(msg)
        {

        }

    }
}
