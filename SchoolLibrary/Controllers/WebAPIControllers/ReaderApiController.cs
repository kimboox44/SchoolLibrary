using System;
using System.Collections.Generic;
using System.Linq;


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

    public class ReaderApiController : ApiController
    {
        private IReaderManager readerManager;

        private IExcelManager excelManager;

        private const string ExcelFormat = ".xlsx";

        public ReaderApiController(IReaderManager readerManager, IExcelManager excelManager)
        {
            this.readerManager = readerManager;
            this.excelManager = excelManager;
        }

        [HttpPut]
        [ActionName("update")]
        public HttpResponseMessage Update([FromBody] ReaderBusinessModel reader)
        {
            if (ModelState.IsValid)
            {
                this.readerManager.UpdateReader(reader);
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage Create(ReaderBusinessModel reader)
        {
            if (ModelState.IsValid && reader != null)
            {
                this.readerManager.CreateReader(reader);
                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [ActionName("getbyid")]
        public HttpResponseMessage GetById(int id)
        {
            var reader = this.readerManager.GetReaderById(id);
            if (reader == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var result = new
            {
                reader.ReaderId,
                reader.FirstName,
                reader.LastName,
                reader.Address,
                reader.Birthday,
                reader.EMail,
                reader.Phone
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [ActionName("readers")]
        public HttpResponseMessage GetReaders(int pageSize, int pageNum, string sortdatafield = "", string sortorder = "")
        {
            var query = Request.GetQueryNameValuePairs();
            var model = this.readerManager.GetReadersForGrid(query, pageSize, pageNum, sortdatafield, sortorder);

            var result = new { TotalRows = model.TotalRows, Rows = model.Readers };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage AddReadersFromFile()
        {
            HttpPostedFile myFile = HttpContext.Current.Request.Files["File"];
            var readersFromFile = new ReadingFromExcelModel();
            string added = string.Empty;
            string errors = string.Empty;
            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = HttpContext.Current.Server.MapPath("~/App_Data/");
                string path = Path.Combine(pathForSaving, myFile.FileName);
                try
                {
                    if (!path.EndsWith(ExcelFormat))
                    {
                        throw new Exception("incorrect file type");
                    }

                    myFile.SaveAs(path);
                    readersFromFile = this.excelManager.GetReadersFromFile(path);
                }
                catch (Exception ex)
                {
                    var errorResult = new
                        {
                            errors = string.Format("File upload failed: {0}", ex.Message), 
                            added = string.Empty,
                            isUploaded = true
                        };
                
                    return Request.CreateResponse(HttpStatusCode.BadRequest, errorResult);
                }

                var addedReaders = this.readerManager.CreateReaders(readersFromFile.Readers);
                added = "These readers where added: " + Environment.NewLine;
                foreach (var reader in addedReaders)
                {
                    added += reader.FirstName + " " + reader.LastName + Environment.NewLine;
                }

                errors = this.excelManager.GenerateErrorString(readersFromFile);
            }

            var result = new { isUploaded = true, added = added, errors = errors };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
