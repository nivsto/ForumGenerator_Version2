using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.Sys.Exceptions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ForumGenerator_Version2_Server.ForumData
{
    [DataContract(IsReference = true)]
    public class SubForum
    {
        [DataMember]
        [Key]
        public int subForumId { get; private set; }
        [DataMember]
        public string subForumTitle { get; private set; }
        [DataMember]
        public virtual List<User> moderators { get; private set; }
        [DataMember]
        public virtual List<Discussion> discussions { get; private set; }
        [DataMember]
        public virtual Forum parentForum { get; private set; }
        [DataMember]
        public int nextDiscussionId;


        public SubForum(int subForumId, string subForumTitle, Forum parentForun)
        {
            this.subForumId = subForumId;
            this.subForumTitle = subForumTitle;
            this.moderators = new List<User>();
            this.parentForum = parentForun;
            User u = this.parentForum.admin;
            this.moderators.Add(parentForum.admin);
            this.discussions = new List<Discussion>();
            this.nextDiscussionId = 1;
        }

        internal Discussion createNewDiscussion(string title, string content, User user)
        {
            int discussionId = this.nextDiscussionId++;
            Discussion newDiscussion = new Discussion(discussionId, title, content, user, this);
            this.discussions.Add(newDiscussion);
            return newDiscussion;
        }

        internal Discussion getDiscussion(int discussionId)
        {
            try
            {
                return discussions.Find(delegate (Discussion d) { return d.discussionId == discussionId; });
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }


        internal Boolean editDiscussion(int discussionId, string newContent)
        {
            try
            {
                Discussion d = getDiscussion(discussionId);
                if (discussions.Remove(d))    // remove old
                {
                    d.content = newContent;
                    discussions.Add(d);      // add edited discussion.
                    return true;
                }
                else
                    throw new Exception("unknown error");
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new DiscussionNotFoundException();
            }
        }


        internal Discussion removeDiscussion(int discussionId)
        {
            Discussion d = this.getDiscussion(discussionId);
            if (d == null)
                throw new DiscussionNotFoundException();
            else
                this.discussions.Remove(d);
            return d;
        }


        public User getModerator(string userName)
        {
            try
            {
                return this.moderators.Find(
                    delegate(User mem) { return mem.userName == userName; });
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }


        public Boolean addModerator(string modUserName)
        {
            User newModerator = parentForum.getUser(modUserName);
            // check if user is registered to the forum
            if (newModerator == null)
            {
                throw new UserNotFoundException();
            }
            // check if user is already a moderator of this subforum
            try
            {
                this.getModerator(modUserName);
                // if found - means that this user is already a moderator
                throw new UnauthorizedOperationException("user is already a moderator");
            }
            catch (Exception)
            {
                this.moderators.Add(newModerator);
                return true;
            }
        }


        internal Boolean removeModerator(string modUserName)
        {
            if (parentForum.admin.userName == modUserName)            // not allowed
            {
                throw new UnauthorizedOperationException("can not remove forum admin from being a moderator");
            }

            User moderator = parentForum.getUser(modUserName);
            if (moderator == null)
            {
                throw new UserNotFoundException();
            }

            return moderators.Remove(moderator);
        }


        internal int getNumOfCommentsSingleUser(User user)
        {
            int result = 0;
            foreach (Discussion d in discussions)
            {
                result += d.getNumOfCommentsSingleUser(user);
            }
            return result;
        }


        internal List<User> getResponsersForSingleUser(User user)
        {
            List<User> responsers = new List<User>();
            foreach (Discussion d in discussions)
            {
                responsers.Concat(d.getResponsersForSingleUser(user));
            }
            return responsers;
        }


        internal int getNumOfComments()
        {
            int result = 0;

            foreach (Discussion d in discussions)
            {
                result += d.getNumOfComments() + 1;  // a discussion's content is considered as a comment
            }
            return result;
        }


        // This method only checks if the user is a moderator. If not - it returns ForumGenerator.MEMBER
        public int getUserType(string userName)
        {
            User user = getModerator(userName);
            if (user != null)
                return (int)ForumGenerator_Version2_Server.Sys.ForumGenerator.userTypes.MODERATOR;
            else
                return (int)ForumGenerator_Version2_Server.Sys.ForumGenerator.userTypes.MEMBER;
        }
    }
}
