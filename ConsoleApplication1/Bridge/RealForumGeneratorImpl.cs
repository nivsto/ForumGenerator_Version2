﻿using System;
using System.Collections.Generic;
using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;

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
 
        bool BridgeForumGenerator.logout(int forumId, string userName, string password)
        {
            return this.forumGen.logout(forumId, userName, password);
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


        public List<User> getUsers(int forumId)
        {
            return this.forumGen.getUsers(forumId);
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

        public Discussion editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent)
        {
            return this.forumGen.editDiscussion(forumId, subForumId, discussionId, userName, pswd, newContent);
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
            return this.forumGen.getResponsersForSingleUser(reqUserName, reqPswd, forumId, memberUserName);
        }

        public List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            return this.forumGen.getMutualUsers(userName, password, forumId1, forumId2);
        }
        public List<User> getModerators(int forumId, int subForumId)
        {
            return this.forumGen.getModerators(forumId, subForumId);
        }

        public void reset()
        {
            this.forumGen.reset();
        }
      
    }

}
