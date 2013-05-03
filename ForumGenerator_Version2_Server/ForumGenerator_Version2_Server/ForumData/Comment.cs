using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.ForumData
{
    public class Comment
    {
        internal int commentId;
        internal string content;
        internal DateTime publishDate;
        internal User publisher;
        internal Discussion parentDiscussion;

        public Comment(int commentId, string content, User user, Discussion parentDiscussion)
        {
            this.commentId = commentId;
            this.content = content;
            this.publishDate = DateTime.Now;
            this.publisher = user;
            this.parentDiscussion = parentDiscussion;
        }

        internal int getCommentId()
        {
            return this.commentId;
        }

        internal string getPublisherName()
        {
            return this.publisher.userName;
        }

        internal string getPublishDate()
        {
            return this.publishDate.ToShortDateString();
        }

        internal string getContent()
        {
            return this.content;
        }
    }
}
