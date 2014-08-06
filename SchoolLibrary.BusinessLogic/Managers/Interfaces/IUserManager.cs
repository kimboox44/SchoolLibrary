namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using System;
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.Models;

    public interface IUserManager
    {
        void ResetPassword(int userId);

        void DeleteUser(int userId);

        void DeleteUsers(int[] usersId);

        void ChangeUserRole(int userId, string newRoleName);

        List<UserProfileBusinessModel> GetAllUsers();

        List<UserProfileBusinessModel> GetUsers(int skip, int take, out int filteredCount,
            List<Func<UserProfileBusinessModel, bool>> filters = null);

        ReaderBusinessModel BindUserWithReader(int userId, int readerId);

        string[] GetAllUnconfirmedUsers();

        List<UsersConfirmationModel> CreateListOfUnconfirmedUsers(string[] allUnconfirmedUsers);

        void ConfirmUsers(List<UsersConfirmationModel> listOfUsersConfirmation);

        UserProfileBusinessModel GetUserByUserName(string userName);

        void ConfirmUserWithReader(int userId, int readerId, string role);

        List<UsersConfirmationModel> GetUnconfirmedUsers(int skip, int take);

        int GetCountOfAllUnconfirmedUsers();
    }
}