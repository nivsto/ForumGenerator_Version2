using ForumGenerator_Version2_Server.ForumData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Users
{
    public class Moderator : Member
    {
        internal Moderator(int memberId, string userName, string password, string email, string signature, Forum forum) :
            base(memberId, userName, password, email, signature, forum)
        { }
    }
}
