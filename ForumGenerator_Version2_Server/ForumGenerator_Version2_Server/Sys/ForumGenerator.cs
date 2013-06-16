﻿using ForumGenerator_Version2_Server.DataLayer;
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
        public enum userTypes
        {
            GUEST,
            MEMBER,
            MODERATOR,
            ADMIN,
            SUPER_USER
        };

        internal SuperUser superUser { get; private set; }
        internal List<Forum> forums { get; private set; }
        public Logger logger { get; private set; }
        public ForumGeneratorContext db { get; set; }


        public ForumGenerator(string superUserName, string superUserPass)
        {
            this.db = new ForumGeneratorContext("ForumGenerator_DB1");
            this.superUser = new SuperUser(superUserName, superUserPass);
            this.forums = new List<Forum>();
            this.logger = new Logger();
            this.db.Forums.SqlQuery("UPDATE Users SET isLoggedIn = 0 WHERE 1=1");
            this.db.SaveChanges();
        }

        public ForumGenerator(string superUserName, string superUserPass, bool test)
        {
            this.db = new ForumGeneratorContext("ForumGenerator_DB1_TEST");
            this.superUser = new SuperUser(superUserName, superUserPass);
            this.forums = new List<Forum>();
            this.logger = new Logger();
            this.db.Forums.SqlQuery("DELETE * FROM Fora");
            this.db.Forums.SqlQuery("DELETE * FROM Comments");
            this.db.Forums.SqlQuery("DELETE * FROM Discussions");
            this.db.Forums.SqlQuery("DELETE * FROM SubForumModerators");
            this.db.Forums.SqlQuery("DELETE * FROM SubForums");
            this.db.Forums.SqlQuery("DELETE * FROM UserFriends");
            this.db.Forums.SqlQuery("DELETE * FROM Users");
            this.db.SaveChanges();
        }

        public void reset()
        {
            this.superUser = new SuperUser(this.superUser.userName, this.superUser.password);
            this.forums = new List<Forum>();
            this.logger = new Logger();
            this.db.Forums.SqlQuery("DELETE * FROM Fora");
            this.db.Forums.SqlQuery("DELETE * FROM Comments");
            this.db.Forums.SqlQuery("DELETE * FROM Discussions");
            this.db.Forums.SqlQuery("DELETE * FROM SubForumModerators");
            this.db.Forums.SqlQuery("DELETE * FROM SubForums");
            this.db.Forums.SqlQuery("DELETE * FROM UserFriends");
            this.db.Forums.SqlQuery("DELETE * FROM Users");
        }


        private bool isSuperUser(string userName, string password)
        {
            return (userName == this.superUser.userName && password == this.superUser.password);
        }

       

        // returns userid
        public User login(int forumId, string userName, string password)
        {
            this.logger.logAction("performing login: forumId: " + forumId + 
                                                  "\tuserName: " + userName + 
                                                  "\tpassword: " + password);
            try
            {
                return getForum(forumId).login(userName, password, db);
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("login: forum " + forumId + " not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (UserNotFoundException)
            {
                this.logger.logError("login: user " + userName + " not found");
                throw new UserNotFoundException("User not found");
            }
            catch (Exception)
            {
                this.logger.logError("login: Unknown Error");
                throw new Exception("Unknown error in login operation");
            }
        }

        // returns 1 for success or 0 for failure
        public bool logout(int forumId, string userName, string password)
        {
            this.logger.logAction("performing logout: forumId: " + forumId + 
                                                    "\tuserName: " + userName + 
                                                    "\tpassword: " + password);
            try
            {
                User loggedOutUser = getForum(forumId).logout(userName, password, db);
                return true;
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("logout: forum " + forumId + " not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (UserNotFoundException)
            {
                this.logger.logError("logout: userName " + userName + " not found");
                throw new UserNotFoundException("User not found");
            }
            catch (UnauthorizedAccessException)
            {
                this.logger.logError("logout: Wrong userName or password");
                throw new UserNotFoundException("Wrong userName or password");
            }
            catch (Exception)
            {
                this.logger.logError("logout: Unknown Error");
                throw new Exception("Unknown error in logout");
            }
        }

        // returns userid
        public SuperUser superUserLogin(string userName, string password)
        {
            this.logger.logAction("performing superUserLogin: userName: " + userName +
                                                           "\tpassword: " + password);
            try
            {
                return this.superUser.login(userName, password);
            }
            catch (UnauthorizedUserException e)
            {
                this.logger.logError("superUserLogin: Wrong userName or password");
                throw new UnauthorizedAccessException("Wrong userName or password");
            }
            catch (Exception)
            {
                this.logger.logError("superUserLogin: Unknown Error");
                throw new Exception("Unknown error");
            }
        }

        public bool superUserLogout(string userName, string password)
        {
            this.logger.logAction("performing superUserLogout: userName: " + userName +
                                                            "\tpassword: " + password);
            try
            {
                return this.superUser.logout(userName, password);
            }
            catch (UnauthorizedAccessException)
            {
                this.logger.logError("superUserLogout: Wrong userName or password");
                throw new UnauthorizedAccessException("Wrong userName or password");
            }
            catch (Exception e)
            {
                this.logger.logError("superUserLogout: " + e.Message);
                throw new Exception("Unknown error");
            }
        }

        // returns 1 for success or 0 for failure
        public User register(int forumId, string userName, string password, string email, string signature)
        {
            this.logger.logAction("performing register: forumId: " + forumId +
                                                        "\tuserName: " + userName +
                                                        "\tpassword: " + password +
                                                        "\temail: " + email +
                                                        "\tsignature: " + signature);

            if (!ContentPolicy.isLegalContent(ContentPolicy.cType.USER_NAME, userName) ||
                !ContentPolicy.isLegalContent(ContentPolicy.cType.PASSWORD, password) ||
                !ContentPolicy.isLegalEmailFormat(email) ||
                !ContentPolicy.isLegalContent(ContentPolicy.cType.MEMBER_SIGNATURE, signature) )
            {
                throw new IllegalContentException("Illegal content");
            }

            try
            {
                return this.getForum(forumId).register(userName, password, email, signature, db);
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("register: forum not found");
                throw new ForumNotFoundException("forum not found");
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
            this.logger.logAction("performing getForums");
            try
            {
                List<Forum> fl = this.db.Forums.ToList();
                List<Forum> returnedList = new List<Forum>();
                foreach (Forum f in fl)
                {
                    User forumAdmin = new User(f.admin);
                    returnedList.Add(new Forum(f.forumId, f.forumName, forumAdmin));
                }
                return returnedList;
                //return this.forums;

            }
            catch (Exception)
            {
                this.logger.logError("getForums: Unknown error");
                throw new Exception("Unknown error");
            }
        }

        //returns an XML list of all the sub-forums in the system
        public List<SubForum> getSubForums(int forumId)
        {
            this.logger.logAction("performing getSubForums: forunId: " + forumId);
            try
            {
                Forum f = getForum(forumId);
                List<SubForum> sfl = f.subForums;
                List<SubForum> returnedList = new List<SubForum>();
                foreach (SubForum sf in sfl)
                {
                    returnedList.Add(new SubForum(sf.subForumId, sf.subForumTitle));
                }
                return returnedList;
             }
            catch (ArgumentOutOfRangeException e)
            {
                this.logger.logError("getSubForums: No such " + e.Message);
                throw new ForumNotFoundException("Forum not found");
            }
            catch (Exception)
            {
                this.logger.logError("getSubForums: Unknown error");
                throw new Exception("Unknown error");
            }
        }

        //returns an XML list of all the discussions in a specific sub-forum
        public List<Discussion> getDiscussions(int forumId, int subForumId)
        {
            this.logger.logAction("performing getDiscussions: forumId: " + forumId +
                                                  "subForumId: " + subForumId);
            try
            {
                List<Discussion> dl = this.getForum(forumId).getSubForum(subForumId).discussions;
                List<Discussion> returnedList = new List<Discussion>();
                foreach (Discussion d in dl)
                {
                    returnedList.Add(new Discussion(d));
                }
                return returnedList;
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("getDiscussions: forum not found");
                throw new ForumNotFoundException("Forum not found");
            }
            catch (SubForumNotFoundException)
            {
                this.logger.logError("getDiscussions: subForum not found");
                throw new SubForumNotFoundException();
            }
            catch (Exception)
            {
                this.logger.logError("getDiscussions: unknown error");
                throw new Exception("Unknown error");
            }
        }

        //returns an XML list of all the comments of a specific discussion
        public List<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            this.logger.logAction("performing getComments: forumId: " + forumId +
                                                  "subForumId: " + subForumId +
                                                " discussionId: " + discussionId);
            try
            {
                List<Comment> cl = this.getForum(forumId).getSubForum(subForumId).getDiscussion(discussionId).comments;
                List<Comment> returnedList = new List<Comment>();
                foreach (Comment c in cl)
                {
                    returnedList.Add(new Comment(c));
                }
                return returnedList;
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("getComments: forum not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (SubForumNotFoundException e)
            {
                this.logger.logError("getComments: subForum not found");
                throw new SubForumNotFoundException("subForum not found");
            }
            catch (DiscussionNotFoundException e)
            {
                this.logger.logError("getComments: discussion not found");
                throw new DiscussionNotFoundException("discussion not found");
            }
            catch (Exception)
            {
                this.logger.logError("getComments: Unknown error");
                throw new Exception("Unknown error");
            }
        }

        //returns an XML list of all the users in a specific forum
        public List<User> getUsers(int forumId)
        {
            this.logger.logAction("performing getUsers: forumId: " + forumId);
            try
            {
                List<User> ul = this.getForum(forumId).members;
                List<User> returnedList = new List<User>();
                foreach (User u in ul)
                {
                    returnedList.Add(new User(u));
                }
                return returnedList;
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("getUsers: forum not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (Exception)
            {
                this.logger.logError("getUsers: unknown error");
                throw new Exception("Unknown error");
            }
        }

        //creates a new forum and a new user which is the forum's administrator
        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            this.logger.logAction("performing createNewForum:  userName: " + userName +
                                                              "\tpassword: " + password +
                                                              "\tforumName: " + forumName +
                                                              "\tadminUserName: " + adminUserName +
                                                              "\tadminPassword: " + adminPassword);
            try
            {
                if (!ContentPolicy.isLegalContent(ContentPolicy.cType.USER_NAME, userName) ||
                    !ContentPolicy.isLegalContent(ContentPolicy.cType.PASSWORD, password) ||
                    !ContentPolicy.isLegalContent(ContentPolicy.cType.FORUM_NAME, forumName) ||
                    !ContentPolicy.isLegalContent(ContentPolicy.cType.USER_NAME, adminUserName) ||
                    !ContentPolicy.isLegalContent(ContentPolicy.cType.PASSWORD, adminPassword))
                {
                    throw new IllegalContentException("Illegal content");
                }

                if (this.getForum(forumName) != null)   // in case there is already such a forum
                    throw new UnauthorizedOperationException("forum name already exists");
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                {
                    this.logger.logError("createNewForum: unauthorized superUser");
                    throw new UnauthorizedUserException("unauthorized superUser");
                }
                Forum newForum = new Forum(forumName, adminUserName, adminPassword, this.db);
                this.forums.Add(newForum);
                this.db.Forums.Add(newForum);
                this.db.SaveChanges();
                return newForum;
            }
            catch (IllegalContentException)
            {
                this.logger.logError("createNewForum: Illegal content");
                throw new IllegalContentException("Illegal content");
            }
            catch (Exception)
            {
                this.logger.logError("createNewForum: unknown error");
                throw new Exception("Unknown error");
            }
        }

        //creates a new sub-forum and a new user which is the forum's administrator
        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            this.logger.logAction("performing createNewSubForum: userName: " + userName +
                                                              "\tpassword: " + password +
                                                              "\tforumId: " + forumId +
                                                              "\tsubForumTitle: " + subForumTitle);

            if (!ContentPolicy.isLegalContent(ContentPolicy.cType.USER_NAME, userName) ||
                !ContentPolicy.isLegalContent(ContentPolicy.cType.PASSWORD, password) ||
                !ContentPolicy.isLegalContent(ContentPolicy.cType.SUBFORUM_TITLE, subForumTitle) )
            {
                throw new IllegalContentException("Illegal content");
            }

            try
            {
                Forum forum = getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkAdminAuthorization(forum, userName, password))
                {
                    this.logger.logError("createNewSubForum: unauthorized admin");
                    throw new UnauthorizedUserException();
                }
                return forum.createNewSubForum(subForumTitle, db);
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("createNewSubForum: forum not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (IllegalContentException)
            {
                this.logger.logError("createNewSubForum: Illegal content");
                throw new IllegalContentException("Illegal content");
            }
            catch (Exception)
            {
                this.logger.logError("createNewSubForum: unknown error");
                throw new Exception("Unknwon error");
            }
        }

        //creates a new discussion and a new user which is the forum's administrator
        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            this.logger.logAction("performing createNewDiscussion: userName: " + userName +
                                                                 "\tpassword: " + password +
                                                                 "\tforumId: " + forumId +
                                                                 "\tsubForumId: " + subForumId +
                                                                 "\ttitle: " + title +
                                                                 "\tcontent: " + content);

            if (!ContentPolicy.isLegalContent(ContentPolicy.cType.USER_NAME, userName) ||
                !ContentPolicy.isLegalContent(ContentPolicy.cType.PASSWORD, password) ||
                !ContentPolicy.isLegalContent(ContentPolicy.cType.DISCUSSION_TITLE, title) ||
                !ContentPolicy.isLegalContent(ContentPolicy.cType.DISCUSSION_CONTENT, content) )
            {
                throw new IllegalContentException("Illegal content");
            }
            
            try
            {
                Forum forum = getForum(forumId);
                SubForum sf = forum.getSubForum(subForumId);
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkMemberAuthorization(forum, userName, password))
                {
                    this.logger.logError("createNewDiscussion: unauthrized member");
                    throw new UnauthorizedUserException("Unauthorized member");
                }
                User user;
                // if(isSuperUser(userName, password)
                //    currently not supported.
                    user = forum.getUser(userName); // user must be found since security check passed, or being mngr / superUser 
                return sf.createNewDiscussion(title, content, user, db);
            }
            catch (IllegalContentException)
            {
                this.logger.logError("createNewDiscussion: Illegal content");
                throw new IllegalContentException("Illegal content");
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("createNewDiscussion: forum not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (SubForumNotFoundException)
            {
                this.logger.logError("createNewDiscussion: subForum not found");
                throw new SubForumNotFoundException("SubForum not found");
            }
            catch (Exception)
            {
                this.logger.logError("createNewDiscussion: unknown error");
                throw new Exception("Unknown error");
            }
        }

        //creates a new comment and a new user which is the forum's administrator
        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            if (!ContentPolicy.isLegalContent(ContentPolicy.cType.USER_NAME, userName) ||
               !ContentPolicy.isLegalContent(ContentPolicy.cType.PASSWORD, password) ||
               !ContentPolicy.isLegalContent(ContentPolicy.cType.COMMENT_CONTENT, content) )
            {
                throw new IllegalContentException();
            }

            this.logger.logAction("performing createNewComment: "); // TODO add content
            try
            {
                Forum forum = getForum(forumId);
                SubForum sf = forum.getSubForum(subForumId);
                Discussion d = sf.getDiscussion(discussionId);
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkMemberAuthorization(forum, userName, password))
                {
                    this.logger.logAction("createNewComment: unauthorized user");
                    throw new UnauthorizedUserException();
                }

                // if(isSuperUser(userName, password)
                //    currently not supported.

                User user = forum.getUser(userName); // user must be found since security check passed, or being a superUser
                return d.createNewComment(content, user, db);
            }
            catch (IllegalContentException)
            {
                this.logger.logError("createNewComment: Illegal content");
                throw new IllegalContentException("Illegal content");
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("createNewComment: forum not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (SubForumNotFoundException)
            {
                this.logger.logError("createNewComment: subForum not found");
                throw new SubForumNotFoundException("SubForum not found");
            }
            catch (DiscussionNotFoundException)
            {
                this.logger.logError("createNewComment: discussion not found");
                throw new DiscussionNotFoundException("Discussion not found");
            }
            catch (Exception)
            {
                this.logger.logError("createNewComment: unknown error");
                throw new Exception("Unknown error");
            }
        }

        // This method never returns false.
        // Throws Exceptions:
        // UnauthorizedUserException - in case user is not authorized to delete this discussion.
        // ForumNotFoundException - forum not found
        // SubForumNotFoundException, DiscussionNotFoundException - same reason
        // Exception - unknown error.
        public bool deleteDiscussion(int forumId, int subForumId, int discussionId,
                     string userName, string password)
        {
            this.logger.logAction("performing deleteDiscussion: userName: " + userName +
                                                              "\tpassword: " + password +
                                                              "\tforumId: " + forumId +
                                                              "\tsubForumId: " + subForumId +
                                                              "\tdiscussionId: " + discussionId);
            try
            {
                Forum f = this.getForum(forumId);
                SubForum sf = f.getSubForum(subForumId);
                Discussion d = sf.getDiscussion(discussionId);
                // check authorization
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkAdminAuthorization(f, userName, password) &&
                    !Security.checkModeratorAuthorization(sf, userName, password) &&
                    !Security.checkPublisherAuthorization(d, userName, password))
                {
                    this.logger.logError("deleteDiscussion: unauthorized user");
                    throw new UnauthorizedUserException("No permission to delete this discussion");
                } // if

                sf.removeDiscussion(discussionId, db);

                return true;
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("deleteDiscussion: forum not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (SubForumNotFoundException)
            {
                this.logger.logError("deleteDiscussion: subForum not found");
                throw new SubForumNotFoundException("SubForum not found");
            }
            catch (DiscussionNotFoundException)
            {
                this.logger.logError("deleteDiscussion: discussion not found");
                throw new DiscussionNotFoundException("Discussion not found");
            }
            catch (Exception)
            {
                this.logger.logError("deleteDiscussion: unknown error");
                throw new Exception("Unknown error");
            }
            
        }
        
        //creates a new comment and a new user which is the forum's administrator
        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            this.logger.logAction("performing changeAdmin: userName: " + userName +
                                                        "\tpassword: " + password +
                                                        "\tforumId: " + forumId +
                                                        "\tnewAdminUserId: " + newAdminUserId);
            try
            {
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                {
                    this.logger.logError("changeAdmin: unauthrozied user");
                    throw new UnauthorizedUserException("No permission to change admin");
                }

                return this.getForum(forumId).changeAdmin(newAdminUserId, db);
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("changeAdmin: forum not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (UserNotFoundException e)
            {
                this.logger.logError("changeAdmin: userId " + newAdminUserId + " not found");
                throw new UserNotFoundException("User not found");
            }
            catch (Exception e)
            {
                this.logger.logError("changeAdmin: unknown error");
                throw new Exception("Unknown error");
            }
        }

        //get a forum by its forumId
        public Forum getForum(int forumId)
        {
            //Forum forum = this.forums.Find(delegate(Forum f) { return f.forumId == forumId; });
            Forum forum = this.db.Forums.Find(forumId);
            if(forum == null)
                throw new ForumNotFoundException("forum not found");
            return forum;
        }


        //get a forum by its forum name
        public Forum getForum(string forumName)
        {
            return this.forums.Find(delegate(Forum f) { return f.forumName == forumName; });
        }

        public SuperUser getSuperUser()
        {
            return this.superUser;
        }

        public int getForumCount()
        {
            return this.forums.Count();
        }

        public bool addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            this.logger.logAction("performing addModerator: modUserName: " + modUserName +
                                                         "\tforumId: " + forumId +
                                                         "\tsubForumId: " + subForumId +
                                                         "\tadderUserName: " + adderUsrName +
                                                         "\tadderPswd: " + adderPswd);
            try
            {
                Forum f = this.getForum(forumId);
                SubForum sf = f.getSubForum(subForumId);
                if (!Security.checkSuperUserAuthorization(this, adderUsrName, adderPswd) &&
                    !Security.checkAdminAuthorization(f, adderUsrName, adderPswd))
                {
                    this.logger.logError("addModerator: unauthorized user");
                    throw new UnauthorizedUserException("No permission to add a moderator");
                }
               
                if (! sf.addModerator(modUserName, db))
                    throw new Exception("unknown error");
                return true;
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("addModerator: forum not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (SubForumNotFoundException e)
            {
                this.logger.logError("addModerator: subForum not found");
                throw new SubForumNotFoundException("SubForum not found");
            }
            catch (Exception e)
            {
                this.logger.logError("addModerator: unknown error");
                throw new Exception("Unknown error");
            }

                    
        }

        public bool removeModerator(string modUserName, int forumId, int subForumId, string rmverUsrName, string rmverPswd)
        {
            this.logger.logAction("performing removeModerator: modUserName: " + modUserName +
                                                            "\tforumId: " + forumId +
                                                            "\tsubForumId: " + subForumId +
                                                            "\trmverUserName: " + rmverUsrName +
                                                            "\trmverPswd: " + rmverPswd);
            try
            {
                Forum f = this.getForum(forumId);
                SubForum sf = f.getSubForum(subForumId);
                if (!Security.checkSuperUserAuthorization(this, rmverUsrName, rmverPswd) &&
                    !Security.checkAdminAuthorization(f, rmverUsrName, rmverPswd))
                {
                    this.logger.logError("removeModerator: unauthorized user");
                    throw new UnauthorizedUserException();
                }
                
                if( !sf.removeModerator(modUserName, db))
                    throw new Exception("unknown error");
                return true;
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("removeModerator: forum not found");
                throw new ForumNotFoundException("Forum not found");
            }
            catch (SubForumNotFoundException e)
            {
                this.logger.logError("removeModerator: subForum not found");
                throw new SubForumNotFoundException("SubForumNotFound");
            }
            catch (UnauthorizedOperationException e)
            {
                this.logger.logError("removeModerator: Unauthorized operation");
                throw new UnauthorizedOperationException("Unauthorized operation!");
            }
            catch (Exception e)
            {
                this.logger.logError("removeModerator: unknown error");
                throw new Exception("Unknown error");
            }
        }

        public bool editDiscussion(int forumId, int subForumId, int discussionId, string userName, string password, string newContent)
        {
            this.logger.logAction("editDiscussion: userName: " + userName +
                                                "\tpassword: " + password +
                                                "\tforumId: " + forumId +
                                                "\tsubForumId: " + subForumId +
                                                "\tnew content: <not detailed in log>");

            if (!ContentPolicy.isLegalContent(ContentPolicy.cType.COMMENT_CONTENT, newContent))
            {
                this.logger.logError("EditDiscussion: Illegal content");
                throw new IllegalContentException("Illegal content");
            }

            try
            {
                Forum f = this.getForum(forumId);
                SubForum sf = f.getSubForum(subForumId);
                Discussion d = sf.getDiscussion(discussionId);
                // check authorization
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkAdminAuthorization(f, userName, password) &&
                    !Security.checkModeratorAuthorization(sf, userName, password) &&
                    !Security.checkPublisherAuthorization(d, userName, password))
                {
                    this.logger.logError("editDiscussion: unauthorized user");
                    throw new UnauthorizedUserException();
                } // if

                return d.editDiscussion(newContent, db);
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("editDiscussion: forum not found");
                throw new ForumNotFoundException("Forum not found");
            }
            catch (SubForumNotFoundException e)
            {
                this.logger.logError("editDiscussion: subForum not found");
                throw new SubForumNotFoundException("SubForumNotFound");
            }
            catch (DiscussionNotFoundException e)
            {
                this.logger.logError("editDiscussion: discussion not found");
                throw new DiscussionNotFoundException("Discussion not found");
            }
            catch (Exception e)
            {
                this.logger.logError("editDiscussion: unknown error");
                throw new Exception("Unknown error");
            }
        }

        public int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName)
        {
            this.logger.logAction("performing getNumOfCommentsSingleUser: reqUserName: " + reqUserName +
                                                        "\treqPswd: " + reqPswd +
                                                        "\tforumId: " + forumId +
                                                        "\tuserName: " + userName);
            try
            {
                Forum forum = this.getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, reqUserName, reqPswd) &&
                    !Security.checkAdminAuthorization(forum, reqUserName, reqPswd))
                {
                    this.logger.logError("getNumOfCommentsSingleUser: unauthorized user");
                    throw new UnauthorizedUserException();
                }

                return forum.getNumOfCommentsSingleUser(userName);
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("getNumOfCommentsSingleUser: forum not found");
                throw new ForumNotFoundException("Forum not found");
            }
            catch (UserNotFoundException e)
            {
                this.logger.logError("getNumOfCommentsSingleUser: user not found");
                throw new UserNotFoundException("User not found");
            }
            catch (Exception e)
            {
                this.logger.logError("getNumOfCommentsSingleUser: unknown error");
                throw new Exception("Unknown error");
            }

        }

        public List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName)
        {
            this.logger.logAction("performing getResponsersForSingleUser: reqUserName: " + reqUserName +
                                                        "\treqPswd: " + reqPswd +
                                                        "\tforumId: " + forumId +
                                                        "\tmemberUserName: " + memberUserName);
            try
            {
                Forum forum = this.getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, reqUserName, reqPswd) &&
                    !Security.checkAdminAuthorization(forum, reqUserName, reqPswd))
                {
                    this.logger.logError("getResponsersForSingleUser: unauthorized user");
                    throw new UnauthorizedUserException();
                }

                return forum.getResponsersForSingleUser(memberUserName);
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("getResponsersForSingleUser: forum not found");
                throw new ForumNotFoundException("Forum not found");
            }
            catch (UserNotFoundException e)
            {
                this.logger.logError("getResponsersForSingleUser: user not found");
                throw new UserNotFoundException("User not found");
            }
            catch (Exception e)
            {
                this.logger.logError("getResponsersForSingleUser: unknown error");
                throw new Exception("Unknown error");
            }
        }

        public int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId)
        {
            this.logger.logAction("performing getNumOfCommentsSubForum: userName: " + userName +
                                                                     "\tpswd: " + pswd +
                                                                     "\tforumId: " + forumId +
                                                                     "\tsubForumId: " + subForumId);
            try
            {
                Forum forum = this.getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, userName, pswd) &&
                    !Security.checkAdminAuthorization(forum, userName, pswd)) 
                {
                    this.logger.logError("getNumOfCommentsSubForum: unauthorized user");
                    throw new UnauthorizedUserException();
                }

                return forum.getNumOfCommentsSubForum(subForumId);
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("getNumOfCommentsSubForum: forum not found");
                throw new ForumNotFoundException("Forum not found");
            }
            catch (SubForumNotFoundException e)
            {
                this.logger.logError("getNumOfCommentsSubForum: subForum not found");
                throw new SubForumNotFoundException("SubForumNotFound");
            }
            catch (Exception e)
            {
                this.logger.logError("getNumOfCommentsSubForum: unknown error");
                throw new Exception("Unknown error");
            }
        }

        public List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            this.logger.logAction("performing getMutualUsers: userName: " + userName +
                                                           "\tpassword: " + password +
                                                           "\tforumId1: " + forumId1 +
                                                           "\tforumId2: " + forumId2);
            try
            {
                Forum forum1 = this.getForum(forumId1);
                Forum forum2 = this.getForum(forumId2);
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                {
                    this.logger.logError("getMutualUsers: unauthorized user");
                    throw new UnauthorizedUserException();
                }
                List<User> ul = forum1.getMutualUsers(forum2);
                List<User> returnedList = new List<User>();
                foreach (User u in ul)
                {
                    returnedList.Add(new User(u));
                }
                return returnedList;
            }
            catch (ForumNotFoundException e)
            {
                this.logger.logError("getMutualUsers: forum not found");
                throw new ForumNotFoundException("Forum not found");
            }
            catch (Exception e)
            {
                this.logger.logError("getMutualUsers: unknown error");
                throw new Exception("Unknown error");
            }
        }

        public int getUserType(int forumId, string userName)
        {
            if (userName == this.superUser.userName)
                return (int)userTypes.SUPER_USER;
            else
                return this.getForum(forumId).getUserType(userName);
            
        }

        public int getUserType(int forumId, int subForumId, string userName)
        {
            this.logger.logAction("getUserType: userName: " + userName +
                                              "\tforumId: " + forumId +
                                              "\tsubForumId: " + subForumId);
                                    
            try
            {
                if (userName == this.superUser.userName)
                    return (int)userTypes.SUPER_USER;
                else
                    return this.getForum(forumId).getUserType(subForumId, userName);
            }
            catch (UserNotFoundException)
            {
                this.logger.logError("getUserType: User " + userName + " not found");
                throw new UserNotFoundException("User not found");
            }
            catch (ForumNotFoundException)
            {
                this.logger.logError("getUserType: forum not found");
                throw new ForumNotFoundException("forum not found");
            }
            catch (SubForumNotFoundException)
            {
                this.logger.logError("getUserType: subForum not found");
                throw new SubForumNotFoundException("SubForum not found");
            }
        }


        public bool collectLogs()
        {
            // throw exception on error
            this.logger.collectLogs();
            return true;
        }


        public bool collectLogs(string logFileName)
        {
            // throw exception on error
            this.logger.collectLogs(logFileName);
            return true;
        }

    }
}
