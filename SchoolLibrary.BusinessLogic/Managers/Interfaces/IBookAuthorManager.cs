using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using SchoolLibrary.BusinessModels.MVCModels;

    public interface IBookAuthorManager
    {
        BookAuthorModel GetBookAuthorById(int bookid, int authorid);
        void RemoveAuthorFromBook(int bookid, int authorid);
        void AddAuthorToBook(int bookid, int authorid);
        //void UpdateAuthorToBook(int bookid, IEnumerable<int> authorid);
    }
}
