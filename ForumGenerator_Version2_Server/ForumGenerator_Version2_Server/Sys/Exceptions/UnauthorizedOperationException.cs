using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys.Exceptions
{
    class UnauthorizedOperationException : Exception
    {
        public UnauthorizedOperationException() : base() { }
        public UnauthorizedOperationException(string msg) : base(msg) { }
    }
}
