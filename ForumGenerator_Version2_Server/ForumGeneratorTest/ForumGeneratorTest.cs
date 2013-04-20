using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumGenerator_Version2_Server.Sys;

namespace ForumGeneratorTest
{
    [TestClass]
    public class ForumGeneratorTest
    {
        [TestMethod]
        public void adminLogged_After_Login()
        {
            // arrange
            ForumGenerator fg = new ForumGenerator("admin", "admin");


            // act
            fg.adminLogin("admin", "admin");

            // assert
            Assert.IsTrue(fg.getSuperUser().isLogged());
        }

        [TestMethod]
        public void adminLoggedOut_After_Logout()
        {
            // arrange
            ForumGenerator fg = new ForumGenerator("admin", "admin");


            // act
            fg.adminLogin("admin", "admin");
            fg.adminLogout();

            // assert
            Assert.IsFalse(fg.getSuperUser().isLogged());
        }

        [TestMethod]
        public void createNewForum_update_ForumsList()
        {
            // arrange
            ForumGenerator fg = new ForumGenerator("admin", "admin");
            fg.adminLogin("admin", "admin");

            // act
            fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");

            // assert
            Assert.AreEqual(1, fg.getForumCount(), "forum list not updated");
            Assert.AreEqual("admin1", fg.getForum(0).getAdminName());
        }

        [TestMethod]
        public void user_Registered_After_Register()
        {
            // arrange
            ForumGenerator fg = new ForumGenerator("admin", "admin");
            fg.adminLogin("admin", "admin");
            fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");

            // act
            fg.register(0, "user1", "user1", "user1@gmail.com", "i'm the first user");

            // assert
            Assert.IsNotNull(fg.getForum(0).getUser("user1"), "user isn't registered");
        }



    }
}
