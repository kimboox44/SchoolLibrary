using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using SchoolLibrary.BusinessLogic.Managers;

namespace SchoolLibrary.Tests.Controllers
{
    [TestClass]
    public class SearchItemControllerTest
    {
        private Fixture fixture;


        private Mock<SearchItemManager> _searchItemManager; 


        //private ItemController itemController;
        private SearchItemController _searchItemController;


        public SearchItemControllerTest()
        {
            this.fixture = new Fixture { RepeatCount = 3 };
            this.fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(ItemBusinessModel), typeof(BookBusinessModel)));

            this._searchItemManager = new Mock<SearchItemManager>(); // good

            this._searchItemController = new SearchItemController(this._searchItemManager.Object);


        }

        //[TestMethod]
        //public void IndexTest()
        //{
        //    var itemBusinessModel = fixture.Create<ItemBusinessModel>();
            

        //    var searchItem = this.fixture.Create<List<ItemBusinessModel>>() as List<ItemBusinessModel>;
        //    this._searchItemManager.Setup(x => x.GetAllItems()).Returns(searchItem);
        //    var result = this._searchItemController.Index(itemBusinessModel.Name, "", 1) as ViewResult;
        //    Assert.IsNotNull(result, "Should have returned a ViewResult");
        //    CollectionAssert.AreEqual(searchItem, (ICollection)result.Model, "Models are not equal");
        //}
    }
}
