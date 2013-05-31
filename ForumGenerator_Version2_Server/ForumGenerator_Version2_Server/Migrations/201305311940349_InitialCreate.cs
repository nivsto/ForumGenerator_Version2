namespace ForumGenerator_Version2_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        forumId = c.Int(nullable: false, identity: true),
                        forumName = c.String(),
                        admin_memberID = c.Int(),
                    })
                .PrimaryKey(t => t.forumId)
                .ForeignKey("dbo.Users", t => t.admin_memberID)
                .Index(t => t.admin_memberID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        memberID = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        password = c.String(),
                        email = c.String(),
                        signature = c.String(),
                        isLoggedIn = c.Boolean(nullable: false),
                        User_memberID = c.Int(),
                        SubForum_subForumId = c.Int(),
                        Forum_forumId = c.Int(),
                    })
                .PrimaryKey(t => t.memberID)
                .ForeignKey("dbo.Users", t => t.User_memberID)
                .ForeignKey("dbo.SubForums", t => t.SubForum_subForumId)
                .ForeignKey("dbo.Fora", t => t.Forum_forumId)
                .Index(t => t.User_memberID)
                .Index(t => t.SubForum_subForumId)
                .Index(t => t.Forum_forumId);
            
            CreateTable(
                "dbo.SubForums",
                c => new
                    {
                        subForumId = c.Int(nullable: false, identity: true),
                        subForumTitle = c.String(),
                        parentForum_forumId = c.Int(),
                    })
                .PrimaryKey(t => t.subForumId)
                .ForeignKey("dbo.Fora", t => t.parentForum_forumId)
                .Index(t => t.parentForum_forumId);
            
            CreateTable(
                "dbo.Discussions",
                c => new
                    {
                        discussionId = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        publishDate = c.DateTime(nullable: false),
                        publisher_memberID = c.Int(),
                        parentSubForum_subForumId = c.Int(),
                    })
                .PrimaryKey(t => t.discussionId)
                .ForeignKey("dbo.Users", t => t.publisher_memberID)
                .ForeignKey("dbo.SubForums", t => t.parentSubForum_subForumId)
                .Index(t => t.publisher_memberID)
                .Index(t => t.parentSubForum_subForumId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        commentId = c.Int(nullable: false, identity: true),
                        content = c.String(),
                        publishDate = c.DateTime(nullable: false),
                        publisher_memberID = c.Int(),
                        parentDiscussion_discussionId = c.Int(),
                    })
                .PrimaryKey(t => t.commentId)
                .ForeignKey("dbo.Users", t => t.publisher_memberID)
                .ForeignKey("dbo.Discussions", t => t.parentDiscussion_discussionId)
                .Index(t => t.publisher_memberID)
                .Index(t => t.parentDiscussion_discussionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "parentDiscussion_discussionId" });
            DropIndex("dbo.Comments", new[] { "publisher_memberID" });
            DropIndex("dbo.Discussions", new[] { "parentSubForum_subForumId" });
            DropIndex("dbo.Discussions", new[] { "publisher_memberID" });
            DropIndex("dbo.SubForums", new[] { "parentForum_forumId" });
            DropIndex("dbo.Users", new[] { "Forum_forumId" });
            DropIndex("dbo.Users", new[] { "SubForum_subForumId" });
            DropIndex("dbo.Users", new[] { "User_memberID" });
            DropIndex("dbo.Fora", new[] { "admin_memberID" });
            DropForeignKey("dbo.Comments", "parentDiscussion_discussionId", "dbo.Discussions");
            DropForeignKey("dbo.Comments", "publisher_memberID", "dbo.Users");
            DropForeignKey("dbo.Discussions", "parentSubForum_subForumId", "dbo.SubForums");
            DropForeignKey("dbo.Discussions", "publisher_memberID", "dbo.Users");
            DropForeignKey("dbo.SubForums", "parentForum_forumId", "dbo.Fora");
            DropForeignKey("dbo.Users", "Forum_forumId", "dbo.Fora");
            DropForeignKey("dbo.Users", "SubForum_subForumId", "dbo.SubForums");
            DropForeignKey("dbo.Users", "User_memberID", "dbo.Users");
            DropForeignKey("dbo.Fora", "admin_memberID", "dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Discussions");
            DropTable("dbo.SubForums");
            DropTable("dbo.Users");
            DropTable("dbo.Fora");
        }
    }
}
