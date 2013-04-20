using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumGenerator_Version2_Server.Sys;

namespace ForumGeneratorTest
{
    [TestClass]
    public class ForumGeneratorTest
    {
        [TestMethod]
        public void CreateNewForum_update_ForumsList()
        {
            // arrange
            ForumGenerator fg = new ForumGenerator("admin", "admin");
            fg.adminLogin("admin", "admin");

            // act
            fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");

            // assert
            Assert.AreEqual(1, fg.getForumCount(), "forum list not updated");
        }
    }
}
