using ForumGenerator_Version2_Server.ForumData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Users
{
    public class Administrator : Member
    {
        public Administrator(int memberId, string adminUserName, string adminPassword, Forum forum) :
            base(memberId, adminUserName, adminPassword, "", "", forum)
        { }

    }
}
