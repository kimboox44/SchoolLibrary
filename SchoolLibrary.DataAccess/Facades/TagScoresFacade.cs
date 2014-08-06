namespace SchoolLibrary.DataAccess.Facades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class TagScoresFacade : ITagScoresFacade
    {
        private ILibraryUow uow;

        public TagScoresFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public void UpdateOrCreateScore(int readerId, int itemId, float newScore)
        {
            try
            {
                var ts = this.uow.TagScores.GetAll().First(t => t.Item.Id == itemId && t.Reader.ReaderId == readerId);
                ts.Score = newScore;
                this.uow.TagScores.Update(ts);
            }
            catch (Exception e)
            {
                var ts = new TagScore();
                ts.Item = this.uow.Items.GetById(itemId);
                ts.Reader = this.uow.Readers.GetById(readerId);
                ts.Score = newScore;
                this.uow.TagScores.Add(ts);
            }
            
            this.uow.Commit();
        }

        public float GetScore(int readerId, int itemId)
        {
            return this.uow.TagScores.GetAll().First(t => t.Item.Id == itemId && t.Reader.ReaderId == readerId).Score;
        }

        public List<TagScoreBusinessModel> GetAllReaderScores(int readerId)
        {
            var items = this.uow.Items.GetAll().ToList();
            var readers = this.uow.Readers.GetAll().ToList();

            var scores = this.uow.TagScores.GetAll().ToList().Where(t => t.Reader.ReaderId == readerId).ToList();
            
            TagScoreMapper mapper = new TagScoreMapper();

            return scores.Select(score => mapper.Map(score)).ToList();
        }
    }
}