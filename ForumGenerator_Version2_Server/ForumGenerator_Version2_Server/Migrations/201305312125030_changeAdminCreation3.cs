namespace ForumGenerator_Version2_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAdminCreation3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "forumId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "forumId");
        }
    }
}
