using System;
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
            try
            {
                User res = _forumGen.login(forumId, userName, password);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }

        public bool logout(int forumId, string userName, string password)
        {
            try
            {
                bool res = _forumGen.logout(forumId, userName, password);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }

        public SuperUser superUserLogin(string userName, string password) 
        {
            try
            {
                SuperUser res = _forumGen.superUserLogin(userName, password);
                return res;
            }
            catch (Exception e)
            {
       //         _forumGen.collectLogs("doron.txt");
                throw new FaultException(e.Message);
            }
        }

        public bool superUserLogout(string userName, string password)
        {
            try
            {
                bool res = _forumGen.superUserLogout(userName, password);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }

        public User register(int forumId, string userName, string password, string email, string signature)
        {
            try
            {
                User res = _forumGen.register(forumId, userName, password, email, signature);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }

        public List<Forum> getForums()
        {
            try
            {
                List<Forum> resList = _forumGen.getForums();
                return resList;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }
  

        public List<SubForum> getSubForums(int forumId)
        {
            try
            {
                List<SubForum> resList = _forumGen.getSubForums(forumId);
                return resList;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public List<Discussion> getDiscussions(int forumId, int subForumId)
        {
            try
            {
                List<Discussion> resList = _forumGen.getDiscussions(forumId, subForumId);
                return resList;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public List<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            try
            {
                List<Comment> resList = _forumGen.getComments(forumId, subForumId, discussionId);
                return resList;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public List<User> getUsers(int forumId)
        {
            try
            {
                List<User> resList = _forumGen.getUsers(forumId);
                return resList;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            try
            {
                Forum res = _forumGen.createNewForum(userName, password, forumName, adminUserName, adminPassword);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            SubForum res = _forumGen.createNewSubForum(userName, password, forumId, subForumTitle);
            return res;
        }


        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            try
            {
                Discussion res = _forumGen.createNewDiscussion(userName, password, forumId, subForumId, title, content);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            try
            {
                Comment res = _forumGen.createNewComment(userName, password, forumId, subForumId, discussionId, content);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            try
            {
                User res = _forumGen.changeAdmin(userName, password, forumId, newAdminUserId);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }

        // added in version 3:

        public Boolean addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            try
            {
                Boolean res = _forumGen.addModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public Boolean removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            try
            {
                Boolean res = _forumGen.removeModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public Boolean deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd)
        {
            try
            {
                Boolean res = _forumGen.deleteDiscussion(forumId, subForumId, discussionId, userName, pswd);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public Boolean editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent)
        {
            try
            {
                Boolean res = _forumGen.editDiscussion(forumId, subForumId, discussionId, userName, pswd, newContent);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName)
        {
            try
            {
                int res = _forumGen.getNumOfCommentsSingleUser(reqUserName, reqPswd, forumId, userName);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId)
        {
            try
            {
                int res = _forumGen.getNumOfCommentsSubForum(userName, pswd, forumId, subForumId);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName)
        {
            try
            {
                List<User> resList = _forumGen.getResponsersForSingleUser(reqUserName, reqPswd, forumId, memberUserName);
                return resList;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            try
            {
                List<User> resList = _forumGen.getMutualUsers(userName, password, forumId1, forumId2);
                return resList;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public int getUserType(int forumId, string userName)
        {
            try
            {
                int res = _forumGen.getUserType(forumId, userName);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public int getUserTypeSubForum(int forumId, int subForumId, string userName)
        {
            try
            {
                int res = _forumGen.getUserType(forumId, subForumId, userName);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


    }
}
