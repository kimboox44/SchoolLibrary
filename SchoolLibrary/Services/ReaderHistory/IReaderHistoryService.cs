using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SchoolLibrary.BusinessModels.Models;

namespace SchoolLibrary.Services.ReaderHistory
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReaderHistoryService" in both code and config file together.
    [ServiceContract]
    public interface IReaderHistoryService
    {
        [OperationContract]
        IEnumerable<HistoryBusinessModelForGrid> GetStudentsBooksToReturn(int readerId, int days);

        [OperationContract]
        ReaderBusinessModel Login(string login, string password);

    }
}
