using System.Transactions;

namespace SchoolLibrary.BusinessLogic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using SchoolLibrary.Models;
    using SchoolLibrary.ServiceAgents;

    public class UserManager : IUserManager, IDisposable
    {
        private ILibraryUow uow;
        private IUsersFacade usersFacade;

        private IReaderManager readerManager;
        
        public UserManager(ILibraryUow uow, IUsersFacade usersFacade, IReaderManager readerManager)
        {
            this.uow = uow;
            this.readerManager = readerManager;
            this.usersFacade = usersFacade;
        }

        /// <summary>
        /// Sets a new generated password for specified user and sends e-mail notification.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        public void ResetPassword(int userId)
        {
            var user = this.usersFacade.GetUserById(userId);
            string newPass = this.usersFacade.ResetPassword(userId, user.UserName);

            MailSender mailer = new MailSender();
            mailer.Send(user.Email, "New password", "Your new Pass is  " + newPass);
        }

        /// <summary>
        /// Deletes all registration data of specified user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        public void DeleteUser(int userId)
        {
            this.readerManager.UnbindReaderAndUser(userId);
            this.usersFacade.DeleteUser(userId);
        }

        /// <summary>
        /// Deletes all registration data of all specified users.
        /// </summary>
        /// <param name="usersId">Array of users ids</param>
        public void DeleteUsers(int[] usersId)
        {
            foreach (var id in usersId)
            {
                this.DeleteUser(id);
            }
        }

        /// <summary>
        /// Removes user from current roles and adds to a new specified role.
        /// </summary>
        /// <param name="userId">User's id.</param>
        /// <param name="newRoleName">Name of the role to set.</param>
        public void ChangeUserRole(int userId, string newRoleName)
        {
            string userName = this.usersFacade.GetUserName(userId);
            this.usersFacade.RemoveUserFromAllRoles(userName);
            this.usersFacade.AddUserToRole(userName, newRoleName);
        }

        /// <summary>
        /// Gets data of all registered users.
        /// </summary>
        /// <returns>List of <see cref="UserProfileBusinessModel"/></returns>
        public List<UserProfileBusinessModel> GetAllUsers()
        {
            return this.usersFacade.GetAllUsers();
        }
        
        public List<UserProfileBusinessModel> GetUsers(int skip, int take, out int filteredCount,
            List<Func<UserProfileBusinessModel, bool>> filters = null)
        {
            var users = this.GetAllUsers().AsQueryable();

            if (filters != null)
            {
                foreach (var predicate in filters)
                {
                    users = users.Where(predicate).AsQueryable();
                }
            }

            filteredCount = users.Count();

            return users.Skip(skip).Take(take).ToList();
        }

        public string[] GetAllUnconfirmedUsers()
        {
            return usersFacade.GetUnconfirmedUsers();
        }

        public List<UsersConfirmationModel> GetUnconfirmedUsers(int skip, int take)
        {
            List<UsersConfirmationModel> allUnconfirmedUsers = CreateListOfUnconfirmedUsers(GetAllUnconfirmedUsers());
            return allUnconfirmedUsers.Skip(skip).Take(take).ToList();
        }

        public List<UsersConfirmationModel> CreateListOfUnconfirmedUsers(string[] allUnconfirmedUsers)
        {
            List<UsersConfirmationModel> list=new List<UsersConfirmationModel>();
            foreach (var item in allUnconfirmedUsers)
            {
                UserProfileBusinessModel user = usersFacade.GetUserByUserName(item);
                ReaderBusinessModel readerBusiness = readerManager.GetReaderByEmail(user.Email);
                UsersConfirmationModel listItem = new UsersConfirmationModel
                {
                    CoincidedReaderBusiness = readerBusiness,
                    UnconfirmedUser = user
                };

                list.Add(listItem);
            }

            return list;
        }

        public ReaderBusinessModel BindUserWithReader(int userId, int readerId)
        {
            ReaderBusinessModel readerBusinessModel = this.readerManager.GetReaderById(readerId);
            UserProfileBusinessModel userProfileBusinessModel = usersFacade.GetUserById(userId);
            readerBusinessModel.UserProfileBusiness = userProfileBusinessModel;
            return readerBusinessModel;
        }

        public void ConfirmUsers(List<UsersConfirmationModel> listOfUsersConfirmation)
        {
            if (listOfUsersConfirmation != null)
            {
                foreach (var item in listOfUsersConfirmation)
                {
                    int userId = item.UnconfirmedUser.UserId;
                    int readerId = item.CoincidedReaderBusiness.ReaderId;
                    string role = item.UnconfirmedUser.Role;
                    if (readerId != 0)
                    {
                        ConfirmUserWithReader(userId, readerId, role);
                    }
                }
            }
        }

        public void ConfirmUserWithReader(int userId, int readerId, string role)
        {
                UserProfileBusinessModel userProfileBusinessModel = usersFacade.SetRoleToUser(userId, role);
                usersFacade.UpdateUserProfile(userProfileBusinessModel);
                ReaderBusinessModel readerBusinessModel = BindUserWithReader(userId, readerId);
                this.readerManager.UpdateReader(readerBusinessModel);
                MailSender mailer = new MailSender();
                string text = "Congradulations! You are successfully registered in the SchoolLibrary. Your user name is " +
                              userProfileBusinessModel.UserName + ". You are confirmed to reader " +
                              readerBusinessModel.FirstName + " " + readerBusinessModel.LastName;
                mailer.Send(readerBusinessModel.EMail, "Confirmation", text);
        }

        public UserProfileBusinessModel GetUserByUserName(string userName)
        {
            return usersFacade.GetUserByUserName(userName);
        }

        public int GetCountOfAllUnconfirmedUsers()
        {
            return usersFacade.GetUnconfirmedUsers().Count();
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }

            if (this.usersFacade as IDisposable != null)
            {
                (this.usersFacade as IDisposable).Dispose();
            }

            if (this.readerManager as IDisposable != null)
            {
                (this.readerManager as IDisposable).Dispose();
            }
        }
    }
}