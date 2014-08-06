namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface IReservedBooksFacade
    {
        List<ReservedBookBusinessModel> GetReservedBooksByReaderId(int readerId);

        List<ReservedBookBusinessModel> GetAllReservedBooksByAllReadersForLibrarian();

        string ReserveBook(int readerId, int bookId);

        string GiveBookToReader(int readerId, int bookId);

        bool DeleteReservedBookById(int reservedBookId);

        BookBusinessModel GetBookInfoById(int id);

        bool SetReadyStatusForBook(int reservedBookId);

        void CheckIfTimeFinished();
    }
}
