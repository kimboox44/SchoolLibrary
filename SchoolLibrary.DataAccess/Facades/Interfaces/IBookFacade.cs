using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;

    public interface IBookFacade
    {
        BookBusinessModel GetBookById(int id);

        void UpdateBook(BookBusinessModel book);

        void CreateBook(BookBusinessModel book);

        List<BookBusinessModel> GetAllBooks();

        void RemoveById(int id);
    }
}
