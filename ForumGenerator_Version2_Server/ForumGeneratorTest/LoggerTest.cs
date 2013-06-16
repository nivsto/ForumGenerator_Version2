using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumGenerator_Version2_Server.Sys;

namespace ForumGeneratorTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class LoggerTest
    {

        //[TestMethod]
        //public void createnewforum_with_unauthorized_user_error_logged()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.adminLogin("admin", "admin");

        //    // act
        //    fg.createNewForum("admin", "admin_wrong", "first forum", "admin1", "admin1");

        //    // assert
        //    Assert.AreEqual(1, fg.logger.errorLog.Count);
        //}

        //[TestMethod]
        //public void createnewsubforum_with_wrong_forumID_logged()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.adminLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");
        //    fg.login(0, "admin1", "admin1");

        //    // act
        //    fg.createNewSubForum("admin1", "admin1", 4, "wrong_sub_forum");

        //    // assert
        //    Assert.AreEqual(1, fg.logger.errorLog.Count);
        //}
    }
}
