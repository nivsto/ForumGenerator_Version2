namespace ForumGenerator_Version2_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAdminCreation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "parentForum_forumId", c => c.Int());
            AddForeignKey("dbo.Users", "parentForum_forumId", "dbo.Fora", "forumId");
            CreateIndex("dbo.Users", "parentForum_forumId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "parentForum_forumId" });
            DropForeignKey("dbo.Users", "parentForum_forumId", "dbo.Fora");
            DropColumn("dbo.Users", "parentForum_forumId");
        }
    }
}
