namespace SchoolLibrary.BusinessLogic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Facades.Interfaces;

    public class SearchItemManager : ISearchItemManager, IDisposable
    {
        private IItemFacade itemFacade;

        private IDiskFacade diskFacade;

        private IMagazineFacade magazineFacade;

        private IBookFacade bookFacade;

        private ISearchItemFacade searchItemFacade;

        public SearchItemManager(IItemFacade itemFacade, IDiskFacade diskFacade, IMagazineFacade magazineFacade,
                                            IBookFacade bookFacade, ISearchItemFacade searchItemFacade)
        {
            this.itemFacade = itemFacade;
            this.diskFacade = diskFacade;
            this.magazineFacade = magazineFacade;
            this.bookFacade = bookFacade;
            this.searchItemFacade = searchItemFacade;
        }

        public List<ItemBusinessModel> GetAllItems()
        {
            return searchItemFacade.GetAllItems();
        }

        public ItemBusinessModel GetItemById(int id)
        {
            return searchItemFacade.GetItemById(id);
        }

        public List<ItemBusinessModel> GetItemByName(string searchString)
        {
            return searchItemFacade.GetItemByName(searchString);
        }

        public List<ItemBusinessModel> GetItemByTag(string tagName)
        {
            return searchItemFacade.GetItemByTag(tagName);
        }

        public List<ItemBusinessModel> GetItemByTagAndName(string searchString, string tagName)
        {
            return searchItemFacade.GetItemByTagAndName(searchString, tagName);
        }

        public List<TagBusinessModel> GetAllTags()
        {
            return searchItemFacade.GetAllTags();
        }

        public List<string> ConvertTagListToStringList(List<TagBusinessModel> list)
        {
            return searchItemFacade.ConvertTagListToStringList(list);
        }

        public void Dispose()
        {
            if (this.searchItemFacade as IDisposable != null)
            {
                (this.searchItemFacade as IDisposable).Dispose();
            }
        }
    }
}
