namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface IReservedItemsFacade
    {
        List<ReservedItemBusinessModel> GetReservedItemsByReaderId(int readerId);

        List<ReservedItemBusinessModel> GetAllReservedItemsByAllReaders();

        string ReserveItem(int readerId, int itemId, int limitReservedItems);

        bool DeleteReservedItemById(int reservedItemId);

        ItemBusinessModel GetItemInfoById(int id);

        bool SetReadyStatusForItem(int reservedItemId);

        void CheckIfTimeFinished(int countDays);
    }
}
