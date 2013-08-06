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

    /* ******************************************************************************************************
     *                                  F I L E S
     * ******************************************************************************************************/
        public const string BAD_WORDS_FILE = "badWords.txt";
        public const string STOP_WORDS_FILE = "DefaultStopWords.txt";

    /* ******************************************************************************************************
     *                                  U S E R S
     * ******************************************************************************************************/
        public const string SU_USERNAME = "admin";
        public const string SU_PSWD = "=:150";

    /* ******************************************************************************************************
     *                                  E R R O R S
     * ******************************************************************************************************/
        // Extra information - for internal use
        const string USE_ONLY_ASCII = "Try only English characters, digits and punctuation.";
        const string TRY_ANOTHER = "Try a different one.";

        // Errors defs
        public const string FORUM_NF = "Forum not found";
        public const string SUBFORUM_NF = "SubFurom not found";
        public const string DISCUSSION_NF = "Discussion not found";
        public const string COMMENT_NF = "Comment not found";
        public const string USER_NF = "Wrong user name or password";

        public const string UNAUTH_SUPERUSER = "Unauthorized SuperUser";
        public const string UNAUTH_USER = "Unauthorized user - you have no permissions to do this action";
        public const string UNAUTH_OP = "Unauthorized operation";

        public const string WRONG_USR_PSWD = "Wrong user name or password";
        public const string INVALID_EMAIL = "Invalid Email address";
        public const string ILL_CONTENT = "Illegal content. " + USE_ONLY_ASCII;
        public const string IRELEVANT_CONTENT = "Content is not relevant to this subForum.";
        public const string INACTIVE_USR = "User account is not activated";

        public const string INVALID_USERNAME = "Invalid userName. Try only English characters and digits.";
        public const string INVALID_SIGNATURE = "Invalid signature. " + USE_ONLY_ASCII;
        public const string INVALID_FORUM_NAME = "Invalid forum name. " + USE_ONLY_ASCII;
        public const string INVALID_SUBFORUM_TITLE = "Invalid subForum title. " + USE_ONLY_ASCII;
        public const string INVALID_DISC_SUBJECT = "Invalid subject. " + USE_ONLY_ASCII;

        public const string ALREADY_IN = "User is already logged in.";
        public const string ALREADY_OUT = "User is already logged out.";
        public const string EXIST_FNAME = "Forum name is already exist." + TRY_ANOTHER;
        public const string EXIST_TITLE = "Title is already exist." + TRY_ANOTHER;
        public const string EXIST_USERNAME = "UserName is already exist." + TRY_ANOTHER;
        
        public const string EXIST_ADMIN = "User is already an admin.";
        public const string EXIST_MODERATOR = "User is already a moderator.";
        public const string F_ADMIN_S_MOD = "Forum admin must be a moderator.";
        
        public const string INTERNAL_ERR = "Internal error.";
        public const string UNKNOWN_ERR = "Unknwon error.";


    /* ******************************************************************************************************
     *                                  I N P U T   D A T A
     * ******************************************************************************************************/

        // Input defs
        public const int INFINITE_LEN = 1048576;

        public const int MIN_USER_NAME_LEN = 0;
        public const int MAX_USER_NAME_LEN = 20;
        public const int MIN_PASSWORD_LEN = 0;
        public const int MAX_PASSWORD_LEN = 10;
        public const int MIN_FORUM_NAME_LEN = 0;
        public const int MAX_FORUM_NAME_LEN = 30;
        public const int MIN_SUBFORUM_TITLE_LEN = 0;
        public const int MAX_SUBFORUM_TITLE_LEN = 30;
        public const int MIN_DISCUSSION_TITLE_LEN = 0;
        public const int MAX_DISCUSSION_TITLE_LEN = 80;
        public const int MIN_DISCUSSION_CONTENT_LEN = 0;
        public const int MAX_DISCUSSION_CONTENT_LEN = 80;
        public const int MIN_COMMENT_CONTENT_LEN = 0;
        public const int MAX_COMMENT_CONTENT_LEN = 80;
        public const int MIN_MEMBER_SIGNATURE_LEN = 0;
        public const int MAX_MEMBER_SIGNATURE_LEN = 40;

    }
}
