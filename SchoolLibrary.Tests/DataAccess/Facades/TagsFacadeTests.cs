namespace SchoolLibrary.Tests.DataAccess.Facades
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using SchoolLibrary.Tests.Fakes;

    [TestClass]
    public class TagsFacadeTests
    {
        private ITagsFacade tagsFacade;
        private Tag tagToAdd;
        private ILibraryUow uow;

        public TagsFacadeTests()
        {
            var reader = new Reader {ReaderId = 1, FirstName = "firstName", LastName = "lastName", Preferences = new List<Tag>()};

            this.uow = Initializer.GetLibraryUow();
            this.tagToAdd = new Tag {Id = 1, Items = null, Readers = new List<Reader>(), Name = "tag"};
            this.tagToAdd.Readers.Add(reader);
            reader.Preferences.Add(this.tagToAdd);
            this.uow.Tags.Add(this.tagToAdd);
            var tag = new Tag { Id = 2, Items = null, Readers = null, Name = "test" };
            this.uow.Tags.Add(tag);
            tag = new Tag { Id = 3, Items = null, Readers = null, Name = "tag1" };
            this.uow.Tags.Add(tag);
            tag = new Tag { Id = 4, Items = null, Readers = null, Name = "test1" };
            this.uow.Tags.Add(tag);
            this.uow.Readers.Add(reader);
            this.tagsFacade = new TagsFacade(this.uow);
        }

        [TestMethod]
        public void GetTagTest()
        {
            var t = this.tagsFacade.GetTag(1);
            Assert.AreEqual(this.tagToAdd.Id, t.id);
            Assert.AreEqual(this.tagToAdd.Name, t.name);
        }

        [TestMethod]
        public void GetTagsTest()
        {
            string token = "tag";
            var t = this.tagsFacade.GetTags(token);
            Assert.IsNotNull(t);
            Assert.AreEqual(2, t.Count);
            foreach (var tag in t)
            {
                Assert.IsNotNull(tag);
                Assert.IsTrue(tag.name.Contains(token));
            }
        }

        [TestMethod]
        public void CreateTagTest()
        {
            string newTag = "newtag";
            this.tagsFacade.Create(newTag);
            Assert.AreEqual(5, this.uow.Tags.GetAll().Count());
            Assert.AreEqual(1, this.uow.Tags.GetAll().Count(t => t.Name == newTag));
        }

        [TestMethod]
        public void GetReaderPreferences()
        {
            var prefs = this.tagsFacade.GetReaderPreferences(1);
            Assert.IsNotNull(prefs);
            Assert.AreEqual(1, prefs.Count);
            Assert.AreEqual(this.tagToAdd.Name, prefs[0].name);
            Assert.AreEqual(this.tagToAdd.Id, prefs[0].id);
        }
    }
}