using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolLibrary.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    using LinqToExcel;
    using MvcPaging;

    using SchoolLibrary.BusinessLogic.Managers;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;

    [Authorize(Roles = "Librarian")]
    public class LibrarianController : Controller
    {
        private const int defaultPageSize = 3;

        private IReaderManager readerManager;

        private IReaderHistoryManager readerHistoryManager;

        public LibrarianController(IReaderManager readerManager, IReaderHistoryManager readerHistoryManager)
        {
            this.readerManager = readerManager;
            this.readerHistoryManager = readerHistoryManager;
        }


        public ActionResult Index()
        {
            return this.RedirectToAction("ReadersSinglePage");
        }

        public ActionResult CreateReader()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateReader(ReaderBusinessModel reader)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.readerManager.CreateReader(reader);
                }

                return RedirectToAction("Readers", "Librarian");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Readers()
        {
            var readers = this.readerManager.GetAllReaders();
            return this.View(readers);
        }

        public ActionResult ReadersSinglePage()
        {
            return this.View();
        }

        public ActionResult ReaderDetails(int id)
        {
            var model = this.readerManager.GetReaderById(id);
            return this.View(model);
        }

        public ActionResult EditReader(int id)
        {
            ReaderBusinessModel reader = this.readerManager.GetReaderById(id);
            if (reader == null)
            {
                return this.HttpNotFound();
            }

            return View(reader);
        }

        [HttpPost]
        public ActionResult EditReader(ReaderBusinessModel reader)
        {
            try
            {
                this.readerManager.UpdateReader(reader);
                return RedirectToAction("ReaderDetails", new { id = reader.ReaderId });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ReaderHistory(int? readerId, string inventoryNumber, int? page)
        {

            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewData["inventoryNumber"] = inventoryNumber;
            ViewData["readerId"] = readerId;
            IEnumerable<ReaderHistoryBusinessModel> readerHistoryModel = new List<ReaderHistoryBusinessModel>();

            if (inventoryNumber == null || inventoryNumber == "" )
            {
                readerHistoryModel = (this.readerHistoryManager.GetReaderHistoriesByReaderId(readerId)).ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                readerHistoryModel = (this.readerHistoryManager.GetReaderHistoryByInventoryNumber(inventoryNumber, readerId)).ToPagedList(currentPageIndex, defaultPageSize);
            }
            if (readerHistoryModel.Count() > 0)
            {

                if (Request.IsAjaxRequest())
                    return PartialView("HistoryList", readerHistoryModel);
                else
                    return View(readerHistoryModel);
            }
            else
            {
                return RedirectToAction("CreateReaderHistory", "Librarian", new { readerId = readerId });
            }
        }

        [HttpGet]
        public ActionResult DeptorsReaders(int? minday, int? maxday)
        {
            List<DeptorsReadersModel> readerHistoryBusiness = new List<DeptorsReadersModel>();
            readerHistoryBusiness = this.readerHistoryManager.GetDebtorsReaders(minday, maxday);

            return View(readerHistoryBusiness);
        }

        [HttpGet]
        public ActionResult SendEmail(int readerId)
        {
            EmailMassageModel emailMassageModel = this.readerHistoryManager.GetMassageModelByReaderId(readerId);
            return View(emailMassageModel);
        }

        [HttpPost]
        public ActionResult SendEmail(EmailMassageModel emailMassageModel)
        {
            this.readerHistoryManager.SendEmailToUser(emailMassageModel);
            {
                return View(emailMassageModel);

            }
        }

        public ActionResult CreateReaderHistory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateReaderHistory(ReaderHistoryBusinessModel readerHistory, int readerId)
        {
            try
            {
                this.readerHistoryManager.AddNewReaderHistory(readerHistory, readerId);
                return RedirectToAction("ReaderHistory", "Librarian", new { readerId = readerId });

            }
            catch
            {
                ViewBag.ErrorMassage = "This inventory of book doesn't exist!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditReaderHistory(int readerId)
        {
            ReaderHistoryBusinessModel readerHistoryBusiness = this.readerHistoryManager.GetReaderHistoryById(readerId);
            return View(readerHistoryBusiness);
        }

        [HttpPost]
        public ActionResult EditReaderHistory(ReaderHistoryBusinessModel readerHistoryBusiness)
        {
            try
            {

                this.readerHistoryManager.UpdateReaderHistory(readerHistoryBusiness);
                return RedirectToAction("ReaderHistory", "Librarian", new { readerId = readerHistoryBusiness.ReaderBusiness.ReaderId });
            }
            catch
            {
                return View();
            }


        }

    }
}
