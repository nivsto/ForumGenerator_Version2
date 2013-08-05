using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Sys;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using System.Web.UI;
using System.Xml.Linq;
using System.Reflection;
using ForumGenerator_Version2_Server;

namespace ForumService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class HttpServer : IForumService
    {
        private ForumGenerator _forumGen;
        //private List<IForumServiceCallback> _subscribers; 
        private List<Subscriber> _subscribers;

        public HttpServer()
        {
            _forumGen = new ForumGenerator(ForumGeneratorDefs.SU_USERNAME, ForumGeneratorDefs.SU_PSWD, this);
            //_subscribers = new List<IForumServiceCallback>();
            _subscribers = new List<Subscriber>();
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


        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword, ForumGenerator_Version2_Server.ForumData.Forum.RegPolicy registrationPolicy)
        {
            try
            {
                Forum res = _forumGen.createNewForum(userName, password, forumName, adminUserName, adminPassword, registrationPolicy);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            try
            {
                SubForum res = _forumGen.createNewSubForum(userName, password, forumId, subForumTitle);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
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

        public Boolean addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd, ForumGenerator_Version2_Server.Users.Moderator.modLevel level)
        {
            try
            {
                Boolean res = _forumGen.addModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd, level);
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


        public bool removeSubForum(int forumId, int subForumId, string userName, string password)
        {
            try
            {
                return _forumGen.removeSubForum(forumId, subForumId, userName, password);
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


        public Discussion editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent)
        {
            try
            {
                Discussion res = _forumGen.editDiscussion(forumId, subForumId, discussionId, userName, pswd, newContent);
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


        public List<Moderator> getModerators(int forumId, int subForumId)
        {
            try
            {
                List<Moderator> resList = _forumGen.getModerators(forumId, subForumId);
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

        public int countDiscussionsPerForum(int forumId)
        {
            try
            {
                int res = _forumGen.countDiscussionsPerForum(forumId);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public int countSubForumsPerForum(int forumId)
        {
            try
            {
                int res = _forumGen.countSubForumsPerForum(forumId);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public int countDiscussionsPerSubForum(int forumId, int subForumId)
        {
            try
            {
                int res = _forumGen.countDiscussionsPerSubForum(forumId, subForumId);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public int countCommentsPerSubForum(int forumId, int subForumId)
        {
            try
            {
                int res = _forumGen.countCommentsPerSubForum(forumId, subForumId);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public int countCommentsPerDiscussion(int forumId, int subForumId, int discussionId)
        {
            try
            {
                int res = _forumGen.countCommentsPerDiscussion(forumId, subForumId, discussionId);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public bool confirmUser(int forumId, string userName)
        {
            try
            {
                bool res = _forumGen.confirmUser(forumId, userName);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public List<User> getConfirmedUsers(int forumId)
        {
            try
            {
                List<User> res = _forumGen.getUnconfirmedUsers(forumId);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public bool changeModLevel(int forumId, int subForumId, string moderatorName, ForumGenerator_Version2_Server.Users.Moderator.modLevel newLevel)
        {
            try
            {
                bool res = _forumGen.changeModLevel(forumId, subForumId, moderatorName, newLevel);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        //allow the client to subscribe for push notifications
        public bool subscribe(int forumId, string userName)
        {
            try
            {
                IForumServiceCallback callback = OperationContext.Current.GetCallbackChannel<IForumServiceCallback>();

                bool doesContainSub = false;
                foreach (Subscriber currSub in _subscribers)
                {
                    if ( (currSub.forumId == forumId) && (currSub.userName == userName) )
                        doesContainSub = true;                    
                }

                if ( !(doesContainSub) )
                {
                    Subscriber newSubscriber = new Subscriber(callback, forumId, userName);
                    _subscribers.Add(newSubscriber);
                }

                return true;              
            }
            catch
            {
                return false;                
            }
        }

        //removes the client from the subscribers list
        public bool unsubscribe(int forumId, string userName)
        {
            try
            {
                IForumServiceCallback callback = OperationContext.Current.GetCallbackChannel<IForumServiceCallback>();

                bool doesContainSub = false;
                foreach (Subscriber currSub in _subscribers)
                {
                    if ((currSub.forumId == forumId) && (currSub.userName == userName))
                        doesContainSub = true;
                }

                if (doesContainSub)
                {
                    Subscriber subscriberToRem = new Subscriber(callback, forumId, userName);
                    _subscribers.Remove(subscriberToRem);
                }
                
                return true;
            }
            catch
            {
                return false;                
            }
        }

        public void notifyAllForum(int forumId, string userName, string newSubforumTitle)
        {
            foreach (Subscriber currSub in _subscribers)
            {
                if (((ICommunicationObject)currSub.callbackChannel).State == CommunicationState.Opened)
                {
                    //if ( !((currSub.forumId == forumId) && (currSub.userName == userName)) )
                    //{ // we dont notify the user who triggered the event
                        currSub.callbackChannel.notify("A new subforum: " + newSubforumTitle + " was created");    
                    //}
                    
                }
                else
                { // callback channel is closed so removing entry from the _subscribers list
                    _subscribers.Remove(currSub);
                }
            }

        }

        //public void notifyAll()
        //{
        //    _subscribers.ForEach(delegate(IForumServiceCallback callback)
        //    {
        //        if (((ICommunicationObject)callback).State == CommunicationState.Opened)
        //        {
        //            callback.notify("Server just sent a notification");
        //        }
        //        else
        //        {
        //            _subscribers.Remove(callback);
        //        }
        //    });
        //}

        public List<User> getUnconfirmedUsers(int forumId)
        {
            throw new NotImplementedException();
        }
    }
}
