using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys
{
    static class Security
    {
        public static bool checkSuperUserAuthorization(ForumGenerator fg, string userName, string password)
        {
            if (fg.getSuperUser().getUserName() == userName && fg.getSuperUser().getPassword() == password && fg.getSuperUser().isLogged())
                return true;
            return false;
        }

        public static bool checkAdminAuthorization(Forum f, string userName, string password)
        {
            if (f.getAdmin().getUserName() == userName && f.getAdmin().getPassword() == password && f.getAdmin().isLogged())
                return true;
            return false;
        }

        public static bool checkMemberAuthorization(Forum f, string userName, string password)
        {
            Member mem = f.getUser(userName);
            if (mem == null)
                return false;
            else if (mem.getUserName() == userName && mem.getPassword() == password && mem.isLogged())
                return true;
            return false;
        }


    }
}
