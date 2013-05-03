using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.Sys
{
    public static class Security
    {
        public static bool checkSuperUserAuthorization(ForumGenerator fg, string userName, string password)
        {
            if (fg.getSuperUser().userName == userName && fg.getSuperUser().password == password && fg.getSuperUser().isLogged())
                return true;
            fg.logger.logError("incorrect password");
            return false;
        }

        public static bool checkAdminAuthorization(Forum f, string userName, string password)
        {
            if (f.admin.userName == userName && f.admin.password == password && f.admin.isLogged())
                return true;
            return false;
        }

        public static bool checkMemberAuthorization(Forum f, string userName, string password)
        {
            User mem = f.getUser(userName);
            if (mem == null)
            {
                return false;
            }
            else if (mem.userName == userName && mem.password == password && mem.isLogged())
                return true;
            return false;
        }


    }
}
