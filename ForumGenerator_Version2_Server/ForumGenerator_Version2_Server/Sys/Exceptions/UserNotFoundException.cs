using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys.Exceptions
{
    class UserNotFoundException : Exception
    {

        public UserNotFoundException() : base() { }
        public UserNotFoundException(string msg)
            : base(msg)
        {

        }

    }
}
