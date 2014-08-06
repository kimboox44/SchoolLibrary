using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.Controllers.ApiControllers
{
    using System.Configuration;
    using System.IO;

    using Moq;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using SchoolLibrary.BusinessLogic.Other;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.Configuration;
    using SchoolLibrary.Controllers.WebAPIControllers;

    [TestClass]
    public class PdfGeneratorTests
    {
        private Fixture fixture;
        public PdfGeneratorTests()
        {
            fixture = new Fixture { RepeatCount = 3 };
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));
        }


        [TestMethod]
        public void PdfInitTest()
        {
           var consignment = this.fixture.Create<ConsignmentBusinessModel>();
            var item = this.fixture.Create<BookBusinessModel>();
            var pdfGenerator = new PdfGenerator();
            pdfGenerator.PdfInit(item, consignment);
            Assert.IsTrue(true);

        }
        [TestMethod]
        public void BarCodeGenerateTest()
        {
           var pdfGenerator = new PdfGenerator();
            pdfGenerator.BarCodeGenerate("0000000000-0000");
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void PdfFinishTest()
        {
            var pdfGenerator = new PdfGenerator();
            var result = pdfGenerator.PdfFinish();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PdfInitFinishTest()
        {
            var consignment = this.fixture.Create<ConsignmentBusinessModel>();
            var item = this.fixture.Create<BookBusinessModel>();
            var pdfGenerator = new PdfGenerator();
            pdfGenerator.PdfInit(item, consignment);
            var result = pdfGenerator.PdfFinish();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void BarCodeGeneratePdfFinishTest()
        {
            var pdfGenerator = new PdfGenerator();
            pdfGenerator.BarCodeGenerate("0000000000-0000");
            var result = pdfGenerator.PdfFinish();
            Assert.IsNotNull(result);
        }
        
    }
}
