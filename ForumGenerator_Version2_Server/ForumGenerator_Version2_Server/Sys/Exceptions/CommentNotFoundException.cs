using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys.Exceptions
{
    class CommentNotFoundException :Exception
    {

        public CommentNotFoundException() 
            : base()
        {
            
        }


        public CommentNotFoundException(string msg)
            : base(msg)
        {

        }



        


    }
}
