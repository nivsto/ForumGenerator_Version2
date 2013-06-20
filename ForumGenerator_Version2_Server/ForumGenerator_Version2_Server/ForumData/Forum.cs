using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.Sys.Exceptions;
using ForumGenerator_Version2_Server.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using ForumGenerator_Version2_Server.DataLayer;
using System.ComponentModel.DataAnnotations.Schema;


namespace ForumGenerator_Version2_Server.ForumData
{
    [DataContract(IsReference = true)]
    public class Forum
    {
        [DataMember]
        [Key]
        public int forumId { get; set; }
        [DataMember]
        public virtual User admin { get; private set; }
        //[DataMember]
        [IgnoreDataMember]
        public virtual List<SubForum> subForums { get; private set; }
        [DataMember]
        public string forumName { get; set; }
        //[DataMember]
        [IgnoreDataMember]
        public virtual List<User> members { get; private set; }


        public Forum(string forumName, string adminUserName, string adminPassword, ForumGeneratorContext db)
        {
            // TODO: Complete member initialization
            this.forumName = forumName;
            this.members = new List<User>();
            this.admin = new User(adminUserName, adminPassword, "", "", this);
            db.Users.Add(this.admin);
            db.SaveChanges();
            this.members.Add(this.admin);
            this.subForums = new List<SubForum>();
        }

        public Forum(Forum f)
        {
            this.forumId = f.forumId;
            this.admin = new User(f.admin);
            this.subForums = null;
            this.forumName = f.forumName;
            this.members = null;
        }

        public Forum() { }


        internal User login(string userName, string password, ForumGeneratorContext db)
        {
            User user = getUser(userName);
            db.Entry(db.Users.Find(user.memberID)).CurrentValues.SetValues(user);
            db.SaveChanges();
            return user.login(password);
        }

        internal User logout(string userName, string password, ForumGeneratorContext db)
        {
            User user = getUser(userName);
            db.Entry(db.Users.Find(user.memberID)).CurrentValues.SetValues(user);
            db.SaveChanges();
            return user.logout(password);
        }

        internal User register(string userName, string password, string email, string signature, ForumGeneratorContext db)
        {
            // Check if userName is already exist
            if (userExists(userName))
                throw new UnauthorizedAccessException(ForumGeneratorDefs.EXIST_USERNAME);
            else
            {
                // userName does not exist - create new User
                User newUser = new User(userName, password, email, signature, this);
                lock (db)
                {
                    this.members.Add(newUser);
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }
                return newUser;
            }
        }

        internal int getSize()
        {
            return this.subForums.Count();
        }

        internal SubForum getSubForum(int subForumId)
        {
            try
            {
                SubForum sf = this.subForums.Find(delegate(SubForum subfrm) { return subfrm.subForumId == subForumId; });
                if (sf == null)
                    throw new SubForumNotFoundException(ForumGeneratorDefs.SUBFORUM_NF);
                return sf;
            }
            catch (ArgumentNullException)
            {
                throw new SubForumNotFoundException(ForumGeneratorDefs.SUBFORUM_NF);
            }
        }

        // #Asa check if delegatre might throw exception
        internal SubForum createNewSubForum(string subForumTitle, ForumGeneratorContext db)
        {
            if (this.subForums.Find(delegate(SubForum subfrm) { return subfrm.subForumTitle == subForumTitle; }) != null)
                throw new UnauthorizedOperationException(ForumGeneratorDefs.EXIST_TITLE);
            SubForum newSubForum = new SubForum(subForumTitle, this);
            lock (db)
            {
                this.subForums.Add(newSubForum);
                db.SubForums.Add(newSubForum);
                db.SaveChanges();
            }
            return newSubForum;
        }

        public User getUser(int userId)
        {
            try
            {
                User u = this.members.Find(delegate(User mem) { return mem.memberID == userId; });
                if (u == null)
                    throw new UserNotFoundException(ForumGeneratorDefs.USER_NF);
                return u;
            }
            catch (ArgumentNullException)
            {
                throw new UserNotFoundException(ForumGeneratorDefs.USER_NF);
            }
        }


        public User getUser(string userName)
        {
            try
            {
                User u = this.members.Find(delegate(User mem) { return mem.userName == userName; });
                if(u == null)
                    throw new UserNotFoundException(ForumGeneratorDefs.USER_NF);
                return u;
            }
            catch (ArgumentNullException)
            {
                throw new UserNotFoundException(ForumGeneratorDefs.USER_NF);
            }
        }


        public bool userExists(string userName)
        {
            try
            {
                if (this.members.Find(delegate(User mem) { return mem.userName == userName; }) == null)
                    return false;
                return true;
            }
            catch (ArgumentNullException)
            {
                throw new UserNotFoundException(ForumGeneratorDefs.USER_NF);
            }
        }

        internal User changeAdmin(int newAdminUserId, ForumGeneratorContext db)
        {
            User currentMember = getUser(newAdminUserId);
            this.admin = currentMember;
            lock (db)
            {
                db.Entry(db.Forums.Find(this.forumId)).CurrentValues.SetValues(this);
                db.SaveChanges();
            }
            return this.admin;
        }


        internal int getNumOfCommentsSingleUser(string userName)
        {
            int result = 0;
            User user = getUser(userName);
            foreach (SubForum sf in this.subForums)
            {
                result += sf.getNumOfCommentsSingleUser(user);
            }
            return result;
        }

        
        internal int getNumOfCommentsSubForum(int subForumId)
        {
            SubForum sf = getSubForum(subForumId);
            return sf.getNumOfComments();
        }

        internal List<User> getResponsersForSingleUser(string userName)
        {
            List<User> responsers = new List<User>();
            User user = getUser(userName);
            foreach (SubForum sf in this.subForums)
            {
                responsers.AddRange(sf.getResponsersForSingleUser(user));
            }
            return responsers;
        }


        internal List<User> getMutualUsers(Forum other)
        {
            List<User> mutuals = new List<User>();
            // Go all over other's members and for each one check
            // if he is a member in this forum.
            foreach (User user in other.members)
            {
                try
                {
                    getUser(user.userName);
                    mutuals.Add(user);
                }
                catch (UserNotFoundException) { }
            }
            return mutuals;
        }

        // #Asa remove also from DB
        public void removeSubForum(int subForumId)
        {
            SubForum sf = this.getSubForum(subForumId);
            this.subForums.Remove(sf);
        }


        public int getUserType(string userName)
        {
            try
            {
                User user = this.getUser(userName);
            }
            catch (UserNotFoundException) { return (int)ForumGenerator.userTypes.GUEST; }

            if (admin.userName == userName)
                return (int)ForumGenerator.userTypes.ADMIN;
            else
                return (int)ForumGenerator.userTypes.MEMBER;
        }


        public int getUserType(int subForumId, string userName)
        {
            User user = this.getUser(userName);
            SubForum sf = getSubForum(subForumId);
            return sf.getUserType(userName);
        }
    }
}
