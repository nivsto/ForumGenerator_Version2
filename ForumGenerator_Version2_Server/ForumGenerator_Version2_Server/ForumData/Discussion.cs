using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using ForumGenerator_Version2_Server.DataLayer;


namespace ForumGenerator_Version2_Server.ForumData
{
    [DataContract(IsReference = true)]
    public class Discussion
    {
        [DataMember]
        [Key]
        public int discussionId { get; private set; }
        [DataMember]
        public string title { get; private set; }
        [DataMember]
        public string content;
        [DataMember]
        public DateTime publishDate { get; private set; }
        [DataMember]
        public virtual User publisher { get; private set; }
        [IgnoreDataMember]
        public virtual List<Comment> comments { get; private set; }
        [IgnoreDataMember]
        public virtual SubForum parentSubForum { get; private set; }


        public Discussion(string title, string content, User publisher, SubForum parentSubForum)
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

        // POCO constructor
        public Discussion(Discussion d)
        {
            this.discussionId = d.discussionId;
            this.title = d.title;
            this.content = d.content;
            this.publishDate = d.publishDate;
            this.publisher = new User(d.publisher);
            this.comments = null;
            this.parentSubForum = null;
        }

        public Discussion() { }


        internal string getPublishDate()
        {
            return this.publishDate.ToShortDateString();
        }

        internal Comment createNewComment(string content, User user, ForumGeneratorContext db)
        {
            Comment newComment = new Comment(content, user, this);
            this.comments.Add(newComment);
            db.Comments.Add(newComment);
            db.SaveChanges();
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
            /* count the first comment, which is a discussion */
            if (this.publisher == user)
                result++;

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

        internal bool editDiscussion(string newContent, ForumGeneratorContext db)
        {
            this.content = newContent;
            db.Entry(db.Discussions.Find(this)).CurrentValues.SetValues(this);
            db.SaveChanges();
            return true;
        }
    }
}
