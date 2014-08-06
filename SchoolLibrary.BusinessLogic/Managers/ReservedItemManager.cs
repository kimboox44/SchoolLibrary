namespace SchoolLibrary.BusinessLogic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Facades.Interfaces;

    public class ReservedItemManager : IReservedItemManager, IDisposable
    {
        private IReservedItemsFacade reservedItemsFacade;

        public ReservedItemManager(IReservedItemsFacade reservedItemsFacade)
        {
            this.reservedItemsFacade = reservedItemsFacade;
        }

        public List<ReservedItemBusinessModel> GetReservedItemsByReaderId(int readerId)
        {
            return this.reservedItemsFacade.GetReservedItemsByReaderId(readerId);
        }

        public List<ReservedItemBusinessModel> GetReservedItemsForLibrarian()
        {
            return this.reservedItemsFacade.GetAllReservedItemsByAllReaders();
        }

        public bool DeleteReservedItemById(int reservedItemId)
        {
            return this.reservedItemsFacade.DeleteReservedItemById(reservedItemId);
        }

        public string DeleteReservedItems(int[] resItemsId)
        {
            var countFailure = resItemsId.Count(id => this.DeleteReservedItemById(id) == false);

            return string.Format(
                "{0} items deleted;\n{1} items not deleted",
                resItemsId.Length - countFailure,
                countFailure);
        }

        public string ReserveItem(int readerId, int itemId, int limitReservedItems)
        {
            return this.reservedItemsFacade.ReserveItem(readerId, itemId, limitReservedItems);
        }

        public string GetItemInfoById(int id)
        {
            var itemBisModel = this.reservedItemsFacade.GetItemInfoById(id);

            if (itemBisModel is BookBusinessModel)
            {
                var authors = (itemBisModel as BookBusinessModel).Authors.ToList();

                string authorsString = string.Empty;

                foreach (var authorBusinessModel in authors)
                {
                    authorsString += string.Format(
                        "{0} {1};",
                        authorBusinessModel.FirstName,
                        authorBusinessModel.LastName);
                }

                return string.Format(
                    "Name: {0}; Authors: {1} Page Count: {2}; Publisher: {3}; Year: {4}",
                    (itemBisModel as BookBusinessModel).Name,
                    authorsString,
                    (itemBisModel as BookBusinessModel).PageCount,
                    (itemBisModel as BookBusinessModel).Publisher,
                    (itemBisModel as BookBusinessModel).Year);
            }

            else if (itemBisModel is MagazineBusinessModel)
            {
                return string.Format(
                    "Name: {0}; Issue: {1}; Page Count: {2}; Publisher: {3}; Year: {4}",
                    (itemBisModel as MagazineBusinessModel).Name,
                    (itemBisModel as MagazineBusinessModel).Issue,
                    (itemBisModel as MagazineBusinessModel).PageCount,
                    (itemBisModel as MagazineBusinessModel).Publisher,
                    (itemBisModel as MagazineBusinessModel).Year);
            }

            else if (itemBisModel is DiskBusinessModel)
            {
                return string.Format(
                    "Name: {0}; Producer: {1}; Type: {2}; Year: {3}",
                    (itemBisModel as DiskBusinessModel).Name,
                    (itemBisModel as DiskBusinessModel).Producer,
                    (itemBisModel as DiskBusinessModel).Type,
                    (itemBisModel as DiskBusinessModel).Year);
            }

            return string.Empty;
        }

        public bool SetReadyStatusForItem(int reservedItemId)
        {
            return this.reservedItemsFacade.SetReadyStatusForItem(reservedItemId);
        }

        public void CheckIfTimeFinished(int countDays)
        {
            this.reservedItemsFacade.CheckIfTimeFinished(countDays);
        }

        public void Dispose()
        {

            if (this.reservedItemsFacade as IDisposable != null)
            {
                (this.reservedItemsFacade as IDisposable).Dispose();
            }
        }
    }
}
