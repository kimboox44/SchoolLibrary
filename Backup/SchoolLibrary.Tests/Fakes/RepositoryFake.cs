namespace SchoolLibrary.Tests.Fakes
{
    using SchoolLibrary.DataAccess.Context;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Repository;
    using System;
    using System.Data.Entity;
    using System.Linq;

    class RepositoryFake<T> : IRepository<T> where T : class
    {
        protected ILibraryContext Context { get; set; }
        protected IDbSet<T> Set { get; set; }

        public RepositoryFake(ILibraryContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.Context = context;

            if (typeof(T) == typeof(Author))
            {
                this.Set = (IDbSet<T>)context.Authors;
            }
            if (typeof(T) == typeof(Item))
            {
                this.Set = (IDbSet<T>)context.Items;
            }
            if (typeof(T) == typeof(Inventory))
            {
                this.Set = (IDbSet<T>)context.Inventory;
            }
            if (typeof(T) == typeof(Consignment))
            {
                this.Set = (IDbSet<T>)context.Consignment;
            }
            if (typeof(T) == typeof(Reader))
            {
                this.Set = (IDbSet<T>)context.Readers;
            }
            if (typeof(T) == typeof(ReaderHistory))
            {
                this.Set = (IDbSet<T>)context.ReaderHistory;
            }
            if (typeof(T) == typeof(ReservedItem))
            {
                this.Set = (IDbSet<T>)context.ReservedItem;
            }
            if (typeof(T) == typeof(ScannedPage))
            {
                this.Set = (IDbSet<T>)context.ScannedPage;
            }
            if (typeof(T) == typeof(UserProfile))
            {
                this.Set = (IDbSet<T>)context.UserProfiles;
            }
            if (typeof(T) == typeof(Tag))
            {
                this.Set = (IDbSet<T>)context.Tags;
            }
            if (typeof(T) == typeof(TagScore))
            {
                this.Set = (IDbSet<T>)context.TagScores;
            }
        }

        public virtual IQueryable<T> GetAll()
        {
            return (this.Set as MemoryDbSet<T>).List.AsQueryable();
        }

        public virtual T GetById(int id)
        {
            return this.Set.Find(id);
        }

        public virtual void Add(T entity)
        {
            this.Set.Add(entity);
        }

        public virtual void Update(T entity)
        {
            if (typeof (T) == typeof (Author))
            {
                this.Delete(this.GetById((entity as Author).Id));
            }
            if (typeof(T) == typeof(Item))
            {
                this.Delete(this.GetById((entity as Item).Id));
            }
            if (typeof(T) == typeof(Inventory))
            {
                this.Delete(this.GetById((entity as Inventory).InventoryId));
            }
            if (typeof(T) == typeof(Consignment))
            {
                this.Delete(this.GetById((entity as Consignment).Id));
            }
            if (typeof(T) == typeof(Reader))
            {
                this.Delete(this.GetById((entity as Reader).ReaderId));
            }
            if (typeof(T) == typeof(ReaderHistory))
            {
                this.Delete(this.GetById((entity as ReaderHistory).ReaderHistoryId));
            }
            if (typeof(T) == typeof(ReservedItem))
            {
                this.Delete(this.GetById((entity as ReservedItem).Id));
            }
            if (typeof(T) == typeof(ScannedPage))
            {
                this.Delete(this.GetById((entity as ScannedPage).Id));
            }
            if (typeof(T) == typeof(UserProfile))
            {
                this.Delete(this.GetById((entity as UserProfile).UserId));
            }
            if (typeof(T) == typeof(Tag))
            {
                this.Delete(this.GetById((entity as Tag).Id));
            }
            if (typeof(T) == typeof(TagScore))
            {
                this.Delete(this.GetById((entity as TagScore).Id));
            }

            this.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            this.Set.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = this.GetById(id);
            if (entity == null)
            {
                return;
            }

            this.Delete(entity);
        }
    }
}