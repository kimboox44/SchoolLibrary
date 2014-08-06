using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolLibrary.Controllers
{
    using System.Diagnostics.CodeAnalysis;

    using SchoolLibrary.BusinessLogic.Managers;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;

    public class AuthorController : Controller
    {
        //
        // GET: /Author/

        private IAuthorManager authorManager;


        public AuthorController(IAuthorManager authorManager)
        {
            this.authorManager = authorManager;
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult Index()
        {
            var allAuthors = this.authorManager.GetAllAuthors();
            return View(allAuthors);
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public ActionResult Create(AuthorBusinessModel author)
        {
            if (ModelState.IsValid)
            {
                this.authorManager.CreateAuthor(author);
              
                return this.RedirectToAction("Details", new { id = author.Id });
            }

            return View(author);
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult Edit(int id)
        {
            var author = this.authorManager.GetAuthorById(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            return View(author);
        }
        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public ActionResult Edit(AuthorBusinessModel author)
        {
            if (ModelState.IsValid)
            {
                this.authorManager.UpdateAuthor(author);
                return RedirectToAction("Details", new { id = author.Id });
            }
            
            return View(author);
        }
        [Authorize(Roles = "Librarian")]
        public ActionResult Details(int id)
        {
            var author = this.authorManager.GetAuthorById(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            return View(author);
        }
    }
}
