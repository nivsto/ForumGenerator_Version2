using ForumGenerator_Version2_Server.ForumData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Users
{
    public class User
    {
        public int memberID { get; private set; }
        public string userName { get; private set; }
        public string password { get; private set; }
        public string email { get; private set; }
        public List<User> friends { get; private set; }
        public string signature { get; private set; }
        public bool isLoggedIn { get; private set; }
        public Forum forum { get; private set; }

        internal User(int memberId, string userName, string password, string email, string signature, Forum forum)
        {
            this.memberID = memberId;
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.friends = new List<User>();
            this.signature = signature;
            this.isLoggedIn = false;
            this.forum = forum;
        }

        internal User login(string password)
        {
            if (this.password == password)
            {
                this.isLoggedIn = true;
                return this;
            }
            else
                throw new UnauthorizedAccessException("incorrect password");
        }

        internal bool logout()
        {
            if (this.isLoggedIn)
            {
                this.isLoggedIn = false;
                return true; ;
            }
            else
                return false;
        }


        public bool isLogged()
        {
            return this.isLoggedIn;
        }
    }
}
