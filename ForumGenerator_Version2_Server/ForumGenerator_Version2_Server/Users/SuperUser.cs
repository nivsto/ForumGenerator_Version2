using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Users
{
    class SuperUser
    {
        protected String userName;
        protected String password;
        protected Boolean isLoggedIn;

        public SuperUser(string superUserName, string superUserPass)
        {
            // TODO: Complete member initialization
            this.userName = superUserName;
            this.password = superUserPass;
            this.isLoggedIn = false;
        }

        internal string login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        internal string logout()
        {
            throw new NotImplementedException();
        }
    }
}
