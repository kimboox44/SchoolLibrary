using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SchoolLibrary.Services
{
    using SchoolLibrary.BusinessModels.Models;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReaderManagment" in both code and config file together.
    [ServiceContract]
    public interface IReaderManagment
    {
        [OperationContract]
        List<ReaderBusinessModel> GetAllReaders();

        [OperationContract]
        void UpdateReader(ReaderBusinessModel reader);
    }
}
