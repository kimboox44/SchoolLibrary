using System;
using System.Text;
using System.Collections.Generic;
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

    /// <summary>
    /// Summary description for InventoryFacadeTests
    /// </summary>
    [TestClass]
    public class InventoryFacadeTests
    {
        private Fixture fixture;
        private IInventoryFacade inventoryFacade;
        private Inventory testInventory;

        public InventoryFacadeTests()
        {
            this.fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Inventory)));

            this.testInventory = this.fixture.Create<Inventory>();
            var uow = Initializer.GetLibraryUow();
            uow.Inventories.Add(this.testInventory);
            this.inventoryFacade = new InventoryFacade(uow);
        }

        [TestMethod]
        public void GetAllInventoryByConsignmentIdTest()
        {
            var result = this.inventoryFacade.GetAllInventoryByConsignmentId(this.testInventory.Consignment.Id);
            foreach (var inventory in result)
            {
                Assert.IsNotNull(inventory);
            }
        }

        [TestMethod]
        public void GetInventoryByIdTest()
        {
            var result = this.inventoryFacade.GetInventoryById(testInventory.InventoryId);
            Assert.AreEqual(testInventory.InventoryId, result.InventoryId);
            Assert.AreEqual(testInventory.Number, result.Number);
            Assert.AreEqual(testInventory.IsAvailable, result.IsAvailable);
            Assert.AreEqual(testInventory.WriteOffDate, result.WriteOffDate);
        }

        [TestMethod]
        public void GetAllInventoryTest()
        {
            var result = this.inventoryFacade.GetAllInventory();
            foreach (var inventory in result)
            {
                Assert.IsNotNull(inventory);
            }
        }

        [TestMethod]
        public void CreateInventoryTest()
        {
            var tempInventory = this.fixture.Create<Inventory>();
            var mapper = new InventoryMapper();
            this.inventoryFacade.CreateInventory(mapper.Map(tempInventory));
            var result = inventoryFacade.GetInventoryById(tempInventory.InventoryId);
            Assert.AreEqual(tempInventory.InventoryId, result.InventoryId);
            Assert.AreEqual(tempInventory.Number, result.Number);
            Assert.AreEqual(tempInventory.IsAvailable, result.IsAvailable);
            Assert.AreEqual(tempInventory.WriteOffDate, result.WriteOffDate);
        }

        [TestMethod]
        public void UpdateInventoryTest()
        {
            var mapper = new InventoryMapper();
            this.inventoryFacade.UpdateInventory(mapper.Map(this.testInventory));
            var result = this.inventoryFacade.GetInventoryById(this.testInventory.InventoryId);
            Assert.AreEqual(this.testInventory.InventoryId, result.InventoryId);
            Assert.AreEqual(this.testInventory.Number, result.Number);
            Assert.AreEqual(this.testInventory.IsAvailable, result.IsAvailable);
            Assert.AreEqual(this.testInventory.WriteOffDate, result.WriteOffDate);
        }

        [TestMethod]
        public void RemoveInventoryByIdTest()
        {
            var result = this.inventoryFacade.GetInventoryById(this.testInventory.InventoryId);
            Assert.IsNotNull(result);
            this.inventoryFacade.RemoveInventoryById(this.testInventory.InventoryId);
            result = this.inventoryFacade.GetInventoryById(this.testInventory.InventoryId);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void WriteOffInventoryByIdTest()
        {
            var result = this.inventoryFacade.GetInventoryById(this.testInventory.InventoryId);
            Assert.IsNotNull(result);
            this.inventoryFacade.WriteOffInventoryById(this.testInventory.InventoryId);
        }
    }
}
