using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.DataAccess.Facades
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.Tests.Fakes;

    [TestClass]
    public class ReadersFacadeTests
    {
        private IReaderFacade readersFacade;

        private Reader testReader;

        private Fixture fixture;

        public ReadersFacadeTests()
        {
            this.fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));
            this.testReader = this.fixture.Create<Reader>();
            var uow = Initializer.GetLibraryUow();
            uow.Readers.Add(this.testReader);
            this.readersFacade = new ReaderFacade(uow);
        }

        [TestMethod]
        public void GetReaderByIdTest()
        {
            var result = this.readersFacade.GetReaderById(this.testReader.ReaderId);
            Assert.AreEqual(this.testReader.ReaderId, result.ReaderId);
            Assert.AreEqual(this.testReader.FirstName, result.FirstName);
            Assert.AreEqual(this.testReader.LastName, result.LastName);
            Assert.AreEqual(this.testReader.Address, result.Address);
            Assert.AreEqual(this.testReader.Birthday, result.Birthday);
            Assert.AreEqual(this.testReader.Phone, result.Phone);
            Assert.AreEqual(this.testReader.EMail, result.EMail);
        }

        [TestMethod]
        public void CreateReaderTest()
        {
            var tempReader = this.fixture.Create<Reader>();
            var mapper = new ReaderMapper();
            this.readersFacade.CreateReader(mapper.Map(tempReader));
            var result = readersFacade.GetReaderById(tempReader.ReaderId);
            Assert.AreEqual(tempReader.ReaderId, result.ReaderId);
            Assert.AreEqual(tempReader.FirstName, result.FirstName);
            Assert.AreEqual(tempReader.LastName, result.LastName);
            Assert.AreEqual(tempReader.Address, result.Address);
            Assert.AreEqual(tempReader.Birthday, result.Birthday);
            Assert.AreEqual(tempReader.Phone, result.Phone);
            Assert.AreEqual(tempReader.EMail, result.EMail);
        }

        [TestMethod]
        public void UpdateReaderTest()
        {
            int index = this.testReader.ReaderId;
            this.testReader = this.fixture.Create<Reader>();
            this.testReader.ReaderId = index;
            var mapper = new ReaderMapper();
            this.readersFacade.UpdateReader(mapper.Map(this.testReader));
            var result = this.readersFacade.GetReaderById(this.testReader.ReaderId);
            Assert.AreEqual(this.testReader.ReaderId, result.ReaderId);
            Assert.AreEqual(this.testReader.FirstName, result.FirstName);
            Assert.AreEqual(this.testReader.LastName, result.LastName);
            Assert.AreEqual(this.testReader.Address, result.Address);
            Assert.AreEqual(this.testReader.Birthday, result.Birthday);
            Assert.AreEqual(this.testReader.Phone, result.Phone);
            Assert.AreEqual(this.testReader.EMail, result.EMail);
        }

        [TestMethod]
        public void RemoveReaderByIdTest()
        {
            var result = this.readersFacade.GetReaderById(this.testReader.ReaderId);
            Assert.IsNotNull(result);
            this.readersFacade.RemoveReaderById(this.testReader.ReaderId);
            result = this.readersFacade.GetReaderById(this.testReader.ReaderId);
            Assert.IsNull(result);
        }
    }
}
