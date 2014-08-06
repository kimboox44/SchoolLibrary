namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessModels.Models;

    public interface ISearchBookManager
    {
        List<BookBusinessModel> GetAllBooks();
        List<BookBusinessModel> GetBookByName(string searchString);
        string GetAllBooksXml(string pathToFile);
        string SearchBooksXml(string pathToFile, string searchString);
    }
}
