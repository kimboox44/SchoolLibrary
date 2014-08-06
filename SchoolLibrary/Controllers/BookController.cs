using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace SchoolLibrary.Controllers
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;

    using Newtonsoft.Json;

    using SchoolLibrary.BusinessLogic.Managers;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.Models;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    using SchoolLibrary;

    using Font = iTextSharp.text.Font;
    using Image = iTextSharp.text.Image;
    using Rectangle = iTextSharp.text.Rectangle;

    public class BookController : Controller
    {
        //
        // GET: /Book/
        private ISearchBookManager searchBookManager;

        private IBookManager bookInfoManager;
        private IAuthorManager authorManager;
        private IBookAuthorManager bookauthorManager;
        private IConsignmentManager consignmentManager;

        public BookController(ISearchBookManager searchBookManager, IBookManager bookInfoManager, IAuthorManager authorManager, IBookAuthorManager bookauthorManager, IConsignmentManager consignmentManager)
        {
            this.searchBookManager = searchBookManager;
            this.bookInfoManager = bookInfoManager;
            this.authorManager = authorManager;
            this.bookauthorManager = bookauthorManager;
            this.consignmentManager = consignmentManager;
        }

        [Authorize(Roles = "Librarian, Registered")]
        public ActionResult Index()
        {
            //IEnumerable<BookBusinessModel> allBooks = new List<BookBusinessModel>();
            //  var allBooks = this.searchBookManager.GetAllBooks();

            var allBooks = this.bookInfoManager.GetAllBooks();
            return View(allBooks);
        }

        // GET: /Book/Details/{id}
        [Authorize(Roles = "Librarian, Registered")]
        public ActionResult Details(int id)
        {

            var book = this.bookInfoManager.GetBookById(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // GET: /Book/Edit/{id}
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit(int id)
        {

            var book = this.bookInfoManager.GetBookWithAuthorsById(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }



        //[HttpGet]
        //public JsonResult GetAuthors(string search = "")
        //{
        //    var aut = this.authorManager.SearchAuthorsShortInfo(search);
        //    return Json(aut, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit([FromJson] BookWithAuthorsShort viewModel)
        {
            if (ModelState.IsValid)
            {
                this.bookInfoManager.UpdateBookWithAuthors(viewModel);

                return RedirectToAction("Details", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        //
        //GET: /Book/Add
        [Authorize(Roles = "Librarian")]
        public ActionResult Add()
        {
            return View();
        }

        //
        //POST: /Book/Add
        [HttpPost, ActionName("Add")]
        [Authorize(Roles = "Librarian")]
        public ActionResult Add([FromJson] BookWithAuthorsShort viewModel)
        {
            if (ModelState.IsValid)
            {
                this.bookInfoManager.CreateBookWithAuthors(viewModel);
                return RedirectToAction("Index", new { viewModel.Id });
            }

            return View(viewModel);
        }

        //
        //GET: /Book/Delete/{id}
        [Authorize(Roles = "Librarian")]
        public ActionResult Delete(int id)
        {
            var book = this.bookInfoManager.GetBookById(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        //
        //POST: /Book/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Librarian")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.bookInfoManager.RemoveBookById(id);
            return RedirectToAction("Index");
        }


        #region Obsolete implementation
        // GET: /Book/Edit/{id}
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit_old(int id)
        {

            var book = this.bookInfoManager.GetBookById(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }



        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit_old(BookBusinessModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.bookInfoManager.UpdateBook(viewModel);

                return RedirectToAction("Details", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult DeleteAuthor(int bookid, int authorid)
        {
            var bookauthor = this.bookauthorManager.GetBookAuthorById(bookid, authorid);
            if (bookauthor == null)
            {
                return HttpNotFound();
            }

            return View(bookauthor);
        }


        [HttpPost, ActionName("DeleteAuthor")]
        [Authorize(Roles = "Librarian")]
        public ActionResult DeleteAuthorConfirmed(int bookid, int authorid)
        {
            this.bookauthorManager.RemoveAuthorFromBook(bookid, authorid);

            return RedirectToAction("Edit_old", new { id = bookid });
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult AddAuthor(int bookid)
        {
            var a = this.authorManager.GetAllAuthors();
            return View(a);
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult AddAuthorConfirmed(int bookid, int authorid)
        {
            if (ModelState.IsValid)
            {
                this.bookauthorManager.AddAuthorToBook(bookid, authorid);
                return RedirectToAction("Edit_old", new { id = bookid });
            }

            return RedirectToAction("AddAuthor", new { id = bookid });
        }

        #endregion

        //
        //GET: /Book/Consignment/{id}
        [Authorize(Roles = "Librarian")]
        public ActionResult Consignment()
        {
            var consignments = this.consignmentManager.GetAllConsignments();
            if (consignments == null)
            {
                return HttpNotFound();
            }

            return View(consignments);
        }

        #region obsolete
        //[System.Web.Http.HttpGet]
        //public FileStreamResult GetBarCodeConsignment(int number, float width, float height)
        //{
        //    //HttpResponseMessage response;

        //    var DPI = 72f;
        //    var hMargin = 10f;
        //    var vMargin = 10f;
        //    var consignment = this.consignmentManager.GetConsignmentByNumber(number);


        //    Rectangle rect = new Rectangle(width * DPI, height * DPI);
        //    Document document = new Document(rect, hMargin, hMargin, vMargin, vMargin);
        //    MemoryStream ms = new MemoryStream();
        //    PdfWriter writer = PdfWriter.GetInstance(document, ms);
        //    //string fontpath = Server.MapPath("..");
        //    //BaseFont baseBarCode = BaseFont.CreateFont(
        //    //    fontpath + @"\Content\code128.ttf", BaseFont.CP1252, BaseFont.EMBEDDED);

        //    //Font barCodeFont = new Font(baseBarCode, 40, Font.NORMAL, BaseColor.BLACK);
        //    BaseFont baseTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        //    Font defaultFont = new Font(baseTimes, 14, Font.NORMAL, BaseColor.BLACK);
        //    Paragraph title1 = new Paragraph(consignment.Item.Name + ".- " + consignment.Item.Year, defaultFont);

        //    //Paragraph title2 = new Paragraph(consignment.ArrivalDate.Value.Date.ToLongDateString());
        //    document.Open();
        //    document.Add(title1);
        //    // document.Add(title2);
        //    foreach (var inv in consignment.Inventories)
        //    {
        //        document.NewPage();
        //        string str = inv.Number;
        //        var bc = new Barcode128();

        //        bc.Code = str;
        //        PdfContentByte cb = writer.DirectContent;
        //        bc.TextAlignment = Element.ALIGN_CENTER;


        //        bc.StartStopText = true;
        //        bc.CodeType = Barcode128.CODE128;
        //        bc.Extended = false;



        //        iTextSharp.text.Image img = bc.CreateImageWithBarcode(cb,
        //          iTextSharp.text.BaseColor.BLACK, iTextSharp.text.BaseColor.BLACK);

        //        var bcRect = new Rectangle(bc.BarcodeSize);
        //        var hScale = rect.Width / bcRect.Width;
        //        var vScale = rect.Height / bcRect.Height;
        //        Rectangle rect1;
        //        if (vScale <= hScale)
        //        {
        //            rect1 = new Rectangle(bcRect.Width* vScale - 2 * hMargin , rect.Height - 2 * vMargin);

        //            img.ScaleAbsolute(rect1);
        //        }
        //        else
        //        {
        //            rect1 = new Rectangle(rect.Width - 2 * hMargin, bcRect.Height* hScale - 2 * vMargin );

        //            img.ScaleAbsolute(rect1);
        //        }

        //        img.SetAbsolutePosition((rect.Width - rect1.Width) / 2, (rect.Height - rect1.Height) / 2);

        //        cb.AddImage(img);

        //    }



        //    document.Close();
        //    writer.Close();
        //    byte[] file = ms.ToArray();
        //    MemoryStream output = new MemoryStream();
        //    output.Write(file, 0, file.Length);
        //    output.Position = 0;
        //    // ms.Close();
        //    //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //    //    {
        //    //        FileName = "mytext.pdf" 
        //    //    };
        //    //response = new HttpResponseMessage(HttpStatusCode.OK);
        //    //response.Content = new StreamContent(ms);
        //    //response.Content.Headers.ContentType =
        //    //    new MediaTypeHeaderValue("application/pdf");
        //    //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //    //response.Content.Headers.ContentDisposition.FileName = "aaa.pdf";

        //    //return response;
        //    string CONSIGNMENT_NUMBER_FORMAT = ":D10";
        //    string filename = String.Format("{0" +CONSIGNMENT_NUMBER_FORMAT+ "}", consignment.Number);
        //    HttpContext.Response.AddHeader("content-disposition", "inline; filename=" + filename + ".pdf");


        //    // Return the output stream
        //    return File(output, "application/pdf"); //new FileStreamResult(output, "application/pdf");
        //}

        #endregion
    }
}
