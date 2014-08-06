using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;

    public interface IInventoryFacade
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
    }
}
