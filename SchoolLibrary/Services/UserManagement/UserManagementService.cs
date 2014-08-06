namespace SchoolLibrary.Services.UserManagement
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using StructureMap;

    public class UserManagementService : IUserManagementService
    {
        private IUserManager userManager;

        public UserManagementService()
        {
            this.userManager = ObjectFactory.GetInstance<IUserManager>();
        }

        public UserManagementService(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public List<UserProfileBusinessModel> GetAllUsers()
        {
            return this.userManager.GetAllUsers();
        }

        public void DeleteUser(int userId)
        {
            this.userManager.DeleteUser(userId);
        }

        public void ResetPassword(int userId)
        {
            this.userManager.ResetPassword(userId);
        }
    }
}