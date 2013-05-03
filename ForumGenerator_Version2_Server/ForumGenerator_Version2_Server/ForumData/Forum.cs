using ForumGenerator_Version2_Server.Communication;
using ForumGenerator_Version2_Server.Users;
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


        public Forum(int forumId, string forumName, string adminUserName, string adminPassword)
        {
            // TODO: Complete member initialization
            this.forumId = forumId;
            this.forumName = forumName;
            this.members = new List<User>();
            this.admin = new User(members.Count(), adminUserName, adminPassword, "", "", this);
            this.members.Add(this.admin);
            this.subForums = new List<SubForum>();
        }

        internal User login(string userName, string password)
        {
            User user = this.members.Find(
                            delegate(User mem)
                                {return mem.userName == userName;});
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
            User newUser = new User(this.members.Count(), userName, password, email, signature, this);
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
                throw new ArgumentOutOfRangeException("SubForum" + forumId);
            }
        }

        internal SubForum createNewSubForum(string subForumTitle)
        {
            if (this.subForums.Find(delegate(SubForum subfrm) { return subfrm.subForumTitle == subForumTitle; }) != null)
                throw new Exception();///////// change!
            int subForumId = this.subForums.Count();
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
                throw new Exception();///////// change!
            this.admin = new User(currentMember.memberID, currentMember.userName, currentMember.password, "", "", this);
            this.members.Insert(this.members.IndexOf(currentMember), this.admin);
            this.members.Remove(currentMember);
            return this.admin;
        }

    }
}
