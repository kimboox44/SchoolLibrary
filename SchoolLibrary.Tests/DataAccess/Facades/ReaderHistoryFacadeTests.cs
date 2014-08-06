using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.DataAccess.Facades
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.Tests.Fakes;

    [TestClass]
    public class ReaderHistoryFacadeTests
    {
        private IReaderHistoryFacade readerHistoryFacade;
        private ReaderHistory readerHistory;
        private ReaderHistoryBusinessModel readerHistoryBusiness;

        public ReaderHistoryFacadeTests(IReaderHistoryFacade readerHistoryFacade)
        {
            var uow = Initializer.GetLibraryUow();
            this.readerHistoryFacade = readerHistoryFacade;
            this.readerHistory = new ReaderHistory
            {
                StartDate = Convert.ToDateTime("2013-07-29"),
                ReturnDate = Convert.ToDateTime("2013-08-15"),
                FinishDate = Convert.ToDateTime("2013-12-10")
            };
            uow.ReadersHistories.Add(this.readerHistory);
            //this.readerHistoryFacade = new ReaderHistoryFacade(uow);
        }

        [TestMethod]
        public void GetReaderHistoriesByReaderId()
        {
            var test = this.readerHistoryFacade.GetReaderHistoriesByReaderId(1);
            foreach (var History in test)
            {
                Assert.AreEqual(this.readerHistory.StartDate, History.StartDate);
                Assert.AreEqual(this.readerHistory.ReturnDate, History.ReturnDate);
                Assert.AreEqual(this.readerHistory.FinishDate, History.FinishDate);

                break;
            }
        }
    }
}
