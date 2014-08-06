using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolLibrary.Controllers.WebAPIControllers
{
    using System.IO;
    using System.Net.Http.Headers;
    using System.Web.Mvc;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.Common;
    using SchoolLibrary.Configuration;

    public class BarCodeApiController : ApiController
    {

        private IConsignmentManager consignmentManager;

        private IInventoryManager inventoryManager;

        public BarCodeApiController(IConsignmentManager consignmentManager, IInventoryManager inventoryManager)
        {
            this.consignmentManager = consignmentManager;
            this.inventoryManager = inventoryManager;
        }

        [HttpGet]
        public HttpResponseMessage GetBarCodeConsignment(int number)
        {
            var pdfresult = this.consignmentManager.GetPdfByConsignmentNumber(number);

            if (pdfresult == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Consignment Not Found");
            }

            string filename = string.Format("{0:D" + InventoryNumberFormat.CONSIGNMENT_FORMAT + "}", number);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(pdfresult) 
                };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = filename + ".pdf" 
                };
            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetBarCodeInventory(string number)
        {
            var pdfresult = this.inventoryManager.GetPdfByInventoryNumber(number);
            if (pdfresult == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Inventory Not Found");
            }

            string filename = number;
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(pdfresult) 
                };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = filename + ".pdf" 
                };
            return response;
        }
    }
}
