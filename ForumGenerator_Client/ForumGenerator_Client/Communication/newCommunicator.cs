using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Client.Objects;

namespace ForumGenerator_Client.Communication
{
    class newCommunicator
    {
        public User login(int forumId, string userName, string password)
        {
            return null;
        }

        public bool logout(int forumId, int userId)
        {
            return false;
        }

        public SuperUser superUserLogin(string userName, string password)
        {
            return null;
        }

        public bool superUserLogout(string userName, string password)
        {
            return false;
        }

        public User register(int forumId, string userName, string password, string email, string signature)
        {
            return null;
        }

        public List<Forum> getForums()
        {
            return null;
        }

        public List<SubForum> getSubForums(int forumId)
        {
            return null;
        }

        public List<Discussion> getDiscussions(int forumId, int subForumId)
        {
            return null;
        }

        public List<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            return null;
        }

        public List<User> getUsers(int forumId)
        {
            return null;
        }

        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            return null;
        }

        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            return null;
        }

        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            return null;
        }

        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            return null;
        }

        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            return null;
        }

        // added in version 3:

        public Boolean addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            return false;
        }

        public Boolean removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            return false;
        }

        public Boolean deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd)
        {
            return false;
        }

        public Boolean editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent)
        {
            return false;
        }

        public List<User> getMutualForumMembers(string userName, string pswd)
        {
            return null;
        }

        public int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName)
        {
            return 123;
        }

        public int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId)
        {
            return 123;
        }

        public List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName)
        {
            return null;
        }

        public List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            return null;
        }

        public int getUserType(int forumId, string userName)
        {
            return 1;
        }

        public int getUserType(int forumId, int subForumId, string userName)
        {
            return 1;
        }

    }
}
