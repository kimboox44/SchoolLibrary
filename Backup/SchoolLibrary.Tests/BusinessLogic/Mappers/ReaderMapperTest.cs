namespace SchoolLibrary.Tests.DataAccess.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using System;
    using SchoolLibrary.DataAccess.Mappers;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Represents ReaderMapper Tests logic
    /// </summary>
    [TestClass]
    public class ReaderMapperTest
    {
        private ReaderMapper _readerMapper;
        private ReaderBusinessModel _readerBusinessModel;
        private Reader _reader;

        public ReaderMapperTest() 
        {
            #region Old Initialization
            //_readerBusinessModel = new ReaderBusinessModel
            //{
            //    ReaderId = 1000,
            //    FirstName = "Mykola",
            //    LastName = "Stepanyak",
            //    Address = "Wall St. 123",
            //    Birthday = new DateTime(2012, 1, 1),
            //    Phone = "067-299-29-99",
            //    EMail = "readerBusinessInfoModel@mail.com",
            //    Preferences = null
            //};

            //_reader = new Reader
            //{
            //    ReaderId = 2000,
            //    FirstName = "Mykola",
            //    LastName = "Stepanyak",
            //    Address = "Wall St. 123",
            //    Birthday = new DateTime(2012, 2, 2),
            //    Phone = "067-299-29-99",
            //    EMail = "readerBusiness@mail.com"
            //};
            #endregion

            _readerMapper = new ReaderMapper();

            Fixture fixture = new Fixture { RepeatCount = 1 };
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));

            _reader = fixture.Create<Reader>();
            _readerBusinessModel = fixture.Create<ReaderBusinessModel>();
        }

        /// <summary>
        /// Reader Model Map To Entity IsNotNull Test
        /// </summary>
        [TestMethod]
        public void ReaderModelToEntityMappingIsNotNullTest()
        {
            var result = _readerMapper.Map(this._readerBusinessModel);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Reader Model Map To Entity AreEqual Test
        /// </summary>
        [TestMethod]
        public void ReaderModelToEntityMappingAreEqualTest()
        {
            var result = _readerMapper.Map(this._readerBusinessModel);

            Assert.AreEqual(this._readerBusinessModel.ReaderId, result.ReaderId, "The ReaderId is incorrect.");
            Assert.AreEqual(this._readerBusinessModel.Address, result.Address, "The Address is incorrect.");
            Assert.AreEqual(this._readerBusinessModel.Birthday, result.Birthday, "The Birthday is incorrect.");
            Assert.AreEqual(this._readerBusinessModel.EMail, result.EMail, "The EMail is incorrect.");
            Assert.AreEqual(this._readerBusinessModel.FirstName, result.FirstName, "The FirstName is incorrect.");
            Assert.AreEqual(this._readerBusinessModel.LastName, result.LastName, "The LastName is incorrect.");
            Assert.AreEqual(this._readerBusinessModel.Phone, result.Phone, "The Phone is incorrect.");
        }

        /// <summary>
        /// Reader Model To Entity Map IsNull Test
        /// </summary>
        [TestMethod]
        public void ReaderModelToEntityMappingIsNullTest()
        {
            _readerBusinessModel = null;
            var result = _readerMapper.Map(_readerBusinessModel);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Reader Entity Map To Model IsNotNull Test
        /// </summary>
        [TestMethod]
        public void ReaderEntityToModelMappingIsNotNull()
        {
            var result = _readerMapper.Map(_reader);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Reader Entity Map To Model AreEqual Test
        /// </summary>
        [TestMethod]
        public void ReaderEntityToModelMappingAreEqualTest()
        {
            var result = _readerMapper.Map(_reader);

            Assert.AreEqual(_reader.ReaderId, result.ReaderId, "The ReaderId is incorrect.");
            Assert.AreEqual(_reader.Address, result.Address, "The Address is incorrect.");
            Assert.AreEqual(_reader.Birthday, result.Birthday, "The Birthday is incorrect.");
            Assert.AreEqual(_reader.EMail, result.EMail, "The EMail is incorrect.");
            Assert.AreEqual(_reader.FirstName, result.FirstName, "The FirstName is incorrect.");
            Assert.AreEqual(_reader.LastName, result.LastName, "The LastName is incorrect.");
            Assert.AreEqual(_reader.Phone, result.Phone, "The Phone is incorrect.");
        }

        /// <summary>
        /// Reader Entity Map To Model IsNull Test
        /// </summary>
        [TestMethod]
        public void ReaderEntityToModelMappingIsNullTest()
        {
            _reader = null;
            var result = _readerMapper.Map(_reader);

            Assert.IsNull(result);
        }
    }
}
