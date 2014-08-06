namespace SchoolLibrary.Tests.BusinessLogic.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;

    /// <summary>
    /// Represents ScannedPageMapper Tests logic
    /// </summary>
    [TestClass]
    public class ScannedPageMapperTests
    {
        private ScannedPageMapper _scannedPageMapper;
        private ScannedPage _scannedPage;
        private ScannedPageBusinessModel _scannedPageBusinessModel;

        public ScannedPageMapperTests()
        {
            this._scannedPageMapper = new ScannedPageMapper();

            Fixture fixture = new Fixture { RepeatCount = 1 };
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));
            fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));

            Reader reader = fixture.Create<Reader>();
            ReaderMapper readerMapper = new ReaderMapper();
            var readerModel = readerMapper.Map(reader);

            var item = fixture.Create<Item>();
            var itemMapper = new ItemMapper();
            var itemModel = itemMapper.Map(item);

            _scannedPage = fixture.Create<ScannedPage>();
            _scannedPageBusinessModel = fixture.Create<ScannedPageBusinessModel>();
        }

        [TestMethod]
        public void ScannedPageEntityToModelMappingIsNotNullTest()
        {
            var result = _scannedPageMapper.Map(_scannedPage);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void ScannedPageEntityToModelMappingIsNullTest()
        {
            _scannedPage = null;
            var result = _scannedPageMapper.Map(_scannedPage);
            Assert.IsNull(result);
        }


        [TestMethod]
        public void ScannedPageEntityToModelMappingAreEqualTest()
        {
            var result = _scannedPageMapper.Map(_scannedPage);

            Assert.AreEqual(_scannedPage.Id, result.Id, "Id is not correct");
            Assert.AreEqual(_scannedPage.ExecutionDate, result.ExecutionDate, "ExecutionDate is not correct");
            Assert.AreEqual(_scannedPage.OrderDate, result.OrderDate, "OrderDate is not correct");
            Assert.AreEqual(_scannedPage.IsLocked, result.IsLocked, "IsLocked is not correct");
            Assert.AreEqual(_scannedPage.IsReady, result.IsReady, "IsReady is not correct");
            Assert.AreEqual(_scannedPage.Message, result.Message, "Message is not correct");
            Assert.AreEqual(_scannedPage.OrderText, result.OrderText, "OrderText is not correct");
            Assert.AreEqual(_scannedPage.Reader.ReaderId, result.Reader.ReaderId, "ReaderId is not correct");
            Assert.AreEqual(_scannedPage.Item.Id, result.Item.Id, "ItemId is not correct");
        }

        [TestMethod]
        public void ScannedPageModelToEntityMappingIsNotNullTest()
        {
            var result = _scannedPageMapper.Map(_scannedPageBusinessModel);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void ScannedPageModelToEntityMappingIsNullTest()
        {
            _scannedPageBusinessModel = null;
            var result = _scannedPageMapper.Map(_scannedPageBusinessModel);
            Assert.IsNull(result);
        }


        [TestMethod]
        public void ScannedPageModelToEntityMappingAreEqualTest()
        {
            var result = _scannedPageMapper.Map(_scannedPageBusinessModel);

            Assert.AreEqual(_scannedPageBusinessModel.Id, result.Id, "Id is not correct");
            Assert.AreEqual(_scannedPageBusinessModel.ExecutionDate, result.ExecutionDate, "ExecutionDate is not correct");
            Assert.AreEqual(_scannedPageBusinessModel.OrderDate, result.OrderDate, "OrderDate is not correct");
            Assert.AreEqual(_scannedPageBusinessModel.IsLocked, result.IsLocked, "IsLocked is not correct");
            Assert.AreEqual(_scannedPageBusinessModel.IsReady, result.IsReady, "IsReady is not correct");
            Assert.AreEqual(_scannedPageBusinessModel.Message, result.Message, "Message is not correct");
            Assert.AreEqual(_scannedPageBusinessModel.OrderText, result.OrderText, "OrderText is not correct");
            Assert.AreEqual(_scannedPageBusinessModel.Reader.ReaderId, result.Reader.ReaderId, "ReaderId is not correct");
            Assert.AreEqual(_scannedPageBusinessModel.Item.Id, result.Item.Id, "ItemId is not correct");
        }
    }
}
