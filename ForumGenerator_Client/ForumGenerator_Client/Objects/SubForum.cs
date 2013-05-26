using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ForumGenerator_Client.Objects
{
    public class SubForum
    {
        internal int subForumId { get; private set; }
        internal string subForumTitle { get; private set; }
        internal List<User> moderators { get; private set; }
        internal List<Discussion> discussions { get; private set; }
        internal Forum parentForum { get; private set; }
        internal int nextDiscussionId = 1;


        public SubForum(int subForumId, string subForumTitle, Forum parentForun)
        {
            this.subForumId = subForumId;
            this.subForumTitle = subForumTitle;
            this.moderators = new List<User>();
            moderators.Add(parentForum.admin);          // forum admin is auto moderator.
            this.discussions = new List<Discussion>();
            this.parentForum = parentForun;
        }

   
    }
}
