using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.BusinessModels.MVCModels;
using SchoolLibrary.DataAccess.Entities;

namespace SchoolLibrary.DataAccess.Mappers
{
    public class DeptorsReadersMapper
    {
        public ReaderHistory Map(DeptorsReadersModel o)
        {
            if (o == null) return null;
            ReaderHistory readerHistory = new ReaderHistory();

            readerHistory.Reader.ReaderId = o.readerId;
            readerHistory.Reader.FirstName = o.FirstName;
            readerHistory.Reader.LastName = o.LastName;
            readerHistory.Reader.Address = o.Address;
            readerHistory.Reader.Phone = o.Phone;
            readerHistory.Inventory.Item.Name = o.ItemName;
            readerHistory.StartDate = o.StartDate;
            readerHistory.FinishDate = o.FinishDate;

            return readerHistory;
        }

        public DeptorsReadersModel Map(ReaderHistory o)
        {
            if (o == null) return null;
            DeptorsReadersModel deptorsReadersBusiness = new DeptorsReadersModel();
            ReaderMapper readerMapper = new ReaderMapper();
            InventoryMapper inventoryMapper = new InventoryMapper();
            ReaderBusinessModel readerBusinessModel = readerMapper.Map(o.Reader);
            InventoryBusinessModel invertoryBusinessModel = inventoryMapper.Map(o.Inventory);

            deptorsReadersBusiness.readerId = readerBusinessModel.ReaderId;
            deptorsReadersBusiness.FirstName = readerBusinessModel.FirstName;
            deptorsReadersBusiness.LastName = readerBusinessModel.LastName;
            deptorsReadersBusiness.Address = readerBusinessModel.Address;
            deptorsReadersBusiness.Phone = readerBusinessModel.Phone;
            if (o.Inventory!= null)
            {
                deptorsReadersBusiness.ItemName = invertoryBusinessModel.Item.Name;
            }
            deptorsReadersBusiness.StartDate = o.StartDate;
            deptorsReadersBusiness.FinishDate = o.FinishDate;

            return deptorsReadersBusiness;

        }

    }
}
