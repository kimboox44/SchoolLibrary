namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessModels.Models;

    public interface ISearchItemManager
    {
        List<ItemBusinessModel> GetAllItems();

        ItemBusinessModel GetItemById(int id);

        List<ItemBusinessModel> GetItemByName(string searchString);

        List<ItemBusinessModel> GetItemByTag(string tagName);

        List<ItemBusinessModel> GetItemByTagAndName(string searchString, string tagName);

        List<TagBusinessModel> GetAllTags();

        List<string> ConvertTagListToStringList(List<TagBusinessModel> list);
    }
}
