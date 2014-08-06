namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface IScannedPageFacade
    {
        string ScanPages(int readerId, int bookId, ScannedPageBusinessModel scannedPage);
        void CreateScanPages(ScannedPageBusinessModel scannedPage);
        List<ScannedPageBusinessModel> GetAllScannedPages();
        List<ScannedPageBusinessModel> GetScannedPageByReaderId(int readerId);
        ScannedPageBusinessModel GetScannedPageById(int Id);
        bool DeleteScannedPageById(int scannedPageId);
        void UpdateScannedPage(ScannedPageBusinessModel scannedPage);
    }
}


