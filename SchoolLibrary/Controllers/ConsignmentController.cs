using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolLibrary.Controllers
{
    using SchoolLibrary.BusinessLogic.Managers;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.Models;

    [Authorize(Roles = "Librarian")]
    public class ConsignmentController : Controller
    {
        private IConsignmentManager consignmentManager;
        private IInventoryManager inventoryManager;
        private IItemManager itemManager;

        public ConsignmentController(IConsignmentManager consignmentManager, IInventoryManager inventoryManager, IItemManager itemManager)
        {
            this.consignmentManager = consignmentManager;
            this.inventoryManager = inventoryManager;
            this.itemManager = itemManager;
        }

        //
        // GET: /Consignment/

        public ActionResult Index()
        {
            var allConsignments = this.consignmentManager.GetAllConsignments();
            return View(allConsignments);
        }
                
        //
        // GET: /Consignment/Details/{consignmentId}

        public ActionResult Details(int id)
        {
            var consignment = this.consignmentManager.GetConsignmentById(id);
            return View(consignment);
        }

        //
        // Post: /Consignment/WriteOff/{consignmentId}

        [HttpPost]
        public ActionResult WriteOff(int id)
        {
            this.consignmentManager.WriteOffConsignmentById(id);
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult WriteOffInventory(int inventoryId)
        {
            this.inventoryManager.WriteOffInventoryById(inventoryId);
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        //
        // GET: /Consignment/Inventories/{consignmentId}

        public ActionResult Inventories(int id)
        {
            var inventories = this.inventoryManager.GetAllInventoryByConsignmentId(id);
            if (inventories == null)
            {
                return HttpNotFound();
            }
            return View(inventories);
        }

        //
        // GET: /Consignment/AllInventories

        public ActionResult AllInventories()
        {
            var allInventories = this.inventoryManager.GetAllInventory();
            return View(allInventories);
        }

        //
        // GET: /Consignment/Item/{itemId}

        public ActionResult Item(int id)
        {
            var consignments = this.consignmentManager.GetAllConsignmentsByItemId(id);
            var item = this.itemManager.GetItemById(id);
            ViewBag.ItemId = item.Id;
            ViewBag.ItemName = item.Name;
            return View(consignments);
        }

        //
        //POST: /Consignment/AddNewConsignment/{id}
        
        [HttpPost]
        public ActionResult AddNewConsignment(int itemId, int count)
        {
            ConsignmentBusinessModel consignment = 
                this.consignmentManager.CreateConsignment(itemId, new ConsignmentBusinessModel { ArrivalDate = DateTime.Now });

            for (int i = 0; i < count; i++)
            {
                this.inventoryManager.CreateInventory(itemId, consignment.Id, i + 1);
            }
             
            return RedirectToAction("Details", new { id = consignment.Id});
        }

        public ActionResult IndexWidgets()
        {
            return Index();
        }

        public ActionResult ItemWidgets(int id)
        {
           return Item(id);
        }

        public ActionResult InventoriesWidgets(int id)
        {
            return Inventories(id);
        }
    }
}
