using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumGenerator_Version2_Server
{
    class Subscriber
    {
        public IForumServiceCallback callbackChannel { get; set; }
        public int forumId { get; set; }
        public string userName { get; set; }

        public Subscriber(IForumServiceCallback callback, int forumId, string userName)
        {
            this.callbackChannel = callbackChannel;
            this.forumId = forumId;
            this.userName = userName;
        }
    }
}
