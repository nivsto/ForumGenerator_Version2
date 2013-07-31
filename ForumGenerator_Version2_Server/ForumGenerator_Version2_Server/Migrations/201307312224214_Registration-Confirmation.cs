namespace ForumGenerator_Version2_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegistrationConfirmation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "isConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "isConfirmed");
        }
    }
}
