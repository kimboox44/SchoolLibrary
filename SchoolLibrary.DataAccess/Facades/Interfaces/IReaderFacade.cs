using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;

    public interface IReaderFacade
    {
        ReaderBusinessModel GetReaderById(int id);

        ReaderBusinessModel GetReaderByUserId(int id);

        void UpdateReader(ReaderBusinessModel reader);

        void CreateReader(ReaderBusinessModel reader);

        ReaderBusinessModel GetReaderByEmail(string email);

        List<ReaderBusinessModel> GetAllReaders();

        void RemoveReaderById(int id);

        ReaderBusinessModel GetReaderByFullName(string firstName,string lastName);
    }
}
