using ForumGenerator_Version2_Server.Communication;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumGenerator_Version2_Server.Sys
{
    public class ForumGenerator
    {
        internal SuperUser superUser;
        internal List<Forum> forums;
        public Logger logger;


        public ForumGenerator(string superUserName, string superUserPass)
        {
            this.superUser = new SuperUser(superUserName, superUserPass);
            this.forums = new List<Forum>();
            this.logger = new Logger();
        }

        // returns userid
        public Tuple<string, string> login(int forumId, string userName, string password)
        {
            try
            {
                return getForum(forumId).login(userName, password);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<string, string>("0", "No such " + e.Message);
            }
            catch (Exception)
            {
                this.logger.logError("Unknown Error");
                return new Tuple<string, string>("0", "Unknown Error");
            }
        }

        // returns 1 for success or 0 for failure
        public Tuple<string, string> logout(int forumId, int userId)
        {
            try
            {
                return getForum(forumId).logout(userId);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<string, string>("0", "No such " + e.Message);
            }
            catch (Exception)
            {
                this.logger.logError("Unknown Error");
                return new Tuple<string, string>("0", "Unknown Error");
            }
        }

        // returns userid
        public Tuple<string, string> adminLogin(string userName, string password)
        {
            try
            {
                return this.superUser.login(userName, password);
            }
            catch (Exception)
            {
                this.logger.logError("Unknown Error");
                return new Tuple<string, string>("0", "Unknown Error");
            }
        }

        // returns 1 for success or 0 for failure
        public Tuple<string, string> adminLogout()
        {
            try
            {
                return this.superUser.logout();
            }
            catch (Exception)
            {
                return new Tuple<string, string>("0", "Unknown Error");
            }
        }

        // returns 1 for success or 0 for failure
        public Tuple<string, string> register(int forumId, string userName, string password, string email, string signature)
        {
            try
            {
                return getForum(forumId).register(userName, password, email, signature);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<string, string>("0", "No such " + e.Message);
            }
            catch (Exception)
            {
                this.logger.logError("Unknown Error");
                return new Tuple<string, string>("0", "Unknown Error");
            }
        }

        //returns an XML list of all the forums in the system
        public Tuple<bool, string, string[], string[,]> getForums()
        {
            try
            {
                string[] properties = { "ID", "Name", "AdminName" };
                string[,] data = new string[this.forums.Count(), properties.Length];
                for (int i = 0; i < this.forums.Count(); i++)
                {
                    Forum current = this.forums.ElementAt(i);
                    data[i, 0] = current.getForumId().ToString();
                    data[i, 1] = current.getForumName();
                    data[i, 2] = current.getAdminName();
                }
                return new Tuple<bool, string, string[], string[,]>(true, "Forum", properties, data);
            }
            catch (Exception)
            {
                return new Tuple<bool, string, string[], string[,]>(false, "Unknown Error",null, null);
            }
        }

        //returns an XML list of all the sub-forums in the system
        public Tuple<bool, string, string[], string[,]> getSubForums(int forumId)
        {
            try
            {
                Forum parentForum = this.getForum(forumId);
                return parentForum.getSubForumsXML();
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<bool, string, string[], string[,]>(false, "No such " + e.Message,null, null);
            }
            catch (Exception)
            {
                return new Tuple<bool, string, string[], string[,]>(false, "Unknown Error",null, null);
            }
        }

        //returns an XML list of all the discussions in a specific sub-forum
        public Tuple<bool, string, string[], string[,]> getDiscussions(int forumId, int subForumId)
        {
            try
            {
                SubForum parentSubForum = this.getForum(forumId).getSubForum(subForumId);
                return parentSubForum.getDiscussionsXML();
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<bool, string, string[], string[,]>(false, "No such " + e.Message,null, null);
            }
            catch (Exception)
            {
                return new Tuple<bool, string, string[], string[,]>(false, "unknown error", null, null);
            }
        }

        //returns an XML list of all the comments of a specific discussion
        public Tuple<bool, string, string[], string[,]> getComments(int forumId, int subForumId, int discussionId)
        {
            try
            {
                Discussion parentDiscussion = this.getForum(forumId).getSubForum(subForumId).getDiscussion(discussionId);
                return parentDiscussion.getCommentsXML();
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<bool, string, string[], string[,]>(false, "No such " + e.Message, null, null);
            }
            catch (Exception)
            {
                return new Tuple<bool, string, string[], string[,]>(false, "unknown error", null, null);
            }
        }

        //returns an XML list of all the users in a specific forum
        public Tuple<bool, string, string[], string[,]> getUsers(int forumId)
        {
            try
            {
                Forum parentForum = this.getForum(forumId);
                return parentForum.getUsers();
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<bool, string, string[], string[,]>(false, "No such " + e.Message, null, null);
            }
            catch (Exception)
            {
                return new Tuple<bool, string, string[], string[,]>(false, "unknown error", null, null);
            }
        }

        //creates a new forum and a new user which is the forum's administrator
        public Tuple<string, string> createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            try
            {
                if (this.forums.Find(delegate(Forum frm) { return frm.forumName == forumName; }) != null)   // in case there is already such a forum
                    return new Tuple<string, string>("0", "forum name already exists");
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                    return new Tuple<string, string>("0", "no permission");
                int forumId = this.forums.Count();
                Forum newForum = new Forum(forumId, forumName, adminUserName, adminPassword);
                this.forums.Add(newForum);
                return new Tuple<string, string>("1", newForum.getForumId().ToString());
            }
            catch (Exception)
            {
                this.logger.logError("unknown error");
                return new Tuple<string, string>("0", "unknown error");
            }
        }

        //creates a new sub-forum and a new user which is the forum's administrator
        public Tuple<string, string> createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            try
            {
                Forum forum = getForum(forumId);
                if (!Security.checkAdminAuthorization(forum, userName, password))
                    return new Tuple<string, string>("0", "no permission");
                return forum.createNewSubForum(subForumTitle);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<string, string>("0", "No such " + e.Message);
            }
            catch (Exception)
            {
                return new Tuple<string, string>("0", "unknown error");
            }
        }

        //creates a new discussion and a new user which is the forum's administrator
        public Tuple<string, string> createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            try
            {
                Forum forum = getForum(forumId);
                if (!Security.checkMemberAuthorization(forum, userName, password))
                    return new Tuple<string, string>("0", "no permission");
                Member user = getForum(forumId).getUser(userName);
                return getForum(forumId).getSubForum(subForumId).createNewDiscussion(title, content, user);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<string, string>("0", "No such " + e.Message);
            }
            catch (Exception)
            {
                return new Tuple<string, string>("0", "unknown error");
            }
        }

        //creates a new comment and a new user which is the forum's administrator
        public Tuple<string, string> createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            try
            {
                Forum forum = getForum(forumId);
                if (!Security.checkMemberAuthorization(forum, userName, password))
                    return new Tuple<string, string>("0", "no permission");
                Member user = getForum(forumId).getUser(userName);
                return getForum(forumId).getSubForum(subForumId).getDiscussion(discussionId).createNewComment(content, user);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<string, string>("0", "No such " + e.Message);
            }
            catch (Exception)
            {
                return new Tuple<string, string>("0", "unknown error");
            }
        }

        //creates a new comment and a new user which is the forum's administrator
        public Tuple<string, string> changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            try
            {
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                    return new Tuple<string, string>("0", "no permission");
                return getForum(forumId).changeAdmin(newAdminUserId);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                return new Tuple<string, string>("0", "No such " + e.Message);
            }
            catch (Exception)
            {
                return new Tuple<string, string>("0", "unknown error");
            }
        }

        //get a forum by its forumId
        public Forum getForum(int forumId)
        {
            try
            {
                return forums.ElementAt(forumId);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Forum" + forumId);
            }
        }

        public SuperUser getSuperUser()
        {
            return this.superUser;
        }

        public int getForumCount()
        {
            return this.forums.Count();
        }

    }
}
