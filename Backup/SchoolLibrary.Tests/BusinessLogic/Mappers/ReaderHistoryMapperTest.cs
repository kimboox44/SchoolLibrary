using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.BusinessLogic.Mappers
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;

    [TestClass]
    public class ReaderHistoryMapperTest
    {
        private readonly ReaderHistoryMapper readerHistoryMapper;

        private readonly ReaderHistory readerHistoryEntityToMap, readerHistoryEntityToCompareWith;
        private readonly Reader readerEntityToMap;
        private readonly Inventory inventoryEntityToMap;

        private readonly ReaderHistoryBusinessModel ReaderHistoryModelToMap, ReaderHistoryModelToCompareWith;
        private readonly ReaderBusinessModel readerModelToMap;
        private readonly InventoryBusinessModel inventoryModelToMap;

        public ReaderHistoryMapperTest()
        {
            this.readerHistoryMapper = new ReaderHistoryMapper();

            this.readerEntityToMap = new Reader { FirstName = "Bob", LastName = "Hopkins" };

            this.readerModelToMap = new ReaderBusinessModel { FirstName = "Bob", LastName = "Hopkins" };

            this.inventoryEntityToMap = new Inventory { Number = "0000000001-001" };

            this.inventoryModelToMap = new InventoryBusinessModel { Number = "0000000001-001" };

            this.readerHistoryEntityToMap = new ReaderHistory
            {
                ReaderHistoryId = 1,
                StartDate = Convert.ToDateTime("2013.12.12"),
                ReturnDate = Convert.ToDateTime("2013.12.20"),
                FinishDate = Convert.ToDateTime("2013.12.28"),
                Reader = this.readerEntityToMap,
                Inventory = this.inventoryEntityToMap
            };

            this.readerHistoryEntityToCompareWith = new ReaderHistory
            {
                ReaderHistoryId = 1,
                StartDate = Convert.ToDateTime("2013.12.12"),
                ReturnDate = Convert.ToDateTime("2013.12.20"),
                FinishDate = Convert.ToDateTime("2013.12.28"),
                Reader = this.readerEntityToMap,
                Inventory = this.inventoryEntityToMap
            };
            this.ReaderHistoryModelToMap = new ReaderHistoryBusinessModel
            {
                Id = 1,
                StartDate = Convert.ToDateTime("2013.12.12"),
                ReturnDate = Convert.ToDateTime("2013.12.20"),
                FinishDate = Convert.ToDateTime("2013.12.28"),
                ReaderBusiness = this.readerModelToMap,
                InventoryBusiness = this.inventoryModelToMap
            };
            this.ReaderHistoryModelToCompareWith = new ReaderHistoryBusinessModel
            {
                Id = 1,
                StartDate = Convert.ToDateTime("2013.12.12"),
                ReturnDate = Convert.ToDateTime("2013.12.20"),
                FinishDate = Convert.ToDateTime("2013.12.28"),
                ReaderBusiness = this.readerModelToMap,
                InventoryBusiness = this.inventoryModelToMap
            };
        }

        [TestMethod]
        public void ReaderHistoryEntityToModelMappingTest()
        {
            var result = readerHistoryMapper.Map(this.readerHistoryEntityToMap);
            Assert.AreEqual(this.ReaderHistoryModelToCompareWith.Id, result.Id);
            Assert.AreEqual(this.ReaderHistoryModelToCompareWith.StartDate, result.StartDate);
            Assert.AreEqual(this.ReaderHistoryModelToCompareWith.ReturnDate, result.ReturnDate);
            Assert.AreEqual(this.ReaderHistoryModelToCompareWith.FinishDate, result.FinishDate);
            Assert.AreEqual(this.ReaderHistoryModelToCompareWith.ReaderBusiness.FirstName, result.ReaderBusiness.FirstName);
            Assert.AreEqual(this.ReaderHistoryModelToCompareWith.ReaderBusiness.LastName, result.ReaderBusiness.LastName);
            Assert.AreEqual(this.ReaderHistoryModelToCompareWith.InventoryBusiness.Number, result.InventoryBusiness.Number);
        }

        [TestMethod]
        public void ReaderHistoryModelToEntityMappingTest()
        {
            var result = this.readerHistoryMapper.Map(this.ReaderHistoryModelToMap);
            Assert.AreEqual(this.readerHistoryEntityToCompareWith.ReaderHistoryId, result.ReaderHistoryId);
            Assert.AreEqual(this.readerHistoryEntityToCompareWith.StartDate, result.StartDate);
            Assert.AreEqual(this.readerHistoryEntityToCompareWith.ReturnDate, result.ReturnDate);
            Assert.AreEqual(this.readerHistoryEntityToCompareWith.FinishDate, result.FinishDate);
            Assert.AreEqual(this.readerHistoryEntityToCompareWith.Reader.FirstName, result.Reader.FirstName);
            Assert.AreEqual(this.readerHistoryEntityToCompareWith.Reader.LastName, result.Reader.LastName);
            Assert.AreEqual(this.readerHistoryEntityToCompareWith.Inventory.Number, result.Inventory.Number);
        }
    }
}
