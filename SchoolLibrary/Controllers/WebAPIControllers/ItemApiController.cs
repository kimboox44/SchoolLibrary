namespace SchoolLibrary.Controllers.WebAPIControllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;

    public class ItemApiController : ApiController
    {
        private IItemManager itemManager;

        public ItemApiController(IItemManager itemManager)
        {
            this.itemManager = itemManager;
        }

        [HttpGet]
        public HttpResponseMessage GetItems(int pageSize, int pageNum)
        {
            var items = this.itemManager.GetItems(pageSize * pageNum, pageSize);
            var result = new { TotalRows = this.itemManager.GetCountOfAllItems(), Rows = items };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [ActionName("getbyid")]
        public HttpResponseMessage Create(int id)
        {
            var item = this.itemManager.GetItemById(id);
            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }
    }
}