
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Client.Objects
{
    public class Discussion
    {
        internal int discussionId { get; private set; }
        internal String title { get; private set; }
        public String content;
        internal DateTime publishDate { get; private set; }
        internal User publisher { get; private set; }
        internal List<Comment> comments { get; private set; }
        internal SubForum parentSubForum { get; private set; }
        internal int nextCommentId = 1;

        public Discussion(int discussionId, string title, string content, User user, SubForum parentSubForum)
        {
            // TODO: Complete member initialization
            this.discussionId = discussionId;
            this.title = title;
            this.content = content;
            this.publishDate = DateTime.Now;
            this.publisher = user;
            this.comments = new List<Comment>();
            this.parentSubForum = parentSubForum;
        }

    
    }
}
