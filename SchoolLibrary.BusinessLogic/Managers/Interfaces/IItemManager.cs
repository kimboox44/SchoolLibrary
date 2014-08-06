using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.BusinessModels.Models;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    public interface IItemManager
    {
        List<ItemBusinessModel> GetAllItems();

        ItemBusinessModel GetItemById(int id);

        List<ItemsForGrid> GetItems(int skip, int take);

        List<ItemsForGrid> CreateListOfAllItems();

        int GetCountOfAllItems();

        void DeleteItem(int id);
    }
}
