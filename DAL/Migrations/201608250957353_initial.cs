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
                "dbo.Comments",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Text = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        NewsId = c.Long(nullable: false),
                        AuthorId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.NewsId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        NewsId = c.Long(nullable: false),
                        AuthorId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "NewsId", "dbo.News");
            DropForeignKey("dbo.Likes", "Id", "dbo.Authors");
            DropForeignKey("dbo.Comments", "NewsId", "dbo.News");
            DropForeignKey("dbo.Comments", "Id", "dbo.Authors");
            DropForeignKey("dbo.News", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Likes", new[] { "NewsId" });
            DropIndex("dbo.Likes", new[] { "Id" });
            DropIndex("dbo.Comments", new[] { "NewsId" });
            DropIndex("dbo.Comments", new[] { "Id" });
            DropIndex("dbo.News", new[] { "AuthorId" });
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
            DropTable("dbo.News");
            DropTable("dbo.Authors");
        }
    }
}
