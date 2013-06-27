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
            this.logger.closeFile();
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

       
        public User login(int forumId, string userName, string password)
        {
            try
            {
                this.logger.logAction("performing login: forumId: " + forumId + 
                                                  "\tuserName: " + userName + 
                                                  "\tpassword: " + password);

                return new User(getForum(forumId).login(userName, password, db));
            }
            catch (Exception e)
            {
                this.logger.logError("login: " + e.Message);
                throw e;
            }
        }


        public bool logout(int forumId, string userName, string password)
        {
            try
            {
                this.logger.logAction("performing logout: forumId: " + forumId +
                                        "\tuserName: " + userName +
                                        "\tpassword: " + password);

                User loggedOutUser = getForum(forumId).logout(userName, password, db);
                return true;
            }
            catch (Exception e)
            {
                this.logger.logError("logout: " + e.Message);
                throw e;
            }
        }


        public SuperUser superUserLogin(string userName, string password)
        {
            try
            {
                this.logger.logAction("performing superUserLogin: userName: " + userName +
                                                           "\tpassword: " + password);

                return this.superUser.login(userName, password);
            }
            catch (Exception e)
            {
                this.logger.logError("superUserLogin: " + e.Message);
                throw e;
            }
        }

        public bool superUserLogout(string userName, string password)
        {
            try
            {
                this.logger.logAction("performing superUserLogout: userName: " + userName +
                                                "\tpassword: " + password);

                return this.superUser.logout(userName, password);
            }
            catch (Exception e)
            {
                this.logger.logError("superUserLogout: " + e.Message);
                throw e;
            }
        }


        public User register(int forumId, string userName, string password, string email, string signature)
        {
            try
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
                    throw new IllegalContentException(ForumGeneratorDefs.ILL_CONTENT);
                }

                return new User(this.getForum(forumId).register(userName, password, email, signature, db));
            }
            catch (Exception e)
            {
                this.logger.logError("register: " + e.Message);
                throw e;
            }
        }


        public List<Forum> getForums()
        {
            try
            {
                this.logger.logAction("performing getForums");

                List<Forum> fl = this.db.Forums.ToList();
                List<Forum> returnedList = new List<Forum>();
                foreach (Forum f in fl)
                {
                    returnedList.Add(new Forum(f));
                }
                return returnedList;
                //return this.forums;
            }
            catch (Exception e)
            {
                this.logger.logError("getForums: " + e.Message);
                throw e;
            }
        }


        public List<SubForum> getSubForums(int forumId)
        {
            try
            {
                this.logger.logAction("performing getSubForums: forunId: " + forumId);

                Forum f = getForum(forumId);
                List<SubForum> sfl = f.subForums;
                List<SubForum> returnedList = new List<SubForum>();
                foreach (SubForum sf in sfl)
                {
                    returnedList.Add(new SubForum(sf));
                }
                return returnedList;
             }
            catch (Exception e)
            {
                this.logger.logError("getSubForums: " + e.Message);
                throw e;
            }
        }


        public List<Discussion> getDiscussions(int forumId, int subForumId)
        {
            try
            {
                this.logger.logAction("performing getDiscussions: forumId: " + forumId +
                                                  "subForumId: " + subForumId);

                List<Discussion> dl = this.getForum(forumId).getSubForum(subForumId).discussions;
                List<Discussion> returnedList = new List<Discussion>();
                foreach (Discussion d in dl)
                {
                    returnedList.Add(new Discussion(d));
                }
                return returnedList;
            }
            catch (Exception e)
            {
                this.logger.logError("getDiscussions: " + e.Message);
                throw e;
            }
        }


        public List<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            try
            {
                this.logger.logAction("performing getComments: forumId: " + forumId +
                                                  "subForumId: " + subForumId +
                                                " discussionId: " + discussionId);

                List<Comment> cl = this.getForum(forumId).getSubForum(subForumId).getDiscussion(discussionId).comments;
                List<Comment> returnedList = new List<Comment>();
                foreach (Comment c in cl)
                {
                    returnedList.Add(new Comment(c));
                }
                return returnedList;
            }
            catch (Exception e)
            {
                this.logger.logError("getComments: " + e.Message);
                throw e;
            }
        }


        public List<User> getUsers(int forumId)
        {
            try
            {
                this.logger.logAction("performing getUsers: forumId: " + forumId);

                List<User> ul = this.getForum(forumId).members;
                List<User> returnedList = new List<User>();
                foreach (User u in ul)
                {
                    returnedList.Add(new User(u));
                }
                return returnedList;
            }
            catch (Exception e)
            {
                this.logger.logError("getUsers: " + e.Message);
                throw e;
            }
        }

        //creates a new forum and a new user which is the forum's administrator
        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            try
            {
                this.logger.logAction("performing createNewForum:  userName: " + userName +
                                                              "\tpassword: " + password +
                                                              "\tforumName: " + forumName +
                                                              "\tadminUserName: " + adminUserName +
                                                              "\tadminPassword: " + adminPassword);

                if (!ContentPolicy.isLegalContent(ContentPolicy.cType.USER_NAME, userName) ||
                    !ContentPolicy.isLegalContent(ContentPolicy.cType.PASSWORD, password) ||
                    !ContentPolicy.isLegalContent(ContentPolicy.cType.FORUM_NAME, forumName) ||
                    !ContentPolicy.isLegalContent(ContentPolicy.cType.USER_NAME, adminUserName) ||
                    !ContentPolicy.isLegalContent(ContentPolicy.cType.PASSWORD, adminPassword))
                {
                    throw new IllegalContentException(ForumGeneratorDefs.ILL_CONTENT);
                }

                // Check if forum name already exist
                if (this.isForumNameExist(forumName))
                    throw new UnauthorizedOperationException(ForumGeneratorDefs.EXIST_FNAME);
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_SUPERUSER);
                }
                Forum newForum;
                lock (db)
                {
                    newForum = new Forum(forumName, adminUserName, adminPassword, this.db);
                    this.forums.Add(newForum);
                    this.db.Forums.Add(newForum);
                    this.db.SaveChanges();
                }
                return new Forum(newForum);
            }
            catch (Exception e)
            {
                this.logger.logError("createNewForum: " + e.Message);
                throw e;
            }
        }

        //creates a new sub-forum and a new user which is the forum's administrator
        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            try
            {
                this.logger.logAction("performing createNewSubForum: userName: " + userName +
                                                                  "\tpassword: " + password +
                                                                  "\tforumId: " + forumId +
                                                                  "\tsubForumTitle: " + subForumTitle);

                if (!ContentPolicy.isLegalContent(ContentPolicy.cType.USER_NAME, userName) ||
                    !ContentPolicy.isLegalContent(ContentPolicy.cType.PASSWORD, password) ||
                    !ContentPolicy.isLegalContent(ContentPolicy.cType.SUBFORUM_TITLE, subForumTitle) )
                {
                    throw new IllegalContentException(ForumGeneratorDefs.ILL_CONTENT);
                }

                Forum forum = getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkAdminAuthorization(forum, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }
                return new SubForum(forum.createNewSubForum(subForumTitle, db));
            }
            catch (Exception e)
            {
                this.logger.logError("createNewSubForum: " + e.Message);
                throw e;
            }
        }

        //creates a new discussion and a new user which is the forum's administrator
        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            try
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
                    throw new IllegalContentException(ForumGeneratorDefs.ILL_CONTENT);
                }
            
                Forum forum = getForum(forumId);
                SubForum sf = forum.getSubForum(subForumId);
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkMemberAuthorization(forum, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }
                User user;
                // if(isSuperUser(userName, password)
                //    currently not supported.
                    user = forum.getUser(userName);
                return new Discussion(sf.createNewDiscussion(title, content, user, db));
            }
            catch (Exception e)
            {
                this.logger.logError("createNewDiscussion: " + e.Message);
                throw e;
            }
        }

        //creates a new comment and a new user which is the forum's administrator
        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            try
            {
                if (!ContentPolicy.isLegalContent(ContentPolicy.cType.USER_NAME, userName) ||
                   !ContentPolicy.isLegalContent(ContentPolicy.cType.PASSWORD, password) ||
                   !ContentPolicy.isLegalContent(ContentPolicy.cType.COMMENT_CONTENT, content) )
                {
                    throw new IllegalContentException(ForumGeneratorDefs.ILL_CONTENT);
                }

                this.logger.logAction("performing createNewComment: "); // TODO add content

                Forum forum = getForum(forumId);
                SubForum sf = forum.getSubForum(subForumId);
                Discussion d = sf.getDiscussion(discussionId);
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkAdminAuthorization(forum, userName, password) &&
                    !Security.checkMemberAuthorization(forum, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }

                // if(isSuperUser(userName, password)
                //    currently not supported.
                User user = forum.getUser(userName);
                return new Comment(d.createNewComment(content, user, db));
            }
            catch (Exception e)
            {
                this.logger.logError("createNewComment: " + e.Message);
                throw e;
            }
        }


        public bool removeSubForum(int forumId, int subForumId, string userName, string password)
        {
            try
            {
                this.logger.logAction("removeSubForum: userName: " + userName +
                                                "\tpassword: " + password +
                                                "\tforumId: " + forumId +
                                                "\tsubForumId: " + subForumId);

                Forum f = this.getForum(forumId);
                // check authorization
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkAdminAuthorization(f, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }

                f.removeSubForum(subForumId);
                return true;
            }
            catch (Exception e)
            {
                this.logger.logError("removeSubForum: " + e.Message);
                throw e;
            }
        }


        // This method never returns false, but throws Exceptions instead.
        public bool deleteDiscussion(int forumId, int subForumId, int discussionId,
                     string userName, string password)
        {
            try
            {
                this.logger.logAction("performing deleteDiscussion: userName: " + userName +
                                                              "\tpassword: " + password +
                                                              "\tforumId: " + forumId +
                                                              "\tsubForumId: " + subForumId +
                                                              "\tdiscussionId: " + discussionId);

                Forum f = this.getForum(forumId);
                SubForum sf = f.getSubForum(subForumId);
                Discussion d = sf.getDiscussion(discussionId);
                // check authorization
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkAdminAuthorization(f, userName, password) &&
                    !Security.checkModeratorAuthorization(sf, userName, password) &&
                    !Security.checkPublisherAuthorization(d, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }
                sf.removeDiscussion(discussionId, db);
                return true;
            }
            catch (Exception e)
            {
                this.logger.logError("deleteDiscussion: " + e.Message);
                throw e;
            }     
        }
        

        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            try
            {
                this.logger.logAction("performing changeAdmin: userName: " + userName +
                                                        "\tpassword: " + password +
                                                        "\tforumId: " + forumId +
                                                        "\tnewAdminUserId: " + newAdminUserId);

                if (!Security.checkSuperUserAuthorization(this, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }
                return new User(this.getForum(forumId).changeAdmin(newAdminUserId, db));
            }
            catch (Exception e)
            {
                this.logger.logError("changeAdmin: " + e.Message);
                throw e;
            }
        }

        //get a forum by its forumId 
        // #Asa how to throw exception
        public Forum getForum(int forumId)
        {
            Forum forum = this.db.Forums.Find(forumId);
            if(forum == null)
                throw new ForumNotFoundException(ForumGeneratorDefs.FORUM_NF);
            return forum;
        }


        public bool isForumNameExist(string forumName)
        {
            try
            {
                Forum forum = this.forums.Find(delegate(Forum f) { return f.forumName == forumName; });
                return (forum != null);
            }
            catch (Exception) { return false; }
        }


        public int getForumCount()
        {
            return this.forums.Count();
        }


        public bool addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            try
            {
                this.logger.logAction("performing addModerator: modUserName: " + modUserName +
                                                         "\tforumId: " + forumId +
                                                         "\tsubForumId: " + subForumId +
                                                         "\tadderUserName: " + adderUsrName +
                                                         "\tadderPswd: " + adderPswd);

                Forum f = this.getForum(forumId);
                SubForum sf = f.getSubForum(subForumId);
                if (!Security.checkSuperUserAuthorization(this, adderUsrName, adderPswd) &&
                    !Security.checkAdminAuthorization(f, adderUsrName, adderPswd))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }
                sf.addModerator(modUserName, db);
                return true;
            }
            catch (Exception e)
            {
                this.logger.logError("addModerator: " + e.Message);
                throw e;
            }
        }

        public bool removeModerator(string modUserName, int forumId, int subForumId, string rmverUsrName, string rmverPswd)
        {
            try
            {
                this.logger.logAction("performing removeModerator: modUserName: " + modUserName +
                                                            "\tforumId: " + forumId +
                                                            "\tsubForumId: " + subForumId +
                                                            "\trmverUserName: " + rmverUsrName +
                                                            "\trmverPswd: " + rmverPswd);

                Forum f = this.getForum(forumId);
                SubForum sf = f.getSubForum(subForumId);
                if (!Security.checkSuperUserAuthorization(this, rmverUsrName, rmverPswd) &&
                    !Security.checkAdminAuthorization(f, rmverUsrName, rmverPswd))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }

                sf.removeModerator(modUserName, db);
                return true;
            }
            catch (Exception e)
            {
                this.logger.logError("removeModerator: " + e.Message);
                throw e;
            }
        }

        public Discussion editDiscussion(int forumId, int subForumId, int discussionId, string userName, string password, string newContent)
        {
            try
            {
                this.logger.logAction("editDiscussion: userName: " + userName +
                                                "\tpassword: " + password +
                                                "\tforumId: " + forumId +
                                                "\tsubForumId: " + subForumId +
                                                "\tnew content: <not detailed in log>");

                if (!ContentPolicy.isLegalContent(ContentPolicy.cType.COMMENT_CONTENT, newContent))
                {
                    throw new IllegalContentException(ForumGeneratorDefs.ILL_CONTENT);
                }

                Forum f = this.getForum(forumId);
                SubForum sf = f.getSubForum(subForumId);
                Discussion d = sf.getDiscussion(discussionId);
                // check authorization
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkAdminAuthorization(f, userName, password) &&
                    !Security.checkModeratorAuthorization(sf, userName, password) &&
                    !Security.checkPublisherAuthorization(d, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }

                return new Discussion(d.editDiscussion(newContent, db));
            }
            catch (Exception e)
            {
                this.logger.logError("EditDiscussion: " + e.Message);
                throw e;
            }
        }

        public int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName)
        {
            try
            {
                this.logger.logAction("performing getNumOfCommentsSingleUser: reqUserName: " + reqUserName +
                                                        "\treqPswd: " + reqPswd +
                                                        "\tforumId: " + forumId +
                                                        "\tuserName: " + userName);

                Forum forum = this.getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, reqUserName, reqPswd) &&
                    !Security.checkAdminAuthorization(forum, reqUserName, reqPswd))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }

                return forum.getNumOfCommentsSingleUser(userName);
            }
            catch (Exception e)
            {
                this.logger.logError("getNumOfCommentsSingleUser: " + e.Message);
                throw e;
            }

        }

        public List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName)
        {
            try
            {
                this.logger.logAction("performing getResponsersForSingleUser: reqUserName: " + reqUserName +
                                                        "\treqPswd: " + reqPswd +
                                                        "\tforumId: " + forumId +
                                                        "\tmemberUserName: " + memberUserName);

                Forum forum = this.getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, reqUserName, reqPswd) &&
                    !Security.checkAdminAuthorization(forum, reqUserName, reqPswd))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }
                List<User> ul = forum.getResponsersForSingleUser(memberUserName);
                List<User> returnedList = new List<User>();
                foreach (User u in ul)
                {
                    returnedList.Add(new User(u));
                }
                return returnedList;
            }
            catch (Exception e)
            {
                this.logger.logError("getResponsersForSingleUser: " + e.Message);
                throw e;
            }
        }

        public int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId)
        {
            try
            {
                this.logger.logAction("performing getNumOfCommentsSubForum: userName: " + userName +
                                                                     "\tpswd: " + pswd +
                                                                     "\tforumId: " + forumId +
                                                                     "\tsubForumId: " + subForumId);

                Forum forum = this.getForum(forumId);
                if (!Security.checkSuperUserAuthorization(this, userName, pswd) &&
                    !Security.checkAdminAuthorization(forum, userName, pswd)) 
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }

                return forum.getNumOfCommentsSubForum(subForumId);
            }
            catch (Exception e)
            {
                this.logger.logError("getNumOfCommentsSubForum: " + e.Message);
                throw e;
            }
        }

        public List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            try
            {
                this.logger.logAction("performing getMutualUsers: userName: " + userName +
                                                           "\tpassword: " + password +
                                                           "\tforumId1: " + forumId1 +
                                                           "\tforumId2: " + forumId2);

                Forum forum1 = this.getForum(forumId1);
                Forum forum2 = this.getForum(forumId2);
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }
                // #Asa what is returnedList for ? deep copy ?
                List<User> ul = forum1.getMutualUsers(forum2);
                List<User> returnedList = new List<User>();
                foreach (User u in ul)
                {
                    returnedList.Add(new User(u));
                }
                return returnedList;
            }
            catch (Exception e)
            {
                this.logger.logError("getMutualUsers: " + e.Message);
                throw e;
            }
        }

        //#Asa throw exception
        public List<User> getModerators(int forumId, int subForumId)
        {
            List<User> ul = getForum(forumId).getSubForum(subForumId).moderators;
            List<User> returnedList = new List<User>();
            foreach (User u in ul)
            {
                returnedList.Add(new User(u));
            }
            return returnedList;
        }

        //#Asa throw exception
        public int getUserType(int forumId, string userName)
        {
            if (userName == this.superUser.userName)
                return (int)userTypes.SUPER_USER;
            else
                return this.getForum(forumId).getUserType(userName); 
        }

        //#Asa throw exception
        public int getUserType(int forumId, int subForumId, string userName)
        {
            this.logger.logAction("getUserType: userName: " + userName +
                                              "\tforumId: " + forumId +
                                              "\tsubForumId: " + subForumId);
            if (userName == this.superUser.userName)
                return (int)userTypes.SUPER_USER;
            else
                return this.getForum(forumId).getUserType(subForumId, userName);
        }

        //#Niv - For web purposes
        public int getDiscussionsPerForum(int forumId)
        {
            int ans = 0;
            List<SubForum> subForums = getForum(forumId).subForums;
            foreach (SubForum sf in subForums)
            {
                ans += sf.discussions.Count();
            }

            return ans;
        }


    }
}
