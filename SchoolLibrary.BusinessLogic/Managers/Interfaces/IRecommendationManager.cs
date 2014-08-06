namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface IRecommendationManager
    {
        void RecalculateReaderTagScoresAsync(ReaderBusinessModel reader);

        void RecalculateItemTagScoresAsync(int itemId);

        void RecalculateAllTagScores();

        List<ItemBusinessModel> GetRecommendationForReader(int readerId);

        void RemoveRecommendation(int readerId, int itemId);
    }
}