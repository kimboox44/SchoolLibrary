using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.BusinessModels.MVCModels;

    public interface IReaderHistoryManager
    {
        IEnumerable<ReaderHistoryBusinessModel> GetReaderHistoriesByReaderId(int? readerId);

        void AddNewReaderHistory(ReaderHistoryBusinessModel readerHistoryBusiness, int readerId);

        ReaderHistoryBusinessModel GetReaderHistoryById(int readerHistoryId);

        void UpdateReaderHistory(ReaderHistoryBusinessModel readerHistoryBusiness);

        List<ReaderHistoryBusinessModel> GetReaderHistoryByInventoryNumber(string searchString, int? readerId);

        IEnumerable<HistoryBusinessModelForGrid> GetStudentsBooksToReturn(int readerId, int days);

        List<DeptorsReadersModel> GetDebtorsReaders(int? minDays, int? maxDays);

        EmailMassageModel GetMassageModelByReaderId(int readerId);

        void SendEmailToUser(EmailMassageModel emailMassageModel);

        List<DeptorsReadersModel> GetDebtorsReadersByDate(int? minDays, int? maxDays,int skip, int take, out int filteredCount,
    List<Func<DeptorsReadersModel, bool>> filters = null);
    }
}
