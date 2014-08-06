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
    public class InventoryMapperTests
    {
        private Fixture fixture;

        public InventoryMapperTests()
        {
            this.fixture = new Fixture();
            this.fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Inventory)));
            this.fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(InventoryBusinessModel)));
        }

        [TestMethod]
        public void InventoryEntityToModelMappingTest()
        {
            var inventory = this.fixture.Create<Inventory>();
            var inventoryMapper = new InventoryMapper();
            var inventoryModel = inventoryMapper.Map(inventory);
            Assert.AreEqual(inventory.InventoryId, inventoryModel.InventoryId, "Id is not correct");
            Assert.AreEqual(inventory.IsAvailable, inventoryModel.IsAvailable, "IsAvailable is not correct");
            Assert.AreEqual(inventory.Number, inventoryModel.Number, "Number is not correct");
            Assert.AreEqual(inventory.WriteOffDate, inventoryModel.WriteOffDate, "WriteOffDate is not correct");
            Assert.AreEqual(inventory.Item, inventoryModel.Item, "Item is not correct");
        }

        [TestMethod]
        public void InventoryModelToEntityMappingTest()
        {
            var inventoryModel = this.fixture.Create<InventoryBusinessModel>();
            var inventoryMapper = new InventoryMapper();
            var inventory = inventoryMapper.Map(inventoryModel);
            Assert.AreEqual(inventoryModel.InventoryId, inventory.InventoryId, "Id is not correct");
            Assert.AreEqual(inventoryModel.IsAvailable, inventory.IsAvailable, "IsAvailable is not correct");
            Assert.AreEqual(inventoryModel.Number, inventory.Number, "Number is not correct");
            Assert.AreEqual(inventoryModel.WriteOffDate, inventory.WriteOffDate, "WriteOffDate is not correct");
            Assert.AreEqual(inventoryModel.Item, inventory.Item, "Item is not correct");
        }
    }
}
