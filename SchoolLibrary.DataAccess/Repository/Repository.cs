namespace SchoolLibrary.DataAccess.Repository
{
    using SchoolLibrary.DataAccess.Context;
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ILibraryContext DbContext { get; set; }

        protected IDbSet<T> DbSet { get; set; }

        public Repository(ILibraryContext dbContext)
        {
            
            if (dbContext == null) 
                throw new ArgumentNullException("dbContext");
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.DbSet;
        }

        public virtual T GetById(int id)
        {
            //return DbSet.FirstOrDefault(PredicateBuilder.GetByIdPredicate<T>(id));
            var a = this.DbSet.Find(id);
            
            return a;
            
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
                
            }
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                
                this.DbSet.Attach(entity);

            }  

            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = this.GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            this.Delete(entity);
        }
    }
}