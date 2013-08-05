using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ForumGenerator_Version2_Server
{
    /// <summary>
    /// this interface is implemented on the client side. It allows the server to push data to the client
    /// </summary>
    interface IForumServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void notify(string msg);
    }
}
