using Moq;
using SchoolLibrary.DataAccess.Entities;

namespace SchoolLibrary.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.Models;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IUserManager userManager;

        private IReaderManager readerManager;

        public AdminController(IUserManager userManager, IReaderManager readerManager)
        {
            this.userManager = userManager;
            this.readerManager = readerManager;
        }

        public ActionResult Index()
        {
            AdminHomeModel model = new AdminHomeModel(this.userManager.GetAllUsers());
            return this.View(model);
        }

        public ActionResult IndexWidgets()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult ResetPassword(int userId)
        {
            this.userManager.ResetPassword(userId);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            this.userManager.DeleteUser(id);
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public EmptyResult DeleteSelectedUsers(int[] usersId)
        {
            this.userManager.DeleteUsers(usersId);
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult ChangeRole(int id, string newRole)
        {
            this.userManager.ChangeUserRole(id, newRole);
            return new EmptyResult();
        }

        public ActionResult ShowUnconfirmedUsers()
        {
            string[] allUnconfirmedUsers = this.userManager.GetAllUnconfirmedUsers();
            List<UsersConfirmationModel> list = this.userManager.CreateListOfUnconfirmedUsers(allUnconfirmedUsers);
            return this.View(list);
        }

        public ActionResult Confirm(string userName, string readerName, string role)
        {
            if (readerName != "Reader with such e-mail does not exist")
            {
                UserProfileBusinessModel user = this.userManager.GetUserByUserName(userName);
                ReaderBusinessModel reader = new ReaderBusinessModel();
                reader = this.readerManager.GetReaderByFullName(readerName);
                int userId = user.UserId;
                int readerId = reader.ReaderId;
                this.userManager.ConfirmUserWithReader(userId, readerId, role);
                return this.RedirectToAction("IndexWidgets");
            }
            else
            {
                return this.RedirectToAction("ShowUnconfirmedUsersWidget");
            }
        }

        [HttpPost]
        public ActionResult ShowUnconfirmedUsers(List<UsersConfirmationModel> listOfUsersConfirmation)
        {
            this.userManager.ConfirmUsers(listOfUsersConfirmation);
            return this.RedirectToAction("IndexWidgets");
        }

        public ActionResult ShowUnconfirmedUsersWidget()
        {
            return this.View();
        }

        public ActionResult ShowUnconfirmedUsersKnockout()
        {
            return this.View();
        }
    }
}