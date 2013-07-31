using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using ForumGenerator_Version2_Server.Sys;
//using ForumGenerator_Version2_Server.Users;
//using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Client.Communication;
using ForumGenerator_Client.Dialogs;
using ForumGenerator_Client.ServiceReference1;

namespace ConsoleApplication1.AccTests
{
    public class RealClientImpl : BridgeForumGenerator
    {

        //const string ADMIN = ForumGenerator.ADMIN;
        const string ADMIN = "admin";

        public Communicator forumGen;


        public RealClientImpl(Communicator forumGen)
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
        public User login(int forumID, string usrName, string usrPswd)
        {
            return this.forumGen.login(forumID, usrName, usrPswd);
        }

        public bool logout(int forumId, string userName, string password)
        {
            return this.forumGen.logout(forumId, userName, password);
        }

        public User register(int forumId, string userName, string password, string email, string signature)
        {
            return this.forumGen.register(forumId, userName, password, email, signature);
        }

        public Forum createNewForum(string superUserName, string superUserpassword, string forumName, string mngrUserName, string mngrPassword)
        {
            return this.forumGen.createNewForum(superUserName, superUserpassword, forumName, mngrUserName, mngrPassword);
        }

        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            return this.forumGen.createNewSubForum(userName, password, forumId, subForumTitle);
        }

        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            return this.forumGen.createNewDiscussion(userName, password, forumId, subForumId, title, content);
        }

        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            return this.forumGen.createNewComment(userName, password, forumId, subForumId, discussionId, content);
        }

        public List<Forum> getForums()
        {
            return this.forumGen.getForums().ToList();
        }

        public List<SubForum> getSubForums(int forumId)
        {
            return this.forumGen.getSubForums(forumId).ToList();
        }

        public List<Discussion> getDiscussions(int forumId, int subForumId)
        {
            return this.forumGen.getDiscussions(forumId, subForumId).ToList();
        }

        public List<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            return this.forumGen.getComments(forumId, subForumId, discussionId).ToList();
        }


        public List<User> getUsers(int forumId)
        {
            return this.forumGen.getUsers(forumId).ToList();
        }

        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            return this.forumGen.changeAdmin(userName, password, forumId, newAdminUserId);
        }

        public bool addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            return this.forumGen.addModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
        }

        public bool removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            return this.forumGen.removeModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
        }

        public bool removeSubForum(int forumId, int subForumId, string userName, string password)
        {
            return this.removeSubForum(forumId, subForumId, userName, password);
        }


        public bool deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd)
        {
            return this.forumGen.deleteDiscussion(forumId, subForumId, discussionId, userName, pswd);
        }

        public void editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent)
        {
           this.forumGen.editDiscussion(forumId, subForumId, discussionId, userName, pswd, newContent);
        }

        public int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName)
        {
            return this.forumGen.getNumOfCommentsSingleUser(reqUserName, reqPswd, forumId, userName);
        }

        public int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId)
        {
            return this.forumGen.getNumOfCommentsSubForum(userName, pswd, forumId, subForumId);
        }

        public List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName)
        {
            return this.forumGen.getResponsersForSingleUser(reqUserName, reqPswd, forumId, memberUserName).ToList();
        }

        public List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            return this.forumGen.getMutualUsers(userName, password, forumId1, forumId2).ToList(); 
        }
        public List<User> getModerators(int forumId, int subForumId)
        {
            return this.forumGen.getModerators(forumId, subForumId).ToList();
        }

        public void reset()
        {
            
        }
      
    }

}
