namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessModels.Models;

    public interface ISearchItemFacade
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