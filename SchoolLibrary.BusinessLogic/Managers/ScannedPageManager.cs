namespace SchoolLibrary.BusinessLogic.Managers
{
    using System;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.UnitOfWork;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades;

    public class ScannedPageManager : IScannedPageManager, IDisposable
    {
        private ILibraryUow uow;
        private IScannedPageFacade scannedPageFacade;

        public ScannedPageManager(ILibraryUow uow, IScannedPageFacade searchBookFacade)
        {
            this.uow = uow;
            this.scannedPageFacade = searchBookFacade;
        }

        public string ScanPages(int readerId, int bookId, ScannedPageBusinessModel scannedPage)
        {
            return this.scannedPageFacade.ScanPages(readerId, bookId, scannedPage);
        }

        public void CreateScanPages(ScannedPageBusinessModel scannedPage)
        {
            this.scannedPageFacade.CreateScanPages(scannedPage);
        }

        public List<ScannedPageBusinessModel> GetAllScannedPages()
        {
            return this.scannedPageFacade.GetAllScannedPages();
        }

        public List<ScannedPageBusinessModel> GetAllScannedPagesForSort(int skip, int take, out int filteredCount,
            List<Func<ScannedPageBusinessModel, bool>> filters = null, string sortdatafield = "", string sortorder = "")
        {
            var scannedPages = this.GetAllScannedPages().AsQueryable();

            if (filters != null)
            {
                foreach (var predicate in filters)
                {
                    scannedPages = scannedPages.Where(predicate).AsQueryable();
                }
            }

            if (sortorder != null && sortorder != string.Empty)
            {
                if (sortorder == "asc")
                {
                    scannedPages = scannedPages.OrderBy(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
                else
                {
                    scannedPages = scannedPages.OrderByDescending(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
            }

            filteredCount = scannedPages.Count();

            return scannedPages.Skip(skip).Take(take).ToList();
        }

        public List<ScannedPageBusinessModel> GetReaderScannedPagesForSort(int skip, int take, int readerId, out int filteredCount,
            List<Func<ScannedPageBusinessModel, bool>> filters = null, string sortdatafield = "", string sortorder = "")
        {
            var scannedPages = this.GetScannedPageByReaderId(readerId).AsQueryable();

            if (filters != null)
            {
                foreach (var predicate in filters)
                {
                    scannedPages = scannedPages.Where(predicate).AsQueryable();
                }
            }

            if (sortorder != null && sortorder != string.Empty)
            {
                if (sortorder == "asc")
                {
                    scannedPages = scannedPages.OrderBy(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
                else
                {
                    scannedPages = scannedPages.OrderByDescending(r => r.GetType().GetProperty(sortdatafield).GetValue(r, null));
                }
            }

            filteredCount = scannedPages.Count();

            return scannedPages.Skip(skip).Take(take).ToList();
        }



        public List<ScannedPageBusinessModel> GetScannedPageByReaderId(int readerId)
        {
            return this.scannedPageFacade.GetScannedPageByReaderId(readerId);
        }

        public ScannedPageBusinessModel GetScannedPageById(int Id)
        {
            return this.scannedPageFacade.GetScannedPageById(Id);
        }

        public bool DeleteScannedPageById(int scannedPageId)
        {
            return this.scannedPageFacade.DeleteScannedPageById(scannedPageId);
        }

        public void UpdateScannedPage(ScannedPageBusinessModel scannedPage)
        {
            this.scannedPageFacade.UpdateScannedPage(scannedPage);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
