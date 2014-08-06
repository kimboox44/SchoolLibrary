using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SchoolLibrary.BusinessLogic.Managers;
using SchoolLibrary.BusinessLogic.Managers.Interfaces;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.DataAccess.Facades;
using SchoolLibrary.DataAccess.UnitOfWork;
using WebMatrix.WebData;

namespace SchoolLibrary.Services.ReaderHistory
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ReaderHistoryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ReaderHistoryService.svc or ReaderHistoryService.svc.cs at the Solution Explorer and start debugging.
    public class ReaderHistoryService : IReaderHistoryService
    {

        private LibraryUow uow;

        private ReaderHistoryManager readerHistoryManager;

        private ReaderManager readerManager;

        private UserManager userManager;

        public ReaderHistoryService()
        {
            uow = new LibraryUow();
            readerHistoryManager = new ReaderHistoryManager(uow, new ReaderHistoryFacade(uow));
            readerManager = new ReaderManager(uow, new ReaderFacade(uow), new UsersFacade(uow));
            userManager = new UserManager(uow, new UsersFacade(uow), readerManager);
        }      

        public IEnumerable<HistoryBusinessModelForGrid> GetStudentsBooksToReturn(int readerId, int days)
        {

            return readerHistoryManager.GetStudentsBooksToReturn(readerId, days);
        }

        public ReaderBusinessModel Login(string login, string password)
        {
            ReaderBusinessModel reader = null;
            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName",
            //           autoCreateTables: true);

            if (WebSecurity.Login(login, password))
            {
                reader = this.readerManager.GetReaderByUserId(userManager.GetUserByUserName(login).UserId);
            }
            return reader;
        }


    }
}
