namespace SchoolLibrary.Controllers
{
    using System.Web.Mvc;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.Configuration;

    using WebMatrix.WebData;

    /// <summary>
    /// Controller for displaying reserved items of reader
    /// </summary>
    public class ReservedItemController : Controller
    {
        private IReservedItemManager reservedItemManager;

        private IReaderManager readerManager;

        public ReservedItemController(IReservedItemManager reservedItemManager, IReaderManager readerManager)
        {
            this.reservedItemManager = reservedItemManager;

            this.readerManager = readerManager;
        }

        #region ActionResults

        [Authorize(Roles = "Registered")]
        public ActionResult ViewReservedItems()
        {
            var reader = this.readerManager.GetReaderByUserId(WebSecurity.CurrentUserId);

            if (reader != null)
            {
                var reservedBooks = this.reservedItemManager.GetReservedItemsByReaderId(reader.ReaderId);
             
                return this.View(reservedBooks);
            }

            return this.View();
        }

        [Authorize(Roles = "Registered")]
        public ActionResult ViewReservedItemsWidgets()
        {
            return this.View();
        }

        [Authorize(Roles = "Registered")]
        public ActionResult ViewReservedItemsKnockout()
        {
            return this.View();
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult ViewReservedItemsForLibrarian()
        {
            var config = new Config().Get();

            var countDays = config.Reserervation.DeleteAfterDays;

            this.reservedItemManager.CheckIfTimeFinished(countDays);

            return this.View();
        }
        #endregion
    }
}
