using System.Data.Entity.Migrations;

namespace SchoolLibrary.DataAccess.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Authors",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(nullable: false, maxLength: 35),
            //            LastName = c.String(nullable: false, maxLength: 35),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Books",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 150),
            //            Year = c.DateTime(nullable: false),
            //            Publisher = c.String(nullable: false, maxLength: 50),
            //            PageCount = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Inventory",
            //    c => new
            //        {
            //            InventoryId = c.Int(nullable: false, identity: true),
            //            Book_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.InventoryId)
            //    .ForeignKey("dbo.Books", t => t.Book_Id)
            //    .Index(t => t.Book_Id);
            
            //CreateTable(
            //    "dbo.ReaderHistories",
            //    c => new
            //        {
            //            ReaderHistoryId = c.Int(nullable: false, identity: true),
            //            StartDate = c.DateTime(),
            //            ReturnDate = c.DateTime(),
            //            FinishDate = c.DateTime(),
            //            Reader_ReaderId = c.Int(),
            //            Inventory_InventoryId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ReaderHistoryId)
            //    .ForeignKey("dbo.Readers", t => t.Reader_ReaderId)
            //    .ForeignKey("dbo.Inventory", t => t.Inventory_InventoryId)
            //    .Index(t => t.Reader_ReaderId)
            //    .Index(t => t.Inventory_InventoryId);
            
            //CreateTable(
            //    "dbo.Readers",
            //    c => new
            //        {
            //            ReaderId = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(nullable: false, maxLength: 50),
            //            LastName = c.String(nullable: false, maxLength: 50),
            //            Address = c.String(nullable: false, maxLength: 62),
            //            Birthday = c.DateTime(nullable: false),
            //            Phone = c.String(nullable: false, maxLength: 30),
            //            UserProfile_UserId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ReaderId)
            //    .ForeignKey("dbo.UserProfile", t => t.UserProfile_UserId)
            //    .Index(t => t.UserProfile_UserId);
            
            //CreateTable(
            //    "dbo.UserProfile",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false, identity: true),
            //            UserName = c.String(),
            //        })
            //    .PrimaryKey(t => t.UserId);
            
            //CreateTable(
            //    "dbo.ReservedItems",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Date = c.DateTime(nullable: false),
            //            Item_Id = c.Int(nullable: false),
            //            Reader_ReaderId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Readers", t => t.Reader_ReaderId, cascadeDelete: true)
            //    .Index(t => t.Book_Id)
            //    .Index(t => t.Reader_ReaderId);
            
            //CreateTable(
            //    "dbo.BookAuthors",
            //    c => new
            //        {
            //            Book_Id = c.Int(nullable: false),
            //            Author_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Book_Id, t.Author_Id })
            //    .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
            //    .Index(t => t.Book_Id)
            //    .Index(t => t.Author_Id);
            
        }
        
        public override void Down()
        {
            //DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            //DropIndex("dbo.BookAuthors", new[] { "Book_Id" });
            //DropIndex("dbo.ReservedItems", new[] { "Reader_ReaderId" });
            //DropIndex("dbo.ReservedItems", new[] { "Book_Id" });
            //DropIndex("dbo.Readers", new[] { "UserProfile_UserId" });
            //DropIndex("dbo.ReaderHistories", new[] { "Inventory_InventoryId" });
            //DropIndex("dbo.ReaderHistories", new[] { "Reader_ReaderId" });
            //DropIndex("dbo.Inventory", new[] { "Book_Id" });
            //DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            //DropForeignKey("dbo.BookAuthors", "Book_Id", "dbo.Books");
            //DropForeignKey("dbo.ReservedItems", "Reader_ReaderId", "dbo.Readers");
            //DropForeignKey("dbo.ReservedItems", "Book_Id", "dbo.Books");
            //DropForeignKey("dbo.Readers", "UserProfile_UserId", "dbo.UserProfile");
            //DropForeignKey("dbo.ReaderHistories", "Inventory_InventoryId", "dbo.Inventory");
            //DropForeignKey("dbo.ReaderHistories", "Reader_ReaderId", "dbo.Readers");
            //DropForeignKey("dbo.Inventory", "Book_Id", "dbo.Books");
            //DropTable("dbo.BookAuthors");
            //DropTable("dbo.ReservedBooks");
            //DropTable("dbo.UserProfile");
            //DropTable("dbo.Readers");
            //DropTable("dbo.ReaderHistories");
            //DropTable("dbo.Inventory");
            //DropTable("dbo.Books");
            //DropTable("dbo.Authors");
        }
    }
}
