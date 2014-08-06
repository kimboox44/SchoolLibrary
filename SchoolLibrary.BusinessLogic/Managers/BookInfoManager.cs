using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers
{
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class BookInfoManager:IBookManager,IDisposable
    {

        private IBookFacade bookFacade;
        private IBookWithAuthorsShortFacade bookWithAuthorsShortFacade;
        public BookInfoManager(IBookFacade bookFacade, IBookWithAuthorsShortFacade bookWithAuthorsShortFacade)
        {
            this.bookFacade = bookFacade;
            this.bookWithAuthorsShortFacade = bookWithAuthorsShortFacade;
        }

        public BookBusinessModel GetBookById(int id)
        {
            return this.bookFacade.GetBookById(id);
        }

        public void UpdateBook(BookBusinessModel book)
        {
            this.bookFacade.UpdateBook(book);
        }

        public void CreateBook(BookBusinessModel book)
        {
            this.bookFacade.CreateBook(book);
        }

        public List<BookBusinessModel> GetAllBooks()
        {
            return this.bookFacade.GetAllBooks();
        }

        public void RemoveBookById(int id)
        {
            this.bookFacade.RemoveById(id);
        }

        public void RemoveBook(BookBusinessModel book)
        {
            this.bookFacade.RemoveById(book.Id);
        }

        public BookWithAuthorsShort GetBookWithAuthorsById(int id)
        {
            return this.bookWithAuthorsShortFacade.GetBookById(id);
        }

        public void UpdateBookWithAuthors(BookWithAuthorsShort book)
        {
            this.bookWithAuthorsShortFacade.UpdateBook(book);
        }

        public void CreateBookWithAuthors(BookWithAuthorsShort book)
        {
            this.bookWithAuthorsShortFacade.CreateBook(book);
        }

        public List<BookWithAuthorsShort> GetAllBooksWithAuthors()
        {
            return this.bookWithAuthorsShortFacade.GetAllBooks();
        }
        
        public void RemoveBookWithAuthorsById(int id)
        {
            this.bookWithAuthorsShortFacade.RemoveById(id);
        }

        public void RemoveBookWithAuthors(BookWithAuthorsShort book)
        {
            this.bookWithAuthorsShortFacade.RemoveById(book.Id);
        }
        
        public List<InventoryBusinessModel> GetAllInventory()
        {
            List<InventoryBusinessModel> inlist;
            using (LibraryUow uow = new LibraryUow())
            {
                inlist = new List<InventoryBusinessModel>();
                var inv = uow.Inventories.GetAll().ToList();
                var invMapper = new InventoryMapper();
  
                foreach (var a in inv)
                {
                    inlist.Add(invMapper.Map(a));
                }
                //var invBusinessModel = inv.Select(a => invMapper.Map(a)).ToList();
               
            }

            return inlist;

        }



        public void Dispose()
        {
            if (this.bookFacade as IDisposable != null)
            {
                (this.bookFacade as IDisposable).Dispose();
            }
        }
    }
}
