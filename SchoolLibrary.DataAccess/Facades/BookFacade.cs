namespace SchoolLibrary.DataAccess.Facades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class BookFacade : IBookFacade, IDisposable
    {
        private ILibraryUow uow;

        public BookFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public BookBusinessModel GetBookById(int id)
        {
            var mapper = new BookMapper();
            var item = this.uow.Items.GetById(id);
            var book = item as Book;
            return mapper.Map(book);
        }

        public void UpdateBook(BookBusinessModel book)
        {
            var mapper = new BookMapper();
            this.uow.Items.Update(mapper.Map(book));
            this.uow.Commit();
        }

        public void CreateBook(BookBusinessModel book)
        {
            var mapper = new BookMapper();
            var bookNew = mapper.Map(book);
            this.uow.Items.Add(bookNew);
            this.uow.Commit();
           
            book.Id = bookNew.Id; // updates the book.Id to Id value from DB
        }

        public List<BookBusinessModel> GetAllBooks()
        {
            var mapper = new BookMapper();
            List<Book> books = this.uow.Items.GetAll().OfType<Book>().ToList();
            return books.Select(mapper.Map).ToList();
        }

        public void RemoveById(int id)
        {
            this.uow.Items.Delete(id);
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
