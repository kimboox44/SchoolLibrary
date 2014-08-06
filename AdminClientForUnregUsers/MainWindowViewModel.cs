using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.BusinessModels.Models;
using AdminClientForUnregUsers.ServiceReference1;

namespace AdminClientForUnregUsers
{
    using System.Collections.ObjectModel;

    class MainWindowViewModel
    {
        private readonly IUnregisteredUserService _client;
        private readonly ObservableCollection<UserProfileBusinessModel> _unregisteredUsers;
        private readonly ObservableCollection<string> _rolesCollection;
        private const string DefaultRole = "Registered";

        public MainWindowViewModel()
        {
            _client = new UnregisteredUserServiceClient();
            _unregisteredUsers = new ObservableCollection<UserProfileBusinessModel>();            
            _rolesCollection = new ObservableCollection<string>(_client.GetAllRoles().Where(r => r != "Unregistered"));
        }
              
        public void Refresh()
        {
                _unregisteredUsers.Clear();
                foreach (var user in _client.GetAllUsers())
                {
                    user.Role = DefaultRole;
                    _unregisteredUsers.Add(user);
                }
        }

        public SubmitResult Submit(int userId)
        {
            var user = _unregisteredUsers.First(u => u.UserId == userId);
            var result = _client.Submit(userId, user.Role);
            return result;
        }

        public ObservableCollection<UserProfileBusinessModel> UnregisteredUsers
        {
            get
            {
                return _unregisteredUsers;
            }
        }

        public ICollection<string> GetAllRoles
        {
            get
            {
                return _rolesCollection;
            }
        } 
    }
}
