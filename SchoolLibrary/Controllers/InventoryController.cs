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
    public class InventoryController : Controller
    {
        private IInventoryManager inventoryManager;
        private IBookManager bookManager;
        private IConsignmentManager consignmentManager;

        public InventoryController(IInventoryManager inventoryManager, IBookManager bookManager, IConsignmentManager consignmentManager)
        {
            this.inventoryManager = inventoryManager;
            this.bookManager = bookManager;
            this.consignmentManager = consignmentManager;
        }

        //
        // GET: /Inventory/

        public ActionResult Index(int id)
        {
            var inventory = this.inventoryManager.GetAllInventoryByConsignmentId(id);
            ViewBag.Book = this.bookManager.GetBookById(id);
            
            return View(inventory);
        }

        //
        // GET: /Inventory/Details/{id}

        public ActionResult Details(int id)
        {
            var inventory = this.inventoryManager.GetInventoryById(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }

            return View(inventory);
        }

        //
        //GET: /Inventory/Edit/{id}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var inventory = this.inventoryManager.GetInventoryById(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }

            return View(inventory);
        }

        //
        //POST: /Inventory/Edit/{id}

        [HttpPost]
        public ActionResult Edit(InventoryBusinessModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.inventoryManager.UpdateInventory(viewModel);
                return RedirectToAction("Details", new { id = viewModel.InventoryId });
            }

            return View(viewModel);
        }

        //
        //POST: /Inventory/Add/{id}

        [HttpPost]
        public ActionResult Add(int bookId, int count)
        {
            
            var consignmentNew = new ConsignmentBusinessModel { ArrivalDate = DateTime.Now };
            var consignmentId = this.consignmentManager.CreateConsignment(bookId, consignmentNew);
            for (int i = 0; i < count; i++)
            {
                this.inventoryManager.CreateInventory(bookId, consignmentId.Number, i + 1);
            }
            ViewBag.Consignment = consignmentId.Number;

            return RedirectToAction("Index", new { id = bookId });
        }


        //
        //GET: /Inventory/Delete/{id}

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var inventory = this.inventoryManager.GetInventoryById(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }

            return View(inventory);
        }

        //
        //POST: /Inventory/Delete/{id}

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var inventory = this.inventoryManager.GetInventoryById(id);
            int bookId = inventory.Item.Id;
            this.inventoryManager.RemoveInventoryById(id);
            if (this.Request.IsAjaxRequest())
            {
                return Json(new { delete = "deleted" });
            }
            else
            {
                return RedirectToAction("Index", new { id = bookId });
            }
        }

        public ActionResult IndexWidgets(int id)
        {
            var inventory = this.inventoryManager.GetAllInventoryByConsignmentId(id);
            ViewBag.Book = this.bookManager.GetBookById(id);
            return View(inventory);
        }
        
        public JsonResult GetInventory(int id, string sortdatafield, string sortorder, int pageSize, int pageNum)
        {
            var inventories = this.inventoryManager.GetAllInventoryByConsignmentId(id);
            int totalRows = inventories.Count;

            if (sortorder != null && sortorder != string.Empty)
            {
                if (sortorder == "asc")
                {
                    inventories = inventories.OrderBy(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null)).ToList();
                }

                else
                {
                    inventories = inventories.OrderByDescending(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null)).ToList();
                }

            }

            var result = new
            {
                TotalRows = totalRows,
                Rows = inventories.Skip(pageNum * pageSize).Take(pageSize)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
