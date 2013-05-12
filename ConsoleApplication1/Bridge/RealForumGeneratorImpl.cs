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

        private IForumGenerator forumGen;


        public RealForumGeneratorImpl(ForumGenerator forumGen)
        {
            Console.Write("Creating ForumGenerator...  ");
            this.forumGen = forumGen;
            Console.WriteLine("done");
        }        



        public SuperUser superUserLogin(string usrName, string usrPswd)
        {
            return this.forumGen.superUserLogin(usrName, usrPswd);
        }

        public bool superUserLogout(string usrName, string usrPswd)
        {
            return this.forumGen.superUserLogout(usrName, usrPswd);
        }

        // Asa
        User BridgeForumGenerator.login(int forumID, string usrName, string usrPswd)
        {
            return this.forumGen.login(forumID, usrName, usrPswd);
        }

        bool BridgeForumGenerator.logout(int forumID, int usrID)
        {
            return this.forumGen.logout(forumID, usrID);
        }

        User BridgeForumGenerator.register(int forumId, string userName, string password, string email, string signature)
        {
            return this.forumGen.register(forumId, userName, password, email, signature);
        }

        Forum BridgeForumGenerator.createNewForum(string superUserName, string superUserpassword, string forumName, string mngrUserName, string mngrPassword)
        {
            return this.forumGen.createNewForum(superUserName, superUserpassword, forumName, mngrUserName, mngrPassword);
        }

        SubForum BridgeForumGenerator.createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            return this.forumGen.createNewSubForum(userName, password, forumId, subForumTitle);
        }

        Discussion BridgeForumGenerator.createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            return this.forumGen.createNewDiscussion(userName, password, forumId, subForumId, title, content);
        }

        Comment BridgeForumGenerator.createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            return this.forumGen.createNewComment(userName, password, forumId, subForumId, discussionId, content);
        }

        List<Forum> BridgeForumGenerator.getForums()
        {
            return this.forumGen.getForums();
        }

        List<SubForum> BridgeForumGenerator.getSubForums(int forumId)
        {
            return this.forumGen.getSubForums(forumId);
        }

        List<Discussion> BridgeForumGenerator.getDiscussions(int forumId, int subForumId)
        {
            return this.forumGen.getDiscussions(forumId, subForumId);
        }

        List<Comment> BridgeForumGenerator.getComments(int forumId, int subForumId, int discussionId)
        {
            return this.forumGen.getComments(forumId, subForumId, discussionId);
        }
    }

}
