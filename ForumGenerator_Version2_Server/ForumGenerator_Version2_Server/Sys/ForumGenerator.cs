using ForumGenerator_Version2_Server.DataLayer;
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
        internal ContentPolicy cp;
        internal HashSet<string> stopWords { get; private set; }


        public ForumGenerator(string superUserName, string superUserPass)
        {
            this.db = new ForumGeneratorContext("ForumGenerator_DB10");
            this.superUser = new SuperUser(superUserName, superUserPass);
            this.forums = new List<Forum>();
            this.logger = new Logger();
            this.db.Database.ExecuteSqlCommand("UPDATE Users SET isLoggedIn = 0 WHERE 1=1");
            this.db.SaveChanges();
            this.cp = new ContentPolicy();
            this.stopWords = TextClassifier.getStopWords("DefaultStopWords.txt");
        }

        public ForumGenerator(string superUserName, string superUserPass, bool test)
        {
            this.db = new ForumGeneratorContext("ForumGenerator_DB10_TEST");
            this.superUser = new SuperUser(superUserName, superUserPass);
            this.forums = new List<Forum>();
            this.logger = new Logger();
            this.db.Database.ExecuteSqlCommand("DELETE FROM Comments");
            this.db.Database.ExecuteSqlCommand("DELETE FROM Discussions");
            this.db.Database.ExecuteSqlCommand("DELETE FROM Moderators");
            this.db.Database.ExecuteSqlCommand("DELETE FROM Words");
            this.db.Database.ExecuteSqlCommand("DELETE FROM SubForums");
            this.db.Database.ExecuteSqlCommand("DELETE FROM Fora");
            this.db.Database.ExecuteSqlCommand("DELETE FROM Users");
            this.db.Database.ExecuteSqlCommand("DELETE FROM UserFriends");
            this.cp = new ContentPolicy();
            this.stopWords = TextClassifier.getStopWords("DefaultStopWords.txt");
        }


        public void reset()
        {
            lock (db)
            {
                this.superUser = new SuperUser(this.superUser.userName, this.superUser.password);
                this.forums = new List<Forum>();
                this.logger.logAction("\n\n\n\t ####################    R E S E T    ####################n\n\n");
                //this.logger.closeFile();
               // this.logger = new Logger();
                this.cp.init();
                this.db.Database.ExecuteSqlCommand("DELETE FROM Comments");
                this.db.Database.ExecuteSqlCommand("DELETE FROM Discussions");
                this.db.Database.ExecuteSqlCommand("DELETE FROM Moderators");
                this.db.Database.ExecuteSqlCommand("DELETE FROM Words");
                this.db.Database.ExecuteSqlCommand("DELETE FROM SubForums");
                this.db.Database.ExecuteSqlCommand("DELETE FROM Fora");
                this.db.Database.ExecuteSqlCommand("DELETE FROM Users");
                this.db.Database.ExecuteSqlCommand("DELETE FROM UserFriends");
                this.db.SaveChanges();
            }
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
                                                  "\tpassword: <not detailed>");
                lock (db)
                {
                    return new User(getForum(forumId).login(userName, password, db));
                }
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
                                        "\tpassword: <not detailed>");
                lock (db)
                {
                    User loggedOutUser = getForum(forumId).logout(userName, password, db);
                }

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
                                                           "\tpassword: <not detailed>");
                lock (db)
                {
                    return this.superUser.login(userName, password);
                }
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
                                                "\tpassword: <not detailed>");
                lock (db)
                {
                    return this.superUser.logout(userName, password);
                }
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
                                                            "\tpassword: <not detailed>" +
                                                            "\temail: " + email +
                                                            "\tsignature: " + signature);
                this.cp.checkLegalContent(ContentPolicy.cType.USER_NAME, userName);
                this.cp.checkLegalEmailFormat(email);
                this.cp.checkLegalContent(ContentPolicy.cType.MEMBER_SIGNATURE, signature);
            
                lock (db)
                {
                    return new User(this.getForum(forumId).register(userName, password, email, signature, db));
                }
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
                                                               "\tsubForumId: " + subForumId);

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
                                                            "\tsubForumId: " + subForumId +
                                                            "\tdiscussionId: " + discussionId);

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
        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, 
                                    string adminPassword, ForumGenerator_Version2_Server.ForumData.Forum.RegPolicy registrationPolicy)
        {
            try
            {
                this.logger.logAction("performing createNewForum:  userName: " + userName +
                                                              "\tpassword: <not detailed>" +
                                                              "\tforumName: " + forumName +
                                                              "\tadminUserName: " + adminUserName +
                                                              "\tadminPassword: " + adminPassword);
                lock (db)
                {
                    this.cp.checkLegalContent(ContentPolicy.cType.FORUM_NAME, forumName);
                    // Check if forum name already exist
                    if (this.isForumNameExist(forumName))
                        throw new UnauthorizedOperationException(ForumGeneratorDefs.EXIST_FNAME);
                    if (!Security.checkSuperUserAuthorization(this, userName, password))
                    {
                        throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_SUPERUSER);
                    }
                    Forum newForum;
                    forumName = this.cp.censor(forumName);

                    newForum = new Forum(forumName, adminUserName, adminPassword, this.db, registrationPolicy);
                    this.forums.Add(newForum);
                    this.db.Forums.Add(newForum);
                    this.db.SaveChanges();
                    return new Forum(newForum);
                }
                
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
                                                                  "\tpassword: <not detailed>" +
                                                                  "\tforumId: " + forumId +
                                                                  "\tsubForumTitle: " + subForumTitle);

                this.cp.checkLegalContent(ContentPolicy.cType.SUBFORUM_TITLE, subForumTitle);
                lock (db)
                {
                    Forum forum = getForum(forumId);
                    if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                        !Security.checkAdminAuthorization(forum, userName, password))
                    {
                        throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                    }
                    subForumTitle = this.cp.censor(subForumTitle);
                    return new SubForum(forum.createNewSubForum(subForumTitle, db));
                }
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
                                                                     "\tpassword: <not detailed>" +
                                                                     "\tforumId: " + forumId +
                                                                     "\tsubForumId: " + subForumId +
                                                                     "\ttitle: " + title +
                                                                     "\tcontent: " + content);

                this.cp.checkLegalContent(ContentPolicy.cType.DISCUSSION_TITLE, title);
                this.cp.checkLegalContent(ContentPolicy.cType.DISCUSSION_CONTENT, content);

                lock (db)
                {
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
                    title = this.cp.censor(title);
                    content = this.cp.censor(content);
                    user = forum.getUser(userName);
                    return new Discussion(sf.createNewDiscussion(title, content, user, db, stopWords));
                }
            }
            catch (Exception e)
            {
                this.logger.logError("createNewDiscussion: " + e.Message);
                throw e;
            }
        }


        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            try
            {


                this.logger.logAction("performing createNewComment:     userName: " + userName +             
                                                                     "\tpassword: <not detailed>" +
                                                                     "\tforumId: " + forumId +
                                                                     "\tsubForumId: " + subForumId +
                                                                     "\tdiscussionId: " + discussionId +
                                                                     "\tcontent: " + content);
              
                this.cp.checkLegalContent(ContentPolicy.cType.COMMENT_CONTENT, content);

                lock (db)
                {
                    Forum forum = getForum(forumId);
                    SubForum sf = forum.getSubForum(subForumId);
                    Discussion d = sf.getDiscussion(discussionId);
                    if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                        !Security.checkAdminAuthorization(forum, userName, password) &&
                        !Security.checkMemberAuthorization(forum, userName, password))
                    {
                        throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                    }
                    sf.checkRelevantContent(content, stopWords);
                    content = this.cp.censor(content);
                    User user = forum.getUser(userName);
                    return new Comment(d.createNewComment(content, user, db));
                }
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
                                                "\tpassword: <not detailed>" +
                                                "\tforumId: " + forumId +
                                                "\tsubForumId: " + subForumId);

                Forum f = this.getForum(forumId);
                // check authorization
                if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                    !Security.checkAdminAuthorization(f, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }
                lock (db)
                {
                    f.removeSubForum(subForumId);
                }
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
                                                              "\tpassword: <not detailed>" +
                                                              "\tforumId: " + forumId +
                                                              "\tsubForumId: " + subForumId +
                                                              "\tdiscussionId: " + discussionId);

                lock (db)
                {
                    Forum f = this.getForum(forumId);
                    SubForum sf = f.getSubForum(subForumId);
                    Discussion d = sf.getDiscussion(discussionId);
                    // check authorization
                    if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                        !Security.checkAdminAuthorization(f, userName, password) &&
                        !Security.checkModeratorAuthorization(sf, userName, password, Moderator.modLevel.DEL) &&
                        !Security.checkModeratorAuthorization(sf, userName, password, Moderator.modLevel.ALL) &&
                        !Security.checkPublisherAuthorization(d, userName, password))
                    {
                        throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                    }
                    sf.removeDiscussion(discussionId, db);
                    return true;
                }
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
                                                        "\tpassword: <not detailed>" +
                                                        "\tforumId: " + forumId +
                                                        "\tnewAdminUserId: " + newAdminUserId);

                if (!Security.checkSuperUserAuthorization(this, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }
                lock (db)
                {
                    return new User(this.getForum(forumId).changeAdmin(newAdminUserId, db));
                }
            }
            catch (Exception e)
            {
                this.logger.logError("changeAdmin: " + e.Message);
                throw e;
            }
        }

        //get a forum by its forumId 
        public Forum getForum(int forumId)
        {
            try
            {
                Forum forum = this.db.Forums.Find(forumId);
                if (forum == null)
                    throw new ForumNotFoundException(ForumGeneratorDefs.FORUM_NF);
                return forum;
            }
            catch (Exception)
            {
                throw new ForumNotFoundException(ForumGeneratorDefs.DISCUSSION_NF);
            }
        }


        internal bool isForumNameExist(string forumName)
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


        public bool addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd, ForumGenerator_Version2_Server.Users.Moderator.modLevel level)
        {
            try
            {
                this.logger.logAction("performing addModerator: modUserName: " + modUserName +
                                                         "\tforumId: " + forumId +
                                                         "\tsubForumId: " + subForumId +
                                                         "\tadderUserName: " + adderUsrName +
                                                         "\tadderPswd: " + adderPswd);
                lock (db)
                {
                    Forum f = this.getForum(forumId);
                    SubForum sf = f.getSubForum(subForumId);
                    if (!Security.checkSuperUserAuthorization(this, adderUsrName, adderPswd) &&
                        !Security.checkAdminAuthorization(f, adderUsrName, adderPswd))
                    {
                        throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                    }
                    sf.addModerator(modUserName, db, level);
                    return true;
                }
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
                lock (db)
                {
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
                                                "\tpassword: <not detailed>" +
                                                "\tforumId: " + forumId +
                                                "\tdiscussionId: " + discussionId +
                                                "\tsubForumId: " + subForumId +
                                                "\tnew content: <not detailed in log>");

                this.cp.checkLegalContent(ContentPolicy.cType.COMMENT_CONTENT, newContent);

                lock (db)
                {
                    Forum f = this.getForum(forumId);
                    SubForum sf = f.getSubForum(subForumId);
                    Discussion d = sf.getDiscussion(discussionId);
                    // check authorization
                    if (!Security.checkSuperUserAuthorization(this, userName, password) &&
                        !Security.checkAdminAuthorization(f, userName, password) &&
                        !Security.checkModeratorAuthorization(sf, userName, password, Moderator.modLevel.EDIT ) &&
                        !Security.checkModeratorAuthorization(sf, userName, password, Moderator.modLevel.ALL) &&
                        !Security.checkPublisherAuthorization(d, userName, password))
                    {
                        throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                    }

                    newContent = this.cp.censor(newContent);
                    sf.checkRelevantContent(newContent, stopWords);
                    return new Discussion(d.editDiscussion(newContent, db));
                }
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
                lock (db)
                {
                    Forum forum = this.getForum(forumId);
                    if (!Security.checkSuperUserAuthorization(this, userName, pswd) &&
                        !Security.checkAdminAuthorization(forum, userName, pswd))
                    {
                        throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                    }

                    return forum.getNumOfCommentsSubForum(subForumId);
                }
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
                                                           "\tpassword: <not detailed>" +
                                                           "\tforumId1: " + forumId1 +
                                                           "\tforumId2: " + forumId2);

                Forum forum1 = this.getForum(forumId1);
                Forum forum2 = this.getForum(forumId2);
                if (!Security.checkSuperUserAuthorization(this, userName, password))
                {
                    throw new UnauthorizedUserException(ForumGeneratorDefs.UNAUTH_USER);
                }
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


        public List<Moderator> getModerators(int forumId, int subForumId)
        {
            this.logger.logAction("performing getModerators: \tforumId: " + forumId +
                                                            "\tsubForumId: " + subForumId);
            try
            {
                List<Moderator> ml = getForum(forumId).getSubForum(subForumId).moderators;
                List<Moderator> returnedList = new List<Moderator>();
                foreach (Moderator m in ml)
                {
                    returnedList.Add(new Moderator(m));
                }
                return returnedList;
            }
            catch (Exception e)
            {
                this.logger.logError("getModerators: " + e.Message);
                throw e;
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
            this.logger.logAction("performing getUserType: userName: " + userName +
                                              "\tforumId: " + forumId +
                                              "\tsubForumId: " + subForumId);
            if (userName == this.superUser.userName)
                return (int)userTypes.SUPER_USER;
            else
                return this.getForum(forumId).getUserType(subForumId, userName);
        }

        //#Niv - For web purposes
        public int countDiscussionsPerForum(int forumId)
        {
            int ans = 0;
            List<SubForum> subForums = getForum(forumId).subForums;
            foreach (SubForum sf in subForums)
            {
                ans += sf.discussions.Count();
            }

            return ans;
        }

        //#Niv - For web purposes
        public int countSubForumsPerForum(int forumId)
        {
            return getForum(forumId).subForums.Count();
        }

        //#Niv - For web purposes
        public int countDiscussionsPerSubForum(int forumId, int subForumId)
        {
            return getForum(forumId).getSubForum(subForumId).discussions.Count();
        }

        //#Niv - For web purposes
        public int countCommentsPerSubForum(int forumId, int subForumId)
        {
            int ans = 0;
            List<Discussion> discussions = getForum(forumId).getSubForum(subForumId).discussions;
            foreach (Discussion d in discussions)
            {
                ans += d.comments.Count();
            }

            return ans;
        }

        public int countCommentsPerDiscussion(int forumId, int subForumId, int discussionId)
        {
            return getForum(forumId).getSubForum(subForumId).getDiscussion(discussionId).comments.Count();
        }

        public bool confirmUser(int forumId, string userName)
        {
            this.logger.logAction("performing confirmUser: forumId: " + forumId +
                                                        "\tuserName: " + userName);
            try{
                Forum forum = this.getForum(forumId);
                User user = forum.getUser(userName);
                user.isConfirmed = true;
                db.Entry(db.Users.Find(user.memberID)).CurrentValues.SetValues(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception e){
                this.logger.logError("confirmUser: " + e.Message);
                return false;
            }
        }

        public List<User> getUnconfirmedUsers(int forumId)
        {
            this.logger.logAction("performing getUnconfirmedUsers: forumId: " + forumId);
            try
            {
                return this.getForum(forumId).members.Where(u => u.isConfirmed == false).ToList();
            }
            catch (Exception e)
            {
                this.logger.logError("getUnconfirmedUsers: " + e.Message);
                throw e;
            }
        }



        public bool changeModLevel(int forumId, int subForumId, string moderatorName, Moderator.modLevel newLevel)
        {
            this.logger.logAction("performing changeModLevel: forumId: " + forumId +
                                                           "\tsubForumId: " + subForumId +
                                                           "\tmoderatorName: " + moderatorName +
                                                           "\tnewLevel: " + newLevel.ToString());
            try
            {
                return this.getForum(forumId).getSubForum(subForumId).changeModLevel(forumId, subForumId, moderatorName, newLevel, this.db);
            }
            catch (Exception e)
            {
                this.logger.logError("changeModLevel: " + e.Message);
                throw e;
            }
        }
    }

}// End of class
