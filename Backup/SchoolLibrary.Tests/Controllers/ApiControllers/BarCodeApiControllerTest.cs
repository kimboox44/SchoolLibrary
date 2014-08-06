using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.Controllers.ApiControllers
{
    using System.IO;

    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Hosting;

    using Moq;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;

    using SchoolLibrary.Controllers.WebAPIControllers;

    [TestClass]
    public class BarCodeApiControllerTest
    {
        private Fixture fixture;

        private Mock<IConsignmentManager> consignmentManager;

        private Mock<IInventoryManager> inventoryManager;

        private BarCodeApiController barCodeApiController;

        public BarCodeApiControllerTest()
        {
            this.fixture = new Fixture { RepeatCount = 3 };
            this.fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));
            this.consignmentManager = new Mock<IConsignmentManager>();
            this.inventoryManager = new Mock<IInventoryManager>();

            this.barCodeApiController = new BarCodeApiController(this.consignmentManager.Object, this.inventoryManager.Object)
            {
                Request = new HttpRequestMessage
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };
        }

        [TestMethod]
        public void GetBarCodeConsignmentTest_StatusCodeOK()
        {
            int number = It.IsAny<int>();
            this.consignmentManager.Setup(x => x.GetPdfByConsignmentNumber(number)).Returns(new MemoryStream());
            
            var result = this.barCodeApiController.GetBarCodeConsignment(number);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        }

        [TestMethod]
        public void GetBarCodeConsignmentTest_StatusCodeNotFound()
        {
            var number = It.IsAny<int>();
            this.consignmentManager.Setup(x => x.GetPdfByConsignmentNumber(number)).Returns((MemoryStream)null);
            
            var result = this.barCodeApiController.GetBarCodeConsignment(number);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);

        }
        [TestMethod]
        public void GetBarCodeInventoryTest_StatusCodeOK()
        {

            var number = It.IsAny<string>();
            this.inventoryManager.Setup(x => x.GetPdfByInventoryNumber(number)).Returns(new MemoryStream());
            
            var result = this.barCodeApiController.GetBarCodeInventory(number);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void GetBarCodeInventoryTest_StatusCodeNotFound()
        {
            var number = It.IsAny<string>();
            this.inventoryManager.Setup(x => x.GetPdfByInventoryNumber(number)).Returns((MemoryStream)null);
            
            var result = this.barCodeApiController.GetBarCodeInventory(number);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);

        }
    }
}
