using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SchoolLibrary.DataAccess.Entities;
using SchoolLibrary.DataAccess.UnitOfWork;
using SchoolLibrary.Models;

namespace SchoolLibrary.Controllers
{
    using System.Web.Security;
    using SchoolLibrary.BusinessLogic.Managers;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.Filters;

    using WebMatrix.WebData;

    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Admin"))
            {
                return RedirectToAction("IndexWidgets", "Admin");
            }

            if (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Librarian"))
            {
                return RedirectToAction("Index", "Librarian");
            }

            if (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Registered"))
            {
                return RedirectToAction("Index", "Reader");
            }

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
