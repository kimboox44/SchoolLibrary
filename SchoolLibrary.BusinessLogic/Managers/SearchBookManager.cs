namespace SchoolLibrary.BusinessLogic.Managers
{
    using System;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades;

    /// <summary>
    /// Represents SearchBookManager logic
    /// </summary>
    public class SearchBookManager : ISearchBookManager, IDisposable
    {

        private ILibraryUow uow;
        private ISearchBookFacade searchBookFacade;

        public SearchBookManager(ILibraryUow uow, ISearchBookFacade searchBookFacade)
        {
            this.uow = uow;
            this.searchBookFacade = searchBookFacade;
        }

        public SearchBookManager()
        {
            this.uow = new LibraryUow();
            this.searchBookFacade = new SearchBookFacade();
        }

        #region Public methods

        public List<BookBusinessModel> GetAllBooks()
        {
            return this.searchBookFacade.GetAllBooks();
        }

        public List<BookBusinessModel> GetBookByName(string searchString)
        {
            return searchBookFacade.GetBookByName(searchString);
        }

        public string GetAllBooksXml(string pathToFile)
        {
            return searchBookFacade.GetAllBooksXml(pathToFile);
        }

        public string SearchBooksXml(string pathToFile, string searchString)
        {
            return searchBookFacade.SearchBooksXml(pathToFile, searchString);
        }

        /// <summary>
        /// Gets All books
        /// </summary>
        /// <returns>The BookBusinessModel list</returns>
        //public List<BookBusinessModel> GetAllBooks()
        //{
        //    var models = new List<BookBusinessModel>();

        //    using (LibraryUow uow = new LibraryUow())
        //    {
        //        var bookInfoMapper = new BookMapper();
        //        var books = uow.Books.GetAll().ToList();
        //        books.ForEach(book => models.Add(bookInfoMapper.Map(book)));
        //    }

        //    return models;
        //}

        //public List<BookBusinessModel> GetBookByName(string searchString)
        //{
        //    var bookModels = new List<BookBusinessModel>();

        //    string[] words = searchString.Trim().Split(' ');

        //    using (LibraryUow uow = new LibraryUow())
        //    {
        //        var bookInfoMapper = new BookMapper();
        //        var books = uow.Books.GetAll().ToList();

        //        // Search by BookName
        //        foreach (var book in books)
        //        {
        //            foreach (string word in words)
        //            {
        //                if (book.Name.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(book.Name.ToLower()))
        //                {
        //                    var bookToMap = bookInfoMapper.Map(book);

        //                    // Distinct
        //                    if (!bookModels.Any(b => b.Id == bookToMap.Id))
        //                    {
        //                        bookModels.Add(bookToMap);
        //                    }
        //                }
        //            }
        //        }

        //        // Search by Authors FirstName or LastName
        //        foreach (var book in books)
        //        {
        //            foreach (var author in book.Authors)
        //            {
        //                foreach (string word in words)
        //                {
        //                    if (author.FirstName.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(author.FirstName.ToLower()) ||
        //                    author.LastName.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(author.LastName.ToLower()))
        //                    {
        //                        var bookToMap = bookInfoMapper.Map(book);

        //                        if (!bookModels.Any(b => b.Id == bookToMap.Id))
        //                        {
        //                            bookModels.Add(bookToMap);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return bookModels;
        //}

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }

            if (this.searchBookFacade as IDisposable != null)
            {
                (this.searchBookFacade as IDisposable).Dispose();
            }
        }

        #endregion

    }
}
