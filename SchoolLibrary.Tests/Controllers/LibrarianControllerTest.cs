using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Moq;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.Controllers;

    [TestClass]
    public class LibrarianControllerTest
    {
        private Fixture fixture;
        private Mock<IReaderHistoryManager> readerHistoryManager;
        private Mock<IReaderManager> readerManager;
        private LibrarianController librarianController;

        public LibrarianControllerTest()
        {
            this.fixture = new Fixture { RepeatCount = 3 };
            this.fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.readerHistoryManager = new Mock<IReaderHistoryManager>();
            this.readerManager = new Mock<IReaderManager>();

            this.librarianController = new LibrarianController(
                this.readerManager.Object,
                this.readerHistoryManager.Object);

            }

        [TestMethod]
        public void ReaderHistoryTest()
        {
            var items = this.fixture.Create<List<ReaderHistoryBusinessModel>>() as List<ReaderHistoryBusinessModel>;
            this.readerHistoryManager.Setup(x => x.GetReaderHistoriesByReaderId(1)).Returns(items);
            var result = this.librarianController.ReaderHistory(1, null,1) as ViewResult;
            CollectionAssert.AreEqual(items, (ICollection)result.Model, "Models are not equal");
        }
    }
}
