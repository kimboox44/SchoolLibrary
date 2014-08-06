using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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


    public class ConsignmentApiController : ApiController
    {

        private IInventoryManager inventoryManager;
        private IBookManager bookManager;
        private IConsignmentManager consignmentManager;

        public ConsignmentApiController(IInventoryManager inventoryManager, IBookManager bookManager, IConsignmentManager consignmentManager)
        {
            this.inventoryManager = inventoryManager;
            this.bookManager = bookManager;
            this.consignmentManager = consignmentManager;
        }

        //
        // GET: /ConsignmentApi/

        [HttpGet]
        [ActionName("getallconsignments")]
        public HttpResponseMessage GetAllConsignments(int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
        {
            var query = Request.GetQueryNameValuePairs();
            int filtersCount = int.Parse(query.Where(p => p.Key == "filterscount").First().Value);
            List<Func<ConsignmentBusinessModel, bool>> predicates = new List<Func<ConsignmentBusinessModel, bool>>();

            for (int i = 0; i < filtersCount; i++)
            {
                string filterDataField = query.Where(p => p.Key == "filterdatafield" + i).First().Value;
                string filterCondition = query.Where(p => p.Key == "filtercondition" + i).First().Value;
                string filterValue = query.Where(p => p.Key == "filtervalue" + i).First().Value;

                switch (filterDataField)
                {
                    case "Number":
                        predicates.Add(u => u.Number.ToString().Contains(filterValue));
                        break;

                    case "ArrivalDate":
                        {
                            DateTime date = DateTime.Parse(filterValue);
                            switch (filterCondition)
                            {
                                case "GREATER_THAN_OR_EQUAL":
                                    predicates.Add(u => u.ArrivalDate.Value.Date >= date);
                                    break;
                                case "LESS_THAN_OR_EQUAL":
                                    predicates.Add(u => u.ArrivalDate.Value.Date <= date);
                                    break;
                            }

                            break;
                        }
                }
            }

            int count;
            var records = this.consignmentManager.GetConsignmentForFiltering(pageSize * pageNum, pageSize, out count, predicates);

            if (sortorder != null && sortorder != string.Empty)
            {
                if (sortorder == "asc")
                {
                    records = records.OrderBy(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null)).ToList();
                }
                else
                {
                    records = records.OrderByDescending(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null)).ToList();
                }
            }

            var result = new { TotalRows = count, Rows = records };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [ActionName("getconsignmentsbyitemid")]
        public HttpResponseMessage GetConsignmentsByItemId(int id, int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
        {            var query = Request.GetQueryNameValuePairs();
            int filtersCount = int.Parse(query.Where(p => p.Key == "filterscount").First().Value);
            List<Func<ConsignmentBusinessModel, bool>> predicates = new List<Func<ConsignmentBusinessModel, bool>>();

            for (int i = 0; i < filtersCount; i++)
            {
                string filterDataField = query.Where(p => p.Key == "filterdatafield" + i).First().Value;
                string filterCondition = query.Where(p => p.Key == "filtercondition" + i).First().Value;
                string filterValue = query.Where(p => p.Key == "filtervalue" + i).First().Value;

                switch (filterDataField)
                {
                    case "Number":
                        predicates.Add(u => u.Number.ToString().Contains(filterValue));
                        break;

                    case "ArrivalDate":
                        {
                            DateTime date = DateTime.Parse(filterValue);
                            switch (filterCondition)
                            {
                                case "GREATER_THAN_OR_EQUAL":
                                    predicates.Add(u => u.ArrivalDate >= date);
                                    break;
                                case "LESS_THAN_OR_EQUAL":
                                    predicates.Add(u => u.ArrivalDate <= date);
                                    break;
                            }

                            break;
                        }
                }
            }
            
            int count;
            var records = this.consignmentManager.GetConsignmentForFiltering(id, pageSize * pageNum, pageSize, out count, predicates);
            
            if (sortorder != null && sortorder != string.Empty)
            {
                if (sortorder == "asc")
                {
                    records = records.OrderBy(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null)).ToList();
                }
                else
                {
                    records = records.OrderByDescending(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null)).ToList();
                }
            }

            var result = new { TotalRows = count, Rows = records };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [ActionName("getinventoriesbyconsignmentid")]
        public HttpResponseMessage GetInventory(int id, int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
        {
            var allInventories = this.inventoryManager.GetAllInventoryByConsignmentId(id);
            
            var inventories = from inventory in allInventories
                              select new
                              {
                                  inventory.InventoryId,
                                  inventory.Number,
                                  inventory.IsAvailable,
                                  inventory.WriteOffDate
                              };

            var query = Request.GetQueryNameValuePairs();
            var filtersCount = int.Parse(query.Where(r => r.Key == "filterscount").Select(r => r.Value).First());
            for (var i = 0; i < filtersCount; i++)
            {
                var filterValue = query.Where(r => r.Key == "filtervalue" + i).Select(r => r.Value).First();
                var filterDataField = query.Where(r => r.Key == "filterdatafield" + i).Select(r => r.Value).First();
                inventories =
                    inventories.Where(
                        r => r.GetType().GetProperty(filterDataField).GetValue(r).ToString().ToLower().Contains(filterValue.ToLower()))
                           .Select(r => r);
            }

            if (sortorder != null && sortorder != string.Empty)
            {
                if (sortorder == "asc")
                {
                    inventories = inventories.OrderBy(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
                else
                {
                    inventories = inventories.OrderByDescending(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
            }

            inventories = inventories.Skip(pageNum * pageSize).Take(pageSize);
            int totalRows = allInventories.Count;
            if (filtersCount > 0)
            {
                totalRows = inventories.Count();
            }

            var result = new
            {
                TotalRows = totalRows,
                Rows = inventories
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [ActionName("getallinventories")]
        public HttpResponseMessage GetInventory(int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
        {
            var allInventories = this.inventoryManager.GetAllInventory();

            var inventories = from inventory in allInventories
                              select new
                              {
                                  inventory.InventoryId,
                                  inventory.Number,
                                  inventory.IsAvailable,
                                  inventory.WriteOffDate
                              };

            var query = Request.GetQueryNameValuePairs();
            var filtersCount = int.Parse(query.Where(r => r.Key == "filterscount").Select(r => r.Value).First());
            for (var i = 0; i < filtersCount; i++)
            {
                var filterValue = query.Where(r => r.Key == "filtervalue" + i).Select(r => r.Value).First();
                var filterDataField = query.Where(r => r.Key == "filterdatafield" + i).Select(r => r.Value).First();
                inventories =
                    inventories.Where(
                        r => r.GetType().GetProperty(filterDataField).GetValue(r).ToString().ToLower().Contains(filterValue.ToLower()))
                           .Select(r => r);
            }

            if (sortorder != null && sortorder != string.Empty)
            {
                if (sortorder == "asc")
                {
                    inventories = inventories.OrderBy(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
                else
                {
                    inventories = inventories.OrderByDescending(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
            }

            inventories = inventories.Skip(pageNum * pageSize).Take(pageSize);
            int totalRows = allInventories.Count;
            if (filtersCount > 0)
            {
                totalRows = inventories.Count();
            }

            var result = new
            {
                TotalRows = totalRows,
                Rows = inventories
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}

