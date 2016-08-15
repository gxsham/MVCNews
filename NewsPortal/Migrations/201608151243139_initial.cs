namespace NewsPortal.Migrations
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
                        ImageLink = c.String(),
                        Author_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Author_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "Author_Id", "dbo.Authors");
            DropIndex("dbo.News", new[] { "Author_Id" });
            DropTable("dbo.News");
            DropTable("dbo.Authors");
        }
    }
}
