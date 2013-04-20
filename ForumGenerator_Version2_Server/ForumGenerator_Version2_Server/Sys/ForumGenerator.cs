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


        public ForumGenerator(string superUserName, string superUserPass)
        {
            this.superUser = new SuperUser(superUserName, superUserPass);
            this.forums = new List<Forum>();
        }

        // returns userid
        public Tuple<string, string> login(int forumId, string userName, string password)
        {
            try
            {
                return getForum(forumId).login(userName, password);
            }
            catch (NullReferenceException)
            {
                return new Tuple<string, string>("0", "No such forum");
            }
        }

        // returns 1 for success or 0 for failure
        public Tuple<string, string> logout(int forumId, int userId)
        {
            return getForum(forumId).logout(userId);
        }

        // returns userid
        public Tuple<string, string> adminLogin(string userName, string password)
        {
            return this.superUser.login(userName, password);
        }

        // returns 1 for success or 0 for failure
        public Tuple<string, string> adminLogout()
        {
            return this.superUser.logout();
        }

        // returns 1 for success or 0 for failure
        public Tuple<string, string> register(int forumId, string userName, string password, string email, string signature)
        {
            return getForum(forumId).register(userName, password, email, signature);
        }

        //returns an XML list of all the forums in the system
        public Tuple<string, string[], string[,]> getForums()
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
            return new Tuple<string, string[], string[,]>("Forum", properties, data);
        }

        //returns an XML list of all the sub-forums in the system
        public Tuple<string, string[], string[,]> getSubForums(int forumId)
        {
            Forum parentForum = this.getForum(forumId);
            return parentForum.getSubForumsXML();
        }

        //returns an XML list of all the discussions in a specific sub-forum
        public Tuple<string, string[], string[,]> getDiscussions(int forumId, int subForumId)
        {
            SubForum parentSubForum = this.getForum(forumId).getSubForum(subForumId);
            return parentSubForum.getDiscussionsXML();
        }

        //returns an XML list of all the comments of a specific discussion
        public Tuple<string, string[], string[,]> getComments(int forumId, int subForumId, int discussionId)
        {
            Discussion parentDiscussion = this.getForum(forumId).getSubForum(subForumId).getDiscussion(discussionId);
            return parentDiscussion.getCommentsXML();
        }

        //returns an XML list of all the users in a specific forum
        public Tuple<string, string[], string[,]> getUsers(int forumId)
        {
            Forum parentForum = this.getForum(forumId);
            return parentForum.getUsers();
        }

        //creates a new forum and a new user which is the forum's administrator
        public Tuple<string, string> createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
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

        //creates a new sub-forum and a new user which is the forum's administrator
        public Tuple<string, string> createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            Forum forum = getForum(forumId);
            if (!Security.checkAdminAuthorization(forum, userName, password))
                return new Tuple<string, string>("0", "no permission");
            return forum.createNewSubForum(subForumTitle);
        }

        //creates a new discussion and a new user which is the forum's administrator
        public Tuple<string, string> createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            Forum forum = getForum(forumId);
            if (!Security.checkMemberAuthorization(forum, userName, password))
                return new Tuple<string, string>("0", "no permission");
            Member user = getForum(forumId).getUser(userName);
            return getForum(forumId).getSubForum(subForumId).createNewDiscussion(title, content, user);
        }

        //creates a new comment and a new user which is the forum's administrator
        public Tuple<string, string> createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            Forum forum = getForum(forumId);
            if (!Security.checkMemberAuthorization(forum, userName, password))
                return new Tuple<string, string>("0", "no permission");
            Member user = getForum(forumId).getUser(userName);
            return getForum(forumId).getSubForum(subForumId).getDiscussion(discussionId).createNewComment(content, user);
        }

        //creates a new comment and a new user which is the forum's administrator
        public Tuple<string, string> changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            if (!Security.checkSuperUserAuthorization(this, userName, password))
                return new Tuple<string, string>("0", "no permission");
            return getForum(forumId).changeAdmin(newAdminUserId);
        }

        //get a forum by its forumId
        public Forum getForum(int forumId)
        {
            return forums.ElementAt(forumId);
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
