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
    public class SearchItemFacadeTest
    {
        private SearchItemFacade _searchItemFacade;
        private ItemMapper _itemMapper;
        private Reader _reader;
        private ReaderBusinessModel _readerBusinessModel;
        private Tag _tag;
        private TagBusinessModel _tagBusinessModel;
        private Item _item;
        private ItemBusinessModel _itemBusinessModel;
        private ILibraryUow uow;
        private Fixture _fixture;

        public SearchItemFacadeTest() 
        {
            _itemMapper = new ItemMapper();

            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));
            _fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));

            _readerBusinessModel = _fixture.Create<ReaderBusinessModel>();
            ReaderMapper readerMapper = new ReaderMapper();
            _reader = readerMapper.Map(_readerBusinessModel);

            _item = _fixture.Create<Item>();
            _itemBusinessModel = _itemMapper.Map(_item);

            _tag = _fixture.Create<Tag>();

            this.uow = Initializer.GetLibraryUow();

            uow.Readers.Add(this._reader);
            uow.Items.Add(this._item);
            uow.Tags.Add(_tag);
            this._searchItemFacade = new SearchItemFacade(uow);
        }


        /// <summary>
        /// SearchItemFacade GetAllItems Count Test
        /// </summary>
        [TestMethod]
        public void GetAllItemsTest()
        {
            var result = this._searchItemFacade.GetAllItems().Count;

            Assert.IsTrue(result > 0, "The GetAllItems result was not greater than zero!");
        }

        /// <summary>
        /// SearchItemFacade GetAllTags Count Test
        /// </summary>
        [TestMethod]
        public void GetAllTagsTest()
        {
            var result = this._searchItemFacade.GetAllTags().Count;

            Assert.IsTrue(result > 0, "The GetAllTags result was not greater than zero!");
        }

        /// <summary>
        /// SearchItemFacade GetItemById AreEqual Test
        /// </summary>
        [TestMethod]
        public void GetItemByIdTest()
        {
            var result = this._searchItemFacade.GetItemById(_item.Id);

            Assert.AreEqual(this._item.Id, result.Id, "The 'Id' is incorrect.");
            Assert.AreEqual(this._item.Name, result.Name, "The 'Name' is incorrect.");
            Assert.AreEqual(this._item.Year, result.Year, "The 'Year' is incorrect.");
        }

        /// <summary>
        /// SearchItemFacade GetItemByTag IsNotNull and Count Test
        /// </summary>
        [TestMethod]
        public void GetItemByTagTest()
        {
            var result = this._searchItemFacade.GetAllItems();

            foreach (var item in result)
            {
                var tags = (item as ItemBusinessModel).Tags;

                foreach (var tag in tags)
                {
                    var result2 = this._searchItemFacade.GetItemByTag(tag.name);
                    Assert.IsNotNull(result2);
                    Assert.IsTrue(result2.Count() > 0, "The number of search results for the 'Tag name'  is not greater than zero!");
                }
            }
        }

        /// <summary>
        /// SearchItemFacade GetItemByTagAndName Test
        /// </summary>
        [TestMethod]
        public void GetItemByTagAndNameTest()
        {
            var result = this._searchItemFacade.GetAllItems();

            foreach (var item in result)
            {
                var tags = (item as ItemBusinessModel).Tags;

                foreach (var tag in tags)
                {
                    var result2 = this._searchItemFacade.GetItemByTagAndName(_itemBusinessModel.Name, tag.name);
                    Assert.IsNotNull(result2);
                    Assert.IsTrue(result2.Count() > 0, "The count of search results for the 'Tag name'  is not greater than zero!");
                }
            }
        }

        /// <summary>
        /// SearchItemFacade GetItemByName Test
        /// </summary>
        [TestMethod]
        public void GetItemByNameTest()
        {
            var result = this._searchItemFacade.GetAllItems();

            foreach (var item in result)
            {
                var result2 = this._searchItemFacade.GetItemByName(_itemBusinessModel.Name);

                Assert.IsNotNull(result2);
                Assert.IsTrue(result2.Count() > 0, "The count of search results for the 'Item name'  is not greater than zero!");
            }
        }
    }
}
