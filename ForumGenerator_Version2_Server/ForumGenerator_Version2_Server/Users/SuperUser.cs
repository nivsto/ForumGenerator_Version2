using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Users
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

        // returns tuple(1 for success or 0 for fail, error msg or user type
        internal SuperUser login(string userName, string password)
        {
            if (this.userName == userName && this.password == password)
            {
                this.isLoggedIn = true;
                return this;
            }
            else if (this.userName == userName && this.password != password)
                throw new Exception();///////// change!;
            else
                throw new Exception();///////// change!;
        }

        internal bool logout()
        {
            if (this.isLoggedIn)
            {
                this.isLoggedIn = false;
                return true;
            }
            else
                throw new Exception();///////// change!
        }

        public bool isLogged()
        {
            return this.isLoggedIn;
        }
    }
}
