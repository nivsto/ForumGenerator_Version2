using ForumGenerator_Version2_Server.ForumData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Users
{
    class Administrator : Member
    {

        public Administrator(int memberId, string adminUserName, string adminPassword, Forum forum)
        {
            // TODO: Complete member initialization
            this.memberID = memberId;
            this.userName = adminUserName;
            this.password = adminPassword;
            this.email = "";
            this.friends = new List<Member>();
            this.signature = "";
            this.isLoggedIn = false;
            this.forum = forum;
        }

        internal string getUserName()
        {
            return this.userName;
        }

    }
}
