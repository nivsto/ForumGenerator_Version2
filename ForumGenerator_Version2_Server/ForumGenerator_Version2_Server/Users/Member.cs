using ForumGenerator_Version2_Server.ForumData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Users
{
    public abstract class Member
    {
        protected int memberID;
        protected string userName;
        protected string password;
        protected string email;
        protected List<Member> friends;
        protected string signature;
        protected Boolean isLoggedIn;
        protected Forum forum;


        internal string getUserName()
        {
            return this.userName;
        }
    }
}
