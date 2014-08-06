using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.BusinessLogic.Mappers
{
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    [TestClass]
    public class DebtorsReadersMapperTest
    {
        private readonly DeptorsReadersMapper debtorsReadersMapper;
        private readonly ReaderMapper readerMapper;

        private readonly ReaderHistory readerHistoryEntityToMap, readerHistoryEntityToCompareWith;
        private readonly Reader readerEntityToMap;
        private readonly Inventory inventoryEntityToMap;
        private readonly Item itemEntityToMap;

        private readonly DeptorsReadersModel deptorsReadersModelToMap, deptorsReadersModelToCompareWith;

        public DebtorsReadersMapperTest()
        {
            this.debtorsReadersMapper = new DeptorsReadersMapper();
            this.readerMapper = new ReaderMapper();

            this.readerEntityToMap = new Reader { ReaderId = 1, FirstName = "Bob", LastName = "Hopkins", Address = "adr",Phone = "555" };

            this.inventoryEntityToMap = new Inventory();

            this.readerHistoryEntityToMap = new ReaderHistory
            {

                StartDate = Convert.ToDateTime("2013.12.12"),
                FinishDate = Convert.ToDateTime("2013.12.28"),
                Reader = this.readerEntityToMap,
                Inventory = null
            };

            this.deptorsReadersModelToCompareWith = new DeptorsReadersModel
            {
                readerId = this.readerEntityToMap.ReaderId,
                FirstName = this.readerEntityToMap.FirstName,
                LastName = this.readerEntityToMap.LastName,
                Address = this.readerEntityToMap.Address,
                Phone = this.readerEntityToMap.Phone,
                StartDate = Convert.ToDateTime("2013.12.12"),
                FinishDate = Convert.ToDateTime("2013.12.28")
            };
        }
       


        [TestMethod]
        public void DebtorsEntityToModelMappingTest()
        {
            var result = debtorsReadersMapper.Map(this.readerHistoryEntityToMap);
            Assert.AreEqual(this.deptorsReadersModelToCompareWith.readerId,result.readerId);
            Assert.AreEqual(this.deptorsReadersModelToCompareWith.FirstName, result.FirstName);
            Assert.AreEqual(this.deptorsReadersModelToCompareWith.LastName,result.LastName);
            Assert.AreEqual(this.deptorsReadersModelToCompareWith.Address, result.Address);
            Assert.AreEqual(this.deptorsReadersModelToCompareWith.Phone, result.Phone);
            Assert.AreEqual(this.deptorsReadersModelToCompareWith.StartDate, result.StartDate);
            Assert.AreEqual(this.deptorsReadersModelToCompareWith.FinishDate, result.FinishDate);
        }

    }
}
