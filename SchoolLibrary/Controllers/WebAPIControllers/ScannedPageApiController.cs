namespace SchoolLibrary.Controllers.WebAPIControllers
{
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using WebMatrix.WebData;
    using System.Web.Security;

    public class ScannedPageApiController : ApiController
    {
        private IScannedPageManager scannedPageManager;
        private IBookManager bookManager;
        private IReaderManager readerManager;

        public ScannedPageApiController(IScannedPageManager scannedPageManager, IBookManager bookManager, IReaderManager readerManager)
        {
            this.scannedPageManager = scannedPageManager;
            this.bookManager = bookManager;
            this.readerManager = readerManager;
        }

        [HttpGet]
        public HttpResponseMessage GetScanedPages(int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
        {
            var query = Request.GetQueryNameValuePairs();
            int filtersCount = int.Parse(query.Where(p => p.Key == "filterscount").First().Value);
            List<Func<ScannedPageBusinessModel, bool>> predicates = new List<Func<ScannedPageBusinessModel, bool>>();

            for (int i = 0; i < filtersCount; i++)
            {
                string filterDataField = query.Where(p => p.Key == "filterdatafield" + i).First().Value;
                string filterCondition = query.Where(p => p.Key == "filtercondition" + i).First().Value;
                string filterValue = query.Where(p => p.Key == "filtervalue" + i).First().Value;
                switch (filterDataField)
                {
                    case "OrderText":
                        predicates.Add(u => u.OrderText.ToLower().Contains(filterValue.ToLower()));
                        break;

                    case "OrderDate":
                        {
                            DateTime date = DateTime.Parse(filterValue);
                            switch (filterCondition)
                            {
                                case "GREATER_THAN_OR_EQUAL":
                                    predicates.Add(u => u.OrderDate >= date);
                                    break;
                                case "LESS_THAN_OR_EQUAL":
                                    predicates.Add(u => u.OrderDate <= date);
                                    break;
                            }
                            break;
                        }

                    case "ExecutionDate":
                        {
                            DateTime date = DateTime.Parse(filterValue);
                            switch (filterCondition)
                            {
                                case "GREATER_THAN_OR_EQUAL":
                                    predicates.Add(u => u.ExecutionDate >= date);
                                    break;
                                case "LESS_THAN_OR_EQUAL":
                                    predicates.Add(u => u.ExecutionDate <= date);
                                    break;
                            }
                            break;
                        }
                }
            }


            int count;
            var scannedPagesToSort = new List<ScannedPageBusinessModel>();

            if ((Roles.IsUserInRole(WebSecurity.CurrentUserName, "Librarian")) || (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Admin")))
            {
                scannedPagesToSort = this.scannedPageManager.GetAllScannedPagesForSort(pageSize * pageNum, pageSize, out count, predicates, sortdatafield, sortorder);
                var result = new { TotalRows = count, Rows = scannedPagesToSort };
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                var reader = this.readerManager.GetReaderByUserId(WebSecurity.CurrentUserId);

                if (reader != null)
                {
                    scannedPagesToSort = this.scannedPageManager.GetReaderScannedPagesForSort(pageSize * pageNum, pageSize, reader.ReaderId, out count, predicates, sortdatafield, sortorder);
                    var result = new { TotalRows = count, Rows = scannedPagesToSort };
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpPut]
        [ActionName("edit")]
        public HttpResponseMessage Edit([FromBody] ScannedPageBusinessModel model)
        {
            var scannedPage = this.scannedPageManager.GetScannedPageById(model.Id);
            scannedPage.IsLocked = model.IsLocked;
            scannedPage.IsReady = model.IsReady;
            scannedPage.OrderText = model.OrderText;
            //scannedPage.OrderDate = model.OrderDate;
            scannedPage.Message = model.Message;
            this.scannedPageManager.UpdateScannedPage(scannedPage);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [ActionName("getbyid")]
        public HttpResponseMessage Create(int id)
        {
            var scannedPage = this.scannedPageManager.GetScannedPageById(id);
            if (scannedPage == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var result = new
            {
                scannedPage.Id,
                scannedPage.ExecutionDate,
                scannedPage.OrderDate,
                scannedPage.OrderText,
                scannedPage.IsLocked, 
                scannedPage.IsReady,
                scannedPage.Message
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }



        [HttpPost]
        [HttpDelete]
        public HttpResponseMessage DeleteScanedPage()
        {
            int scannedPageId = int.Parse(HttpContext.Current.Request.Params["Id"]);
            this.scannedPageManager.DeleteScannedPageById(scannedPageId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
