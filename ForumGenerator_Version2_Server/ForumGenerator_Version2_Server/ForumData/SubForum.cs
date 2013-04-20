using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.Communication;

namespace ForumGenerator_Version2_Server.ForumData
{
    class SubForum
    {
        internal int subForumId;
        internal string subForumTitle;
        internal List<Moderator> moderators;
        internal List<Discussion> discussions;
        internal Forum parentForum;

        public SubForum(int subForumId, string subForumTitle, Forum parentForun)
        {
            this.subForumId = subForumId;
            this.subForumTitle = subForumTitle;
            this.moderators = new List<Moderator>();
            this.discussions = new List<Discussion>();
            this.parentForum = parentForun;
        }


        public Tuple<string, string[], string[,]> getDiscussionsXML()
        {
            string[] properties = { "ID", "Title", "Publisher", "PublishDate", "Content" };
            string[,] data = new string[this.discussions.Count(), properties.Length];
            for (int i = 0; i < this.discussions.Count(); i++)
            {
                Discussion current = this.discussions.ElementAt(i);
                data[i, 0] = current.getDiscussionId().ToString();
                data[i, 1] = current.getTitle();
                data[i, 2] = current.getPublisherName();
                data[i, 3] = current.getPublishDate();
                data[i, 4] = current.getContent();
            }
            return new Tuple<string, string[], string[,]>("Discussion", properties, data);
        }

        internal int getSubForumId()
        {
            throw new NotImplementedException();
        }

        internal string getSubForumTitle()
        {
            throw new NotImplementedException();
        }

        //internal Discussion getDiscussionsXML(int discussionId)
        //{
        //    return discussions.ElementAt(discussionId);
        //}

        internal Tuple<string, string> createNewDiscussion(string title, string content, Member user)
        {
            int discussionId = this.discussions.Count();
            Discussion newDiscussion = new Discussion(discussionId, title, content, user, this);
            this.discussions.Add(newDiscussion);
            return new Tuple<string, string>("1", newDiscussion.getDiscussionId().ToString());
        }

        internal Discussion getDiscussion(int discussionId)
        {
            return discussions.ElementAt(discussionId);
        }

        internal Forum getParentForum()
        {
            return this.parentForum;
        }
    }
}
