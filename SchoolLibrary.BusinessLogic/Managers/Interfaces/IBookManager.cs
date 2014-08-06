using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;

    public interface IBookManager
    {
        BookBusinessModel GetBookById(int id);

        void UpdateBook(BookBusinessModel book);

        void CreateBook(BookBusinessModel book);

        List<BookBusinessModel> GetAllBooks();

        void RemoveBookById(int id);

        void RemoveBook(BookBusinessModel book);

        BookWithAuthorsShort GetBookWithAuthorsById(int id);

        void UpdateBookWithAuthors(BookWithAuthorsShort book);

        void CreateBookWithAuthors(BookWithAuthorsShort book);

        List<BookWithAuthorsShort> GetAllBooksWithAuthors();

        void RemoveBookWithAuthorsById(int id);

        void RemoveBookWithAuthors(BookWithAuthorsShort book);
    }
}
