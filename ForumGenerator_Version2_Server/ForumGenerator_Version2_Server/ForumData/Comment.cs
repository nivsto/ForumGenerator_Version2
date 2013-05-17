using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ForumGenerator_Version2_Server.ForumData
{
    [DataContract(IsReference = true)]
    public class Comment
    {
        [DataMember]
        internal int commentId;
        [DataMember]
        internal string content;
        [DataMember]
        internal DateTime publishDate;
        [DataMember]
        internal User publisher;
        [DataMember]
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
