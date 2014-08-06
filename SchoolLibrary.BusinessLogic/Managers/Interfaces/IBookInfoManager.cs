using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public interface IBookInfoManager
    {
        BookBusinessModel GetBookById(int id);

        BookBusinessModel GetBook(Book book);

        Book Update(BookBusinessModel bookBusinessModel);

        Book CreateBook(BookBusinessModel bookBusinessModel);

        Book RemoveBook(BookBusinessModel bookBusinessModel);

        void RemoveAuthorFromBook(int bookid, int authorid);

        void AddAuthorToBook(int bookid, int authorid);

        List<BookBusinessModel> GetAllBooks();

        List<InventoryBusinessModel> GetAllInventories(int bookid);
    }
}
