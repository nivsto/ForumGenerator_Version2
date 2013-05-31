namespace ForumGenerator_Version2_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAdminCreation4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "parentForum_forumId", "dbo.Fora");
            DropIndex("dbo.Users", new[] { "parentForum_forumId" });
            DropColumn("dbo.Users", "forumId");
            DropColumn("dbo.Users", "parentForum_forumId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "parentForum_forumId", c => c.Int());
            AddColumn("dbo.Users", "forumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "parentForum_forumId");
            AddForeignKey("dbo.Users", "parentForum_forumId", "dbo.Fora", "forumId");
        }
    }
}
