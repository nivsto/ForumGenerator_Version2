using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using ForumGenerator_Version2_Server;
using ForumGenerator_Version2_Server.Sys;
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
        User login(int forumId, string userName, string password);
        bool logout(int forumId, string userName, string password);
        SuperUser superUserLogin(string userName, string password);
        bool superUserLogout(string userName, string password);
        User register(int forumId, string userName, string password, string email, string signature);
        List<Forum> getForums();
        List<SubForum> getSubForums(int forumId);
        List<Discussion> getDiscussions(int forumId, int subForumId);
        List<Comment> getComments(int forumId, int subForumId, int discussionId);
        List<User> getUsers(int forumId);
        Forum createNewForum(string userName, string password, string forumName, string adminUserName,
                            string adminPassword, ForumGenerator_Version2_Server.ForumData.Forum.RegPolicy registrationPolicy);
        SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle);
        Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content);
        Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content);
        User changeAdmin(string userName, string password, int forumId, int newAdminUserId);
        // added in version 3:
        bool addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd, ForumGenerator_Version2_Server.Users.Moderator.modLevel level);
        Boolean removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd);
        Boolean deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd);
        Discussion editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent);
        int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName);
        int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId);
        List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName);
        List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2);
        List<Moderator> getModerators(int forumId, int subForumId);
        bool removeSubForum(int forumId, int subForumId, string userName, string password);
        void reset();
       

    }
}
