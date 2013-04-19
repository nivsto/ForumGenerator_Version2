using ForumGenerator_Version2_Server.ForumData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Users
{
    public abstract class Member
    {
        internal int memberID;
        internal string userName;
        internal string password;
        internal string email;
        internal List<Member> friends;
        internal string signature;
        internal Boolean isLoggedIn;
        internal Forum forum;


        internal string getUserName()
        {
            return this.userName;
        }
    }
}
