using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolLibrary.Tests.DataAccess.Facades
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.Tests.Fakes;

    /// <summary>
    /// Summary description for ConsignmentFacadeTests
    /// </summary>
    [TestClass]
    public class ConsignmentFacadeTests
    {
        private Fixture fixture;
        private IConsignmentFacade consignmentFacade;
        private Consignment testConsignment;

        public ConsignmentFacadeTests()
        {
            this.fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));
          
            this.testConsignment = this.fixture.Create<Consignment>();           
            var uow = Initializer.GetLibraryUow();
            uow.Consignments.Add(this.testConsignment);
            this.consignmentFacade = new ConsignmentFacade(uow);
        }
        
        [TestMethod]
        public void GetConsignmentByIdTest()
        {
            var result = this.consignmentFacade.GetConsignmentById(this.testConsignment.Id);
            Assert.AreEqual(this.testConsignment.Id, result.Id);
            Assert.AreEqual(this.testConsignment.Number, result.Number);
            Assert.AreEqual(this.testConsignment.ArrivalDate, result.ArrivalDate);
            Assert.AreEqual(this.testConsignment.WriteOffDate, result.WriteOffDate);
        }
        
        [TestMethod]
        public void GetAllConsignmentsTest()
        {
            var result = this.consignmentFacade.GetAllConsignments();
            foreach (var consignment in result)
            {
                Assert.IsNotNull(consignment);
            }
        }

        [TestMethod]
        public void GetAllConsignmentByItemIdTest()
        {
            var result = this.consignmentFacade.GetAllConsignmentsByItemId(1);
            foreach (var consignment in result)
            {
                Assert.IsNotNull(consignment);
            }
        }

        [TestMethod]
        public void UpdateConsignmentTest()
        {
            this.testConsignment = this.fixture.Create<Consignment>();
            this.testConsignment.Id = 1;
            var mapper = new ConsignmentMapper();
            this.consignmentFacade.UpdateConsignment(mapper.Map(this.testConsignment));
            var result = this.consignmentFacade.GetConsignmentById(this.testConsignment.Id);
            Assert.AreEqual(this.testConsignment.Id, result.Id);
            Assert.AreEqual(this.testConsignment.ArrivalDate, result.ArrivalDate);
            Assert.AreEqual(this.testConsignment.WriteOffDate, result.WriteOffDate);
        }

        [TestMethod]
        public void GetConsignmentByNumberTest()
        {
            var result = this.consignmentFacade.GetConsignmentByNumber(this.testConsignment.Number);
            Assert.AreEqual(this.testConsignment.Id, result.Id);
            Assert.AreEqual(this.testConsignment.Number, result.Number);
            Assert.AreEqual(this.testConsignment.ArrivalDate, result.ArrivalDate);
            Assert.AreEqual(this.testConsignment.WriteOffDate, result.WriteOffDate);
        }

        [TestMethod]
        public void WriteOffConsignmentByIdTest()
        {
            var result = this.consignmentFacade.GetConsignmentById(this.testConsignment.Id);
            Assert.IsNotNull(result);
            this.consignmentFacade.WriteOffConsignmentById(this.testConsignment.Id);
        }
    }
}
