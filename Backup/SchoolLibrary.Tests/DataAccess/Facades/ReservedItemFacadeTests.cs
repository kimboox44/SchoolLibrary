
namespace SchoolLibrary.Tests.DataAccess.Facades
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using SchoolLibrary.Tests.Fakes;

    [TestClass]
    public class ReservedItemFacadeTests
    {
        private IReservedItemsFacade resItemsFacade;

        private ILibraryUow uow;

        private ReservedItem testResItem;

        private Item testItem;

        private Fixture fixture;

        public ReservedItemFacadeTests()
        {
            this.fixture = new Fixture();
            this.fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));

            this.testItem = this.fixture.Create<Item>();
            this.testResItem = this.fixture.Create<ReservedItem>();

            this.testResItem.Item = this.testItem;

            this.uow = Initializer.GetLibraryUow();

            this.uow.Items.Add(this.testItem);
            this.uow.ReservedItems.Add(this.testResItem);
            this.resItemsFacade = new ReservedItemsFacade(this.uow);
        }

        [TestMethod]
        public void DeleteReservedItemByIdTest()
        {
            var result = this.uow.ReservedItems.GetById(this.testResItem.Id);
            Assert.IsNotNull(result);

            this.resItemsFacade.DeleteReservedItemById(this.testResItem.Id);
            result = this.uow.ReservedItems.GetById(this.testItem.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllReservedItemsByAllReadersTest()
        {
            var result = this.resItemsFacade.GetAllReservedItemsByAllReaders();
            Assert.IsNotNull(result);

            if (result != null)
            {
                var model = new ReservedItemMapper().Map(this.testResItem);
                Assert.IsTrue(model.Id.Equals(result[0].Id));
            }
        }

        [TestMethod]
        public void GetItemInfoByIdTest()
        {
            var result = this.resItemsFacade.GetItemInfoById(this.testItem.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetReservedItemsByReaderIdTest()
        {
            var result = this.resItemsFacade.GetReservedItemsByReaderId(this.testResItem.Reader.ReaderId);
            Assert.IsNotNull(result);

            if (result != null)
            {
                var model = new ReservedItemMapper().Map(this.testResItem);
                Assert.IsTrue(model.Id.Equals(result[0].Id));
            }

        }

        [TestMethod]
        public void ReserveItemTest()
        {
            var itemForReserve = this.fixture.Create<Item>();

            var readerForReserve = this.fixture.Create<Reader>();

            var result = this.resItemsFacade.ReserveItem(readerForReserve.ReaderId, itemForReserve.Id, 3);

            Assert.IsTrue(result.Contains("Successfully"));
        }

        [TestMethod]
        public void SetReadyStatusForItemTest()
        {
            var beforeResult = testResItem.IsReady;

            var result = this.resItemsFacade.SetReadyStatusForItem(testResItem.Id);
            
            Assert.AreNotEqual(beforeResult, result);
        }
    }
}
