using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using SchoolLibrary.BusinessModels.MVCModels;

    public interface IBookWithAuthorsShortFacade
    {
        BookWithAuthorsShort GetBookById(int id);

        void UpdateBook(BookWithAuthorsShort book);

        void CreateBook(BookWithAuthorsShort book);

        List<BookWithAuthorsShort> GetAllBooks();

        void RemoveById(int id);
    }
}
