namespace SchoolLibrary.DataAccess.Facades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class ItemFacade : IItemFacade, IDisposable
    {
        private ILibraryUow uow;

        public ItemFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public List<ItemBusinessModel> GetAllItems()
        {
            List<ItemBusinessModel> listItemBusinessModels = new List<ItemBusinessModel>();
            List<Item> list = this.uow.Items.GetAll().ToList();
            foreach (var item in list)
            {
                if (item is Book)
                {
                    listItemBusinessModels.Add(new BookMapper().Map(item as Book));
                }
                else if (item is Magazine)
                {
                    listItemBusinessModels.Add(new MagazineMapper().Map(item as Magazine));
                }
                else
                {
                    listItemBusinessModels.Add(new DiskMapper().Map(item as Disk));
                }
            }

            return listItemBusinessModels;
        }

        public ItemBusinessModel GetItemById(int id)
        {
            Item item = this.uow.Items.GetById(id);
            if (item is Book)
            {
                return new BookMapper().Map(item as Book);
            }
            else if (item is Magazine)
            {
                return new MagazineMapper().Map(item as Magazine);
            }
            else
            {
                return new DiskMapper().Map(item as Disk);
            }
        }

        public void DeleteItem(int id)
        {
            this.uow.Items.Delete(id);
            this.uow.Commit();
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }
        }
    }
}
