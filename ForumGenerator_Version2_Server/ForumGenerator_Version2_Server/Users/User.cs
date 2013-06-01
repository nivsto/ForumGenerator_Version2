﻿using ForumGenerator_Version2_Server.ForumData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumGenerator_Version2_Server.Users
{
    [DataContract(IsReference = true)]
    public class User
    {
        [DataMember]
        [Key]
        public int memberID { get; private set; }
        [DataMember]
        public string userName { get; private set; }
        [DataMember]
        public string password { get; private set; }
        [DataMember]
        public string email { get; private set; }
        [IgnoreDataMember]
        public virtual List<User> friends { get; private set; }
        [DataMember]
        public string signature { get; private set; }
        [DataMember]
        public bool isLoggedIn { get; private set; }
        //[DataMember]
        //public virtual Forum parentForum { get; private set; }

        internal User(int memberId, string userName, string password, string email, string signature, Forum forum)
        {
            this.memberID = memberId;
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.friends = new List<User>();
            this.signature = signature;
            this.isLoggedIn = false;
            //this.parentForum = forum;
        }

        public User() { }

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

        internal User logout(string password)
        {
            if (this.password == password && this.isLoggedIn)
            {
                this.isLoggedIn = false;
                return this;
            }
            else
                throw new UnauthorizedAccessException("incorrect password");
        }


        public bool isLogged()
        {
            return this.isLoggedIn;
        }
    }
}
