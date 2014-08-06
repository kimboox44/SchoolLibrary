namespace SchoolLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ListOfUsersConfirmation
    {
        public ListOfUsersConfirmation(List<UsersConfirmationModel> list)
        {
            this.UsersConfirmationModelsList = list;
        }

        public List<UsersConfirmationModel> UsersConfirmationModelsList { get; set; }
    }
}