namespace ForumGenerator_Version2_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubForumModeratorsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "SubForum_subForumId", "dbo.SubForums");
            DropIndex("dbo.Users", new[] { "SubForum_subForumId" });
            CreateTable(
                "dbo.SubForumModerators",
                c => new
                    {
                        subForumID = c.Int(nullable: false),
                        moderatorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.subForumID, t.moderatorID })
                .ForeignKey("dbo.SubForums", t => t.subForumID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.moderatorID, cascadeDelete: true)
                .Index(t => t.subForumID)
                .Index(t => t.moderatorID);
            
            DropColumn("dbo.Users", "SubForum_subForumId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "SubForum_subForumId", c => c.Int());
            DropIndex("dbo.SubForumModerators", new[] { "moderatorID" });
            DropIndex("dbo.SubForumModerators", new[] { "subForumID" });
            DropForeignKey("dbo.SubForumModerators", "moderatorID", "dbo.Users");
            DropForeignKey("dbo.SubForumModerators", "subForumID", "dbo.SubForums");
            DropTable("dbo.SubForumModerators");
            CreateIndex("dbo.Users", "SubForum_subForumId");
            AddForeignKey("dbo.Users", "SubForum_subForumId", "dbo.SubForums", "subForumId");
        }
    }
}
