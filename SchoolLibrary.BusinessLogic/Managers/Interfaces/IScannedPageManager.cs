namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using System;
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface IScannedPageManager
    {
        string ScanPages(int readerId, int bookId, ScannedPageBusinessModel scannedPage);
        void CreateScanPages(ScannedPageBusinessModel scannedPage);
        List<ScannedPageBusinessModel> GetAllScannedPages();
        List<ScannedPageBusinessModel> GetAllScannedPagesForSort(int skip, int take, out int filteredCount,
            List<Func<ScannedPageBusinessModel, bool>> filters = null, string sortdatafield = "", string sortorder = "");
        List<ScannedPageBusinessModel> GetReaderScannedPagesForSort(int skip, int take, int readerId, out int filteredCount,
            List<Func<ScannedPageBusinessModel, bool>> filters = null, string sortdatafield = "", string sortorder = "");

        List<ScannedPageBusinessModel> GetScannedPageByReaderId(int readerId);
        ScannedPageBusinessModel GetScannedPageById(int Id);
        bool DeleteScannedPageById(int scannedPageId);
        void UpdateScannedPage(ScannedPageBusinessModel scannedPage);

    }
}
