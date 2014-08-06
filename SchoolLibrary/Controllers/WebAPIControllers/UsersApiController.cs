namespace SchoolLibrary.Controllers.WebAPIControllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.Models;

    public class UsersApiController : ApiController
    {
        private IUserManager userManager;

        public UsersApiController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public HttpResponseMessage GetUsers(int pageSize, int pageNum)
        {
            var query = Request.GetQueryNameValuePairs();
            int filtersCount = int.Parse(query.Where(p => p.Key == "filterscount").First().Value);
            List<Func<UserProfileBusinessModel, bool>> predicates = new List<Func<UserProfileBusinessModel, bool>>();

            for (int i = 0; i < filtersCount; i++)
            {
                string filterDataField = query.Where(p => p.Key == "filterdatafield" + i).First().Value;
                string filterCondition = query.Where(p => p.Key == "filtercondition" + i).First().Value;
                string filterValue = query.Where(p => p.Key == "filtervalue" + i).First().Value;
                switch (filterDataField)
                {
                    case "UserName":
                        predicates.Add(u => u.UserName.ToLower().Contains(filterValue.ToLower()));
                        break;
                    case "Email":
                        predicates.Add(u => u.Email.ToLower().Contains(filterValue.ToLower()));
                        break;
                    case "CreationDate":
                    {
                        DateTime date = DateTime.Parse(filterValue);
                        switch (filterCondition)
                        {
                            case "GREATER_THAN_OR_EQUAL":
                                predicates.Add(u => u.CreationDate >= date);
                                break;
                            case "LESS_THAN_OR_EQUAL":
                                predicates.Add(u => u.CreationDate <= date);
                                break;
                        }

                        break;
                    }
                }
            }

            int count;
            var users = this.userManager.GetUsers(pageSize * pageNum, pageSize, out count, predicates);

            var result = new {TotalRows = count, Rows = users};

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage DeleteUser()
        {
            int userId = int.Parse(HttpContext.Current.Request.Params["id"]);
            this.userManager.DeleteUser(userId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage DeleteUsers()
        {
            var s = HttpContext.Current.Request.Params["usersId"].Split(',');
            int[] usersId = new int[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                usersId[i] = int.Parse(s[i]);
            }

            this.userManager.DeleteUsers(usersId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage ResetPassword()
        {
            int userId = int.Parse(HttpContext.Current.Request.Params["userId"]);
            this.userManager.ResetPassword(userId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage ChangeRole()
        {
            int userId = int.Parse(HttpContext.Current.Request.Params["id"]);
            string newRole = HttpContext.Current.Request.Params["newRole"];
            this.userManager.ChangeUserRole(userId, newRole);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public HttpResponseMessage GetUnconfirmedUsers(int pageSize, int pageNum)
        {
            var unconfirmedUsers = this.userManager.GetUnconfirmedUsers(pageSize * pageNum, pageSize);
            var listOfUnconfirmedUsers = from user in unconfirmedUsers
                                         select new UnconfirmedUsersForWidgetsModel(user);
            var ul = listOfUnconfirmedUsers.ToList();
            var result = new { TotalRows = this.userManager.GetCountOfAllUnconfirmedUsers(), Rows = ul };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        public HttpResponseMessage GetUnconfirmed()
        {
            string[] allUnconfirmedUsers = this.userManager.GetAllUnconfirmedUsers();
            List<UsersConfirmationModel> list = this.userManager.CreateListOfUnconfirmedUsers(allUnconfirmedUsers);
            var users = from user in list
                        select new UnconfirmedUsersForWidgetsModel(user);
            var ul = users.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, ul);
        }
    }
}