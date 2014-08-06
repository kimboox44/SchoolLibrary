using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SchoolLibrary.Controllers.WebAPIControllers
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Http;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;


    public class InventoryApiController : ApiController
    {
        private IInventoryManager inventoryManager;
        private IBookManager bookManager;
        private IConsignmentManager consignmentManager;

        public InventoryApiController(IInventoryManager inventoryManager, IBookManager bookManager, IConsignmentManager consignmentManager)
        {
            this.inventoryManager = inventoryManager;
            this.bookManager = bookManager;
            this.consignmentManager = consignmentManager;
        }

        [HttpGet]
        [ActionName("getdetails")]
        public HttpResponseMessage Details(int id)
        {
            var inventory = this.inventoryManager.GetInventoryById(id);
            return Request.CreateResponse(HttpStatusCode.OK, inventory);
            
        }

        [HttpGet]
        [ActionName("getbyid")]
        public HttpResponseMessage GetInventory(int id, int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
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

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    
    }
}
