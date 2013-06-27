using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Sys;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using System.Web.UI;
using System.Xml.Linq;
using System.Reflection;

namespace ForumService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class HttpServer : IForumService, BrowserService
    {
        private ForumGenerator _forumGen;

        public HttpServer()
        {
            _forumGen = new ForumGenerator("admin", "admin");
        }

        public User login(int forumId, string userName, string password)
        {
            try
            {
                User res = _forumGen.login(forumId, userName, password);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }

        public bool logout(int forumId, string userName, string password)
        {
            try
            {
                bool res = _forumGen.logout(forumId, userName, password);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }

        public SuperUser superUserLogin(string userName, string password) 
        {
            try
            {
                SuperUser res = _forumGen.superUserLogin(userName, password);
                return res;
            }
            catch (Exception e)
            {
       //         _forumGen.collectLogs("doron.txt");
                throw new FaultException(e.Message);
            }
        }

        public bool superUserLogout(string userName, string password)
        {
            try
            {
                bool res = _forumGen.superUserLogout(userName, password);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }

        public User register(int forumId, string userName, string password, string email, string signature)
        {
            try
            {
                User res = _forumGen.register(forumId, userName, password, email, signature);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }

        public List<Forum> getForums()
        {
            try
            {
                List<Forum> resList = _forumGen.getForums();
                return resList;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }
  

        public List<SubForum> getSubForums(int forumId)
        {
            try
            {
                List<SubForum> resList = _forumGen.getSubForums(forumId);
                return resList;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public List<Discussion> getDiscussions(int forumId, int subForumId)
        {
            try
            {
                List<Discussion> resList = _forumGen.getDiscussions(forumId, subForumId);
                return resList;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public List<Comment> getComments(int forumId, int subForumId, int discussionId)
        {
            try
            {
                List<Comment> resList = _forumGen.getComments(forumId, subForumId, discussionId);
                return resList;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public List<User> getUsers(int forumId)
        {
            try
            {
                List<User> resList = _forumGen.getUsers(forumId);
                return resList;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword)
        {
            try
            {
                Forum res = _forumGen.createNewForum(userName, password, forumName, adminUserName, adminPassword);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle)
        {
            try
            {
                SubForum res = _forumGen.createNewSubForum(userName, password, forumId, subForumTitle);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content)
        {
            try
            {
                Discussion res = _forumGen.createNewDiscussion(userName, password, forumId, subForumId, title, content);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content)
        {
            try
            {
                Comment res = _forumGen.createNewComment(userName, password, forumId, subForumId, discussionId, content);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }


        public User changeAdmin(string userName, string password, int forumId, int newAdminUserId)
        {
            try
            {
                User res = _forumGen.changeAdmin(userName, password, forumId, newAdminUserId);
                return res;
            }
            catch (Exception e)
            {
               
                throw new FaultException(e.Message);
            }
        }

        // added in version 3:

        public Boolean addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            try
            {
                Boolean res = _forumGen.addModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public Boolean removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            try
            {
                Boolean res = _forumGen.removeModerator(modUserName, forumId, subForumId, adderUsrName, adderPswd);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public bool removeSubForum(int forumId, int subForumId, string userName, string password)
        {
            try
            {
                return _forumGen.removeSubForum(forumId, subForumId, userName, password);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public Boolean deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd)
        {
            try
            {
                Boolean res = _forumGen.deleteDiscussion(forumId, subForumId, discussionId, userName, pswd);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public Discussion editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent)
        {
            try
            {
                Discussion res = _forumGen.editDiscussion(forumId, subForumId, discussionId, userName, pswd, newContent);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName)
        {
            try
            {
                int res = _forumGen.getNumOfCommentsSingleUser(reqUserName, reqPswd, forumId, userName);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId)
        {
            try
            {
                int res = _forumGen.getNumOfCommentsSubForum(userName, pswd, forumId, subForumId);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName)
        {
            try
            {
                List<User> resList = _forumGen.getResponsersForSingleUser(reqUserName, reqPswd, forumId, memberUserName);
                return resList;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2)
        {
            try
            {
                List<User> resList = _forumGen.getMutualUsers(userName, password, forumId1, forumId2);
                return resList;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public List<User> getModerators(int forumId, int subForumId)
        {
            try
            {
                List<User> resList = _forumGen.getModerators(forumId, subForumId);
                return resList;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public int getUserType(int forumId, string userName)
        {
            try
            {
                int res = _forumGen.getUserType(forumId, userName);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        public int getUserTypeSubForum(int forumId, int subForumId, string userName)
        {
            try
            {
                int res = _forumGen.getUserType(forumId, subForumId, userName);
                return res;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        internal void attachHtmlHead(HtmlTextWriter writer, string pageTitle)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Head);
            writer.AddAttribute(HtmlTextWriterAttribute.Content, "text/html; charset=utf-8");
            writer.RenderBeginTag(HtmlTextWriterTag.Meta);
            writer.RenderBeginTag(HtmlTextWriterTag.Title);
            writer.Write(pageTitle);
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "style.css");
            writer.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
            writer.RenderBeginTag(HtmlTextWriterTag.Link);
            writer.RenderEndTag();
        }

        public Stream web_index()
        {
            string pageTitle = "The Nimbus Forum Generator";
            List<Forum> forumList = _forumGen.getForums();
            
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(stringWriter);

            //Section: Head
            attachHtmlHead(writer, pageTitle);

            //Section: Body
            int height = forumList.Count() * 60 + 200;
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, height.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Body);

            //Header
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "Header");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.H1);
            writer.Write(pageTitle);
            writer.RenderEndTag();
            writer.RenderEndTag();

            //BodyBox
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "BodyBox");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ForumsList");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ListHeader");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.H2);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ListHeaderTitle");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write("Forums List");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ListHeaderStatistics");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write("Sub Forums / Discussions");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ListHeaderLast");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write("Administrator");
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "forumChilds");
            writer.RenderBeginTag(HtmlTextWriterTag.Ol);

            foreach (Forum forum in forumList)
            {
                int subForumsCounter = _forumGen.getSubForums(forum.forumId).Count();
                int ThreadsCounter = _forumGen.getDiscussionsPerForum(forum.forumId);
                
                writer.RenderBeginTag(HtmlTextWriterTag.Li);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "forum");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "forumInfo");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddAttribute(HtmlTextWriterAttribute.Src, "img/forum_sign.png");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "forumIcon");
                writer.AddAttribute(HtmlTextWriterAttribute.Alt, "forum_sign");
                writer.RenderBeginTag(HtmlTextWriterTag.Img);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "forumTitleContainer");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "forumTitle");
                writer.RenderBeginTag(HtmlTextWriterTag.H2);

                writer.AddAttribute(HtmlTextWriterAttribute.Href, "index?forumId="+forum.forumId);
                writer.RenderBeginTag(HtmlTextWriterTag.A);

                writer.Write(forum.forumName);

                writer.RenderEndTag();  //Link
                writer.RenderEndTag();  //forumTitle H2
                writer.RenderEndTag();  //forumTitleContainer Div
                writer.RenderEndTag();  //forumInfo img
                writer.RenderEndTag();  //forumInfo Div

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "forumStats");
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("Sub Forums: " + subForumsCounter);
                writer.RenderEndTag();  //Li
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("Discussions: " + ThreadsCounter);
                writer.RenderEndTag();  //Li
                writer.RenderEndTag();  //forumStats Ul

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "forumAdmin");
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("Admin: " + forum.admin.userName);
                writer.RenderEndTag();  //Li
                writer.RenderEndTag();  //forumAdmin Ul

                writer.RenderEndTag();  //Forum Div
                writer.RenderEndTag();  //Forum ListItem
            }

            writer.RenderEndTag();  //forumChilds
            writer.RenderEndTag();  //ForumList
            writer.RenderEndTag();  //BodyBox
            writer.RenderEndTag();  //Body

            
            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";

            byte[] htmlBytes = Encoding.UTF8.GetBytes(stringWriter.ToString());

            return new MemoryStream(htmlBytes);
        }

        public Stream web_css()
        {
            //string style = System.IO.File.ReadAllText(@"style.css");
            StreamReader _textStreamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("ForumGenerator_Version2_Server.style.css"));
            string style = _textStreamReader.ReadToEnd();

            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/css";

            byte[] htmlBytes = Encoding.UTF8.GetBytes(style);

            return new MemoryStream(htmlBytes);
        }

        public Stream web_getForum(int forumId)
        {
            string pageTitle = "The Nimbus Forum Generator";
            List<Forum> forumList = _forumGen.getForums();

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(stringWriter);

            //Section: Head
            attachHtmlHead(writer, pageTitle);

            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";

            byte[] htmlBytes = Encoding.UTF8.GetBytes(stringWriter.ToString());

            return new MemoryStream(htmlBytes);
        }

        public Stream web_getImg(string imageName)
        {
            string path = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
           
            
            FileStream fs = File.OpenRead(path + "\\img\\"+imageName);
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            return fs;
        }
    }
}
