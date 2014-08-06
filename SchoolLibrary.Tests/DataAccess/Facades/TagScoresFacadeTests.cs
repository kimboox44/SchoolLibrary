namespace SchoolLibrary.Tests.DataAccess.Facades
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using SchoolLibrary.Tests.Fakes;

    [TestClass]
    public class TagScoresFacadeTests
    {
        private ILibraryUow uow;
        private ITagScoresFacade tagScoresFacade;
        private Reader reader;
        private Item item;
        private TagScore score, newScore;

        public TagScoresFacadeTests()
        {
            this.uow = Initializer.GetLibraryUow();

            Fixture fixture = new Fixture{RepeatCount = 1};
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customizations.Add(new TypeRelay(typeof(Item), typeof(Book)));
            this.item = fixture.Create<Item>();
            this.reader = fixture.Create<Reader>();
            this.score = new TagScore { Id = 1, Item = this.item, Reader = this.reader, Score = 0.5f };
            var i = fixture.Create<Item>();
            this.newScore = new TagScore { Id = 2, Item = i, Reader = this.reader, Score = 0.7f };
            this.uow.TagScores.Add(this.score);
            this.uow.Readers.Add(this.reader);
            this.uow.Items.Add(this.item);this.uow.Items.Add(i);

            this.tagScoresFacade = new TagScoresFacade(this.uow);
        }

        [TestInitialize]
        public void Initialize()
        {
            var ids = this.uow.TagScores.GetAll().Select(s => s.Id).ToList();
            foreach (var id in ids)
            {
                this.uow.TagScores.Delete(id);
            }

            this.uow.TagScores.Add(this.score);
        }

        [TestMethod]
        public void GetScoreTest()
        {
            float s = this.tagScoresFacade.GetScore(this.reader.ReaderId, this.item.Id);
            Assert.AreEqual(this.score.Score, s);
        }

        [TestMethod]
        public void GetAllReaderScoresTest()
        {
            var scores = this.tagScoresFacade.GetAllReaderScores(this.reader.ReaderId);
            Assert.IsNotNull(scores);
            Assert.AreEqual(1, scores.Count);
            Assert.AreEqual(this.score.Score, scores[0].Score);
            Assert.IsTrue(scores.Select(s => s.Reader.ReaderId).All(s => s == this.reader.ReaderId));
        }

        [TestMethod]
        public void UpdateOrCreateScore_Update()
        {
            float s = 0.7f;
            this.tagScoresFacade.UpdateOrCreateScore(this.reader.ReaderId, this.item.Id, s);
            Assert.AreEqual(s, this.uow.TagScores.GetById(this.score.Id).Score);
        }

        [TestMethod]
        public void UpdateOrCreateScore_Create()
        {
            this.tagScoresFacade.UpdateOrCreateScore(this.newScore.Reader.ReaderId, this.newScore.Item.Id, this.newScore.Score);
            Assert.AreEqual(2, this.uow.TagScores.GetAll().Count());
            var created = this.uow.TagScores.GetAll().First(s => s.Item.Id == this.newScore.Item.Id
                                                                 && s.Reader.ReaderId == this.newScore.Reader.ReaderId);
            Assert.IsNotNull(created);
            Assert.AreEqual(this.newScore.Score, created.Score);
        }
    }
}