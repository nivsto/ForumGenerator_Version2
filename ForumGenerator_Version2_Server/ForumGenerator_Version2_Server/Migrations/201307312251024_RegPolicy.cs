namespace ForumGenerator_Version2_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegPolicy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fora", "registrationPolicy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fora", "registrationPolicy");
        }
    }
}
