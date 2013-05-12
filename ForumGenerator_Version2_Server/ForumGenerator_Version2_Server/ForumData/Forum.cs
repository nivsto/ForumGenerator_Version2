using ForumGenerator_Version2_Server.Communication;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.Sys.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumGenerator_Version2_Server.ForumData
{
    public class Forum
    {
        internal int forumId { get; private set; }
        internal User admin { get; private set; }
        internal List<SubForum> subForums { get; private set; }
        internal string forumName { get; private set; }
        internal List<User> members { get; private set; }
        internal int nextSubForumId = 1;
        internal int nextUserId = 1;

        public Forum(int forumId, string forumName, string adminUserName, string adminPassword)
        {
            // TODO: Complete member initialization
            this.forumId = forumId;
            this.forumName = forumName;
            this.members = new List<User>();
            int userId = nextUserId++;
            this.admin = new User(userId, adminUserName, adminPassword, "", "", this);
            this.members.Add(this.admin);
            this.subForums = new List<SubForum>();
        }

        internal User login(string userName, string password)
        {
            User user = this.members.Find(
                            delegate(User mem)
                            { return mem.userName == userName; });
            if (user == null)
                throw new NullReferenceException("no user named " + userName);
            else
                return user.login(password);
        }

        internal bool logout(int userId)
        {
            return this.members.ElementAt(userId).logout();
        }

        internal User register(string userName, string password, string email, string signature)
        {
            if (this.members.Find(delegate(User mem) { return mem.userName == userName; }) != null)
                throw new UnauthorizedAccessException("username already exists");
            int userId = this.nextUserId++;
            User newUser = new User(userId, userName, password, email, signature, this);
            this.members.Add(newUser);
            return newUser;
        }

        internal int getSize()
        {
            return this.subForums.Count();
        }

        internal SubForum getSubForum(int subForumId)
        {
            try
            {
                return subForums.ElementAt(subForumId);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new SubForumNotFoundException();
            }
        }

        internal SubForum createNewSubForum(string subForumTitle)
        {
            if (this.subForums.Find(delegate(SubForum subfrm) { return subfrm.subForumTitle == subForumTitle; }) != null)
                throw new Exception();///////// change!
            int subForumId = this.nextSubForumId++;
            SubForum newSubForum = new SubForum(subForumId, subForumTitle, this);
            this.subForums.Add(newSubForum);
            return newSubForum;
        }

        public User getUser(int userId)
        {
            return this.members.ElementAt(userId);
        }

        public User getUser(string userName)
        {
            return this.members.Find(delegate(User mem) { return mem.userName == userName; });
        }

        internal User changeAdmin(int newAdminUserId)
        {
            User currentMember = getUser(newAdminUserId);
            if (currentMember == null)
                throw new UserNotFoundException();
            this.admin = new User(currentMember.memberID, currentMember.userName, currentMember.password, "", "", this);
            this.members.Insert(this.members.IndexOf(currentMember), this.admin);
            this.members.Remove(currentMember);
            return this.admin;
        }


        internal int getNumOfCommentsSingleUser(string userName)
        {
            int result = 0;
            User user = getUser(userName);
            if (user == null)
                throw new UserNotFoundException();

            foreach (SubForum sf in this.subForums)
            {
                result += sf.getNumOfCommentsSingleUser(user);
            }
            return result;
        }


        internal int getNumOfCommentsSubForum(int subForumId)
        {
            SubForum sf = getSubForum(subForumId);

            if (sf == null)
                throw new UserNotFoundException();
            return sf.getNumOfComments();
        }

        internal List<User> getResponsersForSingleUser(string userName)
        {
            List<User> responsers = new List<User>();
            User user = getUser(userName);
            if (user == null)
                throw new UserNotFoundException();

            foreach (SubForum sf in this.subForums)
            {
                responsers.Concat(sf.getResponsersForSingleUser(user));
            }
            return responsers;
        }


        internal List<User> getMutualUsers(Forum other)
        {
            List<User> mutuals = new List<User>();
            // Go all over other's members and for each one check
            // if he is a member in this forum.
            foreach (User user in other.members)
            {
                if (this.getUser(user.memberID) != null)
                {
                    mutuals.Add(user);
                }
            }
            return mutuals;
        }


        public int getUserType(int forumId, string userName)
        {
            User user = this.getUser(userName);
            if (user == null)
                throw new UserNotFoundException();
            if (admin.userName == userName)
                return (int)ForumGenerator_Version2_Server.Sys.ForumGenerator.userTypes.ADMIN;
            else
                return (int)ForumGenerator_Version2_Server.Sys.ForumGenerator.userTypes.MEMBER;
        }


        public int getUserType(int forumId, int subForumId, string userName)
        {
            User user = this.getUser(userName);
            if (user == null)
                throw new UserNotFoundException();
            SubForum sf = getSubForum(subForumId);
            if (sf == null)
                throw new SubForumNotFoundException();

            return sf.getUserType(userName);
        }
    }
}
