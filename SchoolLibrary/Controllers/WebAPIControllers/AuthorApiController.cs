using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolLibrary.Controllers.WebAPIControllers
{
    using System.Net.Http.Headers;
    using System.Web.Helpers;
    using System.Web.Mvc;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.MVCModels;

    public class AuthorApiController : ApiController
    {

        private IAuthorManager authorManager;

        public AuthorApiController(IAuthorManager authorManager)
        {
            this.authorManager = authorManager;
        }

        [HttpGet]
        public HttpResponseMessage GetAuthors(string search = "")
        {
            var aut = this.authorManager.SearchAuthorsShortInfo(search);
            return Request.CreateResponse(HttpStatusCode.OK, aut);
        }


        #region unimplemented
        // GET api/authorapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/authorapi/5
        public string Get(int id)
        {
            return "value";
        }



        // POST api/authorapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/authorapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/authorapi/5
        public void Delete(int id)
        {
        }
        #endregion
    }
}
