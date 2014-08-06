using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SchoolLibrary.BusinessModels.Models;
using WebMatrix.WebData;


namespace SchoolLibrary.Services.UnregisteredUserManagment
{     
    [ServiceContract]
    public interface IUnregisteredUserService
    {
        [OperationContract]
        IList<UserProfileBusinessModel> GetAllUsers();

        [OperationContract]
        SubmitResult Submit(int userId, string role);

        [OperationContract]
        IList<string> GetAllRoles();
    }

    [DataContract]
    [Serializable()]
    public class SubmitResult
    {
        [DataMember]
        public bool Success { set; get; }
        [DataMember]
        public string ErrorMessage { set; get; }
    }
}
