
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Client.Objects
{
    public class SuperUser
    {
        internal String userName { get; private set; }
        internal String password { get; private set; }
        internal bool isLoggedIn { get; private set; }

        public SuperUser(string superUserName, string superUserPass)
        {
            this.userName = superUserName;
            this.password = superUserPass;
            this.isLoggedIn = false;
        }

    }
}
