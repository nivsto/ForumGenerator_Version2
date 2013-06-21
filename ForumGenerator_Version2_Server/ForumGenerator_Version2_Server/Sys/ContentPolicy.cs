using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace ForumGenerator_Version2_Server.Sys
{
    public static class ContentPolicy
    {

        public enum cType
        {
            USER_NAME = 0,
            PASSWORD,
            FORUM_NAME,
            SUBFORUM_TITLE,
            DISCUSSION_TITLE,
            DISCUSSION_CONTENT,
            COMMENT_CONTENT,
            MEMBER_SIGNATURE
        }

        // For each cType, there is a minLength and maxLength.
        // Range for USER_NAME is in index 0,
        // Range for PASSWORD is in index 1, and so on.
        // -1 in Index[i,1] means there is no limitation on maxLength.
        private static int[,] ranges = new int[,]
        { 
       /* minLen, maxLen */
              {1, 20},      //  USER_NAME
              {1, 20},      //  PASSWORD
              {0, 50},      //  FORUM_NAME
              {0, 80},      //  SUBFORUM_TITLE
              {0, 80},      //  DISCUSSION_TITLE
              {0, -1},      //  DISCUSSION_CONTENT
              {0, -1},      //  COMMENT_CONTENT
              {0, 20}       //  MEMBER_SIGNATURE                          
        };


        // Regular expr of contents
        const string LETTERS_ONLY = "a-zA-Z";
        const string LETTERS_NUMS = "a-zA-Z0-9";
        const string LETTERS_NUMS_SIGNS = "a-zA-Z0-9,: /&|+-?!@#$%^*()><=_{}[]~"; // not checking '-'
        



        // Checks if the given data contains only legal chars and if its length is in the
        // given range, according to forum policy.
        public static bool isLegalContent(cType contentType, string content)
        {
            if (content == null)
                return false;

            int minLen = ranges[(int)contentType, 0];
            int maxLen = ranges[(int)contentType, 1];

            if (maxLen < 0)
                return isLegalContent(content, minLen);
            else
                return isLegalContent(content, minLen, maxLen);
        }


        private static bool isLegalContent(string content, int minLen)
        {
            Regex RgxUrl = new Regex("[^"+LETTERS_NUMS_SIGNS+"]");             
            return (!RgxUrl.IsMatch(content) && content.Length >= minLen);
        }


        private static bool isLegalContent(string content, int minLen, int maxLen)
        {
            Regex RgxUrl = new Regex("[^" + LETTERS_NUMS_SIGNS + "]");
            return (!RgxUrl.IsMatch(content) && content.Length >= minLen && content.Length <= maxLen);
        }


        public static bool isLegalEmailFormat(string email)
        {
            if (email == null)
                return false;
            if (email == "")
                return true; // TODO remove
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch(Exception)
            {
                return false;
            }

        }


    }
}
