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
        internal int nextForumId = 1;

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
            this.logger.logAction("login: forumId: " + forumId + " userName: " + userName + 
                                                                " password: " + password);
            try
            {
                return getForum(forumId).login(userName, password);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("login: " + e.Message);
                throw e;
            }
            catch (Exception)
            {
                this.logger.logError("login: Unknown Error");
                return null;
            }
        }

        // returns 1 for success or 0 for failure
        public bool logout(int forumId, int userId)
        {
            this.logger.logAction("logout: forumId: " + forumId + 
                                         " userId: " + userId);
            try
            {
                return getForum(forumId).logout(userId);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("logout: No such " + e.Message);
                throw new ForumNotFoundException();
            }
            catch (Exception)
            {
                this.logger.logError("logout: Unknown Error");
                throw new Exception();
            }
        }

        // returns userid
        public SuperUser superUserLogin(string userName, string password)
        {
            this.logger.logAction("superUserLogin: userName: " + userName +
                                                  " password: " + password);
            try
            {
                return this.superUser.login(userName, password);
            }
            catch (UnauthorizedUserException e)
            {
                this.logger.logError("superUserLogin: wrong userName or password");
                throw e;
            }
            catch (Exception)
            {
                this.logger.logError("superUserLogin: Unknown Error");
                throw new Exception();
            }
        }

        // returns 1 for success or 0 for failure
        public bool superUserLogout()
        {
            this.logger.logAction("superUserLogout");
            try
            {
                return this.superUser.logout();
            }
            catch (Exception e)
            {
                this.logger.logError("superUserLogout: " + e.Message);
                throw new Exception();
            }
        }

        // returns 1 for success or 0 for failure
        public User register(int forumId, string userName, string password, string email, string signature)
        {
            this.logger.logAction("register: "); // TODO add content
            try
            {
                Forum f = this.getForum(forumId);
                return f.register(userName, password, email, signature);
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("register: forum not found");
                throw e;
            }
            catch(UnauthorizedAccessException e)
            {
                this.logger.logError("register: registration to forum " + forumId + " failed");
                throw e;
            }
            catch (Exception)
            {
                this.logger.logError("register: Unknown Error");
                throw new Exception();
            }
        }

        //returns an XML list of all the forums in the system
        public List<Forum> getForums()
        {
            this.logger.logAction("getForums");
            try
            {
                return this.forums;
            }
            catch (Exception)
            {
                this.logger.logError("getForums: Unknown error");
                throw new Exception();///////// change!
            }
        }

        //returns an XML list of all the sub-forums in the system
        public List<SubForum> getSubForums(int forumId)
        {
            this.logger.logAction("getSubForums: forunId: " + forumId);
            try
            {
                Forum parentForum = this.getForum(forumId);
                return parentForum.subForums;
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("getSubForums: No such " + e.Message);
                throw new ForumNotFoundException();
            }
            catch (Exception)
            {
                this.logger.logError("getSubForums: Unknown error");
                throw new Exception();
            }
        }

        //returns an XML list of all the discussions in a specific sub-forum
        public List<Discussion> getDiscussions(int forumId, int subForumId)
        {
            this.logger.logAction("getDiscussions: forumId: " + forumId +
                                                  "subForumId: " + subForumId);
            try
            {
                Forum f = this.getForum(forumId);
                SubForum parentSubForum = f.getSubForum(subForumId);
                return parentSubForum.discussions;
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("getDiscussions: forum not found");
                throw new ForumNotFoundException();
            }
            catch (SubForumNotFoundException)
            {
                this.logger.logError("getDiscussions: subForum not found");
                throw new SubForumNotFoundException();
            }
            catch (Exception)
            {
                this.logger.logError("getDiscussions: unknown error");
                throw new Exception();
            }
        }

        //returns an XML list of all the comments of a specific discussion
        public List<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            this.logger.logAction("getComments: forumId: " + forumId +
                                                  "subForumId: " + subForumId +
                                                " discussionId: " + discussionId);
            try
            {
                Forum f = this.getForum(forumId);
                SubForum sf = f.getSubForum(subForumId);
                Discussion parentDiscussion = sf.getDiscussion(discussionId);
                return parentDiscussion.comments;
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("getComments: forum not found");
                throw e;
            }
            catch (SubForumNotFoundException e)
            {
                this.logger.logError("getComments: subForum not found");
                throw e;
            }
            catch (DiscussionNotFoundException e)
            {
                this.logger.logError("getComments: discussion not found");
                throw e;
            }
            catch (Exception)
            {
                throw new Exception("getComments: unknown error");
            }
        }


        //returns an XML list of all the users in a specific forum
        public List<User> getUsers(int forumId)
        {
            this.logger.logAction("getUsers: forumId: " + forumId);
            try
            {
                Forum parentForum = this.getForum(forumId);
                return parentForum.members;
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("getUsers: forum not found");
                throw e;
            }
            catch (Exception e)
            {
                this.logger.logError("getUsers: unknown error");
                throw e;
            }
        }


        //creates a new forum and a new user which is the forum's administrator
        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            this.logger.logAction("createNewForum: "); // TODO add content
            try
            {
                if (this.forums.Find(delegate(Forum frm) { return frm.forumName == forumName; }) != null)   // in case there is already such a forum
                    throw new UnauthorizedOperationException("forum name already exists");
                if (!Security.checkSuperUserAuthorization(this, userName, password)) {
                    this.logger.logError("createNewForum: unauthorized superUser");
                    throw new UnauthorizedUserException();
                }
                int forumId = nextForumId++;
                Forum newForum = new Forum(forumId, forumName, adminUserName, adminPassword);
                this.forums.Add(newForum);
                return newForum;
            }
            catch (Exception)
            {
                this.logger.logError("createNewForum: unknown error");
                throw new Exception();
            }
        }


        //creates a new sub-forum and a new user which is the forum's administrator
        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            this.logger.logAction("createNewSubForum: "); // TODO add content
            try
            {
                Forum forum = getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, userName, password) ||
                    !Security.checkAdminAuthorization(forum, userName, password))
                {
                    this.logger.logError("createNewSubForum: unauthorized admin");
                    throw new UnauthorizedUserException();
                }
                return forum.createNewSubForum(subForumTitle);
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("createNewSubForum: forum not found");
                throw e;
            }
            catch (Exception e)
            {
                this.logger.logError("createNewSubForum: unknown error");
                throw e;
            }
        }


        //creates a new discussion and a new user which is the forum's administrator
        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            this.logger.logAction("createNewDiscussion: "); // TODO add content
            try
            {
                Forum forum = getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, userName, password) ||
                    !Security.checkMemberAuthorization(forum, userName, password))
                {
                    this.logger.logError("createNewDiscussion: unauthrized member");
                    throw new UnauthorizedUserException();
                }
                User user = forum.getUser(userName);  // user must be found since security check passed
                SubForum sf = forum.getSubForum(subForumId);
                return sf.createNewDiscussion(title, content, user);
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("createNewDiscussion: forum not found");
                throw e;
            }
            catch (SubForumNotFoundException e)
            {
                this.logger.logError("createNewDiscussion: subForum not found");
                throw e;
            }
            catch (Exception e)
            {
                this.logger.logError("createNewDiscussion: unknown error");
                throw e;
            }
        }


        //creates a new comment and a new user which is the forum's administrator
        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            this.logger.logAction("createNewComment: "); // TODO add content
            try
            {
                Forum forum = getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, userName, password) ||
                    !Security.checkMemberAuthorization(forum, userName, password))
                {
                    this.logger.logAction("createNewComment: unauthorized user");
                    throw new UnauthorizedUserException();
                }
                User user = forum.getUser(userName); // user must be found since security check passed
                SubForum sf = forum.getSubForum(subForumId);
                Discussion d = sf.getDiscussion(discussionId);
                return d.createNewComment(content, user);
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("createNewComment: forum not found");
                throw e;
            }
            catch (SubForumNotFoundException e)
            {
                this.logger.logError("createNewComment: subForum not found");
                throw e;
            }
            catch (DiscussionNotFoundException e)
            {
                this.logger.logError("createNewComment: discussion not found");
                throw e;
            }
            catch (Exception e)
            {
                this.logger.logError("createNewComment: unknown error");
                throw e;
            }
        }


        



        //creates a new comment and a new user which is the forum's administrator
        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            this.logger.logAction("changeAdmin: "); // TODO add content
            try
            {
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                {
                    this.logger.logError("changeAdmin: unauthrozied user");
                    throw new UnauthorizedUserException();
                }
                Forum forum = this.getForum(forumId);
                return forum.changeAdmin(newAdminUserId);
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("changeAdmin: forum not found");
                throw e;
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
                throw new ForumNotFoundException();
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
