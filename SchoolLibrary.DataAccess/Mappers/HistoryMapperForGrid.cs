using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.DataAccess.Entities;

namespace SchoolLibrary.DataAccess.Mappers
{
    public class HistoryMapperForGrid : IMapper<ReaderHistory, HistoryBusinessModelForGrid>
    {
        public ReaderHistory Map(HistoryBusinessModelForGrid o)
        {
            if (o == null) return null;
            ReaderHistory readerHistory = new ReaderHistory();

            readerHistory.Reader.FirstName = o.ReaderName;
            readerHistory.Inventory.Item.Name= o.ItemName;
            readerHistory.StartDate = o.StartDate;
            readerHistory.FinishDate = o.FinishDate;

            return readerHistory;
        }

        public HistoryBusinessModelForGrid Map(ReaderHistory o)
        {
            if (o == null) return null;
            HistoryBusinessModelForGrid readerHistoryBusiness = new HistoryBusinessModelForGrid();
            ReaderMapper readerMapper = new ReaderMapper();
            InventoryMapper inventoryMapper = new InventoryMapper();
            ReaderBusinessModel readerBusinessModel = readerMapper.Map(o.Reader);
            InventoryBusinessModel invertoryBusinessModel = inventoryMapper.Map(o.Inventory);

            readerHistoryBusiness.ReaderName = readerBusinessModel.FirstName;
            readerHistoryBusiness.ItemName = invertoryBusinessModel.Item.Name;
            readerHistoryBusiness.StartDate = o.StartDate;
            readerHistoryBusiness.FinishDate = o.FinishDate;

            return readerHistoryBusiness;

        }

    }
}
