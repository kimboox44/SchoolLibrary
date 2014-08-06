namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISearchBookFacade
    {
        List<BookBusinessModel> GetAllBooks();

        List<BookBusinessModel> GetBookByName(string searchString);

        string GetAllBooksXml(string pathToFile);

        string SearchBooksXml(string pathToFile, string searchString);
    }
}
