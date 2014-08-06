namespace SchoolLibrary.DataAccess.Context
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using SchoolLibrary.DataAccess.Entities;

    public interface ILibraryContext
    {
        IDbSet<Author> Authors { get; set; }

        IDbSet<Item> Items { get; set; }

        IDbSet<Inventory> Inventory { get; set; }

        IDbSet<Consignment> Consignment { get; set; }

        IDbSet<Reader> Readers { get; set; }

        IDbSet<ReaderHistory> ReaderHistory { get; set; }

        IDbSet<ReservedItem> ReservedItem { get; set; }

        IDbSet<ScannedPage> ScannedPage { get; set; }

        IDbSet<UserProfile> UserProfiles { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<TagScore> TagScores { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry Entry(object entity);

        int SaveChanges();
    }
}