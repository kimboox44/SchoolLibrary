using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers
{
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.DataAccess.Facades.Interfaces;


    public class BookAuthorManager:IBookAuthorManager,IDisposable
    {
        private IBookFacade bookFacade;

        private IAuthorFacade authorFacade;
        private IBookAuthorFacade bookAuthorFacade;

        public BookAuthorManager(IBookFacade bookFacade, IAuthorFacade authorFacade, IBookAuthorFacade bookAuthorFacade)
        {
            this.authorFacade = authorFacade;
            this.bookFacade = bookFacade;
            this.bookAuthorFacade = bookAuthorFacade;
        }

        public BookAuthorModel GetBookAuthorById(int bookid, int authorid)
        {
            var book=this.bookFacade.GetBookById(bookid);
            var author = this.authorFacade.GetAuthorById(authorid);
            if (book == null)
            {
                return null;
            }

            if (author == null)
            {
                return null;
            }

            var bookauthor = new BookAuthorModel { BookName = book.Name, AuthorName = author.FirstName + " " + author.LastName, BookId = book.Id, AuthorId = author.Id };
            return bookauthor;
        }

        public void RemoveAuthorFromBook(int bookid, int authorid)
        {
            this.bookAuthorFacade.RemoveAuthorFromBook(bookid, authorid);

        }

        public void AddAuthorToBook(int bookid, int authorid)
        {
            this.bookAuthorFacade.AddAuthorToBook(bookid, authorid);
        }

        public void UpdateAuthorToBook(int bookid, IEnumerable<int> authorid)
        {
            var list = authorid.ToList();
            var book = this.bookFacade.GetBookById(bookid);
            foreach (var aut in book.Authors)
            {
                if (list.Contains(aut.Id))
                {
                    list.Remove(aut.Id);
                }
                else
                {
                    this.RemoveAuthorFromBook(bookid, aut.Id);
                }
            }
            foreach (var a in list)
            {
                this.AddAuthorToBook(bookid, a);
            }
        }

        public void Dispose()
        {
            if (this.bookFacade as IDisposable != null)
            {
                (this.bookFacade as IDisposable).Dispose();
            }

            if (this.authorFacade as IDisposable != null)
            {
                (this.authorFacade as IDisposable).Dispose();
            }
        }
    }
}
