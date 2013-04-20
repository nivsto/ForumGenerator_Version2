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
        internal int forumId;
        internal Administrator admin;
        internal List<SubForum> subForums;
        internal string forumName;
        internal List<Member> members;


        public Forum(int forumId, string forumName, string adminUserName, string adminPassword)
        {
            // TODO: Complete member initialization
            this.forumId = forumId;
            this.forumName = forumName;
            this.members = new List<Member>();
            this.admin = new Administrator(members.Count(), adminUserName, adminPassword, this);
            this.members.Add(this.admin);
        }



        internal Tuple<string, string> login(string userName, string password)
        {
            Member user = this.members.Find(
                            delegate(Member mem)
                                {return mem.userName == userName;});
            if (user == null)
                return new Tuple<string, string>("0", "incorrect username or password");
            else
                return user.login(password);
        }

        internal Tuple<string, string> logout(int userId)
        {
            return this.members.ElementAt(userId).logout();
        }

        internal Tuple<string, string> register(string userName, string password, string email, string signature)
        {
            if (this.members.Find(delegate(Member mem) { return mem.userName == userName; }) != null)
                return new Tuple<string, string>("0", "username already exists");
            Member newUser = new Member(this.members.Count(), userName, password, email, signature, this);
            this.members.Add(newUser);
            return new Tuple<string, string>("1", "Member");
        }

        public int getForumId()
        {
            return this.forumId;
        }

        public string getForumName()
        {
            return this.forumName;
        }

        public string getAdminName()
        {
            return this.admin.getUserName();
        }

        internal int getSize()
        {
            return this.subForums.Count();
        }

        internal List<SubForum> getSubForums()
        {
            return this.subForums;
        }

        public Tuple<string, string[], string[,]> getSubForumsXML()
        {
            string[] properties = { "ID", "Title" };
            string[,] data = new string[this.subForums.Count(), properties.Length];
            for (int i = 0; i < this.subForums.Count(); i++)
            {
                SubForum current = this.subForums.ElementAt(i);
                data[i, 0] = current.getSubForumId().ToString();
                data[i, 1] = current.getSubForumTitle();
            }
            return new Tuple<string, string[], string[,]>("SubForum", properties, data);
        }

        internal SubForum getSubForum(int subForumId)
        {
            return subForums.ElementAt(subForumId);
        }

        internal Tuple<string, string> createNewSubForum(string subForumTitle)
        {
            if (this.subForums.Find(delegate(SubForum subfrm) { return subfrm.subForumTitle == subForumTitle; }) != null)
                return new Tuple<string, string>("0", "forum name already exists");
            int subForumId = this.subForums.Count();
            SubForum newSubForum = new SubForum(subForumId, subForumTitle, this);
            this.subForums.Add(newSubForum);
            return new Tuple<string, string>("1", newSubForum.getSubForumId().ToString());
        }

        public Member getUser(int userId)
        {
            return this.members.ElementAt(userId);
        }

        public Member getUser(string userName)
        {
            return this.members.Find(delegate(Member mem) { return mem.userName == userName; });
        }

        internal Tuple<string, string[], string[,]> getUsers()
        {
            string[] properties = { "ID", "UserName","Email" };
            string[,] data = new string[this.members.Count(), properties.Length];
            for (int i = 0; i < this.members.Count(); i++)
            {
                Member current = this.members.ElementAt(i);
                data[i, 0] = current.getMemberID().ToString();
                data[i, 1] = current.getUserName();
                data[i, 2] = current.getEmail();
            }
            return new Tuple<string, string[], string[,]>("User", properties, data);
        }

        internal Tuple<string, string> changeAdmin(int newAdminUserId)
        {
            Member currentMember = getUser(newAdminUserId);
            if (currentMember == null)
                return new Tuple<string, string>("0", "No such UserID");
            this.admin = new Administrator(currentMember.getMemberID(), currentMember.getUserName(), currentMember.getPassword(), this);
            this.members.Insert(this.members.IndexOf(currentMember), this.admin);
            this.members.Remove(currentMember);
            return new Tuple<string, string>("1", "OK");
        }

        internal Administrator getAdmin()
        {
            return this.admin;
        }
    }
}
