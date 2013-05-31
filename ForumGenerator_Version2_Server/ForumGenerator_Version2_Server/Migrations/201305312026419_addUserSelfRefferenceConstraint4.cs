namespace ForumGenerator_Version2_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserSelfRefferenceConstraint4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserUsers", newName: "UserFriends");
            RenameColumn(table: "dbo.UserFriends", name: "User_memberID", newName: "memberID");
            RenameColumn(table: "dbo.UserFriends", name: "User_memberID1", newName: "friendID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.UserFriends", name: "friendID", newName: "User_memberID1");
            RenameColumn(table: "dbo.UserFriends", name: "memberID", newName: "User_memberID");
            RenameTable(name: "dbo.UserFriends", newName: "UserUsers");
        }
    }
}
