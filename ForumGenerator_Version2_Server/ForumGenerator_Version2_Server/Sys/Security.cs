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
            if (fg.superUser.userName == userName && fg.superUser.password == password)
                return true;

            return false;
        }


        public static bool checkAdminAuthorization(Forum f, string userName, string password)
        {
            if (f.admin.userName == userName && f.admin.password == password && f.admin.isLogged())
                return true;
            return false;
        }


        public static bool checkModeratorAuthorization(SubForum sf, string userName, string password, Moderator.modLevel modLevel)
        {
            // forum admin is also a subforum moderator
            try
            {
                Moderator moderator = sf.getModerator(userName);
                if (moderator != null && moderator.user.password == password && 
                    moderator.user.isLogged() && moderator.level == modLevel)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
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

        // check if user <@param user name> is the publisher of the discussion d.
        public static bool checkPublisherAuthorization(Discussion d, string userName, string password)
        {
            User publisher = d.publisher;
            if (publisher != null && publisher.password == password && publisher.isLogged())
                return true;
            return false;
        }


        // check if user <@param user name> is the publisher of the comment c.
        public static bool checkPublisherAuthorization(Comment c, string userName, string password)
        {
            User publisher = c.publisher;
            if (publisher != null && publisher.password == password && publisher.isLogged())
                return true;
            return false;
        }


        // check end cases


    }
}
