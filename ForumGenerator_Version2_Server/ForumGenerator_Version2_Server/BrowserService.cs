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
        //[OperationContract]
        //[WebGet(UriTemplate = "/index",
        //ResponseFormat = WebMessageFormat.Xml)]
        //Stream web_index();

        //[OperationContract]
        //[WebGet(UriTemplate = "/style.css",
        //ResponseFormat = WebMessageFormat.Xml)]
        //Stream web_css();

        //[OperationContract]
        //[WebGet(UriTemplate = "/index?forumId={forumId}",
        //ResponseFormat = WebMessageFormat.Xml)]
        //Stream web_getForum(int forumId);

        //[OperationContract]
        //[WebGet(UriTemplate = "/img/{imageName}",
        //ResponseFormat = WebMessageFormat.Xml)]
        //Stream web_getImg(string imageName);
    }
}
