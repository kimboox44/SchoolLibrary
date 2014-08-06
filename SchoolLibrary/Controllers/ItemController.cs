namespace SchoolLibrary.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using SchoolLibrary;
    using Newtonsoft.Json;
    using SchoolLibrary.BusinessLogic.Managers;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.Models;
    
    public class ItemController : Controller
    {
        private IItemManager itemManager;
        private IBookManager bookInfoManager;
        private IDiskManager diskManager;
        private IMagazineManager magazineManager;
        private IAuthorManager authorManager;
        private IBookAuthorManager bookauthorManager;
        private IRecommendationManager recommendationManager;

        public ItemController(IItemManager itemManager,
                              IBookManager bookInfoManager,
                              IMagazineManager magazineManager,
                              IDiskManager diskManager,
                              IAuthorManager authorManager,
                              IBookAuthorManager bookauthorManager,
                              IRecommendationManager recommendationManager)
        {
            this.itemManager = itemManager;
            this.bookInfoManager = bookInfoManager;
            this.diskManager = diskManager;
            this.magazineManager = magazineManager;
            this.authorManager = authorManager;
            this.bookauthorManager = bookauthorManager;
            this.recommendationManager = recommendationManager;
        }

        [Authorize(Roles = "Librarian, Registered")]
        public ActionResult Index()
        {
            //IEnumerable<BookBusinessModel> allBooks = new List<BookBusinessModel>();
            //  var allBooks = this.searchBookManager.GetAllBooks();

            var allBooks = this.itemManager.GetAllItems();
            return this.View(allBooks);
        }

        public ActionResult IndexWithWidgets()
        {
            return this.View();
        }

        // GET: /Book/Details/{id}
        [Authorize(Roles = "Librarian, Registered")]
        public ActionResult Details(int id)
        {
            var item = this.itemManager.GetItemById(id);
            if (item == null)
            {
                return this.HttpNotFound();
            }

            return this.View(item);
        }

        // GET: /Book/Edit/{id}
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit(int id)
        {
            var item = this.itemManager.GetItemById(id);

            if (item == null)
            {
                return this.HttpNotFound();
            }

            if (item is BookBusinessModel)
            {
                Book book = new BookMapper().Map(item as BookBusinessModel);
                BookWithAuthorsShort bookWithAuthors = new BookWithAuthorsShortMapper().Map(book);
                return this.View("_EditBook", bookWithAuthors);
            }
            else if (item is MagazineBusinessModel)
            {
                return this.View("_EditMagazine", item as MagazineBusinessModel);
            }
            else
            {
                return this.View("_EditDisk", item as DiskBusinessModel);
            }
        }

        //[HttpGet]
        //public JsonResult GetAuthors(string search = "")
        //{
        //    var aut = this.authorManager.SearchAuthorsShortInfo(search);
        //    return Json(aut, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost, ActionName("Edit")]
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit([FromJson] BookWithAuthorsShort viewModel)
        {
            if (ModelState.IsValid)
            {
                this.bookInfoManager.UpdateBookWithAuthors(viewModel);
                this.recommendationManager.RecalculateItemTagScoresAsync(viewModel.Id);
                return this.RedirectToAction("Details", new { id = viewModel.Id });
            }

            return this.View("_EditBook", viewModel);
        }

        [HttpPost, ActionName("EditDisk")]
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit([FromJson] DiskBusinessModel disk)
        {
            if (ModelState.IsValid)
            {
                this.diskManager.UpdateDisk(disk);
                this.recommendationManager.RecalculateItemTagScoresAsync(disk.Id);
                return this.RedirectToAction("Details", new { id = disk.Id });
            }

            return this.View("_EditDisk", disk);
        }

        [HttpPost, ActionName("EditMagazine")]
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit([FromJson] MagazineBusinessModel magazine)
        {
            if (ModelState.IsValid)
            {
                this.magazineManager.UpdateMagazine(magazine);
                this.recommendationManager.RecalculateItemTagScoresAsync(magazine.Id);
                return this.RedirectToAction("Details", new { id = magazine.Id });
            }

            return this.View("_EditMagazine", magazine);
        }

        //GET: /Item/Add
        [Authorize(Roles = "Librarian")]
        public ActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = "Librarian")]
        public virtual ActionResult LoadPartial(string value)
        {
            switch (value)
            {
                case "book":
                    var model1 = new BookWithAuthorsShort();
                    return this.PartialView("_AddBook", model1);
                case "disk":
                    var model2 = new DiskBusinessModel();
                    return this.PartialView("_AddDisk", model2);
                case "magazine":
                    var model3 = new MagazineBusinessModel();
                    return this.PartialView("_AddMagazine", model3);
                default:
                    return null;
            }
        }

        //POST: /Item/Add
        [HttpPost, ActionName("Add")]
        [Authorize(Roles = "Librarian")]
        public ActionResult Add([FromJson] BookWithAuthorsShort viewModel)
        {
            if (ModelState.IsValid)
            {
                this.bookInfoManager.CreateBookWithAuthors(viewModel);
                this.recommendationManager.RecalculateItemTagScoresAsync(viewModel.Id);
                return this.RedirectToAction("IndexWithWidgets");
            }

            return this.PartialView("_AddBook", viewModel);
        }

        [HttpPost, ActionName("AddDisk")]
        [Authorize(Roles = "Librarian")]
        public ActionResult Add(DiskBusinessModel newDisk)
        {
            if (ModelState.IsValid)
            {
                this.diskManager.CreateDisk(newDisk);
                this.recommendationManager.RecalculateItemTagScoresAsync(newDisk.Id);
                return this.RedirectToAction("IndexWithWidgets");
            }

            return this.PartialView("_AddDisk", newDisk);
        }

        [HttpPost, ActionName("AddMagazine")]
        [Authorize(Roles = "Librarian")]
        public ActionResult Add(MagazineBusinessModel newMagazine)
        {
            if (ModelState.IsValid)
            {
                this.magazineManager.CreateMagazine(newMagazine);
                this.recommendationManager.RecalculateItemTagScoresAsync(newMagazine.Id);
                return this.RedirectToAction("IndexWithWidgets");
            }

            return this.PartialView("_AddMagazine", newMagazine);
        }

        //POST: /Book/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Librarian")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.bookInfoManager.RemoveBookById(id);
            return this.RedirectToAction("Index");
        }

        #region Obsolete implementation
        // GET: /Book/Edit/{id}
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit_old(int id)
        {
            var book = this.bookInfoManager.GetBookById(id);

            if (book == null)
            {
                return this.HttpNotFound();
            }

            return this.View(book);
        }

        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit_old(BookBusinessModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.bookInfoManager.UpdateBook(viewModel);

                return this.RedirectToAction("Details", new { id = viewModel.Id });
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult DeleteAuthor(int bookid, int authorid)
        {
            var bookauthor = this.bookauthorManager.GetBookAuthorById(bookid, authorid);
            if (bookauthor == null)
            {
                return this.HttpNotFound();
            }

            return this.View(bookauthor);
        }

        [HttpPost, ActionName("DeleteAuthor")]
        [Authorize(Roles = "Librarian")]
        public ActionResult DeleteAuthorConfirmed(int bookid, int authorid)
        {
            this.bookauthorManager.RemoveAuthorFromBook(bookid, authorid);

            return this.RedirectToAction("Edit_old", new { id = bookid });
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult AddAuthor(int bookid)
        {
            var a = this.authorManager.GetAllAuthors();
            return this.View(a);
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult AddAuthorConfirmed(int bookid, int authorid)
        {
            if (ModelState.IsValid)
            {
                this.bookauthorManager.AddAuthorToBook(bookid, authorid);
                return this.RedirectToAction("Edit_old", new { id = bookid });
            }

            return this.RedirectToAction("AddAuthor", new { id = bookid });
        }

        #endregion


    }
}
