using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Users
{
    class SuperUser
    {
        internal String userName;
        internal String password;
        internal bool isLoggedIn;

        public SuperUser(string superUserName, string superUserPass)
        {
            this.userName = superUserName;
            this.password = superUserPass;
            this.isLoggedIn = false;
        }

        // returns tuple(1 for success or 0 for fail, error msg or user type
        internal Tuple<int,string> login(string userName, string password)
        {
            if (this.userName == userName && this.password == password)
            {
                this.isLoggedIn = true;
                return new Tuple<int,string>(1,"SuperUser");
            }
            else if (this.userName == userName && this.password != password)
                return new Tuple<int, string>(0, "incorrect password");
            else
                return new Tuple<int, string>(0, "incorrect username or password");
        }

        internal Tuple<int,string> logout()
        {
            if (this.isLoggedIn)
            {
                this.isLoggedIn = false;
                return new Tuple<int,string>(1,"OK");;
            }
            else
                return new Tuple<int, string>(0, "Superuser wasn't loggedin");
        }

        internal string getUserName()
        {
            return this.userName;
        }

        internal string getPassword()
        {
            return this.password;
        }

        internal bool isLogged()
        {
            return this.isLoggedIn;
        }
    }
}
