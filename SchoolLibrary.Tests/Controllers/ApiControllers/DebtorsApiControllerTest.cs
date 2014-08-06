using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.Controllers.ApiControllers
{
    using System.Collections.Generic;
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
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.Controllers.WebAPIControllers;
    [TestClass]
    public class DebtorsApiControllerTest
    {
        private Fixture fixture;

        public DebtorsApiControllerTest()
        {
            this.fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public void GetDebtorsReaders()
        {
            var readerHistoryManagerFake = new Mock<IReaderHistoryManager>();
            var gridModel = this.fixture.Create<DeptorsReadersModel>();

            var number = It.IsAny<string>();
            readerHistoryManagerFake.Setup(m => m.GetDebtorsReaders(0, 0)).Returns(new List<DeptorsReadersModel>());
            var controller = new DeptorsReadersApiController(readerHistoryManagerFake.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var result = controller.GetDeptorsReaders("", "", 1, 2, "", "");
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void GetDebtorsReadersBadRequest()
        {
            var readerHistoryManagerFake = new Mock<IReaderHistoryManager>();
            //readerHistoryManagerFake.Setup(m => m.GetDebtorsReaders((0,0)).Returns((ReaderBusinessModel)null);
            var controller = new DeptorsReadersApiController(readerHistoryManagerFake.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var result = controller.GetDeptorsReaders("", "", 1, 2, "", "");
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
