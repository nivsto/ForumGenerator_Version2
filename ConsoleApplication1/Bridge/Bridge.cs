using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
   public interface Bridge
    {
       string adminLogin(string xmlRequest);
       string adminLogout(string xmlRequest);
       string login(string xmlRequest);
       string logout(string xmlRequest);
       string createNewForum(string xmlRequest);
       string createNewSubForum(string xmlRequest);
       string register(string xmlRequest);
       string createNewThead(string xmlRequest);
       string addNewReply(string xmlRequest);
       string getForums(string xmlRequest);
       string getSubForums(string xmlRequest);
       string getThreads(string xmlRequest);
       string getReplies(string xmlRequest);

    }
}
