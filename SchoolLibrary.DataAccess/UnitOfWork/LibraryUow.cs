using System;
using SchoolLibrary.DataAccess.Entities;
using SchoolLibrary.DataAccess.Repository;

namespace SchoolLibrary.DataAccess.UnitOfWork
{
    using System.Data.Entity;
    using SchoolLibrary.DataAccess.Context;

    public class LibraryUow : ILibraryUow, IDisposable
    {
        private ILibraryContext context;

        private Repository<Author> authorsRepository;

        private Repository<Item> itemsRepository;

        private Repository<Inventory> inventoriesRepository;

        private Repository<Consignment> consignmentRepository;

        private Repository<Reader> readersRepository;

        private Repository<ReaderHistory> readersHistoriesRepository;

        private Repository<ReservedItem> reservedItemsRepository;

        private Repository<ScannedPage> scannedPagesRepository;

        private Repository<UserProfile> usersProfilesRepository;

        private Repository<Tag> tagsRepository;

        private Repository<TagScore> tagScoresRepository;

        public LibraryUow()
        {
            this.context = new LibraryContext();
        }

        public LibraryUow(ILibraryContext context)
        {
            this.context = context;
        }
        
        public void Commit()
        {
            this.context.SaveChanges();
        }

        public IRepository<Author> Authors
        {
            get
            {
                if (this.authorsRepository == null)
                {
                    this.authorsRepository = new Repository<Author>(this.context);
                }

                return this.authorsRepository;
            }
        }

        public IRepository<Item> Items
        {
            get
            {
                if (this.itemsRepository == null)
                {
                    this.itemsRepository = new Repository<Item>(this.context);
                }

                return this.itemsRepository;
            }
        }

        public IRepository<Inventory> Inventories
        {
            get
            {
                if (this.inventoriesRepository == null)
                {
                    this.inventoriesRepository = new Repository<Inventory>(this.context);
                }

                return this.inventoriesRepository;
            }
        }

        public IRepository<Consignment> Consignments
        {
            get
            {
                if (this.consignmentRepository == null)
                {
                    this.consignmentRepository = new Repository<Consignment>(this.context);
                }

                return this.consignmentRepository;
            }
        }

        public IRepository<Reader> Readers
        {
            get
            {
                if (this.readersRepository == null)
                {
                    this.readersRepository = new Repository<Reader>(this.context);
                }

                return this.readersRepository;
            }
        }

        public IRepository<ReaderHistory> ReadersHistories
        {
            get
            {
                if (this.readersHistoriesRepository == null)
                {
                    this.readersHistoriesRepository = new Repository<ReaderHistory>(this.context);
                }

                return this.readersHistoriesRepository;
            }
        }

        public IRepository<ReservedItem> ReservedItems
        {
            get
            {
                if (this.reservedItemsRepository == null)
                {
                    this.reservedItemsRepository= new Repository<ReservedItem>(this.context);
                }

                return this.reservedItemsRepository;
            }
        }

        public IRepository<ScannedPage> ScannedPages
        {
            get
            {
                if (this.scannedPagesRepository == null)
                {
                    this.scannedPagesRepository = new Repository<ScannedPage>(this.context);
                }

                return this.scannedPagesRepository;
            }
        }

        public IRepository<UserProfile> UsersProfiles
        {
            get
            {
                if (this.usersProfilesRepository == null)
                {
                    this.usersProfilesRepository = new Repository<UserProfile>(this.context);
                }

                return this.usersProfilesRepository;
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                if (this.tagsRepository == null)
                {
                    this.tagsRepository = new Repository<Tag>(this.context);
                }

                return this.tagsRepository;
            }
        }

        public IRepository<TagScore> TagScores
        {
            get
            {
                if (this.tagScoresRepository == null)
                {
                    this.tagScoresRepository = new Repository<TagScore>(this.context);
                }

                return this.tagScoresRepository;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context as DbContext != null)
                {
                    (this.context as DbContext).Dispose();
                }
            }
        }
    }
}