namespace SchoolLibrary.Tests.BusinessLogic.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;

    [TestClass]
    public class TagScoreMapperTests
    {
        private readonly TagScoreMapper mapper;
        private readonly TagScore entityWithBook, entityWithDisk, entityWithMagazine;

        private readonly TagScoreBusinessModel modelWithBook, modelWithDisk, modelWithMagazine;

        public TagScoreMapperTests()
        {
            this.mapper = new TagScoreMapper();

            Fixture fixture = new Fixture{RepeatCount = 1};
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));

            Reader reader = fixture.Create<Reader>();
            ReaderMapper readerMapper = new ReaderMapper();
            var readerModel = readerMapper.Map(reader);

            // item is book
            Book book = fixture.Create<Book>();
            this.entityWithBook = new TagScore{Id = 1, Item = book, Reader = reader, Score = 0.5f};
            BookMapper bookMapper = new BookMapper();
            var bookModel = bookMapper.Map(book);
            this.modelWithBook = new TagScoreBusinessModel{Item = bookModel, Reader = readerModel, Score = 0.5f};
            
            // item is disk
            Disk disk = fixture.Create<Disk>();
            this.entityWithDisk = new TagScore { Id = 1, Item = disk, Reader = reader, Score = 0.5f };
            DiskMapper diskMapper = new DiskMapper();
            var diskModel = diskMapper.Map(disk);
            this.modelWithDisk = new TagScoreBusinessModel{Item = diskModel, Reader = readerModel, Score = 0.5f};

            // item is magazine
            Magazine magazine = fixture.Create<Magazine>();
            this.entityWithMagazine = new TagScore { Id = 1, Item = magazine, Reader = reader, Score = 0.5f };
            MagazineMapper magazineMapper = new MagazineMapper();
            var magazineModel = magazineMapper.Map(magazine);
            this.modelWithMagazine =
                new TagScoreBusinessModel { Item = magazineModel, Reader = readerModel, Score = 0.5f };
        }

        [TestMethod]
        public void EntityWithBookToModelMappingTest()
        {
            var result = this.mapper.Map(this.entityWithBook);
            Assert.AreEqual(this.modelWithBook.Score, result.Score);
            Assert.AreEqual(this.modelWithBook.Reader.ReaderId, result.Reader.ReaderId);
            Assert.AreEqual(this.modelWithBook.Item.Id, result.Item.Id);
        }

        [TestMethod]
        public void EntityWithDiskToModelMapperTest()
        {
            var result = this.mapper.Map(this.entityWithDisk);
            Assert.AreEqual(this.modelWithDisk.Score, result.Score);
            Assert.AreEqual(this.modelWithDisk.Reader.ReaderId, result.Reader.ReaderId);
            Assert.AreEqual(this.modelWithDisk.Item.Id, result.Item.Id);
        }

        [TestMethod]
        public void EntityWithMagazineToModelMappingTest()
        {
            var result = this.mapper.Map(this.entityWithMagazine);
            Assert.AreEqual(this.modelWithMagazine.Score, result.Score);
            Assert.AreEqual(this.modelWithMagazine.Reader.ReaderId, result.Reader.ReaderId);
            Assert.AreEqual(this.modelWithMagazine.Item.Id, result.Item.Id);
        }

        [TestMethod]
        public void ModelWithBookToEntityMappingTest()
        {
            var result = mapper.Map(this.modelWithBook);
            Assert.AreEqual(this.entityWithBook.Score, result.Score);
            Assert.AreEqual(this.entityWithBook.Reader.ReaderId, result.Reader.ReaderId);
            Assert.AreEqual(this.entityWithBook.Item.Id, result.Item.Id);
        }

        [TestMethod]
        public void ModelWithDiskToEntityMappingTest()
        {
            var result = mapper.Map(this.modelWithDisk);
            Assert.AreEqual(this.entityWithDisk.Score, result.Score);
            Assert.AreEqual(this.entityWithDisk.Reader.ReaderId, result.Reader.ReaderId);
            Assert.AreEqual(this.entityWithDisk.Item.Id, result.Item.Id);
        }

        [TestMethod]
        public void ModelWithMagazineToEntityMappingTest()
        {
            var result = mapper.Map(this.modelWithMagazine);
            Assert.AreEqual(this.entityWithMagazine.Score, result.Score);
            Assert.AreEqual(this.entityWithMagazine.Reader.ReaderId, result.Reader.ReaderId);
            Assert.AreEqual(this.entityWithMagazine.Item.Id, result.Item.Id);
        }        
    }
}