namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        NewsId = c.Long(nullable: false),
                        AuthorId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .Index(t => t.NewsId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Topic = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Category = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        Rating = c.Int(nullable: false),
                        AuthorId = c.Long(nullable: false),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NewsId = c.Long(nullable: false),
                        AuthorId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .Index(t => t.NewsId)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Likes", "NewsId", "dbo.News");
            DropForeignKey("dbo.Comments", "NewsId", "dbo.News");
            DropForeignKey("dbo.News", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Likes", new[] { "AuthorId" });
            DropIndex("dbo.Likes", new[] { "NewsId" });
            DropIndex("dbo.News", new[] { "AuthorId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropIndex("dbo.Comments", new[] { "NewsId" });
            DropTable("dbo.Likes");
            DropTable("dbo.News");
            DropTable("dbo.Comments");
            DropTable("dbo.Authors");
        }
    }
}
