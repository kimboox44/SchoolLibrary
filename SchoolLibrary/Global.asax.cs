using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace SchoolLibrary
{
    using System.Data.Entity;
    using System.Web.Http.Validation.Providers;
    using SchoolLibrary.DataAccess.Context;
    using SchoolLibrary.DataAccess.Entities;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configuration.Services.RemoveAll(
                 typeof(System.Web.Http.Validation.ModelValidatorProvider),
                 v => v is InvalidModelValidatorProvider);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            
            Database.SetInitializer<LibraryContext>(new LibraryContextInitializer());
          //  new LibraryContext().UserProfiles.Find(1);
        }
    }
}