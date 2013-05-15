using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.ForumData
{
    public class Discussion
    {
        public int discussionId { get; private set; }
        internal String title { get; private set; }
        public String content;
        internal DateTime publishDate { get; private set; }
        internal User publisher { get; private set; }
        public List<Comment> comments { get; private set; }
        internal SubForum parentSubForum { get; private set; }
        internal int nextCommentId = 1;

        public Discussion(int discussionId, string title, string content, User publisher, SubForum parentSubForum)
        {
            // TODO: Complete member initialization
            this.discussionId = discussionId;
            this.title = title;
            this.content = content;
            this.publishDate = DateTime.Now;
            this.publisher = publisher;
            this.comments = new List<Comment>();
            this.parentSubForum = parentSubForum;
        }

        internal string getPublishDate()
        {
            return this.publishDate.ToShortDateString();
        }

        internal Comment createNewComment(string content, User user)
        {
            int commentId = this.nextCommentId++;
            Comment newComment = new Comment(commentId, content, user, this);
            this.comments.Add(newComment);
            return newComment;
        }


        internal int getNumOfCommentsSingleUser(User user)
        {
            int result = 0;
            foreach (Comment c in comments)
            {
                if (c.publisher == user)
                    result++;
            }
            return result;
        }


        internal List<User> getResponsersForSingleUser(User user)
        {
            List<User> responsers = new List<User>();
            foreach (Comment c in comments)
            {
                if (c.publisher != user)
                    responsers.Add(c.publisher);
            }
            return responsers;
        }


        internal int getNumOfComments()
        {
            return comments.Count;
        }
    }
}
