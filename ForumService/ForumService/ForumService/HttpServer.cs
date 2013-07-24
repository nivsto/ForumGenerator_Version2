﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Sys;
using System.ServiceModel;

namespace ForumService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class HttpServer : IForumService
    {
        private ForumGenerator _forumGen;

        public HttpServer()
        {
            _forumGen = new ForumGenerator("admin", "admin");
        }

        public User login(int forumId, string userName, string password)
        {
            User res = _forumGen.login(forumId, userName, password);
            return res;
        }

        public bool logout(int forumId, string userName, string password)
        {
            bool res = _forumGen.logout(forumId, userName, password);
            return res;
        }

        public SuperUser superUserLogin(string userName, string password)
        {
            SuperUser res = _forumGen.superUserLogin(userName, password);
            return res;
        }

        public bool superUserLogout(string userName, string password)
        {
            bool res = _forumGen.superUserLogout(userName, password);
            return res;
        }

        public User register(int forumId, string userName, string password, string email, string signature)
        {
            User res = _forumGen.register(forumId, userName, password, email, signature);
            return res;
        }

        public List<Forum> getForums()
        {
            List<Forum> resList = _forumGen.getForums();
            return resList;
        }

        public List<SubForum> getSubForums(int forumId)
        {
            List<SubForum> resList = _forumGen.getSubForums(forumId);
            return resList;
        }

        public List<Discussion> getDiscussions(int forumId, int subForumId)
        {
            List<Discussion> resList = _forumGen.getDiscussions(forumId, subForumId);
            return resList;
        }

        public List<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            List<Comment> resList = _forumGen.getComments(forumId, subForumId, discussionId);
            return resList;
        }

        public List<User> getUsers(int forumId)
        {
            List<User> resList = _forumGen.getUsers(forumId);
            return resList;
        }

        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            Forum res = _forumGen.createNewForum(userName, password, forumName, adminUserName, adminPassword);
            return res;
        }

        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            SubForum res = _forumGen.createNewSubForum(userName, password, forumId, subForumTitle);
            return res;
        }

        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            Discussion res = _forumGen.createNewDiscussion(userName, password, forumId, subForumId, title, content);
            return res;
        }

        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            Comment res = _forumGen.createNewComment(userName, password, forumId, subForumId, discussionId, content);
            return res;
        }

        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            User res = _forumGen.changeAdmin(userName, password, forumId, newAdminUserId);
            return res;
        }

        public Boolean addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            Boolean res = _forumGen.addModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
            return res;
        }

        public Boolean removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            Boolean res = _forumGen.removeModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
            return res;
        }

        public Boolean deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd)
        {
            Boolean res = _forumGen.deleteDiscussion(forumId, subForumId, discussionId, userName, pswd);
            return res;
        }

        public Discussion editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent)
        {
            Discussion d = _forumGen.editDiscussion(forumId, subForumId, discussionId, userName, pswd, newContent);
            return d;
        }

        public int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName)
        {
            int res = _forumGen.getNumOfCommentsSingleUser(reqUserName, reqPswd, forumId, userName);
            return res;
        }

        public int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId)
        {
            int res = _forumGen.getNumOfCommentsSubForum(userName, pswd, forumId, subForumId);
            return res;
        }

        public List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName)
        {
            List<User> resList = _forumGen.getResponsersForSingleUser(reqUserName, reqPswd, forumId, memberUserName);
            return resList;
        }

        public List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            List<User> resList = _forumGen.getMutualUsers(userName, password, forumId1, forumId2);
            return resList;
        }

        public int getUserType(int forumId, string userName)
        {
            int res = _forumGen.getUserType(forumId, userName);
            return res;
        }

        public int getUserTypeSubForum(int forumId, int subForumId, string userName)
        {
            int res = _forumGen.getUserType(forumId, subForumId, userName);
            return res;
        }


    }
}
