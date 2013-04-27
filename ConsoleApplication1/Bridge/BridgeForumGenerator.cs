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
    public interface BridgeForumGenerator
    {
        Tuple<string, string> adminLogin(string usrName, string usrPswd);
        Tuple<string, string> adminLogout();
        Tuple<string, string> login(int forumID, string usrName, string usrPswd);
        Tuple<string, string> logout(int forumID, int usrID);
        Tuple<string, string> register(int forumId, string userName, string password, string email, string signature);

        Tuple<string, string> createNewForum(string userName, string password,
                                             string forumName, string adminUserName, string adminPassword);
        Tuple<string, string> createNewSubForum(string userName, string password, int forumId, string subForumTitle);

        Tuple<string, string> createNewDiscussion(string userName, string password,
                                                                int forumId, int subForumId, string title, string content);
        Tuple<string, string> createNewComment(string userName, string password, int forumId,
                                                          int subForumId, int discussionId, string content);

        Tuple<bool, string, string[], string[,]> getForums();
        Tuple<bool, string, string[], string[,]> getSubForums(int forumId);
        Tuple<bool, string, string[], string[,]> getThreads(int forumId, int subForumId);
        Tuple<bool, string, string[], string[,]> getComments(int forumId, int subForumId, int discussionId);
    }
}
