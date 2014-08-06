using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.BusinessLogic.Mappers
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;

    [TestClass]
    public class ConsignmentMapperTests
    {
        private Fixture fixture;

        public ConsignmentMapperTests()
        {
            this.fixture = new Fixture();
            this.fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Consignment)));
            this.fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(ConsignmentBusinessModel)));
        }

        [TestMethod]
        public void ConsignmentEntityToModelMappingTest()
        {
            var consignment = fixture.Create<Consignment>();
            var consignmentMapper = new ConsignmentMapper();
            var consignmentModel = consignmentMapper.Map(consignment);
            Assert.AreEqual(consignment.Id, consignmentModel.Id, "Id is not correct");
            Assert.AreEqual(consignment.Number, consignmentModel.Number, "Number is not correct");
            Assert.AreEqual(consignment.ArrivalDate, consignmentModel.ArrivalDate, "ArrivalDate is not correct");
            Assert.AreEqual(consignment.WriteOffDate, consignmentModel.WriteOffDate, "WriteOffDate is not correct");
            Assert.AreEqual(consignment.Item, consignmentModel.Item, "Item is not correct");
            Assert.IsNotNull(consignmentModel.Inventories);
        }

        [TestMethod]
        public void ConsignmentModelToEntityMappingTest()
        {
            var consignmentModel = fixture.Create<ConsignmentBusinessModel>();
            var consignmentMapper = new ConsignmentMapper();
            var consignment = consignmentMapper.Map(consignmentModel);
            Assert.AreEqual(consignmentModel.Id, consignment.Id, "Id is not correct");
            Assert.AreEqual(consignmentModel.ArrivalDate, consignment.ArrivalDate, "ArrivalDate is not correct");
            Assert.AreEqual(consignmentModel.WriteOffDate, consignment.WriteOffDate, "WriteOffDate is not correct");
            Assert.AreEqual(consignmentModel.Item, consignment.Item, "Item is not correct");
        }
    }
}
