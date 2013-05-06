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
        SuperUser superUserLogin(string usrName, string usrPswd);
        bool superUserLogout();
        User login(int forumID, string usrName, string usrPswd);
        bool logout(int forumID, int usrID);
        User register(int forumId, string userName, string password, string email, string signature);

        Forum createNewForum(string superUserName, string superUserpassword,
                                             string forumName, string mngrUserName, string mngrPassword);
        SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle);

        Discussion createNewDiscussion(string userName, string password,
                                                                int forumId, int subForumId, string title, string content);
        Comment createNewComment(string userName, string password, int forumId,
                                                          int subForumId, int discussionId, string content);

        LinkedList<Forum> getForums();
        LinkedList<SubForum> getSubForums(int forumId);
        LinkedList<Discussion> getDiscussions(int forumId, int subForumId);
        LinkedList<Comment> getComments(int forumId, int subForumId, int discussionId);
    }
}
