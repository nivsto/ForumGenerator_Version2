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

        bool isForumExist(LinkedList<Forum> forums, string forumName)
        {
            bool res = false;
            foreach (Forum f in forums)
            {
                if (f.name.equals(forumName))
                {
                    res = true;
                    break;
                }
            }
            return res;
        }


        bool isUserNameExist(Forum forum, string userName)
        {
            bool res = false;
            LinkedList<User> users = forum.users;

            foreach (User u in users)
            {
                if (u.userName.equals(userName))
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

    }
}
