using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;

    public interface IReaderManager
    {
        ReaderBusinessModel GetReaderById(int id);

        ReaderBusinessModel GetReaderByUserId(int id);

        void UpdateReader(ReaderBusinessModel reader);

        void CreateReader(ReaderBusinessModel reader);

        List<ReaderBusinessModel> CreateReaders(IEnumerable<ReaderBusinessModel> readers);

        ReaderBusinessModel GetReaderByEmail(string email);

        List<ReaderBusinessModel> GetAllReaders();

        void RemoveReaderById(int id);

        void UnbindReaderAndUser(int userId);

        List<ReaderBusinessModel> GetReadersFromRange(int skip, int take);

        ReaderBusinessModel GetReaderByFullName(string fullName);

        ReadersGridModel GetReadersForGrid(IEnumerable<KeyValuePair<string,string>> query,
            int pageSize, int pageNum, string sortdatafield = "", string sortorder = "");
    }
}