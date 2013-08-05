using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumGenerator_Version2_Server.Sys;

namespace ForumGeneratorTest
{
    [TestClass]
    public class SecurityTest
    {
 
        [TestMethod]
        public void login_as_superuser_with_wrong_pass()
        {
            // arrange
            ForumGenerator fg = new ForumGenerator("admin", "admin", null);

            // act
            bool ans = Security.checkSuperUserAuthorization(fg, "admin", "wrong");

            // assert
            Assert.IsFalse(ans);
        }

        //[TestMethod]
        //public void login_as_superuser_with_pass()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.adminLogin("admin", "admin");

        //    // act
        //    bool ans = Security.checkSuperUserAuthorization(fg, "admin", "admin");

        //    // assert
        //    Assert.IsTrue(ans);
        //}

        //[TestMethod]
        //public void login_as_admin_with_pass()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.adminLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");
        //    fg.login(0, "admin1", "admin1");
            
        //    // act
        //    bool ans = Security.checkAdminAuthorization(fg.getForum(0),"admin1","admin1");

        //    // assert
        //    Assert.IsTrue(ans);
        //}

        //[TestMethod]
        //public void login_as_admin_with_wrong_pass()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.adminLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");
        //    fg.login(0, "admin1", "admin1");

        //    // act
        //    bool ans = Security.checkAdminAuthorization(fg.getForum(0), "admin1", "wrong");

        //    // assert
        //    Assert.IsFalse(ans);
        //}

        //[TestMethod]
        //public void login_as_admin_with_wrong_username()
        //{
        //    // arrange
        //    ForumGenerator fg = new ForumGenerator("admin", "admin");
        //    fg.adminLogin("admin", "admin");
        //    fg.createNewForum("admin", "admin", "first forum", "admin1", "admin1");
        //    fg.login(0, "admin1", "admin1");

        //    // act
        //    bool ans = Security.checkAdminAuthorization(fg.getForum(0), "wrong", "admin1");

        //    // assert
        //    Assert.IsFalse(ans);
        //}
    }
}
