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
    class ForumGenerator
    {
        internal SuperUser superUser;
        internal List<Forum> forums;


        public ForumGenerator(string superUserName, string superUserPass)
        {
            this.superUser = new SuperUser(superUserName, superUserPass);
            this.forums = new List<Forum>();
        }

        // returns userid
        public Tuple<int,string> login(int forumId, string userName, string password)
        {
            return getForum(forumId).login(userName, password);
        }

        // returns 1 for success or 0 for failure
        public Tuple<int, string> logout(int forumId, int userId)
        {
            return getForum(forumId).logout(userId);
        }

        // returns userid
        public Tuple<int, string> adminLogin(string userName, string password)
        {
            return this.superUser.login(userName, password);
        }

        // returns 1 for success or 0 for failure
        public Tuple<int, string> adminLogout()
        {
            return this.superUser.logout();
        }

        // returns 1 for success or 0 for failure
        public Tuple<int, string> register(int forumId, string userName, string password, string email, string signature)
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
        public Tuple<int, string> createNewForum(int userId, string forumName, string adminUserName, string adminPassword)
        {
            if (this.forums.Find(delegate(Forum frm) { return frm.forumName == forumName; }) != null)
                return new Tuple<int, string>(0, "forum name already exists");
            int forumId = this.forums.Count();
            Forum newForum = new Forum(forumId, forumName, adminUserName, adminPassword);
            this.forums.Add(newForum);
            return new Tuple<int, string>(1, newForum.getForumId().ToString());
        }

        //creates a new sub-forum and a new user which is the forum's administrator
        public Tuple<int, string> createNewSubForum(int userId, int forumId, string subForumTitle)
        {
            return getForum(forumId).createNewSubForum(subForumTitle);
        }

        //creates a new discussion and a new user which is the forum's administrator
        public Tuple<int, string> createNewDiscussion(int userId, int forumId, int subForumId, string title, string content)
        {
            Member user = getForum(forumId).getUser(userId);
            return getForum(forumId).getSubForum(subForumId).createNewDiscussion(title, content, user);
        }

        //creates a new comment and a new user which is the forum's administrator
        public Tuple<int, string> createNewComment(int userId, int forumId, int subForumId, int discussionId, string content)
        {
            Member user = getForum(forumId).getUser(userId);
            return getForum(forumId).getSubForum(subForumId).getDiscussion(discussionId).createNewComment(content, user);
        }

        //get a forum by its forumId
        internal Forum getForum(int forumId)
        {
            return forums.ElementAt(forumId);
        }
    }
}
