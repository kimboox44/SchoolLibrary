namespace SchoolLibrary.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using MvcPaging;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;

    public class SearchItemController : Controller
    {
        private ISearchItemManager searchItemManager;

        public SearchItemController(ISearchItemManager searchItemManager)
        {
            this.searchItemManager = searchItemManager;
        }

        private const int defaultPageSize = 5;

        [Authorize(Roles = "Admin, Librarian, Registered")]
        public ActionResult Index(string itemName, string allTags, int? page)
        {
            var tagLst = new List<string>();
            var tags = this.searchItemManager.GetAllTags();
            tagLst = searchItemManager.ConvertTagListToStringList(tags);

            ViewData["allTags"] = new SelectList(tagLst);
            ViewData["tag"] = allTags;
            ViewData["itemName"] = itemName;



            int currentPageIndex = page.HasValue ? page.Value : 1;

            IEnumerable<ItemBusinessModel> itemModels = new List<ItemBusinessModel>();

            if ((string.IsNullOrWhiteSpace(allTags)) && (string.IsNullOrWhiteSpace(itemName)))
            {   // Get all Items
                itemModels = (this.searchItemManager.GetAllItems()).ToPagedList(currentPageIndex, defaultPageSize);
            }
            else if ((!string.IsNullOrWhiteSpace(itemName)) && (string.IsNullOrWhiteSpace(allTags)))
            {   // Search only for BookNmae or AuthorName
                itemModels = this.searchItemManager.GetItemByName(itemName).ToPagedList(currentPageIndex, defaultPageSize);
            }
            else if (!(string.IsNullOrWhiteSpace(allTags)) && (string.IsNullOrWhiteSpace(itemName)))
            {   // Search only for TagName
                itemModels = (this.searchItemManager.GetItemByTag(allTags)).ToPagedList(currentPageIndex, defaultPageSize);
            }
            else if ((!string.IsNullOrWhiteSpace(allTags)) && (!string.IsNullOrWhiteSpace(itemName)))
            {   // Get Items for tagName and BookName or AuthorName
                itemModels = (this.searchItemManager.GetItemByTagAndName(itemName, allTags)).ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            { 
                itemModels = (this.searchItemManager.GetAllItems()).ToPagedList(currentPageIndex, defaultPageSize);
            }

            if (Request.IsAjaxRequest())
                return PartialView("_SearchItem", itemModels);
            else
                return View(itemModels);
        }

    }
}
