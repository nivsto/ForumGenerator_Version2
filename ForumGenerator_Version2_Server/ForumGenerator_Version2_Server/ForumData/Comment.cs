using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ForumGenerator_Version2_Server.ForumData
{
    [DataContract(IsReference = true)]
    public class Comment
    {
        [DataMember]
        [Key]
        public int commentId { get; private set; }
        [DataMember]
        public string content { get; private set; }
        [DataMember]
        public DateTime publishDate { get; private set; }
        [DataMember]
        public virtual User publisher { get; private set; }
        [DataMember]
        public virtual Discussion parentDiscussion { get; private set; }

        public Comment(int commentId, string content, User user, Discussion parentDiscussion)
        {
            this.commentId = commentId;
            this.content = content;
            this.publishDate = DateTime.Now;
            this.publisher = user;
            this.parentDiscussion = parentDiscussion;
        }

        public Comment() { }

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
