namespace SchoolLibrary.Tests.BusinessLogic.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;

    [TestClass]
    public class TagsMapperTests
    {
        private readonly TagMapper mapper;
        private readonly Tag entityToMap, entityToCompareWith;
        private readonly TagBusinessModel modelToMap, modelToCompareWith;

        public TagsMapperTests()
        {
            this.mapper = new TagMapper();
            this.entityToMap = new Tag { Id = 1, Items = null, Name = "Tag", Readers = null };
            this.entityToCompareWith = new Tag { Id = 1, Items = null, Name = "Tag", Readers = null };
            this.modelToMap = new TagBusinessModel { id = 1, name = "Tag" };
            this.modelToCompareWith = new TagBusinessModel { id = 1, name = "Tag" };
        }

        [TestMethod]
        public void EntityToModelMappingTest()
        {
            var result = mapper.Map(this.entityToMap);
            Assert.AreEqual(this.modelToCompareWith.id, result.id);
            Assert.AreEqual(this.modelToCompareWith.name, result.name);
        }

        [TestMethod]
        public void ModelToEntityMappingTest()
        {
            var result = this.mapper.Map(this.modelToMap);
            Assert.AreEqual(this.entityToCompareWith.Id, result.Id);
            Assert.AreEqual(this.entityToCompareWith.Name, result.Name);
            Assert.IsNull(result.Items);
            Assert.IsNull(result.Readers);
        }
    }
}