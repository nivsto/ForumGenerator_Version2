using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.Communication;

namespace ForumGenerator_Version2_Server.ForumData
{
    public class SubForum
    {
        internal int subForumId { get; private set; }
        internal string subForumTitle { get; private set; }
        internal List<User> moderators { get; private set; }
        internal List<Discussion> discussions { get; private set; }
        internal Forum parentForum { get; private set; }

        public SubForum(int subForumId, string subForumTitle, Forum parentForun)
        {
            this.subForumId = subForumId;
            this.subForumTitle = subForumTitle;
            this.moderators = new List<User>();
            this.discussions = new List<Discussion>();
            this.parentForum = parentForun;
        }

        internal Discussion createNewDiscussion(string title, string content, User user)
        {
            int discussionId = this.discussions.Count();
            Discussion newDiscussion = new Discussion(discussionId, title, content, user, this);
            this.discussions.Add(newDiscussion);
            return newDiscussion;
        }

        internal Discussion getDiscussion(int discussionId)
        {
            try
            {
                return discussions.ElementAt(discussionId);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Discussion" + discussionId);
            }
        }

    }
}
