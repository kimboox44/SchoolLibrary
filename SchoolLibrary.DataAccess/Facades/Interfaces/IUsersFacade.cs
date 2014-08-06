namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface IUsersFacade
    {
        List<UserProfileBusinessModel> GetAllUsers();

        string GetUserName(int userId);

        void RemoveUserFromAllRoles(string userName);

        void AddUserToRole(string userName, string newRoleName);

        string ResetPassword(int userId, string userName);

        void DeleteUser(int userId);
        
        string[] GetUnconfirmedUsers();

        void UpdateUsersEmail(int userId, string newEmail);

        UserProfileBusinessModel GetUserByUserName(string userName);

        UserProfileBusinessModel GetUserById(int id);

        UserProfileBusinessModel SetRoleToUser(int userId, string role);

        void UpdateUserProfile(UserProfileBusinessModel userProfileBusinessModel);
    }
}