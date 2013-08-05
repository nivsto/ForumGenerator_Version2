using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumGenerator_Version2_Server
{
    class Subscriber
    {
        internal IForumServiceCallback callbackChannel { get; set; }
        internal int forumId { get; set; }
        internal string userName { get; set; }

        public Subscriber(IForumServiceCallback callback, int forumId, string userName)
        {
            this.callbackChannel = callback;
            this.forumId = forumId;
            this.userName = userName;
        }
    }
}
