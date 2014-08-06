using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.Controllers.ApiControllers
{
    using System.Collections.Generic;
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
    public class ReaderApiControllerTests
    {
        private Fixture fixture;

        public ReaderApiControllerTests()
        {
            this.fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            //fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));
        }

        [TestMethod]
        public void ReaderApiCreateBadRequest()
        {
            var readerManagerFake = new Mock<IReaderManager>();
            var excelManagerFake = new Mock<IExcelManager>();
            var controller = new ReaderApiController(readerManagerFake.Object, excelManagerFake.Object);
            controller.Request = new HttpRequestMessage();
            var result = controller.Create(null);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void ReaderApiCreateCreated()
        {
            var readerManagerFake = new Mock<IReaderManager>();
            var excelManagerFake = new Mock<IExcelManager>();
            var controller = new ReaderApiController(readerManagerFake.Object, excelManagerFake.Object);
            controller.Request = new HttpRequestMessage();
            var result = controller.Create(this.fixture.Create<ReaderBusinessModel>());
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [TestMethod]
        public void ReaderApiGetReadersOK()
        {
            var readerManagerFake = new Mock<IReaderManager>();
            var excelManagerFake = new Mock<IExcelManager>();
            var gridModel = this.fixture.Create<ReadersGridModel>();
            readerManagerFake.Setup(m => m.GetReadersForGrid(It.IsAny<IEnumerable<KeyValuePair<string,string>>>(), 1, 2, "", "")).Returns(gridModel);
            var controller = new ReaderApiController(readerManagerFake.Object, excelManagerFake.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var result = controller.GetReaders(1, 2, string.Empty, string.Empty);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void ReaderApiControllerGetReaderByIdBadRequest()
        {
            var readerManagerFake = new Mock<IReaderManager>();
            var excelManagerFake = new Mock<IExcelManager>();
            readerManagerFake.Setup(m => m.GetReaderById(It.IsAny<int>())).Returns((ReaderBusinessModel)null);
            var controller = new ReaderApiController(readerManagerFake.Object, excelManagerFake.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var result = controller.GetById(1);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void ReaderApiControllerGetReaderByIdOK()
        {
            var readerManagerFake = new Mock<IReaderManager>();
            var excelManagerFake = new Mock<IExcelManager>();
            var testReader = this.fixture.Create<ReaderBusinessModel>();
            readerManagerFake.Setup(m => m.GetReaderById(It.IsAny<int>())).Returns(testReader);
            var controller = new ReaderApiController(readerManagerFake.Object, excelManagerFake.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var result = controller.GetById(1);



            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
