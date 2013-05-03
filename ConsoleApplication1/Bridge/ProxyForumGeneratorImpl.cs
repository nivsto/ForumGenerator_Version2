using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    public class ProxyForumGeneratorImpl : BridgeForumGenerator
    {

        public User superUserLogin(string usrName, string usrPswd)
        {
            throw new NotImplementedException();
        }

        public bool superUserLogout()
        {
            throw new NotImplementedException();
        }

        public User login(int forumID, string usrName, string usrPswd)
        {
            throw new NotImplementedException();
        }

        public bool logout(int forumID, int usrID)
        {
            throw new NotImplementedException();
        }

        public User register(int forumId, string userName, string password, string email, string signature)
        {
            throw new NotImplementedException();
        }

        public ForumGenerator_Version2_Server.ForumData.Forum createNewForum(string superUserName, string superUserpassword, string forumName, string mngrUserName, string mngrPassword)
        {
            throw new NotImplementedException();
        }

        public ForumGenerator_Version2_Server.ForumData.SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            throw new NotImplementedException();
        }

        public ForumGenerator_Version2_Server.ForumData.Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            throw new NotImplementedException();
        }

        public ForumGenerator_Version2_Server.ForumData.Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            throw new NotImplementedException();
        }

        public LinkedList<ForumGenerator_Version2_Server.ForumData.Forum> getForums()
        {
            throw new NotImplementedException();
        }

        public LinkedList<ForumGenerator_Version2_Server.ForumData.SubForum> getSubForums(int forumId)
        {
            throw new NotImplementedException();
        }

        public LinkedList<ForumGenerator_Version2_Server.ForumData.Discussion> getThreads(int forumId, int subForumId)
        {
            throw new NotImplementedException();
        }

        public LinkedList<ForumGenerator_Version2_Server.ForumData.Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            throw new NotImplementedException();
        }
    }
}
