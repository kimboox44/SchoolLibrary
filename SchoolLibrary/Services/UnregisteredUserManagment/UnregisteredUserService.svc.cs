using System;
using System.Collections.Generic;
using System.Linq;
using SchoolLibrary.BusinessLogic.Managers.Interfaces;
using SchoolLibrary.BusinessModels.Models;
using StructureMap;


namespace SchoolLibrary.Services.UnregisteredUserManagment
{  
    public class UnregisteredUserService : IUnregisteredUserService
    {
        private readonly IUserManager _userManager;
        private readonly IReaderManager _readerManager;

        public UnregisteredUserService()
        {
            _userManager = ObjectFactory.GetInstance<IUserManager>();
            _readerManager = ObjectFactory.GetInstance<IReaderManager>();
        } 
            
        public UnregisteredUserService(IUserManager userManager)
        {            
            _userManager = userManager;
        }
        public UnregisteredUserService(IUserManager userManager, IReaderManager readerManager)
        {
            _userManager = userManager;
            _readerManager = readerManager;
        }
        public IList<UserProfileBusinessModel> GetAllUsers()
        {
            var unregistered = _userManager.GetAllUsers().Where(u => u.Role == "Unregistered");
            return unregistered.ToList();
        }

        public IList<string> GetAllRoles()
        {
            return System.Web.Security.Roles.GetAllRoles();
        }

        public SubmitResult Submit(int userId, string role)
        {
            try 
            {
                var user = _userManager.GetAllUsers().First(u => u.UserId == userId);
                EnsureCanSubmit(user);
                var reader = _readerManager.GetReaderByEmail(user.Email);
                _userManager.ConfirmUserWithReader(userId, reader.ReaderId, role);
            }
            catch (Exception e)
            {
                return new SubmitResult
                {
                    Success = false,
                    ErrorMessage = e.Message
                };
            }
            return new SubmitResult{Success = true};
        }

        private void EnsureCanSubmit(UserProfileBusinessModel user) 
        {
            var reader = _readerManager.GetReaderByEmail(user.Email);           
 
            if (reader == null) {
                throw new Exception("There is no such reader profile");
            }
            if (user.Role != "Unregistered")
            {
                throw  new Exception("The user is already registered");
            }         
        }
    }
}
