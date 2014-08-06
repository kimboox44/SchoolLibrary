namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface IReservedBookManager
    {
        List<ReservedBookBusinessModel> GetReservedBooksByReaderId(int readerId);

        List<ReservedBookBusinessModel> GetReservedBooksForLibrarian();

        bool DeleteReservedBookById(int reservedBookId);

        string DeleteReservedBooks(int[] resBooksId);

        string ReserveBook(int readerId, int bookId);

        string GetBookInfoById(int id);

        bool SetReadyStatusForBook(int reservedBookId);

        void CheckIfTimeFinished();
    }
}
