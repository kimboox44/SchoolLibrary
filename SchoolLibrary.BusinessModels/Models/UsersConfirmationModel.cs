using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolLibrary.BusinessModels.Models;

namespace SchoolLibrary.Models
{
    public class UsersConfirmationModel
    {
        public UserProfileBusinessModel UnconfirmedUser { get; set; }

        public ReaderBusinessModel CoincidedReaderBusiness { get; set; }

        public string[] ListOfRoles { get; set; }

        public UsersConfirmationModel()
        {
            ListOfRoles = new[] {"Admin", "Librarian", "Registered", "Unregistered"};
        }
    }
}