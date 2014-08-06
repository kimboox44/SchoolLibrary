namespace SchoolLibrary.DataAccess.Mappers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    /// <summary>
    /// The book mapper. Mapping Book to BookBusinessModel and vice versa
    /// </summary>
    public class BookMapper : IMapper<Book, BookBusinessModel>
    {
        /// <summary>
        /// Maps the Book instance to the BookBusinessModel instance
        /// </summary>
        /// <param name="source">
        /// The source of type Book
        /// </param>
        /// <returns>
        /// The <see cref="BookBusinessModel"/> 
        /// </returns>
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

            if (source.Tags != null)
            {
                var tagMapper = new TagMapper();
                destination.Tags =
                source.Tags.Select(
                a => tagMapper.Map(new Tag { Id = a.Id, Name = a.Name }))
                .ToList();
            }
            return destination;
        }

        /// <summary>
        /// Maps the BookBusinessModel instance to the Book instance
        /// </summary>
        /// <param name="source">
        /// The source is of type BookBusinessModel
        /// </param>
        /// <returns>
        /// The <see cref="Book"/>.
        /// </returns>
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
            if (source.Tags != null)
            {
                var tagMapper = new TagMapper();
                destination.Tags =
                source.Tags.Select(
                a => tagMapper.Map(new TagBusinessModel { id = a.id, name = a.name }))
                .ToList();
            }
            return destination;
        }
    }
}
