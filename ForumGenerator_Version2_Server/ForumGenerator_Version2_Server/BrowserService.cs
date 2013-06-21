using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ForumService
{
    [ServiceContract]
    interface BrowserService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/",
        ResponseFormat = WebMessageFormat.Xml)]
        Stream index();

    }
}
