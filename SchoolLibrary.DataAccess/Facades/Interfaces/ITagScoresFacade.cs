namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface ITagScoresFacade
    {
        void UpdateOrCreateScore(int readerId, int itemId, float newScore);

        float GetScore(int readerId, int itemId);

        List<TagScoreBusinessModel> GetAllReaderScores(int readerId);
    }
}