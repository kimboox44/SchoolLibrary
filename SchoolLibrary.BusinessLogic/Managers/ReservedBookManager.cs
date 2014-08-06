namespace SchoolLibrary.BusinessLogic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Facades.Interfaces;

    public class ReservedBookManager : IReservedBookManager, IDisposable
    {
        private IReservedBooksFacade reservedBooksFacade;

        public ReservedBookManager(IReservedBooksFacade reservedBooksFacade)
        {
            this.reservedBooksFacade = reservedBooksFacade;
        }

        public List<ReservedBookBusinessModel> GetReservedBooksByReaderId(int readerId)
        {
            return this.reservedBooksFacade.GetReservedBooksByReaderId(readerId);
        }

        public List<ReservedBookBusinessModel> GetReservedBooksForLibrarian()
        {
            return this.reservedBooksFacade.GetAllReservedBooksByAllReadersForLibrarian();
        }

        public bool DeleteReservedBookById(int reservedBookId)
        {
            return this.reservedBooksFacade.DeleteReservedBookById(reservedBookId);


        }

        public string DeleteReservedBooks(int[] resBooksId)
        {
            var countFailure = resBooksId.Count(id => this.DeleteReservedBookById(id) == false);

            return string.Format(
                "{0} books deleted;\n{1} books not deleted",
                resBooksId.Length - countFailure,
                countFailure);
        }

        public string ReserveBook(int readerId, int bookId)
        {
            return this.reservedBooksFacade.ReserveBook(readerId, bookId);
        }

        public string GetBookInfoById(int id)
        {
            var bookBisModel = this.reservedBooksFacade.GetBookInfoById(id);

            string authors = bookBisModel.Authors.ToList()
                .Aggregate(string.Empty, (current, s) => current + string.Format("{0} {1},", s.FirstName, s.LastName));

            authors = authors.Remove(authors.Length - 1);

            return string.Format(
                "Name: {0}; Authors: {1}; Page Count: {2}; Publisher: {3}; Year: {4}",
                bookBisModel.Name,
                authors,
                bookBisModel.PageCount,
                bookBisModel.Publisher,
                bookBisModel.Year);
        }

        public bool SetReadyStatusForBook(int reservedBookId)
        {
            return this.reservedBooksFacade.SetReadyStatusForBook(reservedBookId);
        }

        public void CheckIfTimeFinished()
        {
            this.reservedBooksFacade.CheckIfTimeFinished();
        }

        public void Dispose()
        {

            if (this.reservedBooksFacade as IDisposable != null)
            {
                (this.reservedBooksFacade as IDisposable).Dispose();
            }
        }
    }
}
