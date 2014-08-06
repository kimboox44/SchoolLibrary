using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.DataAccess.Entities;
using SchoolLibrary.DataAccess.Mappers;

namespace SchoolLibrary.Tests.BusinessLogic.Mappers
{
    [TestClass]
    public class DiskMapperTest
    {
        private readonly Disk entitytoMap;

        private readonly DiskBusinessModel modelToMap;

        public DiskMapperTest()
        {
            entitytoMap = new Disk
            {
                Consignment = null,
                Id = 1,
                Inventories = null,
                Name = "Disk",
                Producer = "Producer",
                ReservedItems = null,
                ScannedPage = null,
                Tags = null,
                Type = "CD",
                Year = 2013
            };
            modelToMap = new DiskBusinessModel
            {
                Id = 1,
                Inventories = null,
                Name = "Disk",
                Producer = "Producer",
                ReservedItems = null,
                ScannedPages = null,
                Tags = null,
                Type = "CD",
                Year = 2013
            };
        }

        [TestMethod]
        public void EntityToModelDiskMapperTest()
        {
            DiskMapper diskMapper=new DiskMapper();
            DiskBusinessModel diskBusiness = diskMapper.Map(entitytoMap);
            Assert.AreEqual(entitytoMap.Id,diskBusiness.Id);
            Assert.AreEqual(entitytoMap.Name,diskBusiness.Name);
            Assert.AreEqual(entitytoMap.Producer,diskBusiness.Producer);
            Assert.AreEqual(entitytoMap.Year,diskBusiness.Year);
            Assert.AreEqual(entitytoMap.Type,diskBusiness.Type);
            Assert.IsNotNull(diskBusiness);
        }

        [TestMethod]
        public void ModelToEntityDiskMapperTest()
        {
            DiskMapper diskMapper=new DiskMapper();
            Disk disk = diskMapper.Map(modelToMap);
            Assert.AreEqual(modelToMap.Id,disk.Id);
            Assert.AreEqual(modelToMap.Name,disk.Name);
            Assert.AreEqual(modelToMap.Producer,disk.Producer);
            Assert.AreEqual(modelToMap.Year,disk.Year);
            Assert.AreEqual(modelToMap.Type, disk.Type);
            Assert.IsNotNull(disk);
        }

        [TestMethod]
        public void EntityToModelDiskMapperNull()
        {
            Disk disk = null;
            DiskMapper diskMapper=new DiskMapper();
            DiskBusinessModel diskBusiness = diskMapper.Map(disk);
            Assert.IsNull(diskBusiness);
        }

        [TestMethod]
        public void ModelToEntityDiskMapperNull()
        {
            DiskBusinessModel diskBusiness = null;
            DiskMapper diskMapper=new DiskMapper();
            Disk disk = diskMapper.Map(diskBusiness);
            Assert.IsNull(disk);
        }
    }
}
