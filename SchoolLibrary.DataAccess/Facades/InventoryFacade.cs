using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using System.Data;

    public class InventoryFacade : IInventoryFacade, IDisposable
    {
        const int CONSIGNMENT_NUMBER = 10;
        const int INVENTORY_NUMBER = 4;

        private ILibraryUow uow;

        public InventoryFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public InventoryBusinessModel GetInventoryById(int id)
        {
            var mapper = new InventoryMapper();
            var inventory = this.uow.Inventories.GetById(id);
            return mapper.Map(inventory);
        }

        public List<InventoryBusinessModel> GetAllInventory()
        {
            var mapper = new InventoryMapper();
            var inventory = this.uow.Inventories.GetAll().ToList().Select(mapper.Map).ToList();
            return inventory;
        }

        public List<InventoryBusinessModel> GetAllInventoryByConsignmentId(int consignmentId)
        {
            InventoryMapper inventoryMapper = new InventoryMapper();
            var inventories = this.uow.Inventories.GetAll()
                                .Where(x => x.Consignment.Id == consignmentId).ToList()
                                .Select(inventoryMapper.Map).ToList();
            return inventories;
        }

        public void UpdateInventory(InventoryBusinessModel inventoryModel)
        {
            var mapper = new InventoryMapper();
            Inventory inv = mapper.Map(inventoryModel);
            this.uow.Inventories.Update(inv);
            this.uow.Commit();
        }

        public void CreateInventory(InventoryBusinessModel inventoryModel)
        {
            var mapper = new InventoryMapper();
            this.uow.Inventories.Add(mapper.Map(inventoryModel));
            this.uow.Commit();
        }

        public void CreateInventory(int itemId, int consignmentId, int number)
        {
            int consignmentNumberLength = CONSIGNMENT_NUMBER;
            int inventoryNumberLength = INVENTORY_NUMBER;
            string strConsignmentId = new string('0', consignmentNumberLength - consignmentId.ToString().Length) + consignmentId.ToString();
            string strInventoryId = new string('0', inventoryNumberLength - number.ToString().Length) + number.ToString();

            var consignment = this.uow.Consignments.GetById(consignmentId);
            var item = this.uow.Items.GetById(itemId);
            var inventory = new Inventory
            {
                Consignment = consignment,
                Item = item,
                Number = string.Format("{0}-{1}", strConsignmentId, strInventoryId)
            };
            this.uow.Inventories.Add(inventory);
            this.uow.Commit();
        }

        public void RemoveInventoryById(int id)
        {
            this.uow.Inventories.Delete(id);
            this.uow.Commit();
        }

        public bool WriteOffInventoryById(int inventoryId)
        {
            var inventory = this.uow.Inventories.GetById(inventoryId);
            if (inventory.IsAvailable)
            {
                inventory.WriteOffDate = DateTime.Now;
                inventory.IsAvailable = false;
                this.uow.Inventories.Update(inventory);
                this.uow.Commit();
                return true;
            }
            return false;
        }

        public InventoryBusinessModel GetInventoryByNumber(string number)
        {
            var inventory = this.uow.Inventories.GetAll().SingleOrDefault(c => c.Number == number);
            var mapper = new InventoryMapper();
            return mapper.Map(inventory);
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }
        }
    }
}
