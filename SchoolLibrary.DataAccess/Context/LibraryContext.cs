namespace SchoolLibrary.DataAccess.Context
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using SchoolLibrary.DataAccess.Entities;

    /// <summary>
    /// DataBase Context for Library
    /// </summary>
    public class LibraryContext : DbContext, ILibraryContext
    {
        public LibraryContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Item> Items { get; set; }

        public IDbSet<Inventory> Inventory { get; set; }

        public IDbSet<Consignment> Consignment { get; set; }

        public IDbSet<Reader> Readers { get; set; }

        public IDbSet<ReaderHistory> ReaderHistory { get; set; }

        public IDbSet<ReservedItem> ReservedItem { get; set; }

        public IDbSet<ScannedPage> ScannedPage { get; set; }

        public IDbSet<UserProfile> UserProfiles { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public IDbSet<TagScore> TagScores { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new DbEntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}