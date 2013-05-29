using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Client.ServiceReference1;
using System.ServiceModel;
using System.Windows.Forms;

namespace ForumGenerator_Client.Communication
{

    class newCommunicator
    {

        ChannelFactory<IForumService> httpFactory;
        IForumService httpProxy;

        public newCommunicator()
        {
            httpFactory = new ChannelFactory<IForumService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8888/methods"));
            httpProxy = httpFactory.CreateChannel();

        }

        
        public User login(int forumId, string userName, string password)
        {
            User ans = null;
            try
            {
                ans = httpProxy.login(forumId, userName, password);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public bool logout(int forumId, int userId)
        {
            bool ans = false;
            try
            {
                ans = httpProxy.logout(forumId, userId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public SuperUser superUserLogin(string userName, string password)
        {
            SuperUser suser = null;
            try
            {
                suser = httpProxy.superUserLogin(userName, password);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return suser;
        }

        public bool superUserLogout(string userName, string password)
        {
            bool ans = false;
            try
            {
                ans = httpProxy.superUserLogout(userName, password);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            
            return ans;
        }

        public User register(int forumId, string userName, string password, string email, string signature)
        {
            User ans = null;
            try
            {
                ans = httpProxy.register(forumId, userName, password, email, signature);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public Forum[] getForums()
        {
            Forum[] ans = null;
            try
            {
                ans = httpProxy.getForums();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public SubForum[] getSubForums(int forumId)
        {
            SubForum[] ans = null;
            try
            {
                ans = httpProxy.getSubForums(forumId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            } 
            return ans;
        }

        public Discussion[] getDiscussions(int forumId, int subForumId)
        {
            Discussion[] ans = null;
            try
            {
                ans = httpProxy.getDiscussions(forumId, subForumId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public Comment[] getComments(int forumId, int subForumId, int discussionId)
        {
            Comment[] ans = null;
            try
            {
                ans = httpProxy.getComments(forumId, subForumId, discussionId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public User[] getUsers(int forumId)
        {
            User[] ans = null;
            try
            {
                ans = httpProxy.getUsers(forumId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            Forum ans = null;
            try
            {
                ans = httpProxy.createNewForum(userName, password, forumName, adminUserName, adminPassword);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            SubForum ans = null;
            try
            {
                ans = httpProxy.createNewSubForum(userName, password, forumId, subForumTitle);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            Discussion ans = null;
            try
            {
                ans = httpProxy.createNewDiscussion(userName, password, forumId, subForumId, title, content);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            } 
            return ans;
        }

        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            Comment ans = null;
            try
            {
                ans = httpProxy.createNewComment(userName, password, forumId, subForumId, discussionId, content);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            User ans = null;
            try
            {
                ans = changeAdmin(userName, password, forumId, newAdminUserId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        // added in version 3:

        public Boolean addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            Boolean ans = false;
            try
            {
                ans = httpProxy.addModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public Boolean removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            Boolean ans = false;
            try
            {
                ans = httpProxy.removeModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public Boolean deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd)
        {
            Boolean ans = false;
            try
            {
                ans = httpProxy.deleteDiscussion(forumId, subForumId, discussionId, userName, pswd);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public Boolean editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent)
        {
            Boolean ans = false;
            try
            {
                ans = httpProxy.editDiscussion(forumId, subForumId, discussionId, userName, pswd, newContent);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public User[] getMutualForumMembers(int forumId1, int forumId2)
        {
            User[] ans = null;
            try
            {
               // ans = httpProxy.getMutualForumMembers(forumId1, forumId2;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName)
        {
            int ans = 0;
            try
            {
                ans = httpProxy.getNumOfCommentsSingleUser( reqUserName, reqPswd, forumId, userName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId)
        {
            int ans = 0;
            try
            {
                ans = httpProxy.getNumOfCommentsSubForum(userName, pswd, forumId, subForumId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public User[] getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName)
        {
            User[] ans = null;
            try
            {
                ans = httpProxy.getResponsersForSingleUser(reqUserName, reqPswd, forumId, memberUserName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public User[] getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            User[] ans = null;
            try
            {
                ans = httpProxy.getMutualUsers(userName, password, forumId1, forumId2);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public int getUserType(int forumId, string userName)
        {
           int ans = 0;
            try
            {
                ans = httpProxy. getUserType(forumId, userName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

        public int getUserType(int forumId, int subForumId, string userName)
        {
            int ans = 0;
            try
            {
           //     ans = httpProxy.getUserType(forumId, subForumId, userName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
            return ans;
        }

    }
}
