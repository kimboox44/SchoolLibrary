using System.Collections.Generic;
using System.Web.Mvc;
using MvcPaging;
using SchoolLibrary.BusinessLogic.Managers;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.BusinessLogic.Managers.Interfaces;

namespace SchoolLibrary.Controllers
{
    using WebMatrix.WebData;

    public class SearchController : Controller
    {
        private ISearchBookManager searchBookManager;
        private IReservedItemManager reservedItemManager;
        private IReaderManager readerManager;

        public SearchController(ISearchBookManager searchBookManager, IReservedItemManager reservedBookManager, IReaderManager readerManager)
        {
            this.searchBookManager = searchBookManager;
            this.reservedItemManager = reservedBookManager;
            this.readerManager = readerManager;
        }

        private const int defaultPageSize = 3;

        [Authorize(Roles = "Admin, Librarian, Registered")]
        public ActionResult Index(string bookName, int? page)
        {
            ViewData["bookName"] = bookName;

            int currentPageIndex = page.HasValue ? page.Value : 1;

            IEnumerable<BookBusinessModel> bookModels = new List<BookBusinessModel>();

            if (string.IsNullOrWhiteSpace(bookName))
            {
                bookModels = (this.searchBookManager.GetAllBooks()).ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                bookModels = this.searchBookManager.GetBookByName(bookName).ToPagedList(currentPageIndex, defaultPageSize);
            }

            if (Request.IsAjaxRequest())
                return PartialView("SearchBook", bookModels);
            else
                return View(bookModels);
            //return View();
        }

        
    }
}
