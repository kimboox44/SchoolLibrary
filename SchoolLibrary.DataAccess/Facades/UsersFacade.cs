namespace SchoolLibrary.DataAccess.Facades
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    public class UsersFacade: IUsersFacade, IDisposable
    {
        private ILibraryUow uow;

        public UsersFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// Sets a new generated password for specified user.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="userName">Username of the user</param>
        public string ResetPassword(int userId, string userName)
        {
            string newPass = Membership.GeneratePassword(8, 2);
            var membershipProvider = Membership.Provider as SimpleMembershipProvider;

            if (membershipProvider.HasLocalAccount(userId))
            {
                var token = membershipProvider.GeneratePasswordResetToken(userName);
                membershipProvider.ResetPasswordWithToken(token, newPass);
            }

            return newPass;
        }

        /// <summary>
        /// Gets username of specified user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns>Username</returns>
        public string GetUserName(int userId)
        {
            return this.uow.UsersProfiles.GetById(userId).UserName;
        }

        /// <summary>
        /// Removes specified user from all of his current roles.
        /// </summary>
        /// <param name="userName">Username of the user.</param>
        public void RemoveUserFromAllRoles(string userName)
        {
            var userRoles = Roles.GetRolesForUser(userName);
            if (userRoles.Length != 0)
            {
                Roles.RemoveUserFromRoles(userName, userRoles);
            }
        }

        /// <summary>
        /// Add user to role.
        /// </summary>
        /// <param name="userName">Username of the user.</param>
        /// <param name="newRoleName">Role to add.</param>
        public void AddUserToRole(string userName, string newRoleName)
        {
            Roles.AddUserToRole(userName, newRoleName);
        }

        /// <summary>
        /// Gets data of all registered users.
        /// </summary>
        /// <returns>List of <see cref="UserProfileBusinessModel"/></returns>
        public List<UserProfileBusinessModel> GetAllUsers()
        {
            List<UserProfileBusinessModel> usersToReturn = new List<UserProfileBusinessModel>();
            List<UserProfile> usersToMap;

            usersToMap = this.uow.UsersProfiles.GetAll().ToList();

            UserProfileMapper mapper = new UserProfileMapper();
            foreach (var user in usersToMap)
            {
                UserProfileBusinessModel businessModel = mapper.Map(user);
                var roles = Roles.GetRolesForUser(businessModel.UserName);
                businessModel.Role = (roles.Length == 0 ? string.Empty : roles[0]);
                businessModel.CreationDate = WebSecurity.GetCreateDate(businessModel.UserName);
                usersToReturn.Add(businessModel);
            }

            return usersToReturn;
        }

        /// <summary>
        /// Deletes all registration data of specified user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        public void DeleteUser(int userId)
        {
            string userName = this.GetUserName(userId);
            var roles = Roles.GetRolesForUser(userName);
            if (roles.Length > 0)
            {
                Roles.RemoveUserFromRoles(userName, roles);
            }

            var membershipProvider = Membership.Provider as SimpleMembershipProvider;

            if (membershipProvider.HasLocalAccount(userId))
            {
                membershipProvider.DeleteAccount(userName);
            }
            else
            {
                var accounts = membershipProvider.GetAccountsForUser(userName);
                if (accounts.Count != 0)
                {
                    foreach (var account in accounts)
                    {
                        membershipProvider.DeleteOAuthAccount(account.Provider, account.ProviderUserId);
                    }
                }
            }

            Membership.DeleteUser(userName, true);
        }
        
        public string[] GetUnconfirmedUsers()
        {
            string[] allUnconfirmedUsers = Roles.GetUsersInRole("Unregistered");
            return allUnconfirmedUsers;
        }

        public UserProfileBusinessModel GetUserByUserName(string userName)
        {
            UserProfileBusinessModel userProfileBusinessModel = new UserProfileBusinessModel();

            UserProfile user = this.uow.UsersProfiles.GetAll().FirstOrDefault(u => u.UserName == userName);
            UserProfileMapper userProfileMapper = new UserProfileMapper();
            userProfileBusinessModel = userProfileMapper.Map(user);

            userProfileBusinessModel.Role = Roles.GetRolesForUser(userProfileBusinessModel.UserName)[0];
            return userProfileBusinessModel;
        }

        public UserProfileBusinessModel GetUserById(int id)
        {
            UserProfileBusinessModel userProfileBusinessModel = new UserProfileBusinessModel();
            UserProfile user = uow.UsersProfiles.GetById(id);
            // UserProfile user = this.uow.UsersProfiles.GetAll().FirstOrDefault(u => u.UserId == id);
            UserProfileMapper userProfileMapper = new UserProfileMapper();
            userProfileBusinessModel = userProfileMapper.Map(user);

            userProfileBusinessModel.Role = Roles.GetRolesForUser(userProfileBusinessModel.UserName)[0];
            return userProfileBusinessModel;
        }

        public UserProfileBusinessModel SetRoleToUser(int userId, string role)
        {
            UserProfileBusinessModel userProfileBusinessModel = this.GetUserById(userId);
            userProfileBusinessModel.Role = role;
            return userProfileBusinessModel;
        }

        public void UpdateUserProfile(UserProfileBusinessModel userProfileBusinessModel)
        {
           // UserProfileMapper userProfileMapper = new UserProfileMapper();
          //  UserProfile userProfile = userProfileMapper.Map(userProfileBusinessModel);
           // this.uow.UsersProfiles.Update(userProfile);
           // this.uow.Commit();

            Roles.RemoveUserFromRole(userProfileBusinessModel.UserName, "Unregistered");
            Roles.AddUserToRole(userProfileBusinessModel.UserName, userProfileBusinessModel.Role);
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }
        }


        public void UpdateUsersEmail(int userId, string newEmail)
        {
            var user = this.uow.UsersProfiles.GetById(userId);
            user.Email = newEmail;
            this.uow.Commit();
        }
    }
}