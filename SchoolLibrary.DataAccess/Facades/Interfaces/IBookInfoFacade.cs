using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public interface IBookInfoFacade
    {
        BookBusinessModel GetBookById(int id);

        BookBusinessModel GetBook(Book book);

        Book Update(BookBusinessModel bookBusinessModel);

        Book CreateBook(BookBusinessModel bookBusinessModel);

        Book RemoveBook(BookBusinessModel bookBusinessModel);

        void RemoveAuthorFromBook(int bookid, int authorid);

        void AddAuthorToBook(int bookid, int authorid);
                
        List<InventoryBusinessModel> GetAllInventories(int bookid);

        List<BookBusinessModel> GetAllBooks();
    }
}
