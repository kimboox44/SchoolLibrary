namespace SchoolLibrary.Services.UserManagement
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using SchoolLibrary.BusinessModels.Models;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserManagementService" in both code and config file together.
    [ServiceContract]
    public interface IUserManagementService
    {
        [OperationContract]
        List<UserProfileBusinessModel> GetAllUsers();

        [OperationContract]
        void DeleteUser(int userId);

        [OperationContract]
        void ResetPassword(int userId);
    }
}