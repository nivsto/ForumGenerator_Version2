
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Client.Objects
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
    }
}
