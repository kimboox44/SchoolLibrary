using System;
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

    [TestClass]
    public class ItemControllerTests
    {
        private Fixture fixture;

        private Mock<IItemManager> itemManager;
        private Mock<IBookManager> bookInfoManager;
        private Mock<IDiskManager> diskManager;
        private Mock<IMagazineManager> magazineManager;
        private Mock<IAuthorManager> authorManager;
        private Mock<IBookAuthorManager> bookauthorManager;
        private Mock<IRecommendationManager> recommendationManager;

        private ItemController itemController;


        public ItemControllerTests()
        {
            this.fixture = new Fixture { RepeatCount = 3 };
            this.fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));
            this.fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookWithAuthorsShort)));
            this.fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(DiskBusinessModel)));
            this.fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(MagazineBusinessModel)));
            this.itemManager = new Mock<IItemManager>();
            this.bookInfoManager = new Mock<IBookManager>();
            this.diskManager = new Mock<IDiskManager>();
            this.magazineManager = new Mock<IMagazineManager>();
            this.authorManager = new Mock<IAuthorManager>();
            this.bookauthorManager = new Mock<IBookAuthorManager>();
            this.recommendationManager = new Mock<IRecommendationManager>();

            this.itemController = new ItemController(
                this.itemManager.Object,
                this.bookInfoManager.Object,
                this.magazineManager.Object,
                this.diskManager.Object,
                this.authorManager.Object,
                this.bookauthorManager.Object,
                this.recommendationManager.Object);

        }
        [TestMethod]
        public void IndexTest()
        {
            var items = this.fixture.Create<List<ItemBusinessModel>>() as List<ItemBusinessModel>;
            this.itemManager.Setup(x => x.GetAllItems()).Returns(items);
            var result = this.itemController.Index() as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            CollectionAssert.AreEqual(items, (ICollection)result.Model, "Models are not equal");
        }

        [TestMethod]
        public void DetailsTest_IsNotNull()
        {
            
            var item = this.fixture.Create<BookBusinessModel>();
            var id = item.Id;
            this.itemManager.Setup(x => x.GetItemById(id)).Returns((ItemBusinessModel)item);
            var result = this.itemController.Details(id) as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(item, result.Model, "Models are not equal");
        }
        [TestMethod]
        public void DetailsTest_IsNull()
        {

            var id = It.IsAny<int>();
            this.itemManager.Setup(x => x.GetItemById(id)).Returns((ItemBusinessModel)null);
            var result = this.itemController.Details(id) as HttpNotFoundResult;
            Assert.IsNotNull(result, "Should have returned a HttpNotFoundResult");
            
        }

        [TestMethod]
        public void EditGetTest_IsBook()
        {
            var item = this.fixture.Create<BookBusinessModel>();
            var id = item.Id;
            this.itemManager.Setup(x => x.GetItemById(id)).Returns((ItemBusinessModel)item);
            var result = this.itemController.Edit((int)id) as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
        }

        [TestMethod]
        public void EditGetTest_IsDisk()
        {
            var item = this.fixture.Create<DiskBusinessModel>();
            var id = item.Id;
            this.itemManager.Setup(x => x.GetItemById(id)).Returns((ItemBusinessModel)item);
            var result = this.itemController.Edit((int)id) as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(item, result.Model, "Models are not equal");
        }
        [TestMethod]
        public void EditGetTest_IsMagazine()
        {
            var item = this.fixture.Create<MagazineBusinessModel>();
            var id = item.Id;
            this.itemManager.Setup(x => x.GetItemById(id)).Returns((ItemBusinessModel)item);
            var result = this.itemController.Edit((int)id) as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(item, result.Model, "Models are not equal");
        }
        [TestMethod]
        public void EditGetTest_IsNull()
        {

            var id = It.IsAny<int>();
            this.itemManager.Setup(x => x.GetItemById(id)).Returns((ItemBusinessModel)null);
            var result = this.itemController.Edit((int)id) as HttpNotFoundResult;
            Assert.IsNotNull(result, "Should have returned a HttpNotFoundResult");

        }
        [TestMethod]
        public void EditPostTest_IsBook()
        {
            var item = this.fixture.Create<BookWithAuthorsShort>();
            this.bookInfoManager.Setup(x=>x.UpdateBookWithAuthors((BookWithAuthorsShort)item)).Verifiable();
            this.recommendationManager.Setup(x=>x.RecalculateItemTagScoresAsync(item.Id)).Verifiable();
            var result = this.itemController.Edit((BookWithAuthorsShort)item) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have returned a RedirectToRouteResult");
        }
        [TestMethod]
        public void EditPostTest_IsBook_Invalid()
        {
            const string expectedViewName = "_EditBook";
            var item = this.fixture.Create<BookWithAuthorsShort>();
            this.itemController.ModelState.AddModelError("Error", "ModelState is Invalid");
            var result = this.itemController.Edit((BookWithAuthorsShort)item) as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should be {0}", expectedViewName);
        }
        [TestMethod]
        public void EditPostTest_IsDisk()
        {
            var item = this.fixture.Create<DiskBusinessModel>();
            this.diskManager.Setup(x => x.UpdateDisk((DiskBusinessModel)item)).Verifiable();
            this.recommendationManager.Setup(x => x.RecalculateItemTagScoresAsync(item.Id)).Verifiable();
            var result = this.itemController.Edit((DiskBusinessModel)item) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have returned a RedirectToRouteResult");
        }
        [TestMethod]
        public void EditPostTest_IsDisk_Invalid()
        {
            const string expectedViewName = "_EditDisk";
            var item = this.fixture.Create<DiskBusinessModel>();
            this.itemController.ModelState.AddModelError("Error", "ModelState is Invalid");
            var result = this.itemController.Edit((DiskBusinessModel)item) as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should be {0}", expectedViewName);
        }
        [TestMethod]
        public void EditPostTest_IsMagazine()
        {
            var item = this.fixture.Create<MagazineBusinessModel>();
            this.magazineManager.Setup(x => x.UpdateMagazine((MagazineBusinessModel)item)).Verifiable();
            this.recommendationManager.Setup(x => x.RecalculateItemTagScoresAsync(item.Id)).Verifiable();
            var result = this.itemController.Edit((MagazineBusinessModel)item) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have returned a RedirectToRouteResult");
        }
        [TestMethod]
        public void EditPostTest_IsMagazine_Invalid()
        {
            const string expectedViewName = "_EditMagazine";
            var item = this.fixture.Create<MagazineBusinessModel>();
            this.itemController.ModelState.AddModelError("Error", "ModelState is Invalid");
            var result = this.itemController.Edit((MagazineBusinessModel)item) as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should be {0}", expectedViewName);
        }
        [TestMethod]
        public void LoadPartialTest_Book()
        {
            string value ="book";
            string expectedViewName = "_Add"+value;
            var result = this.itemController.LoadPartial(value) as PartialViewResult;
            Assert.IsNotNull(result, "Should have returned a PartialViewResult");
            Assert.AreEqual(expectedViewName.ToLower(), result.ViewName.ToLower(), "View name should be {0}", expectedViewName);
        }
        [TestMethod]
        public void LoadPartialTest_Disk()
        {
            string value = "disk";
            string expectedViewName = "_Add" + value;
            var result = this.itemController.LoadPartial(value) as PartialViewResult;
            Assert.IsNotNull(result, "Should have returned a PartialViewResult");
            Assert.AreEqual(expectedViewName.ToLower(), result.ViewName.ToLower(), "View name should be {0}", expectedViewName);
        }
        [TestMethod]
        public void LoadPartialTest_Magazine()
        {
            string value = "magazine";
            string expectedViewName = "_Add" + value;
            var result = this.itemController.LoadPartial(value) as PartialViewResult;
            Assert.IsNotNull(result, "Should have returned a PartialViewResult");
            Assert.AreEqual(expectedViewName.ToLower(), result.ViewName.ToLower(), "View name should be {0}", expectedViewName);
        }
        [TestMethod]
        public void LoadPartialTest_Null()
        {
            string value = "";
            string expectedViewName = "_Add" + value;
            var result = this.itemController.LoadPartial(value) as PartialViewResult;
            Assert.IsNull(result, "Should have returned a null");
            
        }
        [TestMethod]
        public void AddGetTest()
        {
            var result = this.itemController.Add() as ViewResult;
            Assert.IsNotNull(result, "Should have returned a ViewResult");

        }
        [TestMethod]
        public void AddPostTest_IsBook()
        {
            var item = this.fixture.Create<BookWithAuthorsShort>();
            this.bookInfoManager.Setup(x => x.CreateBookWithAuthors((BookWithAuthorsShort)item)).Verifiable();
            this.recommendationManager.Setup(x => x.RecalculateItemTagScoresAsync(item.Id)).Verifiable();
            var result = this.itemController.Add((BookWithAuthorsShort)item) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have returned a RedirectToRouteResult");
        }
        [TestMethod]
        public void AddPostTest_IsBook_Invalid()
        {
            const string expectedViewName = "_AddBook";
            var item = this.fixture.Create<BookWithAuthorsShort>();
            this.itemController.ModelState.AddModelError("Error", "ModelState is Invalid");
            var result = this.itemController.Add((BookWithAuthorsShort)item) as PartialViewResult;
            Assert.IsNotNull(result, "Should have returned a PartialViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should be {0}", expectedViewName);
        }
        [TestMethod]
        public void AddPostTest_IsDisk()
        {
            var item = this.fixture.Create<DiskBusinessModel>();
            this.diskManager.Setup(x => x.CreateDisk((DiskBusinessModel)item)).Verifiable();
            this.recommendationManager.Setup(x => x.RecalculateItemTagScoresAsync(item.Id)).Verifiable();
            var result = this.itemController.Add((DiskBusinessModel)item) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have returned a RedirectToRouteResult");
        }
        [TestMethod]
        public void AddPostTest_IsDisk_Invalid()
        {
            const string expectedViewName = "_AddDisk";
            var item = this.fixture.Create<DiskBusinessModel>();
            this.itemController.ModelState.AddModelError("Error", "ModelState is Invalid");
            var result = this.itemController.Add((DiskBusinessModel)item) as PartialViewResult;
            Assert.IsNotNull(result, "Should have returned a PartialViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should be {0}", expectedViewName);
        }
        [TestMethod]
        public void AddPostTest_IsMagazine()
        {
            var item = this.fixture.Create<MagazineBusinessModel>();
            this.magazineManager.Setup(x => x.CreateMagazine((MagazineBusinessModel)item)).Verifiable();
            this.recommendationManager.Setup(x => x.RecalculateItemTagScoresAsync(item.Id)).Verifiable();
            var result = this.itemController.Add((MagazineBusinessModel)item) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have returned a RedirectToRouteResult");
        }
        [TestMethod]
        public void AddPostTest_IsMagazine_Invalid()
        {
            const string expectedViewName = "_AddMagazine";
            var item = this.fixture.Create<MagazineBusinessModel>();
            this.itemController.ModelState.AddModelError("Error", "ModelState is Invalid");
            var result = this.itemController.Add((MagazineBusinessModel)item) as PartialViewResult;
            Assert.IsNotNull(result, "Should have returned a PartialViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should be {0}", expectedViewName);
        }
    }
}
