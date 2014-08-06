using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolLibrary.BusinessLogic.Managers.Interfaces;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.BusinessModels.MVCModels;

namespace SchoolLibrary.Controllers.WebAPIControllers
{
    public class DeptorsReadersApiController : ApiController
    {
        private IReaderHistoryManager readerHistoryManager;

        public DeptorsReadersApiController(IReaderHistoryManager readerHistoryManager)
        {
            this.readerHistoryManager = readerHistoryManager;
        }

        [HttpGet]
        public HttpResponseMessage GetDeptorsReaders(string minday, string maxday, int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
        {
            int min = Convert.ToInt32(minday);
            int max = Convert.ToInt32(maxday);

            var deptorsReaders = this.readerHistoryManager.GetDebtorsReaders(min, max);

            var query = Request.GetQueryNameValuePairs();
            int filtersCount = int.Parse(query.Where(p => p.Key == "filterscount").First().Value);
            List<Func<DeptorsReadersModel, bool>> predicates = new List<Func<DeptorsReadersModel, bool>>();

            for (int i = 0; i < filtersCount; i++)
            {
                string filterDataField = query.Where(p => p.Key == "filterdatafield" + i).First().Value;
                string filterCondition = query.Where(p => p.Key == "filtercondition" + i).First().Value;
                string filterValue = query.Where(p => p.Key == "filtervalue" + i).First().Value;

                switch (filterDataField)
                {
                    case "FirstName":
                        predicates.Add(u => u.FirstName.ToLower().Contains(filterValue.ToLower()));
                        break;
                    case "LastName":
                        predicates.Add(u => u.LastName.ToLower().Contains(filterValue.ToLower()));
                        break;
                    case "Address":
                        predicates.Add(u => u.Address.ToLower().Contains(filterValue.ToLower()));
                        break;
                    case "Phone":
                        predicates.Add(u => u.Phone.ToLower().Contains(filterValue.ToLower()));
                        break;
                    case "ItemName":
                        predicates.Add(u => u.ItemName.ToLower().Contains(filterValue.ToLower()));
                        break;

                    case "StartDate":
                        {
                            DateTime date = DateTime.Parse(filterValue);
                            switch (filterCondition)
                            {
                                case "GREATER_THAN_OR_EQUAL":
                                    predicates.Add(u => u.StartDate >= date);
                                    break;
                                case "LESS_THAN_OR_EQUAL":
                                    predicates.Add(u => u.StartDate <= date);
                                    break;
                            }

                            break;
                        }
                    case "FinishDate":
                        {
                            DateTime date = DateTime.Parse(filterValue);
                            switch (filterCondition)
                            {
                                case "GREATER_THAN_OR_EQUAL":
                                    predicates.Add(u => u.FinishDate >= date);
                                    break;
                                case "LESS_THAN_OR_EQUAL":
                                    predicates.Add(u => u.FinishDate <= date);
                                    break;
                            }

                            break;
                        }
                }
            }


            int count;
            var records = this.readerHistoryManager.GetDebtorsReadersByDate(min, max, pageSize * pageNum, pageSize, out count, predicates);

            var result = new { TotalRows = count, Rows = records };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}