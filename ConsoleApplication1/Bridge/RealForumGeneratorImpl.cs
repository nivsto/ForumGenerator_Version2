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
    public class RealForumGeneratorImpl : BridgeForumGenerator
    {

        //const string ADMIN = ForumGenerator.ADMIN;
        const string ADMIN = "admin";

        private ForumGenerator forumGen;


        public RealForumGeneratorImpl(ForumGenerator forumGen)
        {
            Console.Write("Creating ForumGenerator...  ");
            this.forumGen = forumGen;
            Console.WriteLine("done");
        }


        public Tuple<string, string> login(int forumID, string usrName, string usrPswd)
        {
            return this.forumGen.login(forumID, usrName, usrPswd);
        }


        public Tuple<string, string> adminLogin(string usrName, string usrPswd)
        {
            return this.forumGen.adminLogin(usrName, usrPswd);
        }


        public Tuple<string, string> adminLogout()
        {
            return this.forumGen.adminLogout();
        }


        public Tuple<string, string> logout(int forumID, int usrID)
        {
            return this.forumGen.logout(forumID, usrID);
        }


        public Tuple<string, string> createNewForum(string userName, string password,
                                                    string forumName, string adminUserName, string adminPassword)
        {
            return this.forumGen.createNewForum(userName, password, forumName, adminUserName, adminPassword);
        }


        public Tuple<string, string> createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            return this.forumGen.createNewSubForum(userName, password, forumId, subForumTitle);
        }


        public Tuple<string, string> register(int forumId, string userName, string password, string email, string signature)
        {
            return this.forumGen.register(forumId, userName, password, email, signature);
        }


        public Tuple<string, string> createNewDiscussion(string userName, string password,
                                                                int forumId, int subForumId, string title, string content)
        {
            return this.forumGen.createNewDiscussion(userName, password, forumId, subForumId, title, content);
        }


        public Tuple<string, string> createNewComment(string userName, string password, int forumId,
                                                          int subForumId, int discussionId, string content)
        {
            return this.forumGen.createNewComment(userName, password, forumId, subForumId, discussionId, content);
        }


        public Tuple<bool, string, string[], string[,]> getForums()
        {
            return this.forumGen.getForums();
        }


        public Tuple<bool, string, string[], string[,]> getSubForums(int forumId)
        {
            return this.forumGen.getSubForums(forumId);
        }


        public Tuple<bool, string, string[], string[,]> getThreads(int forumId, int subForumId)
        {
            return this.forumGen.getDiscussions(forumId, subForumId);
        }


        public Tuple<bool, string, string[], string[,]> getComments(int forumId, int subForumId, int discussionId)
        {
            return this.forumGen.getComments(forumId, subForumId, discussionId);
        }
    }

}
