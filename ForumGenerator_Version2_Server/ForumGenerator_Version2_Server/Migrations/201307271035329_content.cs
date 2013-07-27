namespace ForumGenerator_Version2_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class content : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discussions", "content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discussions", "content");
        }
    }
}
