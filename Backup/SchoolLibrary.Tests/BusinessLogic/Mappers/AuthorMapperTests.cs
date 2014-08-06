using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.DataAccess.Mappers
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;

    [TestClass]
    public class AuthorMapperTests
    {

        private Author author;

        private AuthorBusinessModel authorModel;

        private AuthorMapper mapper;

        public AuthorMapperTests()
        {
            Fixture fixture = new Fixture { RepeatCount = 1 };
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));
            fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));

            this.author = fixture.Create<Author>();
            this.authorModel = fixture.Create<AuthorBusinessModel>();
            this.mapper = new AuthorMapper();
        }

        [TestMethod]
        public void EntityToModelMappingTest()
        {
            var result = mapper.Map(author);
            Assert.AreEqual(author.Id, result.Id, "Id is incorrect");
            Assert.AreEqual(author.FirstName, result.FirstName, "First name is incorrect");
            Assert.AreEqual(author.LastName, result.LastName, "Last name is incorrect");
            Assert.AreEqual(author.MiddleName, result.MiddleName, "Middle name is incorrect");
        }

        [TestMethod]
        public void ModelToEntityMappingTest()
        {
            var result = mapper.Map(this.authorModel);
            Assert.AreEqual(this.authorModel.Id, result.Id, "Id is incorrect");
            Assert.AreEqual(this.authorModel.FirstName, result.FirstName, "First name is incorrect");
            Assert.AreEqual(this.authorModel.LastName, result.LastName, "Last name is incorrect");
            Assert.AreEqual(this.authorModel.MiddleName, result.MiddleName, "Middle name is incorrect");
        }
    }
}
