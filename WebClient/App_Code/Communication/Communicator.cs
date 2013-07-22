﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ForumGenerator.WebClient;
using System.ServiceModel;
using ServiceReference1;

namespace ForumGenerator.WebClient.Communication
{

    public class Communicator
    {

        ChannelFactory<IForumService> httpFactory;
        IForumService httpProxy;

        public Communicator()
        {
           // httpFactory = new ChannelFactory<IForumService>(new BasicHttpBinding(), new EndpointAddress("http://192.168.1.117:8888/methods"));
           httpFactory = new ChannelFactory<IForumService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8080/methods"));
           httpProxy = httpFactory.CreateChannel();

 //           httpFactory = new ChannelFactory<IForumService>(new BasicHttpBinding(), new EndpointAddress("http://192.168.1.117:8888/methods"));
            httpFactory = new ChannelFactory<IForumService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8080/methods"));
            httpProxy = httpFactory.CreateChannel();

        }


        public User login(int forumId, string userName, string password)
        {
            User ans = null;
            try
            {
                ans = httpProxy.login(forumId, userName, password);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public bool logout(int forumId, string userName, string password)
        {
            bool ans = false;
            try
            {
                ans = httpProxy.logout(forumId, userName, password);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public SuperUser superUserLogin(string userName, string password)
        {
            SuperUser suser = null;
            try
            {
                suser = httpProxy.superUserLogin(userName, password);
                return suser;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public bool superUserLogout(string userName, string password)
        {
            bool ans = false;
            try
            {
                ans = httpProxy.superUserLogout(userName, password);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public User register(int forumId, string userName, string password, string email, string signature)
        {
            User ans = null;
            try
            {
                ans = httpProxy.register(forumId, userName, password, email, signature);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public List<Forum> getForums()
        {
            List<Forum> ans = null;
            try
            {
                ans = httpProxy.getForums();
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public List<SubForum> getSubForums(int forumId)
        {
            List<SubForum> ans = null;
            try
            {
                ans = httpProxy.getSubForums(forumId);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public List<Discussion> getDiscussions(int forumId, int subForumId)
        {
            List<Discussion> ans = null;
            try
            {
                ans = httpProxy.getDiscussions(forumId, subForumId);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public List<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            List<Comment> ans = null;
            try
            {
                ans = httpProxy.getComments(forumId, subForumId, discussionId);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public List<User> getUsers(int forumId)
        {
            List<User> ans = null;
            try
            {
                ans = httpProxy.getUsers(forumId);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public List<User> getModerators(int forumId, int subForumId)
        {
            List<User> ans = null;
            try
            {
                ans = httpProxy.getModerators(forumId, subForumId);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }


        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            Forum ans = null;
            try
            {
                ans = httpProxy.createNewForum(userName, password, forumName, adminUserName, adminPassword);
                return ans;
            }
            catch (FaultException fe)
            {
                // TODO Identify error type and throw exception according to type.
                throw new Exception(fe.Message);
            }
        }

        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            SubForum ans = null;
            try
            {
                ans = httpProxy.createNewSubForum(userName, password, forumId, subForumTitle);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            Discussion ans = null;
            try
            {
                ans = httpProxy.createNewDiscussion(userName, password, forumId, subForumId, title, content);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            Comment ans = null;
            try
            {
                ans = httpProxy.createNewComment(userName, password, forumId, subForumId, discussionId, content);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            User ans = null;
            try
            {
                ans = changeAdmin(userName, password, forumId, newAdminUserId);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        // added in version 3:

        public Boolean addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            Boolean ans = false;
            try
            {
                ans = httpProxy.addModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
                throw fe;
            }
        }

        public Boolean removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            Boolean ans = false;
            try
            {
                ans = httpProxy.removeModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public Boolean deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd)
        {
            Boolean ans = false;
            try
            {
                ans = httpProxy.deleteDiscussion(forumId, subForumId, discussionId, userName, pswd);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public void editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent)
        {
            try
            {
                httpProxy.editDiscussion(forumId, subForumId, discussionId, userName, pswd, newContent);
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public List<User> getMutualForumMembers(string userName, string password, int forumId1, int forumId2)
        {
            List<User> ans = null;
            try
            {
                ans = httpProxy.getMutualUsers(userName, password, forumId1, forumId2);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName)
        {
            int ans = 0;
            try
            {
                ans = httpProxy.getNumOfCommentsSingleUser(reqUserName, reqPswd, forumId, userName);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId)
        {
            int ans = 0;
            try
            {
                ans = httpProxy.getNumOfCommentsSubForum(userName, pswd, forumId, subForumId);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName)
        {
            List<User> ans = null;
            try
            {
                ans = httpProxy.getResponsersForSingleUser(reqUserName, reqPswd, forumId, memberUserName);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            List<User> ans = null;
            try
            {
                ans = httpProxy.getMutualUsers(userName, password, forumId1, forumId2);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public int getUserType(int forumId, string userName)
        {
            int ans = 0;
            try
            {
                ans = httpProxy.getUserType(forumId, userName);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public int getUserType(int forumId, int subForumId, string userName)
        {
            int ans = 0;
            try
            {
                ans = httpProxy.getUserType(forumId, userName);
                return ans;
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }


        public void removeSubForum(int forumId, int subForumId, string userName, string password)
        {
            try
            {
                httpProxy.removeSubForum(forumId, subForumId, userName, password);
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public int countDiscussionsPerForum(int forumId)
        {
            try
            {
                return httpProxy.countDiscussionsPerForum(forumId);
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public int countSubForumsPerForum(int forumId)
        {
            try
            {
                return httpProxy.countSubForumsPerForum(forumId);
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public int countDiscussionsPerSubForum(int forumId, int subForumId)
        {
            try
            {
                return httpProxy.countDiscussionsPerSubForum(forumId, subForumId);
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

        public int countCommentsPerSubForum(int forumId, int subForumId)
        {
            try
            {
                return httpProxy.countCommentsPerSubForum(forumId, subForumId);
            }
            catch (FaultException fe)
            {
                throw new Exception(fe.Message);
            }
        }

    }
}
