namespace NewsPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class explicitId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.News", name: "Author_Id", newName: "AuthorId");
            RenameIndex(table: "dbo.News", name: "IX_Author_Id", newName: "IX_AuthorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.News", name: "IX_AuthorId", newName: "IX_Author_Id");
            RenameColumn(table: "dbo.News", name: "AuthorId", newName: "Author_Id");
        }
    }
}
