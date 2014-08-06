namespace SchoolLibrary.DataAccess.Facades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class SearchItemFacade : ISearchItemFacade, IDisposable
    {
        private ILibraryUow uow;

        public SearchItemFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public SearchItemFacade() { }

        /// <summary>
        /// Method GetAllItems()
        /// </summary>
        /// <returns>The ItemBusinessModel list</returns>
        public List<ItemBusinessModel> GetAllItems()
        {
            List<ItemBusinessModel> listItemBusinessModels = new List<ItemBusinessModel>();
            List<Item> list = uow.Items.GetAll().ToList();
            foreach (var item in list)
            {
                if (item is Book)
                    listItemBusinessModels.Add(new BookMapper().Map(item as Book));
                else if (item is Magazine)
                {
                    listItemBusinessModels.Add(new MagazineMapper().Map(item as Magazine));
                }
                else
                {
                    listItemBusinessModels.Add(new DiskMapper().Map(item as Disk));
                }
            }
            return listItemBusinessModels;
        }

        /// <summary>
        /// Method GetAllTags()
        /// </summary>
        /// <returns>The TagBusinessModel list</returns>
        public List<TagBusinessModel> GetAllTags()
        {
            List<TagBusinessModel> tagsBusinessModels = new List<TagBusinessModel>();
            List<Tag> tags = uow.Tags.GetAll().OrderBy(x => x.Name).ToList();

            TagMapper mapper = new TagMapper();
            foreach (var tag in tags)
            {
                tagsBusinessModels.Add(mapper.Map(tag));
            }

            return tagsBusinessModels;
        }

        /// <summary>
        /// Method ConvertTagListToStringList(List<TagBusinessModel> list)
        /// </summary>
        /// <returns>The string list</returns>
        public List<string> ConvertTagListToStringList(List<TagBusinessModel> list)
        {
            return list.Select(f => f.name).ToList();
        }

        /// <summary>
        /// Method GetItemById(int id)
        /// </summary>
        /// <returns>The ItemBusinessModel</returns>
        public ItemBusinessModel GetItemById(int id)
        {
            Item item = uow.Items.GetById(id);
            if (item is Book)
                return new BookMapper().Map(item as Book);
            else if (item is Magazine)
                return new MagazineMapper().Map(item as Magazine);
            else
            {
                return new DiskMapper().Map(item as Disk);
            }
        }

        /// <summary>
        /// Method GetItemByName(string searchString)
        /// </summary>
        /// <returns>The ItemBusinessModel list</returns>
        public List<ItemBusinessModel> GetItemByName(string searchString)
        {
            var itemModels = new List<ItemBusinessModel>();

            string[] words = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                words = searchString.Trim().Split(' ');
            }
            
            var itemInfoMapper = new ItemMapper();
            var items = uow.Items.GetAll().OfType<Item>().ToList();

            // Search by ItemName
            foreach (var item in items)
            {
                foreach (string word in words)
                {
                    if (item.Name.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(item.Name.ToLower()))
                    {
                        var itemToMap = itemInfoMapper.Map(item);

                        // Distinct
                        if (!itemModels.Any(b => b.Id == itemToMap.Id))
                        {
                            itemModels.Add(itemToMap);
                        }
                    }
                }
            }

            // Search by Book Authors FirstName or LastName
            foreach (var item in items)
            {
                if (item is Book)
                {
                    var authors = (item as Book).Authors;

                    foreach (var author in authors)
                    {
                        foreach (string word in words)
                        {
                            if (author.FirstName.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(author.FirstName.ToLower()) ||
                            author.LastName.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(author.LastName.ToLower()))
                            {
                                var bookToMap = itemInfoMapper.Map(item);

                                if (!itemModels.Any(b => b.Id == bookToMap.Id))
                                {
                                    itemModels.Add(bookToMap);
                                }
                            }
                        }
                    }
                }

                // Search by Disk Producer
                if (item is Disk)
                {
                    foreach (string word in words)
                    {
                        if ((item as Disk).Producer.ToLower().Contains(word.ToLower()) || word.ToLower().Contains((item as Disk).Producer.ToLower()))
                        {
                            var itemToMap = itemInfoMapper.Map(item);

                            // Distinct
                            if (!itemModels.Any(b => b.Id == itemToMap.Id))
                            {
                                itemModels.Add(itemToMap);
                            }
                        }
                    }
                }

                // Search by Magazine Publisher
                if (item is Magazine)
                {
                    foreach (string word in words)
                    {
                        if ((item as Magazine).Publisher.ToLower().Contains(word.ToLower()) || word.ToLower().Contains((item as Magazine).Publisher.ToLower()))
                        {
                            var itemToMap = itemInfoMapper.Map(item);

                            // Distinct
                            if (!itemModels.Any(b => b.Id == itemToMap.Id))
                            {
                                itemModels.Add(itemToMap);
                            }
                        }
                    }
                }
            }
            
            return itemModels;
        }

        /// <summary>
        /// Method GetItemByTag(string tagName)
        /// </summary>
        /// <returns>The ItemBusinessModel list</returns>
        public List<ItemBusinessModel> GetItemByTag(string tagName)
        {
            var itemModels = new List<ItemBusinessModel>();

            var itemInfoMapper = new ItemMapper();
            var items = uow.Items.GetAll().OfType<Item>().ToList();
            foreach (var item in items)
            {
                // Get Items by TagName
                var tags = (item as Item).Tags;

                foreach (var tag in tags)
                {
                    if (tag.Name.ToLower() == tagName.ToLower())
                    {
                        var itemToMap = itemInfoMapper.Map(item);

                        if (!itemModels.Any(b => b.Id == itemToMap.Id))
                        {
                            itemModels.Add(itemToMap);
                        }
                    }
                }
            }
            
            return itemModels;
        }

        /// <summary>
        /// Method GetItemByTagAndName(string searchString, string tagName)
        /// </summary>
        /// <returns>The ItemBusinessModel list</returns>
        public List<ItemBusinessModel> GetItemByTagAndName(string searchString, string tagName)
        {
            var itemModels = new List<ItemBusinessModel>();

            string[] words = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                words = searchString.Trim().Split(' ');
            }

            
            var itemInfoMapper = new ItemMapper();
            var items = uow.Items.GetAll().OfType<Item>().ToList();

            // Search by ItemName
            foreach (var item in items)
            {
                var tags = (item as Item).Tags;
                foreach (var tag in tags)
                {
                    foreach (string word in words)
                    {
                        if ((item.Name.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(item.Name.ToLower()))
                            && (tag.Name.ToLower() == tagName.ToLower()))
                        {
                            var itemToMap = itemInfoMapper.Map(item);

                            // Distinct
                            if (!itemModels.Any(b => b.Id == itemToMap.Id))
                            {
                                itemModels.Add(itemToMap);
                            }
                        }
                    }
                }
            }

            // Search by Book Authors FirstName or LastName
            foreach (var item in items)
            {
                var tags = (item as Item).Tags;
                foreach (var tag in tags)
                {
                    if (item is Book)
                    {
                        var authors = (item as Book).Authors;

                        foreach (var author in authors)
                        {
                            foreach (string word in words)
                            {
                                if ((author.FirstName.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(author.FirstName.ToLower()) ||
                                author.LastName.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(author.LastName.ToLower()))
                                && (tag.Name.ToLower() == tagName.ToLower()))
                                {
                                    var bookToMap = itemInfoMapper.Map(item);

                                    if (!itemModels.Any(b => b.Id == bookToMap.Id))
                                    {
                                        itemModels.Add(bookToMap);
                                    }
                                }
                            }
                        }
                    }

                    // Search by Disk Producer
                    if (item is Disk)
                    {
                        foreach (string word in words)
                        {
                            if (((item as Disk).Producer.ToLower().Contains(word.ToLower()) || word.ToLower().Contains((item as Disk).Producer.ToLower()))
                                && (tag.Name.ToLower() == tagName.ToLower()))
                            {
                                var itemToMap = itemInfoMapper.Map(item);

                                // Distinct
                                if (!itemModels.Any(b => b.Id == itemToMap.Id))
                                {
                                    itemModels.Add(itemToMap);
                                }
                            }
                        }
                    }

                    // Search by Magazine Publisher
                    if (item is Magazine)
                    {
                        foreach (string word in words)
                        {
                            if (((item as Magazine).Publisher.ToLower().Contains(word.ToLower()) || word.ToLower().Contains((item as Magazine).Publisher.ToLower()))
                                && (tag.Name.ToLower() == tagName.ToLower()))
                            {
                                var itemToMap = itemInfoMapper.Map(item);

                                // Distinct
                                if (!itemModels.Any(b => b.Id == itemToMap.Id))
                                {
                                    itemModels.Add(itemToMap);
                                }
                            }
                        }
                    }
                }
            }
            return itemModels;
        }

        public void Dispose()
        {
            if (this.uow as IDisposable != null)
            {
                (this.uow as IDisposable).Dispose();
            }
        }
    }
}
