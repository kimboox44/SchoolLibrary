namespace SchoolLibrary.Tests.Fakes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using SchoolLibrary.DataAccess.Entities;

    public class MemoryDbSet<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        public List<TEntity> List;

        public MemoryDbSet()
        {
            this.List = new List<TEntity>();
        }

        public TEntity Add(TEntity o)
        {
            this.List.Add(o);
            return o;
        }

        public TEntity Attach(TEntity o)
        {
            return o;
        }

        public TEntity Create()
        {
            return null;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity 
        {
            return null;
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public TEntity Remove(TEntity entity)
        {
            this.List.Remove(entity);
            return entity;
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

        public ObservableCollection<TEntity> Local { get; set; }

        public Type ElementType { get; set; }

        public Expression Expression { get; private set; }
        
        public IQueryProvider Provider { get; set; }
    }

    public class AuthorsSet : MemoryDbSet<Author>
    {
        public override Author Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.Id == (int) keyValues.Single());
        }
    }

    public class ItemsSet : MemoryDbSet<Item>
    {
        public override Item Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.Id == (int)keyValues.Single());
        }
    }

    public class InventorySet : MemoryDbSet<Inventory>
    {
        public override Inventory Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.InventoryId == (int)keyValues.Single());
        }
    }

    public class ConsignmentSet : MemoryDbSet<Consignment>
    {
        public override Consignment Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.Id == (int)keyValues.Single());
        }
    }

    public class ReaderSet : MemoryDbSet<Reader>
    {
        public override Reader Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.ReaderId == (int)keyValues.Single());
        }
    }

    public class ReaderHistorySet : MemoryDbSet<ReaderHistory>
    {
        public override ReaderHistory Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.ReaderHistoryId == (int)keyValues.Single());
        }
    }

    public class ReservedItemSet : MemoryDbSet<ReservedItem>
    {
        public override ReservedItem Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.Id == (int)keyValues.Single());
        }
    }

    public class ScannedPageSet : MemoryDbSet<ScannedPage>
    {
        public override ScannedPage Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.Id == (int)keyValues.Single());
        }
    }
    
    public class UserProfileSet : MemoryDbSet<UserProfile>
    {
        public override UserProfile Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.UserId == (int)keyValues.Single());
        }
    }

    public class TagsSet : MemoryDbSet<Tag>
    {
        public override Tag Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.Id == (int)keyValues.Single());
        }
    }

    public class TagScoresSet : MemoryDbSet<TagScore>
    {
        public override TagScore Find(params object[] keyValues)
        {
            return base.List.SingleOrDefault(a => a.Id == (int)keyValues.Single());
        }
    }
}