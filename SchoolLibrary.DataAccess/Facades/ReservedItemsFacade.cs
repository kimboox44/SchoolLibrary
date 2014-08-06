namespace SchoolLibrary.DataAccess.Facades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class ReservedItemsFacade : IReservedItemsFacade, IDisposable
    {
        private ILibraryUow uow;

        public ReservedItemsFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public List<ReservedItemBusinessModel> GetReservedItemsByReaderId(int readerId)
        {
            var reservedItems = this.uow.ReservedItems.GetAll().Where(r => r.Reader.ReaderId == readerId)
                .Select(r => r).ToList();

            var reservedItemMapper = new ReservedItemMapper();

            var reservedItemsBusiness = reservedItems.Select(reservedItemMapper.Map).ToList();

            return reservedItemsBusiness.Count == 0 ? null : reservedItemsBusiness;

        }

        public List<ReservedItemBusinessModel> GetAllReservedItemsByAllReaders()
        {
            var reservedItems = this.uow.ReservedItems.GetAll().ToList();

            var reservedItemMapper = new ReservedItemMapper();

            var reservedItemsBusiness = reservedItems.Select(reservedItemMapper.Map).ToList();

            return reservedItemsBusiness.Count == 0 ? null : reservedItemsBusiness;
        }

        public string ReserveItem(int readerId, int itemId, int limitReservedItems)
        {
            var reader = this.uow.Readers.GetById(readerId);

            var item = this.uow.Items.GetById(itemId);

            var currentItem =
                this.uow.ReservedItems.GetAll()
                    .Where(r => (r.Reader.ReaderId == readerId) && (r.Item.Id == itemId))
                    .ToList();

            if (currentItem.Count != 0)
            {
                return "This item is already reserved by you";
            }

            var allReservedItemsCount = this.uow.ReservedItems.GetAll().Count(r => r.Reader.ReaderId == readerId);

            if (allReservedItemsCount >= limitReservedItems)
            {
                return string.Format("Sorry, you can reserve no more than {0} items", limitReservedItems);
            }

            this.uow.ReservedItems.Add(new ReservedItem
            {
                Reader = reader,
                Item = item,
                Date = DateTime.Now,
                IsReady = false
            });

            this.uow.Commit();

            return "Successfully reserved";

        }

        public bool DeleteReservedItemById(int reservedItemId)
        {
            this.uow.ReservedItems.Delete(reservedItemId);

            this.uow.Commit();

            //check if reserved book was really deleted, if not - false is returned
            return this.uow.ReservedItems.GetById(reservedItemId) == null;
        }

        public ItemBusinessModel GetItemInfoById(int id)
        {
            var item = this.uow.Items.GetById(id);
            ItemBusinessModel itemBusinessModel;
            if (item is Book)
            {
                itemBusinessModel = new BookMapper().Map(item as Book);
            }
            else if (item is Magazine)
            {
                itemBusinessModel = new MagazineMapper().Map(item as Magazine);
            }
            else
            {
                itemBusinessModel = new DiskMapper().Map(item as Disk);
            }

            return itemBusinessModel;
        }

        public bool SetReadyStatusForItem(int reservedItemId)
        {
            var resItem = this.uow.ReservedItems.GetById(reservedItemId);

            if (resItem.IsReady)
            {
                resItem.ReadyDate = null;

                resItem.IsReady = false;
            }
            else
            {
                resItem.ReadyDate = DateTime.Now;

                resItem.IsReady = true;    
            }

            var reader = resItem.Reader.Address;

            var itemName = resItem.Item.Name;

            this.uow.ReservedItems.Update(resItem);

            this.uow.Commit();

            return true;
        }

        public void CheckIfTimeFinished(int countDays)
        {
            var resItems = this.uow.ReservedItems.GetAll();

            foreach (var reservedItem in resItems)
            {
                if (reservedItem.ReadyDate != null &&
                    (reservedItem.IsReady && (reservedItem.ReadyDate.Value.AddDays(countDays)) < DateTime.Now))
                {
                    this.uow.ReservedItems.Delete(reservedItem);
                }
            }

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
