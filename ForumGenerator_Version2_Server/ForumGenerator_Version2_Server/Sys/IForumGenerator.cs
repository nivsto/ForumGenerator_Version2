using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys
{
    public interface IForumGenerator
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

        Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword);

        SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle);

        Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content);

        Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content);

        User changeAdmin(string userName, string password, int forumId, int newAdminUserId);

        // added in version 3:

        Boolean addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd);

        Boolean removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd);

        Boolean deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd);

        Discussion editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent);

        int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName);

        int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId);

        List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName);

        List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2);

        List<User> getModerators(int forumId, int subForumId);

        int getUserType(int forumId, string userName);

        int getUserType(int forumId, int subForumId, string userName);

        bool removeSubForum(int forumId, int subForumId, string userName, string password);

        void reset();       

    }
}
