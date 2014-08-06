namespace SchoolLibrary.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;
    using MvcPaging;
    using SchoolLibrary.BusinessLogic.Managers;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using WebMatrix.WebData;

    public class ScannedPageController : Controller
    {
        private IScannedPageManager scannedPageManager;
        private IBookManager bookManager;
        private IReaderManager readerManager;
        private IItemManager itemManager;


        public ScannedPageController(IScannedPageManager scannedPageManager, IBookManager bookManager, IReaderManager readerManager, IItemManager itemManager)
        {
            this.scannedPageManager = scannedPageManager;
            this.bookManager = bookManager;
            this.readerManager = readerManager;
            this.itemManager = itemManager;
        }

        [Authorize(Roles = "Admin, Librarian, Registered")]
        public ActionResult Index()
        {
            List<ScannedPageBusinessModel> scannedPages;

            if ((Roles.IsUserInRole(WebSecurity.CurrentUserName, "Librarian")) || (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Admin")))
            {
                scannedPages = this.scannedPageManager.GetAllScannedPages();
                return this.View(scannedPages);
            }
            else
            {
                var reader = this.readerManager.GetReaderByUserId(WebSecurity.CurrentUserId);

                if (reader != null)
                {
                    scannedPages = this.scannedPageManager.GetScannedPageByReaderId(reader.ReaderId);
                    return this.View(scannedPages);
                }
                return this.View();
            }
        }

        public ActionResult IndexWidgets()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin, Librarian, Registered")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            this.scannedPageManager.DeleteScannedPageById(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, Librarian, Registered")]
        public ActionResult Edit(int id)
        {
            ScannedPageBusinessModel scannedPage = this.scannedPageManager.GetScannedPageById(id);
            if (scannedPage == null)
            {
                return this.HttpNotFound();
            }
            return View(scannedPage);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Librarian, Registered")]
        public ActionResult Edit(ScannedPageBusinessModel model)
        {
            var scannedPage = this.scannedPageManager.GetScannedPageById(model.Id);
            scannedPage.IsLocked = model.IsLocked;
            scannedPage.IsReady = model.IsReady;
            scannedPage.OrderText = model.OrderText;
            scannedPage.OrderDate = model.OrderDate;
            scannedPage.Message = model.Message;
            this.scannedPageManager.UpdateScannedPage(scannedPage);
            return RedirectToAction("Index");
        }





        //[Authorize(Roles = "Admin, Librarian, Registered")]
        //public ActionResult Add()
        //{
        //    return View();
        //}

        //public ActionResult CreateOrder(int Id)
        //{
        //    Session["bookId"] = Id;
        //    return View();
        //}

        //[HttpPost]
        //public string CreateOrder(int bookId, ScannedPageBusinessModel scannedPage)
        //{
        //    int readerId;

        //    try
        //    {
        //        bookId = bookManager.GetBookById(bookId).Id;
        //        readerId = readerManager.GetReaderByUserId(WebSecurity.CurrentUserId).ReaderId;
        //    }
        //    catch
        //    {
        //        return "You must be Reader in order to reserve books";
        //    }

        //    return this.scannedPageManager.ScanPages((int)readerId, bookId, scannedPage);

        //}


        public ActionResult CreateOrder(int Id)
        {
            ItemBusinessModel item = this.itemManager.GetItemById(Id);
            ScannedPageBusinessModel mySkanPage = new ScannedPageBusinessModel();
            mySkanPage.Item = item;
            mySkanPage.OrderText = "{start page}-{end page}(count copies); \n For example: \n{1}-{3}(1);\n{11}-{13}(1);";
            return View(mySkanPage);
        }



        [HttpPost]
        [Authorize(Roles = "Admin, Librarian, Registered")]
        public ActionResult CreateOrder(int Id, ScannedPageBusinessModel scannedPage)
        {
            try
            {
                var reader = this.readerManager.GetReaderByUserId(WebSecurity.CurrentUserId);
                var item = this.itemManager.GetItemById(Id);

                scannedPage.Reader = reader;
                scannedPage.Item = item;

                //this.scannedPageManager.ScanPages(readerId, Id, scannedPage);
                this.scannedPageManager.CreateScanPages(scannedPage);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
