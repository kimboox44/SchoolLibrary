namespace SchoolLibrary.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.Models;
    using MvcPaging;
    using WebMatrix.WebData;

    [Authorize]
    public class ReaderController : Controller
    {
        private IReaderManager readerManager;
        private IReaderHistoryManager readerHistoryManager;
        private ITagsManager tagManager;
        private IRecommendationManager recommendationManager;
        private IUserManager userManager;

        public ReaderController(IReaderManager readerManager,
                                IReaderHistoryManager readerHistoryManager,
                                ITagsManager tagsManager,
                                IRecommendationManager recommendationManager,
                                IUserManager userManager)
        {
            this.readerManager = readerManager;
            this.readerHistoryManager = readerHistoryManager;
            this.tagManager = tagsManager;
            this.recommendationManager = recommendationManager;
            this.userManager = userManager;
        }

        private const int defaultPageSize = 3;

        public ActionResult Index()
        {
            ReaderRecommendationModel model = new ReaderRecommendationModel();
            var currentUser = this.userManager.GetUserByUserName(WebSecurity.CurrentUserName);
            model.ReaderId = this.readerManager.GetReaderByEmail(currentUser.Email).ReaderId;
            model.Items = this.recommendationManager.GetRecommendationForReader(model.ReaderId);
            return this.View(model);
        }

        public ActionResult ReadrHistory()
        {

            int readerId = readerManager.GetReaderByUserId(WebSecurity.CurrentUserId).ReaderId;

            return RedirectToAction("ReaderHistory", "Reader", new { readerId = readerId });
        }

        [HttpGet]
        public ActionResult ReaderHistory(int readerId, int? page)
        {
            int currentReaderId = readerManager.GetReaderByUserId(WebSecurity.CurrentUserId).ReaderId;

            if(readerId != currentReaderId) {
                readerId = currentReaderId;
            }
            int currentPageIndex = page.HasValue ? page.Value : 1;
            IEnumerable<ReaderHistoryBusinessModel> readerHistoryModel = new List<ReaderHistoryBusinessModel>();

            readerHistoryModel = (this.readerHistoryManager.GetReaderHistoriesByReaderId(readerId)).ToPagedList(currentPageIndex, defaultPageSize);

            if (Request.IsAjaxRequest())
                return PartialView("HistoryList", readerHistoryModel);
            else
            {
                return View(readerHistoryModel);
            }
        }

        public ActionResult Details(int id)
        {
            var reader = this.readerManager.GetReaderById(id);
            if (WebSecurity.CurrentUserId != reader.UserProfileBusiness.UserId && !Roles.IsUserInRole(WebSecurity.CurrentUserName, "Librarian"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(reader);
        }

        // GET: /readerBusiness/Edit/5

        public ActionResult Edit(int id)
        {
            ReaderBusinessModel reader = this.readerManager.GetReaderById(id);
            if (reader == null)
            {
                return this.HttpNotFound();
            }

            if (WebSecurity.CurrentUserId != reader.UserProfileBusiness.UserId && !Roles.IsUserInRole(WebSecurity.CurrentUserName, "Librarian"))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ReaderSelfEditModel
                            {
                                Id = reader.ReaderId,
                                Address = reader.Address,
                                Phone = reader.Phone,
                                EMail = reader.EMail
                            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ReaderSelfEditModel model)
        {
            try
            {
                List<TagBusinessModel> preferences = new List<TagBusinessModel>();

                if (model.TagsId != null)
                {
                    var tagIds = model.TagsId.Split(',').Select(s => int.Parse(s)).ToList();
                    foreach (var tagId in tagIds)
                    {
                        preferences.Add(this.tagManager.GetTag(tagId));
                    }
                }

                var reader = this.readerManager.GetReaderById(model.Id);
                reader.Address = model.Address;
                reader.Phone = model.Phone;
                reader.EMail = model.EMail;

                reader.Preferences = preferences;
                this.readerManager.UpdateReader(reader);
                recommendationManager.RecalculateReaderTagScoresAsync(reader);

                return RedirectToAction("Details", new { id = reader.ReaderId });
            }
            catch(Exception e)
            {
                return View(model);
            }
        }
    }
}