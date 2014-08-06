namespace SchoolLibrary.Controllers.WebAPIControllers
{
    using System.Web;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class TagsApiController : ApiController
    {
        private ITagsManager tagsManager;
        private IRecommendationManager recommendationManager;

        public TagsApiController(ITagsManager tagsManager, IRecommendationManager recommendationManager)
        {
            this.tagsManager = tagsManager;
            this.recommendationManager = recommendationManager;
        }

        [HttpGet]
        public HttpResponseMessage GetTags(string token)
        {
            var tags = this.tagsManager.GetTags(token);
            return Request.CreateResponse(HttpStatusCode.OK, tags);
        }

        [HttpPost]
        public HttpResponseMessage Create()
        {
            string tag = HttpContext.Current.Request.Params["tag"];
            this.tagsManager.Create(tag);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public HttpResponseMessage GetReaderPreferences(int readerId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this.tagsManager.GetReaderPreferences(readerId));
        }

        [HttpPost]
        public HttpResponseMessage RecalculateAllTagScores()
        {
            this.recommendationManager.RecalculateAllTagScores();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage RemoveRecommendation()
        {
            int readerId = int.Parse(HttpContext.Current.Request.Params["readerId"]);
            int itemId = int.Parse(HttpContext.Current.Request.Params["itemId"]);
            this.recommendationManager.RemoveRecommendation(readerId, itemId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}