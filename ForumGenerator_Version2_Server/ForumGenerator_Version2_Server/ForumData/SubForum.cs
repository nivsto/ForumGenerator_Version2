using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.Communication;
using ForumGenerator_Version2_Server.Sys.Exceptions;

namespace ForumGenerator_Version2_Server.ForumData
{
    public class SubForum
    {
        internal int subForumId { get; private set; }
        internal string subForumTitle { get; private set; }
        internal List<User> moderators { get; private set; }
        internal List<Discussion> discussions { get; private set; }
        internal Forum parentForum { get; private set; }
        internal int nextDiscussionId = 1;


        public SubForum(int subForumId, string subForumTitle, Forum parentForun)
        {
            this.subForumId = subForumId;
            this.subForumTitle = subForumTitle;
            this.moderators = new List<User>();
            moderators.Add(parentForum.admin);          // forum admin is auto moderator.
            this.discussions = new List<Discussion>();
            this.parentForum = parentForun;
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
                return discussions.ElementAt(discussionId);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Discussion" + discussionId);
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


        public User getModerator(string userName)
        {
            return this.moderators.Find(
                delegate(User mem) { return mem.userName == userName; });
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
                result += getNumOfCommentsSingleUser(user);
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
                result += d.getNumOfComments();
            }
            return result;
        }

    }
}
