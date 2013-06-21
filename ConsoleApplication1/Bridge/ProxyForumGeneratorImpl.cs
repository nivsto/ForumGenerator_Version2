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

    public class ProxyForumGeneratorImpl : BridgeForumGenerator
    {

        public SuperUser superUserLogin(string usrName, string usrPswd)
        {
            throw new NotImplementedException();
        }

        public bool superUserLogout(string usrName, string usrPswd)
        {
            throw new NotImplementedException();
        }

        public User login(int forumID, string usrName, string usrPswd)
        {
            throw new NotImplementedException();
        }

        public bool logout(int forumId, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public User register(int forumId, string userName, string password, string email, string signature)
        {
            throw new NotImplementedException();
        }

        public Forum createNewForum(string superUserName, string superUserpassword, string forumName, string mngrUserName, string mngrPassword)
        {
            throw new NotImplementedException();
        }

        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            throw new NotImplementedException();
        }

        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            throw new NotImplementedException();
        }

        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            throw new NotImplementedException();
        }

        public LinkedList<Forum> getForums()
        {
            throw new NotImplementedException();
        }

        public LinkedList<SubForum> getSubForums(int forumId)
        {
            throw new NotImplementedException();
        }

        public LinkedList<Discussion> getDiscussions(int forumId, int subForumId)
        {
            throw new NotImplementedException();
        }

        public LinkedList<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            throw new NotImplementedException();
        }


        List<Forum> BridgeForumGenerator.getForums()
        {
            throw new NotImplementedException();
        }

        List<SubForum> BridgeForumGenerator.getSubForums(int forumId)
        {
            throw new NotImplementedException();
        }

        List<Discussion> BridgeForumGenerator.getDiscussions(int forumId, int subForumId)
        {
            throw new NotImplementedException();
        }

        List<Comment> BridgeForumGenerator.getComments(int forumId, int subForumId, int discussionId)
        {
            throw new NotImplementedException();
        }

        public List<User> getUsers(int forumId)
        {
            throw new NotImplementedException();
        }

        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            throw new NotImplementedException();
        }

        public bool addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            throw new NotImplementedException();
        }

        public bool removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            throw new NotImplementedException();
        }

        public bool removeSubForum(int forumId, int subForumId, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd)
        {
            throw new NotImplementedException();
        }

        public Discussion editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent)
        {
            throw new NotImplementedException();
        }

        public int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName)
        {
            throw new NotImplementedException();
        }

        public int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId)
        {
            throw new NotImplementedException();
        }

        public List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName)
        {
            throw new NotImplementedException();
        }

        public List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            throw new NotImplementedException();
        }

        public void reset()
        {
            throw new NotImplementedException();
        }

    }
}
