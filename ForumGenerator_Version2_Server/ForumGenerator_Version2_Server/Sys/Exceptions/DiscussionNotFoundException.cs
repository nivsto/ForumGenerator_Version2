﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys.Exceptions
{
    class DiscussionNotFoundException : Exception
    {

        public DiscussionNotFoundException()
            : base()
        {

        }


        public DiscussionNotFoundException(string msg)
            : base(msg)
        {

        }

    }
}
