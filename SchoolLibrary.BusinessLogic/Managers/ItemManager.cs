using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.BusinessLogic.Managers.Interfaces;
using SchoolLibrary.BusinessModels.Models;
using SchoolLibrary.DataAccess.Facades.Interfaces;

namespace SchoolLibrary.BusinessLogic.Managers
{
    public class ItemManager : IItemManager, IDisposable
    {
        private IItemFacade itemFacade;

        private IDiskFacade diskFacade;

        private IMagazineFacade magazineFacade;

        private IBookFacade bookFacade;

        public ItemManager(IItemFacade itemFacade, IDiskFacade diskFacade, IMagazineFacade magazineFacade, IBookFacade bookFacade)
        {
            this.itemFacade = itemFacade;
            this.diskFacade = diskFacade;
            this.magazineFacade = magazineFacade;
            this.bookFacade = bookFacade;
        }

        public List<ItemBusinessModel> GetAllItems()
        {
            return itemFacade.GetAllItems();
        }

        public List<ItemsForGrid> GetItems(int skip, int take)
        {
            return CreateListOfAllItems().Skip(skip).Take(take).ToList();
        }

        public List<ItemsForGrid> CreateListOfAllItems()
        {
            List<ItemBusinessModel> itemsList = itemFacade.GetAllItems();
            List<ItemsForGrid> list = new List<ItemsForGrid>();
            foreach (var item in itemsList)
            {
                int inventoryCount = item.Inventories.Count(inv => inv.IsAvailable) - item.ReservedItems.Count();
                ItemsForGrid itemsForGrid = new ItemsForGrid()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Year = item.Year,
                    InventoriesCount = inventoryCount < 0 ? 0 : inventoryCount
                };
                if (item is BookBusinessModel)
                {
                    itemsForGrid.Publisher = (item as BookBusinessModel).Publisher;
                    itemsForGrid.Type = "Book";
                }
                else if (item is MagazineBusinessModel)
                {
                    itemsForGrid.Publisher = (item as MagazineBusinessModel).Publisher;
                    itemsForGrid.Type = "Magazine";
                }
                else
                {
                    itemsForGrid.Publisher = (item as DiskBusinessModel).Producer;
                    itemsForGrid.Type = "Disk";
                }
                list.Add(itemsForGrid);
            }
            return list;

        }

        public ItemBusinessModel GetItemById(int id)
        {
            return itemFacade.GetItemById(id);
        }

        public int GetCountOfAllItems()
        {
            return itemFacade.GetAllItems().Count();
        }

        public void DeleteItem(int id)
        {
            itemFacade.DeleteItem(id);
        }

        public void Dispose()
        {
            if (this.itemFacade as IDisposable != null)
            {
                (this.itemFacade as IDisposable).Dispose();
            }
        }
    }
}
