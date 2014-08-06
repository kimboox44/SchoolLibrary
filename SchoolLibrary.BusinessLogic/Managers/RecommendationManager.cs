namespace SchoolLibrary.BusinessLogic.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Facades.Interfaces;

    public class RecommendationManager: IRecommendationManager
    {
        private ITagScoresFacade tagScoresFacade;
        private IItemManager itemManager;
        private IReaderManager readerManager;
        private IReservedItemManager reservedItemManager;
        private IReaderHistoryManager readerHistoryManager;

        public RecommendationManager(ITagScoresFacade tagScoresFacade,
                                     IItemManager itemManager,
                                     IReaderManager readerManager,
                                     IReservedItemManager reservedItemManager,
                                     IReaderHistoryManager readerHistoryManager)
        {
            this.tagScoresFacade = tagScoresFacade;
            this.itemManager = itemManager;
            this.readerManager = readerManager;
            this.reservedItemManager = reservedItemManager;
            this.readerHistoryManager = readerHistoryManager;
        }

        public void RecalculateReaderTagScoresAsync(ReaderBusinessModel reader)
        {
            Task t = new Task(() => this.RecalculateReaderTagScores(reader));
            t.Start();
        }

        public void RecalculateItemTagScoresAsync(int itemId)
        {
            Task t = new Task(() => this.RecalculateItemTagScores(itemId));
            t.Start();
        }

        public void RecalculateAllTagScores()
        {
            var allItems = this.itemManager.GetAllItems();
            var allReaders = this.readerManager.GetAllReaders();
            foreach (var reader in allReaders)
            {
                foreach (var item in allItems)
                {
                    this.tagScoresFacade.UpdateOrCreateScore(reader.ReaderId,
                        item.Id,
                        this.CalculateScore(reader.Preferences, item.Tags.ToList()));
                }
            }
        }

        public List<ItemBusinessModel> GetRecommendationForReader(int readerId)
        {
            var reservedByReader = this.reservedItemManager.GetReservedItemsByReaderId(readerId);
            if (reservedByReader == null)
            {
                reservedByReader = new List<ReservedItemBusinessModel>();
            }
            
            var readByReader = this.readerHistoryManager.GetReaderHistoriesByReaderId(readerId);
            if (readByReader == null)
            {
                readByReader = new List<ReaderHistoryBusinessModel>();
            }
            
            var readerScores = this.tagScoresFacade.GetAllReaderScores(readerId).AsQueryable();
            readerScores = readerScores.OrderByDescending(s => s.Score).Where(s => s.Score > 0);

            readerScores = readerScores.Where(s => reservedByReader.All(r => r.Item.Id != s.Item.Id));
            readerScores = readerScores.Where(s => readByReader.All(r => r.InventoryBusiness.Item.Id != s.Item.Id));

            return readerScores.Select(s => s.Item).Take(5).ToList();
        }

        public void RemoveRecommendation(int readerId, int itemId)
        {
            this.tagScoresFacade.UpdateOrCreateScore(readerId, itemId, -1);
        }

        private void RecalculateItemTagScores(int itemId)
        {
            var item = this.itemManager.GetItemById(itemId);
            var allReaders = this.readerManager.GetAllReaders();
            foreach (var reader in allReaders)
            {
                this.tagScoresFacade.UpdateOrCreateScore(reader.ReaderId,
                    item.Id,
                    this.CalculateScore(reader.Preferences, item.Tags.ToList()));
            }
        }

        private void RecalculateReaderTagScores(ReaderBusinessModel reader)
        {
            var allItems = this.itemManager.GetAllItems();
            foreach (var item in allItems)
            {
                if (this.tagScoresFacade.GetScore(reader.ReaderId, item.Id) != -1)
                {
                    this.tagScoresFacade.UpdateOrCreateScore(reader.ReaderId,
                                                             item.Id,
                                                             this.CalculateScore(reader.Preferences, item.Tags.ToList()));
                }
            }
        }

        private float CalculateScore(List<TagBusinessModel> preferences, List<TagBusinessModel> itemTags)
        {
            if (preferences.Count == 0 && itemTags.Count == 0)
            {
                return 0;
            }
            
            var l1 = preferences.Select(t => t.id).ToList();
            var l2 = itemTags.Select(t => t.id).ToList();

            return 1f * l1.Intersect(l2).Count() / l1.Union(l2).Count();
        }
    }
}