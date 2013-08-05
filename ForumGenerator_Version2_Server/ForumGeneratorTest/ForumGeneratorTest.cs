using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server.DataLayer;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Sys.Exceptions;
using ForumGenerator_Version2_Server.Users;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ForumGeneratorTest
{
    [TestClass]
    public class ForumGeneratorTest
    {
        [TestMethod]
        public void adminLogged_After_Login()
        {
            // arrange
            ForumGenerator fg = new ForumGenerator("admin", "admin",true, null);

            // act
            fg.superUserLogin("admin", "admin");

            // assert
            //Assert.IsTrue(fg.getSuperUser().isLogged());
        }


        //[TestMethod]
        //public void db_is_empty()
        //{
        //    ForumGenerator fg1 = new ForumGenerator("admin", "admin", true);
        //    ForumGeneratorContext db = fg1.db;
        //    List<Forum> fl = db.Forums.ToList();
        //    Assert.AreEqual(0, fl.Count);

        //}
        //public void adminLoggedOut_After_Logout()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");


        //    // act
        //    fg.superUserLogin("admin", "admin");
        //    fg.superUserLogout("admin", "admin");

        //    // assert
        //    Assert.IsFalse(fg.getSuperUser().isLogged());
        //}

        //[TestMethod]
        //public void createNewForum_update_ForumsList()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.superUserLogin("admin", "admin");

        //    // act
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");

        //    // assert
        //    Assert.AreEqual(1, fg.getForumCount(), "forum list not updated");
        //    Assert.AreEqual("admin1", fg.getForum(0).getAdminName());
        //}

        //[TestMethod]
        //public void user_Registered_After_Register()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.superUserLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");

        //    // act
        //    fg.register(0, "user1", "user1", "user1@gmail.com", "i'm the first user");

        //    // assert
        //    Assert.IsNotNull(fg.getForum(0).getUser("user1"), "user isn't registered");
        //}

        //[TestMethod]
        //public void forum_returned_on_getForums()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.superUserLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");

        //    // act
        //    int forumCounter = fg.getForums().Item4.GetLength(0);

        //    // assert
        //    Assert.AreEqual(1, forumCounter);
        //}

        //[TestMethod]
        //public void subforum_created_on_createSubForum()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.superUserLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");
        //    fg.login(0, "admin1", "admin1");

        //    // act
        //    fg.createNewSubForum("admin1", "admin1", 0, "subforum1");
        //    int subforumCounter = fg.getSubForums(0).Item4.GetLength(0);

        //    // assert
        //    Assert.AreEqual(1, subforumCounter);
        //    Assert.AreEqual("subforum1", fg.getSubForums(0).Item4[0,1]);
        //}

        //[TestMethod]
        //public void error_returned_on_createsubforum_with_wrong_forumID()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.superUserLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");
        //    fg.login(0, "admin1", "admin1");

        //    // act
        //    string ans = fg.createNewSubForum("admin1", "admin1", 4, "subforum1").Item1;

        //    // assert
        //    Assert.AreEqual("0", ans);
        //}

        //[TestMethod]
        //public void discussion_created_on_createDiscussion()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.superUserLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");
        //    fg.login(0, "admin1", "admin1");
        //    fg.createNewSubForum("admin1", "admin1", 0, "subforum1");

        //    // act
        //    fg.createNewDiscussion("admin1", "admin1", 0, 0, "Discussion1", "no content");
        //    int discussionCounter = fg.getDiscussions(0,0).Item4.GetLength(0);

        //    // assert
        //    Assert.AreEqual(1, discussionCounter);
        //    Assert.AreEqual("Discussion1", fg.getDiscussions(0, 0).Item4[0, 1]);
        //}

        //[TestMethod]
        //public void error_returned_on_creatediscussion_with_loggedout_user()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.superUserLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");
        //    fg.createNewSubForum("admin1", "admin1", 0, "subforum1");

        //    // act
        //    string ans = fg.createNewDiscussion("admin1", "admin1", 0, 0, "Discussion1", "no content").Item1;

        //    // assert
        //    Assert.AreEqual("0", ans);
        //}

        //[TestMethod]
        //public void error_returned_on_getdiscussions_with_wrong_subForumID()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.superUserLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");
        //    fg.createNewSubForum("admin1", "admin1", 0, "subforum1");
        //    fg.login(0, "admin1", "admin1");
        //    fg.createNewDiscussion("admin1", "admin1", 0, 0, "Discussion1", "no content");
            
        //    // act
        //    bool ans = fg.getDiscussions(0, 2).Item1;

        //    // assert
        //    Assert.IsFalse(ans);
        //}

        //[TestMethod]
        //public void comment_created_on_createComment()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.superUserLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");
        //    fg.login(0, "admin1", "admin1");
        //    fg.createNewSubForum("admin1", "admin1", 0, "subforum1");
        //    fg.createNewDiscussion("admin1", "admin1", 0, 0, "Discussion1", "no content");

        //    // act
        //    fg.createNewComment("admin1", "admin1", 0, 0, 0, "comment1");
        //    int commentsCounter = fg.getComments(0, 0, 0).Item4.GetLength(0);

        //    // assert
        //    Assert.AreEqual(1, commentsCounter);
        //    Assert.AreEqual("comment1", fg.getComments(0, 0, 0).Item4[0, 3]);
        //}

    }
}
