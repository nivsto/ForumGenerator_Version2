﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

using ForumGenerator_Version2_Server.Sys.Exceptions;

namespace ForumGenerator_Version2_Server.Users
{
    [DataContract(IsReference = true)]
    public class SuperUser
    {
        [DataMember]
        public String userName { get; private set; }
        [DataMember]
        public String password { get; private set; }
        [DataMember]
        public bool isLoggedIn { get; private set; }

        public SuperUser(string superUserName, string superUserPass)
        {
            this.userName = superUserName;
            this.password = superUserPass;
            this.isLoggedIn = false;
        }

 
        internal SuperUser login(string userName, string password)
        {
            if (this.userName == userName && this.password == password)
            {
                this.isLoggedIn = true;
                return this;
            }
            else if (this.userName != userName || this.password != password)
                throw new UnauthorizedUserException();
            else
                throw new Exception();
        }

        internal bool logout(string userName, string password)
        {
            if (this.userName == userName && this.password == password)
            {
                this.isLoggedIn = false;
                return true;
            }
            else if (this.userName != userName || this.password != password || !this.isLoggedIn)
                throw new UnauthorizedUserException();
            else
                throw new Exception("unknown error");
        }

        public bool isLogged()
        {
            return this.isLoggedIn;
        }
    }
}
