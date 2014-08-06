namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface IReservedItemManager
    {
        List<ReservedItemBusinessModel> GetReservedItemsByReaderId(int readerId);

        List<ReservedItemBusinessModel> GetReservedItemsForLibrarian();

        bool DeleteReservedItemById(int reservedItemId);

        string DeleteReservedItems(int[] resItemsId);

        string ReserveItem(int readerId, int itemId, int limitReservedItems);

        string GetItemInfoById(int id);

        bool SetReadyStatusForItem(int reservedItemId);

        void CheckIfTimeFinished(int countDays);

    }
}
