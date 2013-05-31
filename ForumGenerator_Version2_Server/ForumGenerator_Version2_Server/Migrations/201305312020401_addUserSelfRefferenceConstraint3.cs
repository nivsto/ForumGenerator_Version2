namespace ForumGenerator_Version2_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserSelfRefferenceConstraint3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "User_memberID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "User_memberID" });
            CreateTable(
                "dbo.UserUsers",
                c => new
                    {
                        User_memberID = c.Int(nullable: false),
                        User_memberID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_memberID, t.User_memberID1 })
                .ForeignKey("dbo.Users", t => t.User_memberID)
                .ForeignKey("dbo.Users", t => t.User_memberID1)
                .Index(t => t.User_memberID)
                .Index(t => t.User_memberID1);
            
            DropColumn("dbo.Users", "User_memberID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "User_memberID", c => c.Int());
            DropIndex("dbo.UserUsers", new[] { "User_memberID1" });
            DropIndex("dbo.UserUsers", new[] { "User_memberID" });
            DropForeignKey("dbo.UserUsers", "User_memberID1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_memberID", "dbo.Users");
            DropTable("dbo.UserUsers");
            CreateIndex("dbo.Users", "User_memberID");
            AddForeignKey("dbo.Users", "User_memberID", "dbo.Users", "memberID");
        }
    }
}
