
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumGenerator_Client.Objects
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

    }
}
