using ForumGenerator_Version2_Server.ForumData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Users
{
    public class Member
    {
        internal int memberID;
        internal string userName;
        internal string password;
        internal string email;
        internal List<Member> friends;
        internal string signature;
        internal bool isLoggedIn;
        internal Forum forum;

        internal Member(int memberId, string userName, string password, string email, string signature, Forum forum)
        {
            // TODO: Complete member initialization
            this.memberID = memberId;
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.friends = new List<Member>();
            this.signature = signature;
            this.isLoggedIn = false;
            this.forum = forum;
        }

        public string getUserName()
        {
            return this.userName;
        }

        internal Tuple<string, string> login(string password)
        {
            if (this.password == password)
            {
                this.isLoggedIn = true;
                return new Tuple<string, string>("1", this.GetType().Name);
            }
            else
                return new Tuple<string, string>("0", "incorrect password");
        }

        internal Tuple<string, string> logout()
        {
            if (this.isLoggedIn)
            {
                this.isLoggedIn = false;
                return new Tuple<string, string>("1", "OK"); ;
            }
            else
                return new Tuple<string, string>("0", "user wasn't loggedin");
        }

        public int getMemberID()
        {
            return this.memberID;
        }

        public string getEmail()
        {
            return this.email;
        }

        internal string getPassword()
        {
            return this.password;
        }

        public bool isLogged()
        {
            return this.isLoggedIn;
        }
    }
}
