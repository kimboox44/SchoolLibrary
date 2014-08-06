namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessModels.Models;

    public interface IItemFacade
    {
        List<ItemBusinessModel> GetAllItems();

        ItemBusinessModel GetItemById(int id);

        void DeleteItem(int id);
    }
}
