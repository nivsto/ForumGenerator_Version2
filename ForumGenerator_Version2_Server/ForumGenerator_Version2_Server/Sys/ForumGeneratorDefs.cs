using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys
{
    /**
     * General definitions for the system ForumGenerator.
     */
    public class ForumGeneratorDefs
    {
        // Errors defs
        public const string FORUM_NF = "Forum not found";
        public const string SUBFORUM_NF = "SubFurom not found";
        public const string DISCUSSION_NF = "Discussion not found";
        public const string COMMENT_NF = "Comment not found";
        public const string USER_NF = "Wrong user name or password";

        public const string UNAUTH_SUPERUSER = "Unauthorized superUser";
        public const string UNAUTH_USER = "Unauthorized user";
        public const string UNAUTH_OP = "Unauthorized operation";

        public const string WRONG_USR_PSWD = "Wrong user name or password";
        public const string INVALID_EMAIL = "Invalid Email address";
        public const string ILL_CONTENT = "Illegal content";
        public const string IRELEVANT_CONTENT = "Content is not relevant to this subForum";
        

        public const string ALREADY_IN = "User is already logged in";
        public const string ALREADY_OUT = "User is already logged out";
        public const string EXIST_FNAME = "Forum name is already exist";
        public const string EXIST_TITLE = "Title is already exist";
        public const string EXIST_USERNAME = "UserName is already exist";

        public const string EXIST_ADMIN = "User is already an admin";
        public const string EXIST_MODERATOR = "User is already a moderator";

        public const string F_ADMIN_S_MOD = "Forum admin must be a moderator";
        

        public const string INTERNAL_ERR = "Internal error";
        public const string UNKNOWN_ERR = "Unknwon error";

        


    }
}
