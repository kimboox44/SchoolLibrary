using System.Data.Entity.Migrations;

namespace SchoolLibrary.DataAccess.Migrations
{
    public partial class Test4 : DbMigration
    {
        public override void Up()
        {
            this.AlterColumn("dbo.Readers", "Address", c => c.String(nullable: false, maxLength: 60));
            this.AlterColumn("dbo.Readers", "Phone", c => c.String(nullable: false, maxLength: 32));
        }
        
        public override void Down()
        {
            this.AlterColumn("dbo.Readers", "Phone", c => c.String(nullable: false, maxLength: 30));
            this.AlterColumn("dbo.Readers", "Address", c => c.String(nullable: false, maxLength: 62));
        }
    }
}
