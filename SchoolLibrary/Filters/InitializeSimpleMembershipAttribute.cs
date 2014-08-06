using System;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using SchoolLibrary.DataAccess.Entities;
using SchoolLibrary.DataAccess.UnitOfWork;
using WebMatrix.WebData;

namespace SchoolLibrary.Filters
{
    using SchoolLibrary.DataAccess.Context;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                try
                {
                    using (var context = new LibraryContext())
                    {
                        if (!context.Database.Exists())
                        {

                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName",
                       autoCreateTables: true);

                    this.InitializeData();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }

            private void InitializeData()
            {
                if (!Roles.RoleExists("Admin"))
                {
                    Roles.CreateRole("Admin");
                }

                if (!Roles.RoleExists("Librarian"))
                {
                    Roles.CreateRole("Librarian");
                }

                if (!Roles.RoleExists("Registered"))
                {
                    Roles.CreateRole("Registered");
                }

                if (!Roles.RoleExists("Unregistered"))
                {
                    Roles.CreateRole("Unregistered");
                }

                LibraryUow uow = new LibraryUow();

                if (!WebSecurity.UserExists("admin"))
                {
                    WebSecurity.CreateUserAndAccount("admin", "Pa$$word");
                    UserProfile user = uow.UsersProfiles.GetById(WebSecurity.GetUserId("admin"));
                    user.Email = "admin@gmail.com";
                    Roles.AddUserToRole("admin", "Admin");
                }

                if (!WebSecurity.UserExists("librarian"))
                {
                    WebSecurity.CreateUserAndAccount("librarian", "123456");
                    UserProfile user = uow.UsersProfiles.GetById(WebSecurity.GetUserId("librarian"));
                    user.Email = "librarian@gmail.com";
                    Roles.AddUserToRole("librarian", "Librarian");
                }

                if (!WebSecurity.UserExists("yarkip"))
                {
                    WebSecurity.CreateUserAndAccount("yarkip", "123456");
                    UserProfile user = uow.UsersProfiles.GetById(WebSecurity.GetUserId("yarkip"));
                    user.Email = "yarkip@gmail.com";
                    Roles.AddUserToRole("yarkip", "Registered");
                }

                //if (!WebSecurity.UserExists("mykola"))
                //{
                //    WebSecurity.CreateUserAndAccount("mykola", "123456");
                //    UserProfile user = uow.UsersProfiles.GetById(WebSecurity.GetUserId("mykola"));
                //    user.Email = "mykola@gmail.com";
                //    //Roles.AddUserToRole("mykola", "Registered");
                //}

                //if (!WebSecurity.UserExists("kola"))
                //{
                //    WebSecurity.CreateUserAndAccount("kola", "123456");
                //    UserProfile user = uow.UsersProfiles.GetById(WebSecurity.GetUserId("kola"));
                //    user.Email = "kola@gmail.com";
                //    //Roles.AddUserToRole("kola", "Registered");
                //}

                if (!WebSecurity.UserExists("mike"))
                {
                    WebSecurity.CreateUserAndAccount("mike", "123456");
                    UserProfile user = uow.UsersProfiles.GetById(WebSecurity.GetUserId("mike"));
                    user.Email = "mike@gmail.com";
                }

                if (!WebSecurity.UserExists("bob"))
                {
                    WebSecurity.CreateUserAndAccount("bob", "123456");
                    UserProfile user = uow.UsersProfiles.GetById(WebSecurity.GetUserId("bob"));
                    user.Email = "bob@gmail.com";
                }

                if (!WebSecurity.UserExists("rick"))
                {
                    WebSecurity.CreateUserAndAccount("rick", "123456");
                    UserProfile user = uow.UsersProfiles.GetById(WebSecurity.GetUserId("rick"));
                    user.Email = "rick@gmail.com";
                }

                //test reader
                if (!WebSecurity.UserExists("alice"))
                {
                    WebSecurity.CreateUserAndAccount("alice", "123456");
                    UserProfile user = uow.UsersProfiles.GetById(WebSecurity.GetUserId("alice"));
                    user.Email = "alice@gmail.com";
                }

                if (Roles.GetRolesForUser("alice").Length == 0)
                {
                    Roles.AddUserToRole("alice", "Unregistered");
                }

                if (Roles.GetRolesForUser("mike").Length == 0)
                {
                    Roles.AddUserToRole("mike", "Unregistered");
                }

                if (Roles.GetRolesForUser("bob").Length == 0)
                {
                    Roles.AddUserToRole("bob", "Registered");
                }

                if (Roles.GetRolesForUser("rick").Length == 0)
                {
                    Roles.AddUserToRole("rick", "Unregistered");
                }

                uow.Commit();

            }
        }
    }
}