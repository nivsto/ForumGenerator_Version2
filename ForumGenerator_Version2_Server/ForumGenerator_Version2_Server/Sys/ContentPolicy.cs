using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Collections;
using System.IO;


namespace ForumGenerator_Version2_Server.Sys
{
    public class ContentPolicy
    {
        private string badWordsFileName = "badWords.txt";
        const int MAX_LEN = 1048576;

        private Tuple<int, int>[] ranges = new Tuple<int, int>[8];
        private HashSet<string> badWords = new HashSet<string>();

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
        

        public void init()
        { 
                                                      /* minLen, maxLen */
            ranges[(int)cType.USER_NAME] = new Tuple<int, int>(1, 20);               //  USER_NAME
            ranges[(int)cType.PASSWORD] = new Tuple<int, int>(1, 20);                //  PASSWORD
            ranges[(int)cType.FORUM_NAME] = new Tuple<int, int>(0, 50);              //  FORUM_NAME
            ranges[(int)cType.SUBFORUM_TITLE] = new Tuple<int, int>(0, 80);          //  SUBFORUM_TITLE
            ranges[(int)cType.DISCUSSION_TITLE] = new Tuple<int, int>(0, 80);        //  DISCUSSION_TITLE
            ranges[(int)cType.DISCUSSION_CONTENT] = new Tuple<int, int>(0, MAX_LEN); //  DISCUSSION_CONTENT
            ranges[(int)cType.COMMENT_CONTENT] = new Tuple<int, int>(0, MAX_LEN);    //  COMMENT_CONTENT
            ranges[(int)cType.MEMBER_SIGNATURE] = new Tuple<int, int>(0, 20);        //  MEMBER_SIGNATURE 
            
            // Init list of bad words out of file
            string[] lines = File.ReadAllLines(badWordsFileName);
            foreach (string line in lines) { badWords.Add(line); };
        }


        // Regular expr of contents
        const string LETTERS_ONLY = "a-zA-Z";
        const string LETTERS_NUMS = "a-zA-Z0-9";
        const string LETTERS_NUMS_SIGNS = "a-zA-Z0-9,: /&|+-?!@#$%^*()><=_{}[]~"; // not checking '-'
        

        // Checks if the given data contains only legal chars and if its length is in the
        // given range, according to forum policy.
        public bool isLegalContent(cType contentType, string content)
        {
            if (content == null)
                return false;
            int minLen = ranges[(int)contentType].Item1;
            int maxLen = ranges[(int)contentType].Item2;

            return isLegalContent(content, minLen, maxLen);
        }


        private bool isLegalContent(string content, int minLen, int maxLen)
        {
            Regex RgxUrl = new Regex("[^" + LETTERS_NUMS_SIGNS + "]");
            return (!RgxUrl.IsMatch(content) && content.Length >= minLen && content.Length <= maxLen);
        }


        public bool isLegalEmailFormat(string email)
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


        // Replace any bad word with **** and returns the new string.
        public string censor(string text)
        {
            string res = "";
            const string CensoredText = "****";
            const string PatternTemplate = @"\b({0})(s?)\b";
            const RegexOptions Options = RegexOptions.IgnoreCase;

            IEnumerable<Regex> badWordMatchers = badWords.
                Select(x => new Regex(string.Format(PatternTemplate, x), Options));
            res = badWordMatchers.
                Aggregate(text, (current, matcher) => matcher.Replace(current, CensoredText));

            return res;
        }


    }
}
