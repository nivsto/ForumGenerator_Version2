using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys.Exceptions
{
    class UnauthorizedUserException : Exception
    {
        public UnauthorizedUserException() : base() { }
        public UnauthorizedUserException(string msg)
            : base(msg)
        {

        }
        
    }
}
