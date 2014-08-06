namespace SchoolLibrary.DataAccess.Facades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class ScannedPageFacade : IScannedPageFacade, IDisposable
    {
        private ILibraryUow uow;
        private ScannedPageMapper _scannedPageMapper;

        public ScannedPageFacade(ILibraryUow uow, ScannedPageMapper _scannedPageMapper)
        {
            this.uow = uow;
            this._scannedPageMapper = new ScannedPageMapper();
        }

        public ScannedPageFacade(ILibraryUow uow)
        {
            this.uow = uow;
            _scannedPageMapper = new ScannedPageMapper();
        }

        public string ScanPages(int readerId, int itemId, ScannedPageBusinessModel scannedPage)
        {
            //const int LimitReservedBooks = 3;

            var reader = this.uow.Readers.GetById(readerId);

            var item = this.uow.Items.GetById(itemId);

#region test 
            //var book = bookId;

            //var allReservedBooksCount = this.uow.ReservedBooks.GetAll().Count(r => r.Reader.ReaderId == readerId);

            //if (allReservedBooksCount >= LimitReservedBooks)
            //{
            //    return string.Format("Sorry, you can reserve no more than {0} books", LimitReservedBooks);
            //}

            //var currentBook =
            //    this.uow.ReservedBooks.GetAll()
            //        .Where(r => (r.Reader.ReaderId == readerId) && (r.Book.Id == bookId))
            //        .ToList();

            //if (currentBook.Count != 0)
            //{
            //    return "This book is already reserved by you";
            //}
#endregion 

            this.uow.ScannedPages.Add(new ScannedPage
            {
                Reader = reader,
                Item = item,
                IsLocked = false,
                IsReady = false,
                OrderDate = DateTime.Now,
                OrderText = scannedPage.OrderText
            });

            this.uow.Commit();

            return "Successfully reserved";
        }

        public void CreateScanPages(ScannedPageBusinessModel scannedPage)
        {
            var reader = this.uow.Readers.GetById(scannedPage.Reader.ReaderId);
            var item = this.uow.Items.GetById(scannedPage.Item.Id);

            this.uow.ScannedPages.Add(new ScannedPage
            {
                Reader = reader,
                Item = item,
                IsLocked = false,
                IsReady = false,
                OrderDate = DateTime.Now,
                OrderText = scannedPage.OrderText
            });

            this.uow.Commit();
        }

        /// <summary>
        /// Gets All ScannedPages
        /// </summary>
        /// <returns>The ScannedPageBusinessModel list</returns>
        public List<ScannedPageBusinessModel> GetAllScannedPages()
        {
            var scannedPage = this.uow.ScannedPages.GetAll().ToList();

            var scannedPageBusiness = scannedPage.Select(_scannedPageMapper.Map).ToList();

            return scannedPageBusiness.Count == 0 ? null : scannedPageBusiness;
        }

        /// <summary>
        /// Gets All ScannedPages
        /// </summary>
        /// <returns>The ScannedPageBusinessModel list</returns>
        public List<ScannedPageBusinessModel> GetScannedPageByReaderId(int readerId)
        {
            var scannedPages = this.uow.ScannedPages.GetAll().Where(r => r.Reader.ReaderId == readerId)
                .Select(r => r).ToList();

            var scannedPagesBusiness = scannedPages.Select(_scannedPageMapper.Map).ToList();

            return scannedPagesBusiness.Count == 0 ? null : scannedPagesBusiness;
        }

        /// <summary>
        /// Gets ScannedPages by Id
        /// </summary>
        /// <returns>The ScannedPageBusinessModel list</returns>
        public ScannedPageBusinessModel GetScannedPageById(int Id)
        {
            var scannedPages = this.uow.ScannedPages.GetById(Id);

            return _scannedPageMapper.Map(scannedPages);
        }

        /// <summary>
        /// Delete select ScannedPages
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteScannedPageById(int scannedPageId)
        {
            this.uow.ScannedPages.Delete(scannedPageId);

            this.uow.Commit();

            //check if Scanned Page was really deleted, if not - false is returned
            return this.uow.ScannedPages.GetById(scannedPageId) == null;
        }

        public void UpdateScannedPage(ScannedPageBusinessModel scannedPage)
        {
            ScannedPage editScannedPage = uow.ScannedPages.GetById(scannedPage.Id);
            editScannedPage.Item.Id = scannedPage.Item.Id;
            editScannedPage.Reader.ReaderId = scannedPage.Reader.ReaderId;
            editScannedPage.OrderText = scannedPage.OrderText;
            editScannedPage.IsLocked = scannedPage.IsLocked;
            editScannedPage.IsReady = scannedPage.IsReady;
            if (scannedPage.IsReady)
            {
                editScannedPage.ExecutionDate = DateTime.Now;
            }
            editScannedPage.Message = scannedPage.Message;
            uow.ScannedPages.Update(editScannedPage);
            uow.Commit();
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }
        }
    }
}
