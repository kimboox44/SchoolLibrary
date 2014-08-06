namespace SchoolLibrary.DataAccess.Mappers
{
    using System.Linq;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class BookWithoutTagsMapper: IMapper<Book, BookBusinessModel>
    {
        public Book Map(BookBusinessModel source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = new Book
            {
                Id = source.Id,
                Name = source.Name,
                Year = source.Year,
                PageCount = source.PageCount,
                Publisher = source.Publisher
            };

            if (source.Authors != null)
            {
                var authorMapper = new AuthorMapper();
                destination.Authors =
                    source.Authors.Select(
                        a => authorMapper.Map(new AuthorBusinessModel { Id = a.Id, FirstName = a.FirstName, MiddleName = a.MiddleName, LastName = a.LastName }))
                        .ToList();
            }

            if (source.Inventories != null)
            {
                var inventoryMapper = new InventoryMapper();
                destination.Inventories =
                    source.Inventories.Select(
                        a =>
                        inventoryMapper.Map(new InventoryBusinessModel { InventoryId = a.InventoryId, IsAvailable = a.IsAvailable }))
                        .ToList();
            }

            if (source.ReservedItems != null)
            {
                var reservedBookMapper = new ReservedItemMapper();
                destination.ReservedItems =
                    source.ReservedItems.Select(
                        a =>
                        reservedBookMapper.Map(new ReservedItemBusinessModel { Item = a.Item, Date = a.Date, Id = a.Id, Reader = a.Reader }))
                        .ToList();
            }

            destination.Tags = null;

            return destination;
        }

        public BookBusinessModel Map(Book source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = new BookBusinessModel
            {
                Id = source.Id,
                Name = source.Name,
                Year = source.Year,
                PageCount = source.PageCount,
                Publisher = source.Publisher,
            };


            if (source.Authors != null)
            {
                var authorMapper = new AuthorMapper();
                destination.Authors =
                source.Authors.Select(
                a => authorMapper.Map(new Author { Id = a.Id, FirstName = a.FirstName, MiddleName = a.MiddleName, LastName = a.LastName }))
                .ToList();
            }

            if (source.Inventories != null)
            {
                var inventoryMapper = new InventoryMapper();
                destination.Inventories =
                    source.Inventories.Select(
                        a =>
                        inventoryMapper.Map(new Inventory { InventoryId = a.InventoryId, IsAvailable = a.IsAvailable }))
                        .ToList();
            }

            if (source.ReservedItems != null)
            {
                var reservedBookMapper = new ReservedItemMapper();
                destination.ReservedItems =
                    source.ReservedItems.Select(
                        a =>
                        reservedBookMapper.Map(new ReservedItem { Item = null, Date = a.Date, Id = a.Id, Reader = a.Reader }))
                        .ToList();
            }

            destination.Tags = null;

            return destination;
        }
    }
}