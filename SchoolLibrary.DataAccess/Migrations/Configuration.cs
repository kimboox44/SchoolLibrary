using System.Data.Entity.Migrations;
using SchoolLibrary.DataAccess.Entities;

namespace SchoolLibrary.DataAccess.Migrations
{
    using SchoolLibrary.DataAccess.Context;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LibraryContext context)
        {
            //LibraryContextInitializer libConInit = new LibraryContextInitializer();
            //libConInit.getSeedMethod(context);
            //context.Database.ExecuteSqlCommand("CREATE INDEX IX_Authors ON Authors (LastName, FirstName)");


            // Example update DB:
            /*
             * 
            Update-Database -ProjectName "SchoolLibrary.DataAccess" -StartUpProjectName "SchoolLibrary"
             * 
             * 
            */
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}