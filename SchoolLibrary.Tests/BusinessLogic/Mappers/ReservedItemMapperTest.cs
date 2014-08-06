namespace SchoolLibrary.Tests.DataAccess.Mappers
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using System;
    using SchoolLibrary.DataAccess.Mappers;
    using System.Reflection;
    using System.Collections;

    [TestClass]
    public class ReservedItemMapperTest
    {
        private ReservedItem item;

        private ReservedItemBusinessModel itemModel;

        private ReservedItemMapper mapper;

        private Reader reader;

        private ReaderBusinessModel readerModel;

        public ReservedItemMapperTest()
        {
            Fixture fixture = new Fixture { RepeatCount = 1 };
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));
            fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));

            this.item = fixture.Create<ReservedItem>();
            this.itemModel = fixture.Create<ReservedItemBusinessModel>();
            


        }

        [TestMethod]
        public void EntityToModelMappingTest()
        {
            this.mapper = new ReservedItemMapper();
            var result = this.mapper.Map(this.item);
            Assert.AreEqual(this.item.Id, result.Id, "Id is incorrect");
            Assert.AreEqual(this.item.IsReady, result.IsReady, "IsReady is incorrect");
            Assert.AreEqual(this.item.ReadyDate, result.ReadyDate, "ReadyDate is incorrect");
            Assert.AreEqual(this.item.Date, result.Date, "Date is incorrect");

            Assert.IsNotNull(this.item.Reader);
            Assert.IsNotNull(this.item.Item);
        }

        [TestMethod]
        public void ModelToEntityMappingTest()
        {
            this.mapper = new ReservedItemMapper();
            var result = this.mapper.Map(this.itemModel);
            Assert.AreEqual(this.itemModel.Id, result.Id, "Id is incorrect");
            Assert.AreEqual(this.itemModel.IsReady, result.IsReady, "IsReady is incorrect");
            Assert.AreEqual(this.itemModel.ReadyDate, result.ReadyDate, "ReadyDate is incorrect");
            Assert.AreEqual(this.itemModel.Date, result.Date, "Date is incorrect");

            Assert.IsNotNull(this.itemModel.Reader);
            Assert.IsNotNull(this.itemModel.Item);
        }
    }
}