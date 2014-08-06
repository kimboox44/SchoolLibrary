using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers
{
    using System.IO;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessLogic.Other;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class InventoryManager : IInventoryManager, IDisposable
    {
        private IInventoryFacade inventoryFacade;

        public InventoryManager(IInventoryFacade inventoryFacade)
        {
            this.inventoryFacade = inventoryFacade;
        }

        public InventoryBusinessModel GetInventoryById(int id)
        {
            return this.inventoryFacade.GetInventoryById(id);
        }

        public List<InventoryBusinessModel> GetAllInventory()
        {
            return this.inventoryFacade.GetAllInventory();
        }

        public List<InventoryBusinessModel> GetAllInventoryByConsignmentId(int consignmentId)
        {
            return this.inventoryFacade.GetAllInventoryByConsignmentId(consignmentId);
        }

        public void UpdateInventory(InventoryBusinessModel inventory)
        {
            this.inventoryFacade.UpdateInventory(inventory);
        }

        public void CreateInventory(InventoryBusinessModel inventory)
        {
            this.inventoryFacade.CreateInventory(inventory);
        }

        public void CreateInventory(int itemId, int consignmentId, int number)
        {
            this.inventoryFacade.CreateInventory(itemId, consignmentId, number);
        }

        public void RemoveInventoryById(int id)
        {
            this.inventoryFacade.RemoveInventoryById(id);
        }

        public bool WriteOffInventoryById(int inventoryId)
        {
            return this.inventoryFacade.WriteOffInventoryById(inventoryId);
        }

        public InventoryBusinessModel GetInventoryByNumber(string number)
        {
            return this.inventoryFacade.GetInventoryByNumber(number);
        }

        public MemoryStream GetPdfByInventoryNumber(string number)
        {
            var inventory = this.inventoryFacade.GetInventoryByNumber(number);
            if (inventory == null)
            {
                return null;
            }

            var pdfGenerator = new PdfGenerator();
            pdfGenerator.PdfInit(inventory.Item, inventory.Consignment);
            pdfGenerator.BarCodeGenerate(inventory.Number);


            return pdfGenerator.PdfFinish();
        }

        public void Dispose()
        {
            if (this.inventoryFacade as IDisposable != null)
            {
                (this.inventoryFacade as IDisposable).Dispose();
            }
        }
    }
}
