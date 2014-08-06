namespace SchoolLibrary.DataAccess.Facades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class ReservedBooksFacade : IReservedBooksFacade, IDisposable
    {
        private ILibraryUow uow;

        public ReservedBooksFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public List<ReservedBookBusinessModel> GetReservedBooksByReaderId(int readerId)
        {
            var reservedBooks = this.uow.ReservedBooks.GetAll().Where(r => r.Reader.ReaderId == readerId)
                .Select(r => r).ToList();

            var reservedBookMapper = new ReservedBookMapper();

            var reservedBooksBusiness = reservedBooks.Select(reservedBookMapper.Map).ToList();

            return reservedBooksBusiness.Count == 0 ? null : reservedBooksBusiness;
        }

        public List<ReservedBookBusinessModel> GetAllReservedBooksByAllReadersForLibrarian()
        {
            var reservedBooks = this.uow.ReservedBooks.GetAll().ToList();

            var reservedBookMapper = new ReservedBookMapper();

            var reservedBooksBusiness = reservedBooks.Select(reservedBookMapper.Map).ToList();

            return reservedBooksBusiness.Count == 0 ? null : reservedBooksBusiness;
        }

        public string ReserveBook(int readerId, int bookId)
        {
            const int LimitReservedBooks = 3;

            var reader = this.uow.Readers.GetById(readerId);

            var book = this.uow.Books.GetById(bookId);

            var allReservedBooksCount = this.uow.ReservedBooks.GetAll().Count(r => r.Reader.ReaderId == readerId);

            if (allReservedBooksCount >= LimitReservedBooks)
            {
                return string.Format("Sorry, you can reserve no more than {0} books", LimitReservedBooks);
            }

            var currentBook =
                this.uow.ReservedBooks.GetAll()
                    .Where(r => (r.Reader.ReaderId == readerId) && (r.Book.Id == bookId))
                    .ToList();

            if (currentBook.Count != 0)
            {
                return "This book is already reserved by you";
            }

            this.uow.ReservedBooks.Add(new ReservedBook
            {
                Reader = reader,
                Book = book,
                Date = DateTime.Now,
                IsReady = false
            });

            this.uow.Commit();

            return "Successfully reserved";
        }

        public string GiveBookToReader(int readerId, int bookId)
        {
            var reader = this.uow.Readers.GetById(readerId);

            var book = this.uow.Books.GetById(bookId);

            var inventories = book.Inventories;

            if (inventories.Count == 0)
            {
                return "Sorry, there are no this book in the library";
            }

            int? inventoryId = null;

            foreach (var inv in inventories.Where(inv => inv.IsAvailable))
            {
                inventoryId = inv.InventoryId;
                inv.IsAvailable = false;
                break;
            }

            if (inventoryId == null)
            {
                return "Sorry, this book is not available now";
            }

            this.uow.ReadersHistories.Add(
                new ReaderHistory
                {
                    StartDate = DateTime.Now,
                    FinishDate = DateTime.Now.AddMonths(1),
                    Reader = reader,
                    Inventory = this.uow.Inventories.GetById((int)inventoryId)
                });

            this.uow.Commit();

            return string.Format("Book has been successfully given to {0} {1}", reader.FirstName, reader.LastName);
        }

        public bool DeleteReservedBookById(int reservedBookId)
        {
            this.uow.ReservedBooks.Delete(reservedBookId);

            this.uow.Commit();

            //check if reserved book was really deleted, if not - false is returned
            return this.uow.ReservedBooks.GetById(reservedBookId) == null;
        }

        public BookBusinessModel GetBookInfoById(int id)
        {
            var book = this.uow.Books.GetById(id);

            return book == null ? null : new BookMapper().Map(book);
        }

        public bool SetReadyStatusForBook(int reservedBookId)
        {
            var resBook = this.uow.ReservedBooks.GetById(reservedBookId);

            resBook.ReadyDate = DateTime.Now;

            resBook.IsReady = true;

            //this.uow.ReservedBooks.Update(resBook);

            this.uow.Commit();

            return true;
        }

        public void CheckIfTimeFinished()
        {
            const int CountDays = 5;

            var resBooks = this.uow.ReservedBooks.GetAll();

            foreach (var reservedBook in resBooks)
            {
                if (reservedBook.ReadyDate != null &&
                    (reservedBook.IsReady && (reservedBook.ReadyDate.Value.AddDays(CountDays)) < DateTime.Now))
                {
                    this.uow.ReservedBooks.Delete(reservedBook);
                }
            }

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
