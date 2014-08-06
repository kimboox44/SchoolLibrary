using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.DataAccess.Mappers
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;

    [TestClass]
    public class BookWithAuthorsShortMapperTests
    {
        private Fixture fixture;

        public BookWithAuthorsShortMapperTests()
        {
            this.fixture = new Fixture { RepeatCount = 3 };
            this.fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));
            this.fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));
        }

        [TestMethod]
        public void EntityToModelMappingTest()
        {
            
            var book = this.fixture.Create<Book>();
            var bookMapper = new BookWithAuthorsShortMapper();
            var bookModel = bookMapper.Map(book);
            Assert.AreEqual(book.Id, bookModel.Id, "Id is not correct");
            Assert.AreEqual(book.Name, bookModel.Name, "Name is not correct");
            Assert.AreEqual(book.PageCount, bookModel.PageCount, "PageCount is not correct");
            Assert.AreEqual(book.Publisher, bookModel.Publisher, "Publisher is not correct");
            Assert.AreEqual(book.Year, bookModel.Year, "Year is not correct");
            Assert.IsNotNull(bookModel.Authors);
            Assert.IsNotNull(bookModel.Tags);
            Assert.IsNotNull(bookModel.Inventories);
            Assert.IsNotNull(bookModel.ReservedItems);
        }

        [TestMethod]
        public void ModelToEntityMappingTest()
        {
            
            var bookModel = this.fixture.Create<BookWithAuthorsShort>();
            var bookMapper = new BookWithAuthorsShortMapper();
            var book = bookMapper.Map(bookModel);
            Assert.AreEqual(bookModel.Id, book.Id, "Id is not correct");
            Assert.AreEqual(bookModel.Name, book.Name, "Name is not correct");
            Assert.AreEqual(bookModel.PageCount, book.PageCount, "PageCount is not correct");
            Assert.AreEqual(bookModel.Publisher, book.Publisher, "Publisher is not correct");
            Assert.AreEqual(bookModel.Year, book.Year, "Year is not correct");
            Assert.IsNotNull(book.Authors);
            Assert.IsNotNull(book.Tags);
            Assert.IsNotNull(book.Inventories);
            Assert.IsNotNull(book.ReservedItems);

        }
    }
}
