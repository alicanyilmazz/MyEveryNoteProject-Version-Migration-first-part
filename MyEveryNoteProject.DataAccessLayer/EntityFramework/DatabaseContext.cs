using MyEveryNoteProject.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNoteProject.DataAccessLayer.EntityFramework
{
    public class DatabaseContext:DbContext
    {
        public DbSet<EveryNoteUser> Dbset_EveryNoteUsers { get; set; }
        public DbSet<Article> Dbset_Articles { get; set; }
        public DbSet<Category> Dbset_Categories { get; set; }
        public DbSet<Comment> Dbset_Comments { get; set; }
        public DbSet<Liked> Dbset_Likes { get; set; }

        public DatabaseContext()
        {
            //Database.SetInitializer(new MyInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, MyEveryNoteProject.DataAccessLayer.Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasMany(n => n.Comments)
                .WithRequired(c => c.Articles)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Article>()
                .HasMany(n => n.Likes)
                .WithRequired(c => c.Articles)
                .WillCascadeOnDelete(true);

        }

    }
}
