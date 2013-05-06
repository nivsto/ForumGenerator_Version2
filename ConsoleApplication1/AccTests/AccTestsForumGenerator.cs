using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using ForumGenerator_Version2_Server;
using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server.Communication;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;
using System.Threading;
using System.Net;
using System.Xml;
using System.IO;

namespace ConsoleApplication1
{
    public abstract class AccTestsForumGenerator : AccTest
    {
        protected BridgeForumGenerator bridge;


        public bool isForumExist(LinkedList<Forum> forums, string forumName)
        {
            bool res = false;
            foreach (Forum f in forums)
            {
                if (f.forumName.equals(forumName))
                {
                    res = true;
                    break;
                }
            }
            return res;
        }


        public string getUniqueForumName(LinkedList<Forum> forums)
        {
            Random random = new Random();
            string res = "forum" + random.Next(0, 9);
            
            while (isForumExist(forums, res))
            {
                res += random.Next(0, 9);
            }

            return res;
        }


        public bool isSubForumExist(Forum forum, string subForumTitle)
        {
            bool res = false;
            foreach (SubForum sf in forum.subForums)
            {
                if (sf.subForumTitle.equals(subForumTitle))
                {
                    res = true;
                    break;
                }
            }
            return res;
        }


        public string getUniqueSubForumTitle(Forum forum)
        {
            Random random = new Random();
            string res = "subForum" + random.Next(0, 9);

            while (isSubForumExist(forum, res))
            {
                res += random.Next(0, 9);
            }

            return res;
        }



        public bool isUserNameExist(Forum forum, string userName)
        {
            bool res = false;
            List<User> users = forum.members;

            foreach (User u in users)
            {
                if (u.userName.Equals(userName))
                {
                    res = true;
                    break;
                }
            }
            return res;
        }


        public string getUniqueUserName(Forum forum)
        {
            Random random = new Random();
            string res = "user" + random.Next(0, 9);

            while (isUserNameExist(forum, res))
            {
                res += random.Next(0, 9);
            }

            return res;
        }

       
    }
}
