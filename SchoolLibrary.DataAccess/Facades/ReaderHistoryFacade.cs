using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades
{
    using System.ComponentModel;
    using System.Net.Mail;
    //using System.Net.Mail;
    using System.Web.Security;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using SchoolLibrary.ServiceAgents;
    using WebMatrix.WebData;

    public class ReaderHistoryFacade : IReaderHistoryFacade, IDisposable
    {
        private ILibraryUow uow;

        private Inventory inventory;

        static bool mailSent = false;

        public ReaderHistoryFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }

        public IEnumerable<ReaderHistoryBusinessModel> GetReaderHistoriesByReaderId(int? readerId)
        {
            List<ReaderHistoryBusinessModel> readerHistoryBusiness = new List<ReaderHistoryBusinessModel>();

            ReaderHistoryMapper readerHistoryMapper = new ReaderHistoryMapper();
            List<ReaderHistory> readerHistory = this.uow.ReadersHistories.GetAll().Where(r => r.Reader.ReaderId == readerId)
                .Select(r => r).ToList();
            for (int i = 0; i < readerHistory.Count; i++)
            {
                readerHistoryBusiness.Add(readerHistoryMapper.Map(readerHistory[i]));
            }

            var readerHistoryBusinessSorted = readerHistoryBusiness.OrderByDescending(x => x.StartDate);

            /*if (readerHistoryBusinessSorted.Count() == 0)
            {
                readerHistoryBusinessSorted.First().ReaderBusiness.ReaderId = readerId;
            }*/

            return readerHistoryBusinessSorted;
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }
        }


        public void GiveBookToStudent(ReaderHistoryBusinessModel readerHistoryBusinessModel)
        {
            ReaderHistoryMapper readerHistoryMapper = new ReaderHistoryMapper();
            ReaderHistory readerHistory = new ReaderHistory();

            readerHistory = readerHistoryMapper.Map(readerHistoryBusinessModel);

            Inventory inventory = this.uow.Inventories.GetAll().Where(inv => inv.Number == readerHistory.Inventory.Number).Select(inv => inv).Single();

            if (inventory.IsAvailable == true)
            {
                inventory.IsAvailable = false;
            }
            else
            {
                throw new Exception();
            }

            this.uow.Inventories.Update(inventory);
            uow.Commit();

        }

        public void AddNewReaderHistory(ReaderHistoryBusinessModel readerHistoryBusiness, int readerId)
        {
            ReaderHistoryMapper readerHistoryMapper = new ReaderHistoryMapper();
            ReaderHistory readerHistory = new ReaderHistory();

            readerHistory = readerHistoryMapper.Map(readerHistoryBusiness);
            var reader = uow.Readers.GetById(readerId);
            Inventory inventory = this.uow.Inventories.GetAll().Where(inv => inv.Number == readerHistory.Inventory.Number).Select(inv => inv).Single();

            uow.ReadersHistories.Add(new ReaderHistory
            {
                StartDate = readerHistory.StartDate,
                FinishDate = readerHistory.FinishDate,
                Reader = reader,
                Inventory = this.uow.Inventories.GetById(inventory.InventoryId)

            });
            uow.Commit();
        }

        public ReaderHistoryBusinessModel GetReaderHistoryById(int readerHistoryId)
        {
            ReaderHistoryMapper mapper = new ReaderHistoryMapper();
            ReaderHistoryBusinessModel readerHistoryBusiness = new ReaderHistoryBusinessModel();
            readerHistoryBusiness = mapper.Map(this.uow.ReadersHistories.GetById(readerHistoryId));

            return readerHistoryBusiness;
        }

        public void UpdateReaderHistory(ReaderHistoryBusinessModel readerHistoryBusiness)
        {
            ReaderHistoryMapper mapper = new ReaderHistoryMapper();
            ReaderHistory readerHistory = new ReaderHistory();

            readerHistory = mapper.Map(readerHistoryBusiness);
            this.uow.ReadersHistories.Update(readerHistory);

            Inventory inventory = this.uow.Inventories.GetById(readerHistoryBusiness.InventoryBusiness.InventoryId);

            if (inventory.IsAvailable == false)
            {
                if (readerHistoryBusiness.ReturnDate != null)
                {
                    inventory.IsAvailable = true;
                }
                else
                {
                    inventory.IsAvailable = false;
                }
            }
            else
            {
                throw new Exception("This inventory already exist in the Library");
            }

            uow.Commit();
        }

        public List<ReaderHistoryBusinessModel> GetReaderHistoryByInventoryNumber(string searchString, int? readerId)
        {
            var readerHisroryModel = new List<ReaderHistoryBusinessModel>();
            List<ReaderHistory> readerHistories;

            using (LibraryUow uow = new LibraryUow())
            {
                var readerHistoryMapper = new ReaderHistoryMapper();

                if (readerId != null)
                {

                    readerHistories = this.uow.ReadersHistories.GetAll().Where(r => r.Reader.ReaderId == readerId && r.ReturnDate == null)
                      .Select(r => r).ToList();
                }
                else
                {
                    readerHistories = this.uow.ReadersHistories.GetAll().Where(r => r.ReturnDate == null).Select(r => r).ToList();
                }

                foreach (var readerHistory in readerHistories)
                {
                    if (readerHistory.Inventory.Number == searchString)
                    {
                        readerHisroryModel.Add(readerHistoryMapper.Map(readerHistory));

                    }

                }


            }
            return readerHisroryModel;
        }

        public IEnumerable<HistoryBusinessModelForGrid> GetStudentsBooksToReturn(int readerId, int days)
        {
            List<HistoryBusinessModelForGrid> readerHistoryBusiness = new List<HistoryBusinessModelForGrid>();

            HistoryMapperForGrid readerHistoryMapper = new HistoryMapperForGrid();

            DateTime data = new DateTime();
            data = DateTime.Now.AddDays(days);

            List<ReaderHistory> readerHistory = this.uow.ReadersHistories.GetAll().Where(r => r.Reader.ReaderId == readerId && r.FinishDate > DateTime.Now && r.FinishDate <= data && r.ReturnDate == null)
                .Select(r => r).ToList();
            for (int i = 0; i < readerHistory.Count; i++)
            {
                readerHistoryBusiness.Add(readerHistoryMapper.Map(readerHistory[i]));
            }

            if (readerHistoryBusiness.Count == 0)
            {
                return null;
            }

            var readerHistoryBusinessSorted = readerHistoryBusiness.OrderBy(x => x.FinishDate);

            return readerHistoryBusinessSorted;
        }


        public List<DeptorsReadersModel> GetDebtorsReaders(int? minDays, int? maxDays)
        {

            DeptorsReadersMapper deptorsReadersMapper = new DeptorsReadersMapper();
            List<DeptorsReadersModel> deptorsReadersBusiness = new List<DeptorsReadersModel>();

            List<ReaderHistory> readerHistory;

            if ((minDays != 0 && maxDays != 0) && (minDays != null && maxDays != null))
            {
                DateTime min = DateTime.Now.AddDays((double)-minDays);
                DateTime max = DateTime.Now.AddDays((double)-maxDays);

                readerHistory = this.uow.ReadersHistories.GetAll().Where(r => r.FinishDate < DateTime.Now && r.ReturnDate == null &&
                  (r.FinishDate > max && r.FinishDate < min))
                    .Select(r => r).ToList();
            }
            else
            {
                readerHistory = this.uow.ReadersHistories.GetAll().Where(r => r.FinishDate < DateTime.Now && r.ReturnDate == null)
                   .Select(r => r).ToList();
            }

            for (int i = 0; i < readerHistory.Count; i++)
            {
                deptorsReadersBusiness.Add(deptorsReadersMapper.Map(readerHistory[i]));

            }

            return deptorsReadersBusiness;

        }


        public EmailMassageModel GetMassageModelByReaderId(int readerId)
        {

            EmailMassageModel emailMassageModel = new EmailMassageModel();
            ReaderHistoryMapper historyMapper = new ReaderHistoryMapper();
            List<ReaderHistoryBusinessModel> readerHistoryBusiness = new List<ReaderHistoryBusinessModel>();

            List<ReaderHistory> readerHistory = this.uow.ReadersHistories.GetAll().Where(r => r.Reader.ReaderId == readerId && r.FinishDate < DateTime.Now && r.ReturnDate == null)
                .Select(r => r).ToList();

            for (int i = 0; i < readerHistory.Count; i++)
            {
                readerHistoryBusiness.Add(historyMapper.Map(readerHistory[i]));
            }

            string books = null;
            foreach (var book in readerHistoryBusiness)
            {
                books += book.InventoryBusiness.Item.Name + ", ";
            }

            emailMassageModel.FirsName = readerHistoryBusiness.First().ReaderBusiness.FirstName;
            emailMassageModel.LastName = readerHistoryBusiness.First().ReaderBusiness.LastName;
            emailMassageModel.Email = readerHistoryBusiness.First().ReaderBusiness.EMail;
            emailMassageModel.Subject = "Owing Book";
            emailMassageModel.Message = "Dear student: " + emailMassageModel.FirsName + " " + emailMassageModel.LastName + ": " +
                "you owe the books: " + books + "Please back them to the library";

            return emailMassageModel;
        }

        public void SendEmailToUser(EmailMassageModel emailMassageModel)
        {
            MailSender mailSender = new MailSender();
            mailSender.Send(emailMassageModel.Email, emailMassageModel.Subject, emailMassageModel.Message);


        }

    }
}
