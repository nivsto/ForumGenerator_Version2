using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server.Sys.Exceptions;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using ForumGenerator_Version2_Server.DataLayer;

using NClassifier.Bayesian;
using NClassifier;


// #Asa 
// Handle editDiscussion, createNewComment
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
        [IgnoreDataMember]
        public virtual List<User> moderators { get; private set; }
        [IgnoreDataMember]
        public virtual List<Discussion> discussions { get; private set; }
        [IgnoreDataMember]
        public virtual Forum parentForum { get; private set; }

        internal BayesianClassifier bc;
        // Number of comments in this subForum that are used in order to
        // train the classifier. Those comments are not being classified.
        public const int MIN_BEFORE_CLASSIFY = 10;
        // Min probability required to classified text, in order to be 
        // considered as relevant to this subForum.
        public const double MIN_RELEVANT_PROB = 0.9d;
        internal bool isClassifies;


        public SubForum(string subForumTitle, Forum parentForun)
        {
            this.subForumTitle = subForumTitle;
            this.moderators = new List<User>();
            this.parentForum = parentForun;
            User u = this.parentForum.admin;
            this.moderators.Add(parentForum.admin);
            this.discussions = new List<Discussion>();

            this.bc = new BayesianClassifier(new SimpleWordsDataSource(), new DefaultTokenizer());
            this.trainClassifier();
            this.isClassifies = (this.getNumOfComments() >= MIN_BEFORE_CLASSIFY);
        }

        public SubForum() { }

        public SubForum (SubForum sf)
        {
            this.subForumId = sf.subForumId;
            this.subForumTitle = sf.subForumTitle;
            this.moderators = null;
            this.discussions = null;
            this.parentForum = null;
        }

        internal Discussion createNewDiscussion(string title, string content, User user, ForumGeneratorContext db)
        {
            if (isClassifies)
            {
                if (!this.checkRelevantContent(content))
                {
                    throw new IllegalContentException(ForumGeneratorDefs.IRELEVANT_CONTENT);
                }
            }
            else
            {
                this.bc.TeachMatch(ICategorizedClassifierConstants.DEFAULT_CATEGORY, content);
                isClassifies = (this.getNumOfComments() + 1) >= MIN_BEFORE_CLASSIFY;
            }
            Discussion newDiscussion = new Discussion(title, content, user, this);
            this.discussions.Add(newDiscussion);
            db.Discussions.Add(newDiscussion);
            db.SaveChanges();

            return newDiscussion;
        }


        internal Discussion getDiscussion(int discussionId)
        {
            try
            {
                Discussion dis = discussions.Find(delegate (Discussion d) { return d.discussionId == discussionId; });
                if(dis == null)
                    throw new DiscussionNotFoundException(ForumGeneratorDefs.DISCUSSION_NF);
                return dis;
            }
            catch (ArgumentNullException)
            {
                throw new DiscussionNotFoundException(ForumGeneratorDefs.DISCUSSION_NF);
            }
        }


        internal Discussion removeDiscussion(int discussionId, ForumGeneratorContext db)
        {
            Discussion d = this.getDiscussion(discussionId);
            lock (db)
            {
                this.discussions.Remove(d);
                db.Discussions.Remove(d);
                db.SaveChanges();
            }
            return d;
        }


        public User getModerator(string userName)
        {
            try
            {
                User u = this.moderators.Find(delegate(User mem) { return mem.userName == userName; });
                if (u == null)
                    throw new UserNotFoundException(ForumGeneratorDefs.USER_NF);
                return u;
            }
            catch (ArgumentNullException)
            {
                throw new UserNotFoundException(ForumGeneratorDefs.USER_NF);
            }
        }


        public bool moderatorExists(string userName)
        {
            try
            {
                if (this.moderators.Find(delegate(User mem) { return mem.userName == userName; }) == null)
                    return false;
                return true;
            }
            catch (ArgumentNullException)
            {
                throw new UserNotFoundException(ForumGeneratorDefs.USER_NF);
            }
        }


        public Boolean addModerator(string modUserName, ForumGeneratorContext db)
        {
            // check if user is registered to the forum
            User newModerator = parentForum.getUser(modUserName);
            if (moderatorExists(modUserName))
                throw new UnauthorizedOperationException(ForumGeneratorDefs.EXIST_MODERATOR);
            else
            {
                // OK, moderator is not exist
                this.moderators.Add(newModerator);
                //lock (db)
                //{
                    db.Entry(db.SubForums.Find(this.subForumId)).CurrentValues.SetValues(this);
                    db.SaveChanges();
               // }
                return true;
            }
        }


        internal Boolean removeModerator(string modUserName, ForumGeneratorContext db)
        {
            if (parentForum.admin.userName == modUserName)     // not allowed
            {
                throw new UnauthorizedOperationException(ForumGeneratorDefs.F_ADMIN_S_MOD);
            }

            User moderator = parentForum.getUser(modUserName);
            bool ans = moderators.Remove(moderator);
            //lock (db)
            //{
                db.Entry(db.SubForums.Find(this.subForumId)).CurrentValues.SetValues(this);
                db.SaveChanges();
          //  }
            return ans;
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
                if(d.publisher.userName == user.userName)
                    responsers.AddRange(d.getResponsersForSingleUser(user));
            }
            return responsers;
        }


        internal int getNumOfComments()
        {
            int result = 0;

            foreach (Discussion d in discussions)
            {
                // a discussion's content is considered as a comment
                result += d.getNumOfComments() + 1;
            }
            return result;
        }


        /**
         * This method only checks if the user is a moderator. 
         * If not - it returns ForumGenerator.MEMBER
         */ 
        public int getUserType(string userName)
        {
            try
            {
                User user = getModerator(userName);
                return (int)ForumGenerator.userTypes.MODERATOR;
            }
            catch (UserNotFoundException)
            {
                return (int)ForumGenerator.userTypes.MEMBER;
            }              
        }

        // The training is based on all discussions' content, then (if needed)
        // on comments.
        private void trainClassifier()
        {
            int i = 0;
            // Train classifier in discussions only.
            foreach (Discussion d in this.discussions)
            {
                i++;
                this.bc.TeachMatch(ICategorizedClassifierConstants.DEFAULT_CATEGORY, d.content);
                if (i >= MIN_BEFORE_CLASSIFY)
                    return;
            }
            // If needed - train classifier on comments also.
            if (i < MIN_BEFORE_CLASSIFY)
            {
                foreach (Discussion d in this.discussions)
                {
                    List<Comment> comments = d.comments;
                    foreach (Comment c in comments)
                    {
                        i++;
                        this.bc.TeachMatch(ICategorizedClassifierConstants.DEFAULT_CATEGORY, c.content);
                        if (i >= MIN_BEFORE_CLASSIFY)
                            return;
                    }
                }
            }
            return;
        }


        public bool checkRelevantContent(string text)
        {
            double prob = this.bc.Classify(ICategorizedClassifierConstants.DEFAULT_CATEGORY, text);
            Console.WriteLine("Match = " + prob);
            if (prob < MIN_RELEVANT_PROB)
            {
                this.bc.TeachNonMatch(text);
            }
            return (prob >= MIN_RELEVANT_PROB);
        }

    }
}
