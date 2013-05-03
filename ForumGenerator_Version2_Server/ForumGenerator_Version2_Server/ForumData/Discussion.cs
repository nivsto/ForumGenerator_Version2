using ForumGenerator_Version2_Server.Communication;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.ForumData
{
    public class Discussion
    {
        internal int discussionId { get; private set; }
        internal String title { get; private set; }
        internal String content { get; private set; }
        internal DateTime publishDate { get; private set; }
        internal User publisher { get; private set; }
        internal List<Comment> comments { get; private set; }
        internal SubForum parentSubForum { get; private set; }

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

        internal string getPublishDate()
        {
            return this.publishDate.ToShortDateString();
        }

        internal Comment createNewComment(string content, User user)
        {
            int commentId = this.comments.Count();
            Comment newComment = new Comment(commentId, content, user, this);
            this.comments.Add(newComment);
            return newComment;
        }
    }
}
