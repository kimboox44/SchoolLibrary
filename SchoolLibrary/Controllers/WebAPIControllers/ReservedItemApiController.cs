namespace SchoolLibrary.Controllers.WebAPIControllers
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.Configuration;

    using WebMatrix.WebData;

    public class ReservedItemApiController : ApiController
    {
        private IReservedItemManager reservedItemManager;

        private IReaderManager readerManager;

        public ReservedItemApiController(IReservedItemManager reservedItemManager, IReaderManager readerManager)
        {
            this.reservedItemManager = reservedItemManager;

            this.readerManager = readerManager;
        }

        [HttpDelete]
        [ActionName("delete")]
        public HttpResponseMessage DeleteReservedItem(int id)
        {
            var msg = this.reservedItemManager.DeleteReservedItemById(id) ? "Deleted successfully" : "Error during deleting";

            return Request.CreateResponse(HttpStatusCode.OK, msg);
        }

        [HttpDelete]
        [ActionName("deletechecked")]
        public HttpResponseMessage DeleteCheckedReservedItems()
        {
            var s = HttpContext.Current.Request.Params["resItemsId"].Split(',');

            var resItemsId = new int[s.Length];

            for (var i = 0; i < s.Length; i++)
            {
                resItemsId[i] = int.Parse(s[i]);
            }

            var msg = this.reservedItemManager.DeleteReservedItems(resItemsId);

            return Request.CreateResponse(HttpStatusCode.OK, msg);
        }

        [HttpGet]
        [ActionName("info")]
        public HttpResponseMessage ViewItemInfo(int id)
        {
            var msg = this.reservedItemManager.GetItemInfoById(id);

            if (string.IsNullOrEmpty(msg))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, msg);
        }

        [HttpGet]
        [ActionName("items")]
        public HttpResponseMessage GetReservedItems(int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
        {
            var readerId = this.readerManager.GetReaderByUserId(WebSecurity.CurrentUserId).ReaderId;

            var allItems = this.reservedItemManager.GetReservedItemsByReaderId(readerId);

            var items = from item in allItems
                        select new
                        {
                            item.Item.Id,
                            reservedItemId = item.Id,
                            item.Name,
                            item.Date,
                            item.Category,
                            item.ReadyDate,
                            item.IsReady
                        };

            var query = Request.GetQueryNameValuePairs();
            var filtersCount = int.Parse(query.Where(r => r.Key == "filterscount").Select(r => r.Value).First());
            for (var i = 0; i < filtersCount; i++)
            {
                var filterValue = query.Where(r => r.Key == "filtervalue" + i).Select(r => r.Value).First();
                var filterDataField = query.Where(r => r.Key == "filterdatafield" + i).Select(r => r.Value).First();
                items =
                    items.Where(
                        r => r.GetType().GetProperty(filterDataField).GetValue(r).ToString().ToLower().Contains(filterValue.ToLower()))
                           .Select(r => r);
            }

            if (!string.IsNullOrEmpty(sortorder))
            {
                if (sortorder == "asc")
                {
                    items = items.OrderBy(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
                else
                {
                    items = items.OrderByDescending(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
            }

            items = items.Skip(pageNum * pageSize).Take(pageSize);
            int totalRows = allItems.Count;
            if (filtersCount > 0)
            {
                totalRows = items.Count();
            }

            var result = new
            {
                TotalRows = totalRows,
                Rows = items
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [ActionName("itemsforlibrarian")]
        public HttpResponseMessage GetReservedItemsForLibrarian(int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
        {
            var allItems = this.reservedItemManager.GetReservedItemsForLibrarian();

            var items = from item in allItems
                        select new
                        {
                            item.Item.Id,
                            reservedItemId = item.Id,
                            readerName = item.Reader.FirstName + " " + item.Reader.LastName,
                            item.Name,
                            item.Date,
                            item.Category,
                            item.ReadyDate,
                            item.IsReady
                        };

            var query = Request.GetQueryNameValuePairs();
            var filtersCount = int.Parse(query.Where(r => r.Key == "filterscount").Select(r => r.Value).First());
            for (var i = 0; i < filtersCount; i++)
            {
                var filterValue = query.Where(r => r.Key == "filtervalue" + i).Select(r => r.Value).First();
                var filterDataField = query.Where(r => r.Key == "filterdatafield" + i).Select(r => r.Value).First();
                items =
                    items.Where(
                        r => r.GetType().GetProperty(filterDataField).GetValue(r).ToString().ToLower().Contains(filterValue.ToLower()))
                           .Select(r => r);
            }

            if (!string.IsNullOrEmpty(sortorder))
            {
                if (sortorder == "asc")
                {
                    items = items.OrderBy(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
                else
                {
                    items = items.OrderByDescending(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
            }

            items = items.Skip(pageNum * pageSize).Take(pageSize);
            int totalRows = allItems.Count;
            if (filtersCount > 0)
            {
                totalRows = items.Count();
            }

            var result = new
            {
                TotalRows = totalRows,
                Rows = items
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [ActionName("reserve")]
        public HttpResponseMessage ReserveItem(int id)
        {
            var reader = this.readerManager.GetReaderByUserId(WebSecurity.CurrentUserId);

            var config = new Config().Get();

            var limitReservedItems = config.Reserervation.MaxReservedItems;

            var msg = reader != null ? this.reservedItemManager.ReserveItem(reader.ReaderId, id, limitReservedItems)
                             : "You must be Reader in order to reserve items";

            if (msg.Contains("Successfully"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, msg);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [ActionName("itemsknockout")]
        public HttpResponseMessage GetReservedItemsKnockout()
        {
            var readerId = this.readerManager.GetReaderByUserId(WebSecurity.CurrentUserId).ReaderId;

            var allItems = this.reservedItemManager.GetReservedItemsByReaderId(readerId);

            var items = from item in allItems
                        select new
                        {
                            item.Item.Id,
                            reservedItemId = item.Id,
                            item.Name,
                            item.Date,
                            item.Category,
                            item.ReadyDate,
                            item.IsReady
                        };

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [HttpPut]
        [ActionName("setready")]
        public HttpResponseMessage SetReadyStatusForItem(int id)
        {
            var msg = this.reservedItemManager.SetReadyStatusForItem(id);

            return Request.CreateResponse(HttpStatusCode.OK, msg);
        }

    }
}
