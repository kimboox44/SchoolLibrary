using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SchoolLibrary
{
    using System.Web.Http;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("Services/UserManagement/UserManagementService.svc");
            routes.IgnoreRoute("Services/UnregisteredUserManagement/UnregisteredUserService.svc");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "Librarian",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Librarian", action = "Readers", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "Admin",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional });
        }
    }
}