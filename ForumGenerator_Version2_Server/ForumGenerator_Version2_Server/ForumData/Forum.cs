using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.Sys.Exceptions;
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

        //Temporaray constructor - POCO
        public Forum(int forumId, string forumName, User admin)
        {
            this.forumId = forumId;
            this.forumName = forumName;
            this.admin = admin;
        }

        public Forum() { }


        internal User login(string userName, string password, ForumGeneratorContext db)
        {
            User user = this.members.Find(
                            delegate(User mem)
                            { return mem.userName == userName; });
            if (user == null)
                throw new UserNotFoundException();
            else
            {
                db.Entry(db.Users.Find(user.memberID)).CurrentValues.SetValues(user);
                db.SaveChanges();
                return user.login(password);
            }
        }

        internal User logout(string userName, string password, ForumGeneratorContext db)
        {
            User user = this.members.Find(
                delegate(User mem)
                { return mem.userName == userName; });
            if (user == null)
                throw new UserNotFoundException();
            else
            {
                db.Entry(db.Users.Find(user.memberID)).CurrentValues.SetValues(user);
                db.SaveChanges();
                return user.logout(password);
            }
        }

        internal User register(string userName, string password, string email, string signature, ForumGeneratorContext db)
        {
            if (this.members.Find(delegate(User mem) { return mem.userName == userName; }) != null)
                throw new UnauthorizedAccessException("username already exists");
            User newUser = new User(userName, password, email, signature, this);
            this.members.Add(newUser);
            db.Users.Add(newUser);
            db.SaveChanges();
            return newUser;
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
                    throw new SubForumNotFoundException();
                return sf;
            }
            catch (ArgumentNullException)
            {
                throw new SubForumNotFoundException();
            }
        }

        internal SubForum createNewSubForum(string subForumTitle, ForumGeneratorContext db)
        {
            if (this.subForums.Find(delegate(SubForum subfrm) { return subfrm.subForumTitle == subForumTitle; }) != null)
                throw new Exception();///////// change!
            SubForum newSubForum = new SubForum(subForumTitle, this);
            this.subForums.Add(newSubForum);
            db.SubForums.Add(newSubForum);
            db.SaveChanges();
            return newSubForum;
        }

        public User getUser(int userId)
        {
            return this.members.Find(delegate(User mem) { return mem.memberID == userId; });
        }

        public User getUser(string userName)
        {
            return this.members.Find(delegate(User mem) { return mem.userName == userName; });
        }

        internal User changeAdmin(int newAdminUserId, ForumGeneratorContext db)
        {
            User currentMember = getUser(newAdminUserId);
            if (currentMember == null)
                throw new UserNotFoundException();
            this.admin = currentMember;
            db.Entry(db.Forums.Find(this)).CurrentValues.SetValues(this);
            db.SaveChanges();
            return this.admin;
        }


        internal int getNumOfCommentsSingleUser(string userName)
        {
            int result = 0;
            User user = getUser(userName);
            if (user == null)
                throw new UserNotFoundException();

            foreach (SubForum sf in this.subForums)
            {
                result += sf.getNumOfCommentsSingleUser(user);
            }
            return result;
        }


        internal int getNumOfCommentsSubForum(int subForumId)
        {
            SubForum sf = getSubForum(subForumId);

            if (sf == null)
                throw new UserNotFoundException();
            return sf.getNumOfComments();
        }

        internal List<User> getResponsersForSingleUser(string userName)
        {
            List<User> responsers = new List<User>();
            User user = getUser(userName);
            if (user == null)
                throw new UserNotFoundException();

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
                if (this.getUser(user.userName) != null)
                {
                    mutuals.Add(user);
                }
            }
            return mutuals;
        }


        public int getUserType(string userName)
        {
            User user = this.getUser(userName);
            if (user == null)
                return (int)ForumGenerator_Version2_Server.Sys.ForumGenerator.userTypes.GUEST;
            if (admin.userName == userName)
                return (int)ForumGenerator_Version2_Server.Sys.ForumGenerator.userTypes.ADMIN;
            else
                return (int)ForumGenerator_Version2_Server.Sys.ForumGenerator.userTypes.MEMBER;
        }


        public int getUserType(int subForumId, string userName)
        {
            User user = this.getUser(userName);
            if (user == null)
                throw new UserNotFoundException();
            SubForum sf = getSubForum(subForumId);
            if (sf == null)
                throw new SubForumNotFoundException();

            return sf.getUserType(userName);
        }

    }
}
