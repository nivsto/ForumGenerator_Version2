using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.ForumData
{
    class Comment
    {
        protected int commentId;
        protected string content;
        protected DateTime publishDate;
        protected Member publisher;
        protected Discussion parentDiscussion;

        public Comment(int commentId, string content, Member user, Discussion parentDiscussion)
        {
            this.commentId = commentId;
            this.content = content;
            this.publisher = user;
            this.parentDiscussion = parentDiscussion;
        }

        internal int getCommentId()
        {
            return this.commentId
        }

        internal string getPublisherName()
        {
            return this.publisher.getUserName();
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
