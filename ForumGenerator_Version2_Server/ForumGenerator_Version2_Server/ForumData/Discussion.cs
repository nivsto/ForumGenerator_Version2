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
        internal int discussionId;
        internal String title;
        internal String content;
        internal DateTime publishDate;
        internal Member publisher;
        internal List<Comment> comments;
        internal SubForum parentSubForum;

        public Discussion(int discussionId, string title, string content, Member user, SubForum parentSubForum)
        {
            // TODO: Complete member initialization
            this.discussionId = discussionId;
            this.title = title;
            this.content = content;
            this.publisher = user;
            this.parentSubForum = parentSubForum;
        }

        public Tuple<string, string[], string[,]> getCommentsXML()
        {
            string[] properties = { "ID", "Publisher", "PublishDate", "Content" };
            string[,] data = new string[this.comments.Count(), properties.Length];
            for (int i = 0; i < this.comments.Count(); i++)
            {
                Comment current = this.comments.ElementAt(i);
                data[i, 0] = current.getCommentId().ToString();
                data[i, 2] = current.getPublisherName();
                data[i, 3] = current.getPublishDate();
                data[i, 4] = current.getContent();
            }
            return new Tuple<string, string[], string[,]>("Comment", properties, data);
        }

        internal int getDiscussionId()
        {
            return this.discussionId;
        }

        internal string getTitle()
        {
            return this.title;
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

        internal Tuple<string, string> createNewComment(string content, Member user)
        {
            int commentId = this.comments.Count();
            Comment newComment = new Comment(commentId, content, user, this);
            this.comments.Add(newComment);
            return new Tuple<string, string>("1", newComment.getCommentId().ToString());
        }
    }
}
