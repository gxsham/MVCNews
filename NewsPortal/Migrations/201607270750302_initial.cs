namespace NewsPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(),
                        FirstName = c.String(),
                        UserName = c.String(),
                        Rating = c.Int(nullable: false),
                        MailId = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Topic = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Category = c.Int(nullable: false),
                        Text = c.String(),
                        Rating = c.Int(nullable: false),
                        MailId = c.String(),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "Author_Id", "dbo.Author");
            DropIndex("dbo.News", new[] { "Author_Id" });
            DropTable("dbo.News");
            DropTable("dbo.Author");
        }
    }
}
