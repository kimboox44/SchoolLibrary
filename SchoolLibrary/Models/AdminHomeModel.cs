namespace SchoolLibrary.Models
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public class AdminHomeModel
    {
        public string[] Roles { get; private set; }

        public List<UserProfileBusinessModel> Users { get; private set; }

        public AdminHomeModel(List<UserProfileBusinessModel> users)
        {
            this.Users = users;
            this.Roles = new[] {"Admin", "Librarian", "Registered", "Unregistered"};
        }
    }
}