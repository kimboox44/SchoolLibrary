using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades
{
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class BookAuthorFacade:IBookAuthorFacade,IDisposable
    {
        private ILibraryUow uow;

        public BookAuthorFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public void RemoveAuthorFromBook(int bookid, int authorid)
        {
            var item = this.uow.Items.GetById(bookid);
            var author = this.uow.Authors.GetById(authorid);
            var book = item as Book;
            book.Authors.Remove(author);
            this.uow.Items.Update(book);
            this.uow.Commit();
        }

        public void AddAuthorToBook(int bookid, int authorid)
        {
            var item = this.uow.Items.GetById(bookid);
            var author = this.uow.Authors.GetById(authorid);
            var book = item as Book; 
            book.Authors.Add(author);
            this.uow.Items.Update(book);
            this.uow.Commit();
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
