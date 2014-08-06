using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.DataAccess.Entities;
using SchoolLibrary.DataAccess.Mappers;

namespace SchoolLibrary.Tests.BusinessLogic.Mappers
{
    [TestClass]
    public class MagazineMapperTest
    {
         private readonly Magazine entitytoMap;

        private readonly MagazineBusinessModel modelToMap;

        public MagazineMapperTest()
        {
            entitytoMap = new Magazine
            {
                Id = 1,
                Consignment = null,
                Inventories = null,
                Issue = 1,
                Name="Magazine",
                PageCount = 1,
                Publisher = "Publisher",
                ReservedItems = null,
                ScannedPage = null,
                Tags = null,
                Year = 2013
            };
            modelToMap = new MagazineBusinessModel()
            {
                Id=1,
                Inventories = null,
                Issue = 1,
                Name="Magazine",
                PageCount = 1,
                Publisher = "Publisher",
                ReservedItems = null,
                ScannedPages = null,
                Tags = null,
                Year = 2013
            };
        }

        [TestMethod]
        public void EntityToModelMagazineMapperTest()
        {
            MagazineMapper magazineMapper=new MagazineMapper();
            MagazineBusinessModel magazineBusiness = magazineMapper.Map(entitytoMap);
            Assert.AreEqual(entitytoMap.Id, magazineBusiness.Id);
            Assert.AreEqual(entitytoMap.Issue,magazineBusiness.Issue);
            Assert.AreEqual(entitytoMap.Name,magazineBusiness.Name);
            Assert.AreEqual(entitytoMap.PageCount,magazineBusiness.PageCount);
            Assert.AreEqual(entitytoMap.Publisher,magazineBusiness.Publisher);
            Assert.AreEqual(entitytoMap.Year,magazineBusiness.Year);
            Assert.IsNotNull(magazineBusiness);
        }

        [TestMethod]
        public void ModelToEntityMagazineMapperTest()
        {
            MagazineMapper magazineMapper=new MagazineMapper();
            Magazine magazine = magazineMapper.Map(modelToMap);
            Assert.AreEqual(entitytoMap.Id, magazine.Id);
            Assert.AreEqual(entitytoMap.Issue,magazine.Issue);
            Assert.AreEqual(entitytoMap.Name,magazine.Name);
            Assert.AreEqual(entitytoMap.PageCount,magazine.PageCount);
            Assert.AreEqual(entitytoMap.Publisher,magazine.Publisher);
            Assert.AreEqual(entitytoMap.Year,magazine.Year);
            Assert.IsNotNull(magazine);
        }

        [TestMethod]
        public void EntityToModelMagazineMapperNull()
        {
            Magazine magazine = null;
            MagazineMapper magazineMapper=new MagazineMapper();
            MagazineBusinessModel magazineBusiness = magazineMapper.Map(magazine);
            Assert.IsNull(magazineBusiness);
        }

        [TestMethod]
        public void ModelToEntityMagazineMapperNull()
        {
            MagazineBusinessModel magazineBusiness = null;
            MagazineMapper magazineMapper=new MagazineMapper();
            Magazine magazine = magazineMapper.Map(magazineBusiness);
            Assert.IsNull(magazine);
        }
    }
}
