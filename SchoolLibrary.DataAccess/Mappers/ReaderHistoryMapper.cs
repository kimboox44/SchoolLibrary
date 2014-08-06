namespace SchoolLibrary.DataAccess.Mappers
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class ReaderHistoryMapper: IMapper<ReaderHistory,ReaderHistoryBusinessModel>
    {
        public ReaderHistory Map(ReaderHistoryBusinessModel o)
        {
            if (o == null) return null;
            ReaderHistory readerHistory = new ReaderHistory();
            ReaderMapper readerMapper = new ReaderMapper();
            InventoryMapper inventoryMapper = new InventoryMapper();
            Reader reader = readerMapper.Map(o.ReaderBusiness);
            Inventory invertory = inventoryMapper.Map(o.InventoryBusiness);

            readerHistory.ReaderHistoryId = o.Id;
            readerHistory.StartDate = o.StartDate;
            readerHistory.ReturnDate = o.ReturnDate;
            readerHistory.FinishDate = o.FinishDate;
            readerHistory.Reader = reader;
            readerHistory.Inventory = invertory;
            
            return readerHistory;
        }

        public ReaderHistoryBusinessModel Map(ReaderHistory o)
        {
            if (o == null) return null;
            ReaderHistoryBusinessModel readerHistoryBusiness = new ReaderHistoryBusinessModel();
            ReaderMapper readerMapper = new ReaderMapper();
            InventoryMapper inventoryMapper = new InventoryMapper();
            ReaderBusinessModel readerBusinessModel = readerMapper.Map(o.Reader);
            InventoryBusinessModel invertoryBusinessModel = inventoryMapper.Map(o.Inventory);

            readerHistoryBusiness.Id = o.ReaderHistoryId;
            readerHistoryBusiness.StartDate = o.StartDate;
            readerHistoryBusiness.ReturnDate = o.ReturnDate;
            readerHistoryBusiness.FinishDate = o.FinishDate;
            readerHistoryBusiness.ReaderBusiness = readerBusinessModel;
            readerHistoryBusiness.InventoryBusiness = invertoryBusinessModel;

            return readerHistoryBusiness;
            
        }
    }
}
