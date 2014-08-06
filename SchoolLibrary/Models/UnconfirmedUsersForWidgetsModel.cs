namespace SchoolLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using SchoolLibrary.BusinessModels.Models;

    public class UnconfirmedUsersForWidgetsModel
    {
        public UnconfirmedUsersForWidgetsModel(UsersConfirmationModel userConfirmationModel)
        {
            UserProfileBusinessModel user = userConfirmationModel.UnconfirmedUser;
            ReaderBusinessModel reader = userConfirmationModel.CoincidedReaderBusiness;
            this.UserName = user != null ? user.UserName : "User does not exist";
            this.ReaderName = reader != null
                ? reader.FirstName + " " + reader.LastName
                : "Reader with such e-mail does not exist";
            this.RoleName = "Unregistered";
        }

        public string UserName { get; set; }

        public string ReaderName { get; set; }

        public string RoleName { get; set; }
    }
}