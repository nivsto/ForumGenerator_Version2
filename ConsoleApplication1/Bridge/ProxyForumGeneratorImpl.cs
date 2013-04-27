using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    public class ProxyForumGeneratorImpl : BridgeForumGenerator
    {
        public Tuple<string, string> adminLogin(string usrName, string usrPswd)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string> adminLogout()
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string> login(int forumID, string usrName, string usrPswd)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string> logout(int forumID, int usrID)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string> register(int forumId, string userName, string password, string email, string signature)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string> createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string> createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string> createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string> createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, string, string[], string[,]> getForums()
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, string, string[], string[,]> getSubForums(int forumId)
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, string, string[], string[,]> getThreads(int forumId, int subForumId)
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, string, string[], string[,]> getComments(int forumId, int subForumId, int discussionId)
        {
            throw new NotImplementedException();
        }
    }
}
