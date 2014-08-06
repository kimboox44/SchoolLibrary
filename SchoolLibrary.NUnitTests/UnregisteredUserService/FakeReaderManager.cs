using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.BusinessLogic.Managers.Interfaces;
using SchoolLibrary.BusinessModels.Models;

namespace SchoolLibrary.NUnitTests.UnregisteredUserService
{
    class FakeReaderManager:IReaderManager
    {
        public ReaderBusinessModel GetReaderById(int id)
        {
            throw new NotImplementedException();
        }

        public ReaderBusinessModel GetReaderByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateReader(ReaderBusinessModel reader)
        {
            throw new NotImplementedException();
        }

        public void CreateReader(ReaderBusinessModel reader)
        {
            throw new NotImplementedException();
        }

        public List<ReaderBusinessModel> CreateReaders(IEnumerable<ReaderBusinessModel> readers)
        {
            throw new NotImplementedException();
        }

        public ReaderBusinessModel GetReaderByEmail(string email)
        {
            return new ReaderBusinessModel();
        }

        public List<ReaderBusinessModel> GetAllReaders()
        {
            throw new NotImplementedException();
        }

        public void RemoveReaderById(int id)
        {
            throw new NotImplementedException();
        }

        public void UnbindReaderAndUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<ReaderBusinessModel> GetReadersFromRange(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public ReaderBusinessModel GetReaderByFullName(string fullName)
        {
            throw new NotImplementedException();
        }

        public ReadersGridModel GetReadersForGrid(IEnumerable<KeyValuePair<string, string>> query, int pageSize, int pageNum, string sortdatafield = "",
            string sortorder = "")
        {
            throw new NotImplementedException();
        }
    }
}
