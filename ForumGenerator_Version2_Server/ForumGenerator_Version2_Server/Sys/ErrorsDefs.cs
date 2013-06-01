using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys
{
    static class ErrorsDefs
    {
        
        public enum err_code
        {
            ILLEGAL_CONTENT = 0,
            UNAUTHO_USER,
            UNAUTHO_OP,
            DATA_NF,
            FORUM_NF,
            SUB_FORUM_NF,
            DISCUSSION_NF,
            COMMENT_NF,
            USER_NF,
            GENERAL_ERROR
        }

    }
}
