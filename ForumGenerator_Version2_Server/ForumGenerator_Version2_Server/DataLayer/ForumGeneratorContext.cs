using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.DataLayer
{
    public class ForumGeneratorContext : DbContext
    {
        public ForumGeneratorContext(string dbName) // "ForumGenerator_DB1" for the real, "ForumGenerator_DB1_TEST" for the tests
            : base(dbName)
        {
        }

        public ForumGeneratorContext() // "ForumGenerator_DB1" for the real, "ForumGenerator_DB1_TEST" for the tests
            : base("ForumGenerator_DB1")
        {
        }

        public DbSet<Forum> Forums { get; set; }
        public DbSet<SubForum> SubForums { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(a => a.friends)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("UserFriends");
                    m.MapLeftKey("memberID");
                    m.MapRightKey("friendID");
                });

            modelBuilder.Entity<SubForum>()
                .HasMany(a => a.moderators)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("SubForumModerators");
                    m.MapLeftKey("subForumID");
                    m.MapRightKey("moderatorID");
                });

            //modelBuilder.Entity<User>().HasKey(t => new { t.memberID, t.paremtForumId });

            //modelBuilder.Entity<Forum>()
            //    .HasRequired(t => t.admin)
            //    .WithRequiredPrincipal(t => t.parentForum);

            //modelBuilder.Entity<Forum>().HasMany<User>(s => s.members)
            //    .WithRequired(s => s.parentForum);
        }
    }

}
