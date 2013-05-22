﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys.Exceptions
{
    class ForumNotFoundException : Exception
    {
        public ForumNotFoundException()
            : base()
        {

        }


        public ForumNotFoundException(string msg)
            : base(msg)
        {

        }

    }
}
