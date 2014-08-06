using System.Text.RegularExpressions;
using SchoolLibrary.BusinessLogic.Managers.Interfaces;
using SchoolLibrary.BusinessModels.Models;
using System;
using System.Collections.Generic;

namespace SchoolLibrary.NUnitTests.UnregisteredUserService
{
    class FakeUserManager:IUserManager
    {
        public int CalledConfirmation { private set; get; }
        public bool CanSubmitProp {  set; get; }

        public void ResetPassword(int userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUsers(int[] usersId)
        {
            throw new NotImplementedException();
        }

        public void ChangeUserRole(int userId, string newRoleName)
        {
            throw new NotImplementedException();
        }

        public bool CanSubmit(UserProfileBusinessModel user, out string errorMessage)
        {
            errorMessage = "";
            return CanSubmitProp;
        }

        public List<UserProfileBusinessModel> GetAllUsers()
        {
            return new List<UserProfileBusinessModel> {
                new UserProfileBusinessModel{
                    UserId = 1,
                    UserName = "User1",
                    Email = "email1",
                    Role = "Unregistered"
                },
                new UserProfileBusinessModel{
                    UserId = 2,
                    UserName = "User2",
                    Email = "email2",
                    Role = "Admin"
                },
            };
        }

        public List<UserProfileBusinessModel> GetUsers(int skip, int take, out int filteredCount, List<Func<UserProfileBusinessModel, bool>> filters = null)
        {
            throw new NotImplementedException();
        }

        public ReaderBusinessModel BindUserWithReader(int userId, int readerId)
        {
            throw new NotImplementedException();
        }

        public string[] GetAllUnconfirmedUsers()
        {
            throw new NotImplementedException();
        }

        public List<Models.UsersConfirmationModel> CreateListOfUnconfirmedUsers(string[] allUnconfirmedUsers)
        {
            throw new NotImplementedException();
        }

        public void ConfirmUsers(List<Models.UsersConfirmationModel> listOfUsersConfirmation)
        {
            throw new NotImplementedException();
        }

        public UserProfileBusinessModel GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public void ConfirmUserWithReader(int userId, int readerId, string role)
        {
            CalledConfirmation++;
        }

        public List<Models.UsersConfirmationModel> GetUnconfirmedUsers(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public int GetCountOfAllUnconfirmedUsers()
        {
            throw new NotImplementedException();
        }
    }
}
