namespace SchoolLibrary.Tests.DataAccess.Facades
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using SchoolLibrary.Tests.Fakes;

    [TestClass]
    public class ScannedPageFacadeTest
    {
        private ScannedPageFacade _scannedPageFacade;
        private ScannedPageMapper _scannedPageMapper;
        private Reader _reader;
        private ReaderBusinessModel _readerBusinessModel;
        private Item _item;
        private ItemBusinessModel _itemBusinessModel;
        private ScannedPage _scannedPage;
        private ILibraryUow uow;
        private ScannedPageBusinessModel _scannedPageBusinessModel;
        private Fixture _fixture;

        public ScannedPageFacadeTest() 
        {
            _scannedPageMapper = new ScannedPageMapper();

            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));
            _fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));

            _scannedPage = _fixture.Create<ScannedPage>();
            _scannedPageBusinessModel = _fixture.Create<ScannedPageBusinessModel>();

            _readerBusinessModel = _fixture.Create<ReaderBusinessModel>();
            ReaderMapper readerMapper = new ReaderMapper();
            _reader = readerMapper.Map(_readerBusinessModel);

            _itemBusinessModel = _fixture.Create<ItemBusinessModel>();
            ItemMapper itemMapper = new ItemMapper();
            _item = itemMapper.Map(_itemBusinessModel);

            this.uow = Initializer.GetLibraryUow();
            uow.ScannedPages.Add(this._scannedPage);
            uow.Readers.Add(this._reader);
            uow.Items.Add(this._item);
            this._scannedPageFacade = new ScannedPageFacade(uow);
        }

        /// <summary>
        /// ScannedPageFacade GetScannedPageById AreEqual Test
        /// </summary>
        [TestMethod]
        public void GetScannedPageByIdTest()
        {
            var result = this._scannedPageFacade.GetScannedPageById(_scannedPage.Id);

            Assert.AreEqual(this._scannedPage.Id, result.Id, "The 'Id' is incorrect.");
            Assert.AreEqual(this._scannedPage.ExecutionDate, result.ExecutionDate, "The 'ExecutionDate' is incorrect.");
            Assert.AreEqual(this._scannedPage.IsLocked, result.IsLocked, "The 'IsLocked' is incorrect.");
            Assert.AreEqual(this._scannedPage.IsReady, result.IsReady, "The 'IsReady' is incorrect.");
            Assert.AreEqual(this._scannedPage.Message, result.Message, "The 'Message' is incorrect.");
            Assert.AreEqual(this._scannedPage.OrderDate, result.OrderDate, "The 'OrderDate' is incorrect.");
            Assert.AreEqual(this._scannedPage.OrderText, result.OrderText, "The 'OrderText' is incorrect.");
            Assert.AreEqual(this._scannedPage.Item.Id, result.Item.Id, "The 'Item.Id' is incorrect.");
            Assert.AreEqual(this._scannedPage.Reader.ReaderId, result.Reader.ReaderId, "The 'ReaderId' is incorrect.");
        }

        /// <summary>
        /// ScannedPageFacade GetScannedPageByReaderId IsNull Test
        /// </summary>
        [TestMethod]
        public void GetScannedPageByReaderIdTest()
        {
            var result = this._scannedPageFacade.GetScannedPageById(_scannedPage.Reader.ReaderId);
            Assert.IsNull(result, "Scanned pages not found!");
        }

        /// <summary>
        /// ScannedPageFacade GetAllScannedPages Count Test
        /// </summary>
        [TestMethod]
        public void GetAllScannedPagesTest()
        {
            var result = this._scannedPageFacade.GetAllScannedPages().Count;

            Assert.IsTrue(result > 0, "The result was not greater than zero!");
        }

        /// <summary>
        /// ScannedPageFacade Add ScanPages AreEqual Test
        /// </summary>
        [TestMethod]
        public void CreateScanPagesTest()
        {
            this._scannedPageBusinessModel.Reader.ReaderId = _readerBusinessModel.ReaderId;
            this._scannedPageBusinessModel.Item.Id = _itemBusinessModel.Id;

            int countBeforeInsert = _scannedPageFacade.GetAllScannedPages().Count();

            this._scannedPageFacade.CreateScanPages(_scannedPageBusinessModel);

            int countAfterInsert = _scannedPageFacade.GetAllScannedPages().Count();

            Assert.IsTrue(countAfterInsert > countBeforeInsert, "Operation insert failed!");
        }

        /// <summary>
        /// ScannedPageFacade DeleteScannedPageById IsNull Test
        /// </summary>
        [TestMethod]
        public void DeleteScannedPageByIdTest()
        {
            var testScannedPage = this._scannedPageFacade.GetScannedPageById(_scannedPage.Id);

            this._scannedPageFacade.DeleteScannedPageById(testScannedPage.Id);
            
            var result = this._scannedPageFacade.GetScannedPageById(_scannedPage.Id);
            Assert.IsNull(result, "Data was not deleted!");
        }

        /// <summary>
        /// ScannedPageFacade UpdateScannedPage IsNull Test
        /// </summary>
        [TestMethod]
        public void UpdateScannedPageTest()
        {
            int scannedPageId = this._scannedPage.Id;
            var newScannedPage = _fixture.Create<ScannedPageBusinessModel>();

            newScannedPage.Id = scannedPageId;

            this._scannedPageFacade.UpdateScannedPage(newScannedPage);

            var result = this._scannedPageFacade.GetScannedPageById(_scannedPage.Id);

            Assert.AreEqual(this._scannedPage.Id, result.Id, "The 'Id' is incorrect.");
            Assert.AreEqual(this._scannedPage.ExecutionDate, result.ExecutionDate, "The 'ExecutionDate' is incorrect.");
            Assert.AreEqual(this._scannedPage.IsLocked, result.IsLocked, "The 'IsLocked' is incorrect.");
            Assert.AreEqual(this._scannedPage.IsReady, result.IsReady, "The 'IsReady' is incorrect.");
            Assert.AreEqual(this._scannedPage.Message, result.Message, "The 'Message' is incorrect.");
            Assert.AreEqual(this._scannedPage.OrderDate, result.OrderDate, "The 'OrderDate' is incorrect.");
            Assert.AreEqual(this._scannedPage.OrderText, result.OrderText, "The 'OrderText' is incorrect.");
            Assert.AreEqual(this._scannedPage.Item.Id, result.Item.Id, "The 'Item.Id' is incorrect.");
            Assert.AreEqual(this._scannedPage.Reader.ReaderId, result.Reader.ReaderId, "The 'ReaderId' is incorrect.");
        }

    }
}
