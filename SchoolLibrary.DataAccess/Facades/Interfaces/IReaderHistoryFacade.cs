using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.BusinessModels.MVCModels;

    public interface IReaderHistoryFacade
    {
        IEnumerable<ReaderHistoryBusinessModel> GetReaderHistoriesByReaderId(int? readerId);

        void GiveBookToStudent(ReaderHistoryBusinessModel readerHistoryBusiness);

        void AddNewReaderHistory(ReaderHistoryBusinessModel readerHistoryBusiness, int readerId);

        ReaderHistoryBusinessModel GetReaderHistoryById(int readerHistoryId);

        void UpdateReaderHistory(ReaderHistoryBusinessModel readerHistoryBusiness);

        List<ReaderHistoryBusinessModel> GetReaderHistoryByInventoryNumber(string searchString, int? readerId);

        IEnumerable<HistoryBusinessModelForGrid> GetStudentsBooksToReturn(int readerId, int days);

        List<DeptorsReadersModel> GetDebtorsReaders(int? minDays, int? maxDays);

        EmailMassageModel GetMassageModelByReaderId(int readerId);

        void SendEmailToUser(EmailMassageModel emailMassageModel);
    }
}
