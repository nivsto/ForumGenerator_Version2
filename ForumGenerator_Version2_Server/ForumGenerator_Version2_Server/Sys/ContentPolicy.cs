using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Collections;
using System.IO;
using ForumGenerator_Version2_Server.Sys.Exceptions;

namespace ForumGenerator_Version2_Server.Sys
{
    public class ContentPolicy
    {
        private Tuple<int, int, string>[] ranges;
        private HashSet<string> badWords;

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

        public ContentPolicy()
        {
            ranges = new Tuple<int, int, string>[8];
            badWords = new HashSet<string>();
            init();
        }

        public void init()
        { 
            ranges[(int)cType.USER_NAME] = new Tuple<int, int, string>
                (ForumGeneratorDefs.MIN_USER_NAME_LEN, ForumGeneratorDefs.MAX_USER_NAME_LEN, LETTERS_NUMS);            //  USER_NAME
            ranges[(int)cType.PASSWORD] = new Tuple<int, int, string>                                               
                (ForumGeneratorDefs.MIN_PASSWORD_LEN, ForumGeneratorDefs.MAX_PASSWORD_LEN, ASCII);                     //  PASSWORD
            ranges[(int)cType.FORUM_NAME] = new Tuple<int, int, string>                                             
                (ForumGeneratorDefs.MIN_FORUM_NAME_LEN, ForumGeneratorDefs.MAX_FORUM_NAME_LEN, ASCII);                 //  FORUM_NAME
            ranges[(int)cType.SUBFORUM_TITLE] = new Tuple<int, int, string>                                         
                (ForumGeneratorDefs.MIN_SUBFORUM_TITLE_LEN, ForumGeneratorDefs.MAX_SUBFORUM_TITLE_LEN, ASCII);         //  SUBFORUM_TITLE
            ranges[(int)cType.DISCUSSION_TITLE] = new Tuple<int, int, string>                                       
                (ForumGeneratorDefs.MIN_DISCUSSION_TITLE_LEN, ForumGeneratorDefs.MAX_DISCUSSION_TITLE_LEN, ASCII);     //  DISCUSSION_TITLE
            ranges[(int)cType.DISCUSSION_CONTENT] = new Tuple<int, int, string>
                (ForumGeneratorDefs.MIN_DISCUSSION_CONTENT_LEN, ForumGeneratorDefs.MAX_DISCUSSION_CONTENT_LEN, ASCII); //  DISCUSSION_CONTENT
            ranges[(int)cType.COMMENT_CONTENT] = new Tuple<int, int, string>
                (ForumGeneratorDefs.MIN_COMMENT_CONTENT_LEN, ForumGeneratorDefs.MAX_COMMENT_CONTENT_LEN, ASCII);       //  COMMENT_CONTENT
            ranges[(int)cType.MEMBER_SIGNATURE] = new Tuple<int, int, string>
                (ForumGeneratorDefs.MIN_MEMBER_SIGNATURE_LEN, ForumGeneratorDefs.MAX_MEMBER_SIGNATURE_LEN, ASCII);     //  MEMBER_SIGNATURE 
            
            // Init list of bad words from file
            string[] lines = File.ReadAllLines(ForumGeneratorDefs.BAD_WORDS_FILE);
            foreach (string line in lines) { badWords.Add(line); };
        }


        // Regular expr of contents
        const string LETTERS_ONLY = "a-zA-Z";
        const string LETTERS_NUMS = "a-zA-Z0-9";
        const string LETTERS_NUMS_SIGNS = LETTERS_NUMS + ",:.?+()!_'@#(){}+"; // not checking '-'
        const string ASCII = "\x00-\x7F";
        

        // Checks if the given data contains only legal chars and if its length is in the
        // given range, according to forum policy.
        public void checkLegalContent(cType contentType, string content)
        {
            if (content == null)
                return;
            int minLen = ranges[(int)contentType].Item1;
            int maxLen = ranges[(int)contentType].Item2;
            string goodChars = ranges[(int)contentType].Item3;

            if (!isLegalContent(content, minLen, maxLen, goodChars))
            {
                string err_msg = "";
                switch (contentType)
                {
                    case (cType.USER_NAME):
                        err_msg = ForumGeneratorDefs.INVALID_USERNAME;
                        break;
                    case (cType.MEMBER_SIGNATURE):
                        err_msg = ForumGeneratorDefs.INVALID_SIGNATURE;
                        break;
                    case (cType.FORUM_NAME):
                        err_msg = ForumGeneratorDefs.INVALID_FORUM_NAME;
                        break;
                    case (cType.SUBFORUM_TITLE):
                        err_msg = ForumGeneratorDefs.INVALID_SUBFORUM_TITLE;
                        break;
                    case (cType.DISCUSSION_TITLE):
                        err_msg = ForumGeneratorDefs.INVALID_DISC_SUBJECT;
                        break;

                    default:
                        err_msg = ForumGeneratorDefs.ILL_CONTENT;
                        break;
                }

                throw new IllegalContentException(err_msg);
            }          
        }


        private bool isLegalContent(string content, int minLen, int maxLen, string regex_goodChars)
        {
            Regex RgxUrl = new Regex("[^" + regex_goodChars + "]");
            return (!RgxUrl.IsMatch(content) && content.Length >= minLen && content.Length <= maxLen);
        }


        public void checkLegalEmailFormat(string email)
        {
            if (email == null)
                return;
            if (email == "")
                return; // TODO remove
            try
            {
                MailAddress m = new MailAddress(email);
                return;
            }
            catch(Exception)
            {
                throw new IllegalContentException(ForumGeneratorDefs.INVALID_EMAIL);
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
