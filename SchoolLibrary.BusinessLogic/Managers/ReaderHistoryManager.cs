using System;
using System.Collections.Generic;
using System.Linq;


namespace SchoolLibrary.BusinessLogic.Managers
{
    using System.Transactions;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class ReaderHistoryManager : IReaderHistoryManager, IDisposable
    {
        private ILibraryUow uow;

        private IReaderHistoryFacade readerHistoryFacade;

        public ReaderHistoryManager(ILibraryUow uow, IReaderHistoryFacade readerHistoryFacade)
        {
            this.uow = uow;
            this.readerHistoryFacade = readerHistoryFacade;
        }

        public IEnumerable<ReaderHistoryBusinessModel> GetReaderHistoriesByReaderId(int? readerId)
        {
            return this.readerHistoryFacade.GetReaderHistoriesByReaderId(readerId);
        }

        public static void RemoveReaderHistoryById(int id)
        {
            using (var uow = new LibraryUow())
            {
                uow.ReadersHistories.Delete(id);
                uow.Commit();
            }
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }

            if (this.readerHistoryFacade as IDisposable != null)
            {
                (this.readerHistoryFacade as IDisposable).Dispose();
            }
        }


        public void AddNewReaderHistory(ReaderHistoryBusinessModel readerHistoryBusiness, int readerId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                this.readerHistoryFacade.GiveBookToStudent(readerHistoryBusiness);
                this.readerHistoryFacade.AddNewReaderHistory(readerHistoryBusiness, readerId);

                scope.Complete();
            }
        }

        public ReaderHistoryBusinessModel GetReaderHistoryById(int readerHistoryId)
        {
            return this.readerHistoryFacade.GetReaderHistoryById(readerHistoryId);
        }

        public void UpdateReaderHistory(ReaderHistoryBusinessModel readerHistoryBusiness)
        {
            this.readerHistoryFacade.UpdateReaderHistory(readerHistoryBusiness);
           
        }

        public List<ReaderHistoryBusinessModel> GetReaderHistoryByInventoryNumber(string searchString, int? readerId)
        {
            return this.readerHistoryFacade.GetReaderHistoryByInventoryNumber(searchString, readerId);
        }


        public IEnumerable<HistoryBusinessModelForGrid> GetStudentsBooksToReturn(int readerId, int days)
        {
            return this.readerHistoryFacade.GetStudentsBooksToReturn(readerId,days);
        }

        public List<DeptorsReadersModel> GetDebtorsReaders(int? minDays, int? maxDays)
        {
            return this.readerHistoryFacade.GetDebtorsReaders(minDays,maxDays);
        }


        public EmailMassageModel GetMassageModelByReaderId(int readerId)
        {
            return this.readerHistoryFacade.GetMassageModelByReaderId(readerId);
        }

        public void SendEmailToUser(EmailMassageModel emailMassageModel)
        {
            this.readerHistoryFacade.SendEmailToUser(emailMassageModel);
        }

        public List<DeptorsReadersModel> GetDebtorsReadersByDate(int? minDays, int? maxDays, int skip, int take, out int filteredCount,
            List<Func<DeptorsReadersModel, bool>> filters = null)
        {
            var deptorsReaders = this.GetDebtorsReaders(minDays, maxDays).AsQueryable();

            if (filters != null)
            {
                foreach (var predicate in filters)
                {
                    deptorsReaders = deptorsReaders.Where(predicate).AsQueryable();
                }
            }

            filteredCount = deptorsReaders.Count();

            return deptorsReaders.Skip(skip).Take(take).ToList();
        }
    }
}
