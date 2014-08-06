using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.DataAccess.Facades.Interfaces;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.BusinessModels.XMLSearchModels;
using SchoolLibrary.DataAccess.Entities;
using SchoolLibrary.DataAccess.Mappers;
using SchoolLibrary.DataAccess.UnitOfWork;
using System.Xml.Serialization;
using System.IO;

namespace SchoolLibrary.DataAccess.Facades
{
    public class SearchBookFacade : ISearchBookFacade, IDisposable
    {
        private ILibraryUow uow;

        public SearchBookFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public SearchBookFacade()
        {
            this.uow = new LibraryUow();
        }

        /// <summary>
        /// Gets All books
        /// </summary>
        /// <returns>The BookBusinessModel list</returns>
        public List<BookBusinessModel> GetAllBooks()
        {
            var models = new List<BookBusinessModel>();

            using (LibraryUow uow = new LibraryUow())
            {
                var bookInfoMapper = new BookMapper();
                var books = uow.Items.GetAll().OfType<Book>().ToList();
                books.ForEach(book => models.Add(bookInfoMapper.Map(book)));
            }

            return models;
        }

        public List<BookModel> ChangeBookType(List<BookBusinessModel> models)
        {
            List<BookModel> myBooks = models.Select(mod => new BookModel
            {
                Id = mod.Id,
                Name = mod.Name,
                Authors = mod.Authors != null ? (mod.Authors.Select(a => new AuthorModel { Id = a.Id, FirstName = a.FirstName, LastName = a.LastName }).ToList()) : null,
                Publisher = mod.Publisher,
                Year = mod.Year,
                PageCount = mod.PageCount
            }).ToList();

            return myBooks;
        }

        public string GetAllBooksXml(string pathToFile)
        {
            if (pathToFile == null)
                return "You must add the path to the destination folder!";

            var models = new List<BookBusinessModel>();
            models = GetAllBooks();

            StreamWriter myStreamWriter = null;

            try
            {
                List<BookModel> myBooks = ChangeBookType(models);

                XmlSerializer myXmlSerializer = new XmlSerializer(typeof(List<BookModel>));
                myStreamWriter = new StreamWriter(pathToFile);
                myXmlSerializer.Serialize(myStreamWriter, myBooks);
                //myStreamWriter.Close();
                return "Books was successfully serialized!!!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                if (myStreamWriter != null)
                    myStreamWriter.Close();
            }
        }

        public string SearchBooksXml(string pathToFile, string searchString)
        {
            if ((pathToFile == null) || (searchString == ""))
                return "You must add the path to the destination folder and enter search string!";

            StreamWriter myStreamWriter = null;

            var models = new List<BookBusinessModel>();
            models = GetBookByName(searchString);

            try
            {
                List<BookModel> myBooks = ChangeBookType(models);

                XmlSerializer myXmlSerializer = new XmlSerializer(typeof(List<BookModel>));
                myStreamWriter = new StreamWriter(pathToFile);
                myXmlSerializer.Serialize(myStreamWriter, myBooks);
                return "Books was successfully serialized!!!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                if (myStreamWriter != null)
                    myStreamWriter.Close();
            }
        }

        //public string GetAllBooksXml()
        //{
        //    var models = new List<BookBusinessModel>();

        //    using (LibraryUow uow = new LibraryUow())
        //    {
        //        var bookInfoMapper = new BookMapper();
        //        var books = uow.Books.GetAll().ToList();
        //        books.ForEach(book => models.Add(bookInfoMapper.Map(book)));
        //    }

        //    List<BookModel> myBooks = models.Select(mod => new BookModel
        //    {
        //        Id = mod.Id,
        //        Name = mod.Name,
        //        Authors = mod.Authors != null ? (mod.Authors.Select(a => new AuthorModel { Id = a.Id, FirstName = a.FirstName, LastName = a.LastName }).ToList()) : null,
        //        Publisher = mod.Publisher,
        //        Year = mod.Year,
        //        PageCount = mod.PageCount
        //    }).ToList();

        //    XmlSerializer myXmlSerializer = new XmlSerializer(typeof(List<BookModel>));
        //    StreamWriter myStreamWriter = new StreamWriter(@"c:\Books.xml");
        //    myXmlSerializer.Serialize(myStreamWriter, myBooks);
        //    myStreamWriter.Close();

        //    string sTemp = "Look for file on folder!!!";

        //    return sTemp;
        //}


        public List<BookBusinessModel> GetBookByName(string searchString)
        {
            var bookModels = new List<BookBusinessModel>();

            string[] words = searchString.Trim().Split(' ');

            using (LibraryUow uow = new LibraryUow())
            {
                var bookInfoMapper = new BookMapper();
                var books = uow.Items.GetAll().OfType<Book>().ToList();

                // Search by BookName
                foreach (var book in books)
                {
                    foreach (string word in words)
                    {
                        if (book.Name.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(book.Name.ToLower()))
                        {
                            var bookToMap = bookInfoMapper.Map(book);

                            // Distinct
                            if (!bookModels.Any(b => b.Id == bookToMap.Id))
                            {
                                bookModels.Add(bookToMap);
                            }
                        }
                    }
                }

                // Search by Authors FirstName or LastName
                foreach (var book in books)
                {
                    foreach (var author in book.Authors)
                    {
                        foreach (string word in words)
                        {
                            if (author.FirstName.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(author.FirstName.ToLower()) ||
                            author.LastName.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(author.LastName.ToLower()))
                            {
                                var bookToMap = bookInfoMapper.Map(book);

                                if (!bookModels.Any(b => b.Id == bookToMap.Id))
                                {
                                    bookModels.Add(bookToMap);
                                }
                            }
                        }
                    }
                }
            }

            return bookModels;
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }
        }
    }
}
