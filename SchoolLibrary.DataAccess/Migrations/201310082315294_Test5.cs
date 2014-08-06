using System.Data.Entity.Migrations;

namespace SchoolLibrary.DataAccess.Migrations
{
    public partial class Test5 : DbMigration
    {
        public override void Up()
        {
            this.AlterColumn("dbo.Readers", "Phone", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            this.AlterColumn("dbo.Readers", "Phone", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
