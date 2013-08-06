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
        public virtual List<Moderator> moderators { get; private set; }
        [IgnoreDataMember]
        public virtual List<Discussion> discussions { get; private set; }
        [IgnoreDataMember]
        public virtual Forum parentForum { get; private set; }
        [IgnoreDataMember]
        public virtual HashSet<Word> vocabulary { get; set; }


        public SubForum(string subForumTitle, Forum parentForun, ForumGeneratorContext db)
        {
            this.subForumTitle = subForumTitle;
            this.moderators = new List<Moderator>();
            this.parentForum = parentForun;
            Moderator m = new Moderator(parentForum.admin, ForumGenerator_Version2_Server.Users.Moderator.modLevel.ALL);
            this.moderators.Add(m);
            db.Moderators.Add(m);
            db.SaveChanges();
            this.discussions = new List<Discussion>();
            this.vocabulary = new HashSet<Word>();
        }

        public SubForum() { }

        public SubForum (SubForum sf)
        {
            this.subForumId = sf.subForumId;
            this.subForumTitle = sf.subForumTitle;
            this.moderators = null;
            this.discussions = null;
            this.parentForum = null;
            this.vocabulary = null;
        }

        internal Discussion createNewDiscussion(string title, string content, User user, ForumGeneratorContext db, HashSet<string> stopWords)
        {
            List<string> keyWords = parseKeyWords(content, stopWords);
            if (keyWords != null && keyWords.Count > 0)
            {
                if (isClassifies())
                {
                    checkRelevantContent(keyWords);
                }
                else
                    this.vocabulary = TextClassifier.addToVocabulary(keyWords, this.vocabulary);
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


        public Moderator getModerator(string userName)
        {
            try
            {
                Moderator m = this.moderators.Find(delegate(Moderator mod) { return mod.user.userName == userName; });
                if (m == null)
                    throw new UserNotFoundException(ForumGeneratorDefs.USER_NF);
                return m;
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
                if (this.moderators.Find(delegate(Moderator mod) { return mod.user.userName == userName; }) == null)
                    return false;
                return true;
            }
            catch (ArgumentNullException)
            {
                throw new UserNotFoundException(ForumGeneratorDefs.USER_NF);
            }
        }


        public Boolean addModerator(string modUserName, ForumGeneratorContext db, ForumGenerator_Version2_Server.Users.Moderator.modLevel level)
        {
            // check if user is registered to the forum
            Moderator newModerator = new Moderator(parentForum.getUser(modUserName), level);
            if (moderatorExists(modUserName))
                throw new UnauthorizedOperationException(ForumGeneratorDefs.EXIST_MODERATOR);
            if (!newModerator.user.isConfirmed)
                throw new UnauthorizedOperationException(ForumGeneratorDefs.INACTIVE_USR);

            else
            {
                // OK, moderator is not exist
                this.moderators.Add(newModerator);
                db.Moderators.Add(newModerator);
                db.SaveChanges();
                return true;
            }
        }


        internal Boolean removeModerator(string modUserName, ForumGeneratorContext db)
        {
            if (parentForum.admin.userName == modUserName)     // not allowed
            {
                throw new UnauthorizedOperationException(ForumGeneratorDefs.F_ADMIN_S_MOD);
            }

            Moderator moderator = this.moderators.Find(delegate(Moderator mod) { return mod.user.userName == modUserName; });
            bool ans = moderators.Remove(moderator);
            //lock (db)
            //{
            db.Moderators.Remove(moderator);
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
                Moderator mod = getModerator(userName);
                return (int)ForumGenerator.userTypes.MODERATOR;
            }
            catch (UserNotFoundException)
            {
                return (int)ForumGenerator.userTypes.MEMBER;
            }              
        }


        private List<string> parseKeyWords(string text, HashSet<string> stopWords)
        {
            List<string> keyWords = TextClassifier.removePanctuation(text);
            keyWords = TextClassifier.removeStopWords(keyWords, stopWords);
            return keyWords;
        }


        public void checkRelevantContent(string text, HashSet<string> stopWords)
        {
            if (isClassifies())
            {
                List<string> keyWords = parseKeyWords(text, stopWords);
                checkRelevantContent(keyWords);
            }
        }

        public void checkRelevantContent(List<string> keyWords)
        {
            if (TextClassifier.isRelevantText(keyWords, this.vocabulary))
            {
                this.vocabulary = TextClassifier.addToVocabulary(keyWords, this.vocabulary);
            }
            else
            {
                throw new UnauthorizedOperationException(ForumGeneratorDefs.IRELEVANT_CONTENT);
            }
        }

        internal bool isClassifies()
        {
            return (this.discussions.Count >= 5);
        }


        internal bool changeModLevel(int forumId, int subForumId, string moderatorName, Moderator.modLevel newLevel, ForumGeneratorContext db)
        {
            Moderator moderator = this.getModerator(moderatorName);
            moderator.level = newLevel;
            db.Entry(db.Moderators.Find(moderator.moderatorId)).CurrentValues.SetValues(moderator);
            db.SaveChanges();
            return true;
        }
    }
}
