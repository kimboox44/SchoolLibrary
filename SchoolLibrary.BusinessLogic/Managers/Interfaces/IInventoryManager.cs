using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using System.IO;

    using SchoolLibrary.BusinessModels.Models;

    public interface IInventoryManager
    {
        InventoryBusinessModel GetInventoryById(int id);

        List<InventoryBusinessModel> GetAllInventory();

        List<InventoryBusinessModel> GetAllInventoryByConsignmentId(int consignmentId);

        void UpdateInventory(InventoryBusinessModel inventory);

        void CreateInventory(InventoryBusinessModel inventory);

        void CreateInventory(int itemId, int consignmentId, int number);

        void RemoveInventoryById(int id);

        bool WriteOffInventoryById(int inventoryId);

        InventoryBusinessModel GetInventoryByNumber(string number);

        MemoryStream GetPdfByInventoryNumber(string number);
    }
}
