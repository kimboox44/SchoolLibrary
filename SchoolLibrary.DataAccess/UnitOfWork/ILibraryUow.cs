using SchoolLibrary.DataAccess.Entities;
using SchoolLibrary.DataAccess.Repository;

namespace SchoolLibrary.DataAccess.UnitOfWork
{
    using System;

    public interface ILibraryUow : IDisposable
    {
        // Repositories
        IRepository<Author> Authors { get; }

        IRepository<Item> Items { get; }

        IRepository<Inventory> Inventories { get; }

        IRepository<Consignment> Consignments { get; }

        IRepository<Reader> Readers { get; }

        IRepository<ReaderHistory> ReadersHistories { get; }

        IRepository<ReservedItem> ReservedItems { get; }

        IRepository<ScannedPage> ScannedPages { get; }

        IRepository<UserProfile> UsersProfiles { get; }

        IRepository<Tag> Tags { get; }

        IRepository<TagScore> TagScores { get; }

        // Save changes to DB
        void Commit();
    }
}