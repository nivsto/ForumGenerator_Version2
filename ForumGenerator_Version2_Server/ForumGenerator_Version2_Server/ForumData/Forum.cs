using ForumGenerator_Version2_Server.Communication;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumGenerator_Version2_Server.ForumData
{
    class Forum
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



        internal string login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        internal string logout(int userId)
        {
            throw new NotImplementedException();
        }

        internal string register(string userName, string password, string email, string signature)
        {
            throw new NotImplementedException();
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

        public string getSubForumsXML()
        {
            string[] properties = {"ID", "Title"};
            string[][] data = new string[this.subForums.Count()][];
            for (int i=0; i<this.subForums.Count(); i++)
            {
                SubForum current = this.subForums.ElementAt(i);
                data[i][0] = current.getSubForumId().ToString();
                data[i][1] = current.getSubForumTitle();
            }
            return new XmlHandler().writeXML("SubForum", properties, data);
        }

        internal SubForum getSubForum(int subForumId)
        {
            return subForums.ElementAt(subForumId);
        }

        internal void createNewSubForum(string subForumTitle)
        {
            int subForumId = this.subForums.Count();
            SubForum newSubForum = new SubForum(subForumId, subForumTitle, this);
            this.subForums.Add(newSubForum);
        }

        internal Member getUser(int userId)
        {
            return this.members.ElementAt(userId);
        }
    }
}
