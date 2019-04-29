namespace MyEveryNoteProject.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 60),
                        Text = c.String(nullable: false, maxLength: 2000),
                        IsDraft = c.Boolean(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        ArticleImageFileName = c.String(maxLength: 270),
                        CategoryId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifierUser = c.String(nullable: false, maxLength: 30),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.EveryNoteUsers", t => t.Owner_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 60),
                        Description = c.String(nullable: false, maxLength: 60),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifierUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 300),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifierUser = c.String(nullable: false, maxLength: 30),
                        Owner_Id = c.Int(),
                        Articles_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EveryNoteUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Articles", t => t.Articles_Id, cascadeDelete: true)
                .Index(t => t.Owner_Id)
                .Index(t => t.Articles_Id);
            
            CreateTable(
                "dbo.EveryNoteUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Surname = c.String(nullable: false, maxLength: 25),
                        Username = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 70),
                        Password = c.String(nullable: false, maxLength: 350),
                        ProfileImageFileName = c.String(maxLength: 270),
                        IsActive = c.Boolean(nullable: false),
                        ActivateGuid = c.Guid(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifierUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikedUser_Id = c.Int(),
                        Articles_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EveryNoteUsers", t => t.LikedUser_Id)
                .ForeignKey("dbo.Articles", t => t.Articles_Id, cascadeDelete: true)
                .Index(t => t.LikedUser_Id)
                .Index(t => t.Articles_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "Articles_Id", "dbo.Articles");
            DropForeignKey("dbo.Comments", "Articles_Id", "dbo.Articles");
            DropForeignKey("dbo.Likes", "LikedUser_Id", "dbo.EveryNoteUsers");
            DropForeignKey("dbo.Comments", "Owner_Id", "dbo.EveryNoteUsers");
            DropForeignKey("dbo.Articles", "Owner_Id", "dbo.EveryNoteUsers");
            DropForeignKey("dbo.Articles", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Likes", new[] { "Articles_Id" });
            DropIndex("dbo.Likes", new[] { "LikedUser_Id" });
            DropIndex("dbo.Comments", new[] { "Articles_Id" });
            DropIndex("dbo.Comments", new[] { "Owner_Id" });
            DropIndex("dbo.Articles", new[] { "Owner_Id" });
            DropIndex("dbo.Articles", new[] { "CategoryId" });
            DropTable("dbo.Likes");
            DropTable("dbo.EveryNoteUsers");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Articles");
        }
    }
}
