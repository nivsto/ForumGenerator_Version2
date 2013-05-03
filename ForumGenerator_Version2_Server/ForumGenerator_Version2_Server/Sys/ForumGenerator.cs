using ForumGenerator_Version2_Server.Communication;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Sys.Exceptions;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumGenerator_Version2_Server.Sys
{
    public class ForumGenerator : IForumGenerator
    {
        internal SuperUser superUser { get; private set; }
        internal List<Forum> forums { get; private set; }
        public Logger logger { get; private set; }


        public ForumGenerator(string superUserName, string superUserPass)
        {
            this.superUser = new SuperUser(superUserName, superUserPass);
            this.forums = new List<Forum>();
            this.logger = new Logger();
        }

        // returns userid
        public User login(int forumId, string userName, string password)
        {
            try
            {
                return getForum(forumId).login(userName, password);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError(e.Message);
                throw e;
            }
            catch (Exception)
            {
                this.logger.logError("Unknown Error");
                return null;
            }
        }

        // returns 1 for success or 0 for failure
        public bool logout(int forumId, int userId)
        {
            try
            {
                return getForum(forumId).logout(userId);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                throw new Exception();///////// change!
            }
            catch (Exception)
            {
                this.logger.logError("Unknown Error");
                throw new Exception();///////// change!
            }
        }

        // returns userid
        public SuperUser superUserLogin(string userName, string password)
        {
            try
            {
                return this.superUser.login(userName, password);
            }
            catch (Exception)
            {
                this.logger.logError("Unknown Error");
                throw new Exception();///////// change!
            }
        }

        // returns 1 for success or 0 for failure
        public bool superUserLogout()
        {
            try
            {
                return this.superUser.logout();
            }
            catch (Exception)
            {
                throw new Exception();///////// change!
            }
        }

        // returns 1 for success or 0 for failure
        public User register(int forumId, string userName, string password, string email, string signature)
        {
            try
            {
                return getForum(forumId).register(userName, password, email, signature);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                throw new Exception();///////// change!
            }
            catch (Exception)
            {
                this.logger.logError("Unknown Error");
                throw new Exception();///////// change!
            }
        }

        //returns an XML list of all the forums in the system
        public List<Forum> getForums()
        {
            try
            {
                return this.forums;
            }
            catch (Exception)
            {
                throw new Exception();///////// change!
            }
        }

        //returns an XML list of all the sub-forums in the system
        public List<SubForum> getSubForums(int forumId)
        {
            try
            {
                Forum parentForum = this.getForum(forumId);
                return parentForum.subForums;
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                throw new Exception();///////// change!
            }
            catch (Exception)
            {
                throw new Exception();///////// change!
            }
        }

        //returns an XML list of all the discussions in a specific sub-forum
        public List<Discussion> getDiscussions(int forumId, int subForumId)
        {
            try
            {
                SubForum parentSubForum = this.getForum(forumId).getSubForum(subForumId);
                return parentSubForum.discussions;
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                throw new Exception();///////// change!
            }
            catch (Exception)
            {
                throw new Exception();///////// change!
            }
        }

        //returns an XML list of all the comments of a specific discussion
        public List<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            try
            {
                Discussion parentDiscussion = this.getForum(forumId).getSubForum(subForumId).getDiscussion(discussionId);
                return parentDiscussion.comments;
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                throw new Exception();///////// change!
            }
            catch (Exception)
            {
                throw new Exception();///////// change!
            }
        }

        //returns an XML list of all the users in a specific forum
        public List<User> getUsers(int forumId)
        {
            try
            {
                Forum parentForum = this.getForum(forumId);
                return parentForum.members;
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                throw new Exception();///////// change!
            }
            catch (Exception)
            {
                throw new Exception();///////// change!
            }
        }

        //creates a new forum and a new user which is the forum's administrator
        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            try
            {
                if (this.forums.Find(delegate(Forum frm) { return frm.forumName == forumName; }) != null)   // in case there is already such a forum
                    throw new UnauthorizedOperationException("forum name already exists");
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                    throw new UnauthorizedUserException();
                int forumId = this.forums.Count();
                Forum newForum = new Forum(forumId, forumName, adminUserName, adminPassword);
                this.forums.Add(newForum);
                return newForum;
            }
            catch (Exception)
            {
                this.logger.logError("unknown error");
                throw new Exception();///////// change!
            }
        }

        //creates a new sub-forum and a new user which is the forum's administrator
        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            try
            {
                Forum forum = getForum(forumId);
                if (!Security.checkAdminAuthorization(forum, userName, password))
                    throw new UnauthorizedUserException();
                return forum.createNewSubForum(subForumTitle);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                throw new Exception();///////// change!
            }
            catch (Exception)
            {
                throw new Exception();///////// change!
            }
        }

        //creates a new discussion and a new user which is the forum's administrator
        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            try
            {
                Forum forum = getForum(forumId);
                if (!Security.checkMemberAuthorization(forum, userName, password))
                    throw new Exception();///////// change!
                User user = getForum(forumId).getUser(userName);
                return getForum(forumId).getSubForum(subForumId).createNewDiscussion(title, content, user);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                throw new Exception();///////// change!
            }
            catch (Exception)
            {
                throw new Exception();///////// change!
            }
        }

        //creates a new comment and a new user which is the forum's administrator
        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            try
            {
                Forum forum = getForum(forumId);
                if (!Security.checkMemberAuthorization(forum, userName, password))
                    throw new Exception();///////// change!
                User user = getForum(forumId).getUser(userName);
                return getForum(forumId).getSubForum(subForumId).getDiscussion(discussionId).createNewComment(content, user);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                throw new Exception();///////// change!
            }
            catch (Exception)
            {
                throw new Exception();///////// change!
            }
        }

        //creates a new comment and a new user which is the forum's administrator
        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            try
            {
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                    throw new Exception();///////// change!
                return getForum(forumId).changeAdmin(newAdminUserId);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("No such " + e.Message);
                throw new Exception();///////// change!
            }
            catch (Exception)
            {
                throw new Exception();///////// change!
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
                throw new ArgumentOutOfRangeException("Forum" + forumId + "doesnt exist");
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
