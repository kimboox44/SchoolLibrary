using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Moq;

    using NUnit.Framework;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.Controllers;

    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
    using CollectionAssert = Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;

    /// <summary>
    /// Summary description for ConsignmentControllerTests
    /// </summary>
    [TestClass]
    public class ConsignmentControllerTests
    {
        private Fixture fixture;

        private Mock<IItemManager> itemManager;
        private Mock<IInventoryManager> inventoryManager;
        private Mock<IConsignmentManager> consignmentManager;

        private ConsignmentController consignmentController;


        public ConsignmentControllerTests()
        {
            this.fixture = new Fixture { RepeatCount = 3 };
            this.fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));
            this.itemManager = new Mock<IItemManager>();
            this.inventoryManager = new Mock<IInventoryManager>();
            this.consignmentManager = new Mock<IConsignmentManager>();


            this.consignmentController = new ConsignmentController(
                this.consignmentManager.Object,
                this.inventoryManager.Object,
                this.itemManager.Object);

        }

        [TestMethod]
        public void IndexTest()
        {
            var consignments = this.fixture.Create<List<ConsignmentBusinessModel>>() as List<ConsignmentBusinessModel>;
            this.consignmentManager.Setup(x => x.GetAllConsignments()).Returns(consignments);
            var result = this.consignmentController.Index() as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            CollectionAssert.AreEqual(consignments, (ICollection)result.Model, "Models are not equal");
        }

        [TestMethod]
        public void ItemTest()
        {
            var consignments = this.fixture.Create<List<ConsignmentBusinessModel>>() as List<ConsignmentBusinessModel>;
            var item = this.fixture.Create<ItemBusinessModel>();
            var id = item.Id;
            this.consignmentManager.Setup(x => x.GetAllConsignmentsByItemId(id)).Returns(consignments);
            var result = this.consignmentController.Index() as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
        }

        [TestMethod]
        public void AllInventoriesTest()
        {
            var inventories = this.fixture.Create<List<InventoryBusinessModel>>() as List<InventoryBusinessModel>;
            this.inventoryManager.Setup(x => x.GetAllInventory()).Returns(inventories);
            var result = this.consignmentController.Index() as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
        }


        [TestMethod]
        public void InventoriesTest()
        {
            var inventories = this.fixture.Create<List<InventoryBusinessModel>>() as List<InventoryBusinessModel>;
            var consignment = this.fixture.Create<ConsignmentBusinessModel>();
            var id = consignment.Id;
            this.inventoryManager.Setup(x => x.GetAllInventoryByConsignmentId(id)).Returns(inventories);
            var result = this.consignmentController.Index() as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
        }

        [TestMethod]
        public void DetailsTest()
        {
            var consignment = this.fixture.Create<ConsignmentBusinessModel>();
            var id = consignment.Id;
            this.consignmentManager.Setup(x => x.GetConsignmentById(id)).Returns((ConsignmentBusinessModel)consignment);
            var result = this.consignmentController.Details(id) as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(consignment, result.Model, "Models are not equal");
        }

        [TestMethod]
        public void WriteOffTest()
        {
            var consignments = this.fixture.Create<List<ConsignmentBusinessModel>>() as List<ConsignmentBusinessModel>;
            var consignment = this.fixture.Create<ConsignmentBusinessModel>();
            var id = consignment.Id;
            this.consignmentManager.Setup(x => x.WriteOffConsignmentById(id));
            var result = this.consignmentController.Index() as ViewResult;
            Assert.IsNotNull(result, "Should have returned a RedirectToRouteResult");
        }

        [TestMethod]
        public void WriteOffInventoryTest()
        {
            var inventories = this.fixture.Create<List<InventoryBusinessModel>>() as List<InventoryBusinessModel>;
            var inventory = this.fixture.Create<InventoryBusinessModel>();
            var id = inventory.InventoryId;
            this.inventoryManager.Setup(x => x.WriteOffInventoryById(id));
            var result = this.consignmentController.Index() as ViewResult;
            Assert.IsNotNull(result, "Should have returned a RedirectToRouteResult");
        }

    }
}
