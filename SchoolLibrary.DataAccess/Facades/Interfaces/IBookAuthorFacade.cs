using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    public interface IBookAuthorFacade
    {
        void RemoveAuthorFromBook(int bookid, int authorid);

        void AddAuthorToBook(int bookid, int authorid);
    }
}
