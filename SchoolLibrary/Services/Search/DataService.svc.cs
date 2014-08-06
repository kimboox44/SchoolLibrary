namespace SchoolLibrary.WcfServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SchoolLibrary.BusinessLogic.Managers;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using StructureMap;

    public class DataService : ISearchService
    {
        private readonly ISearchItemManager _searchItemManager;

        public DataService()
        {
            _searchItemManager = ObjectFactory.GetInstance<ISearchItemManager>();
        }

        public DataService(ISearchItemManager searchItemManager)
        {
            _searchItemManager = searchItemManager;
        }

        public List<ItemBusinessModel> SearchItems(string searchString)
        {
            var itemModels = new List<ItemBusinessModel>();

            if (string.IsNullOrWhiteSpace(searchString))
            {
                itemModels = (this._searchItemManager.GetAllItems()).ToList();
            }
            else
            {
                itemModels = this._searchItemManager.GetItemByName(searchString).ToList();
            }

            return itemModels;
        }
    }
}
