using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoolLibrary.DataAccess.Mappers
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    public class BookWithAuthorsShortMapper : IMapper<Book, BookWithAuthorsShort>
    {
        public Book Map(BookWithAuthorsShort source)
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

            // Undone: Tags and Authors adding moved to facade methods.

            //TagMapper tagMapper = new TagMapper();
            //ICollection<Tag> destTags = new Collection<Tag>();
            //foreach (var tagBusinessModel in source.Tags)
            //{
            //    destTags.Add(tagMapper.Map(tagBusinessModel));
            //}

            //destination.Tags = destTags;

            //if (source.Authors != null)
            //{
            //    destination.Authors =
            //        source.Authors.Select(a => new Author { Id = a.id })
            //            .ToList();
            //}

            return destination;
        }

        public BookWithAuthorsShort Map(Book source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = new BookWithAuthorsShort
            {
                Id = source.Id,
                Name = source.Name,
                Year = source.Year,
                PageCount = source.PageCount,
                Publisher = source.Publisher,
            };

            var destTags = new List<TagBusinessModel>();
            TagMapper tagMapper = new TagMapper();
            foreach (var tag in source.Tags)
            {
                destTags.Add(tagMapper.Map(tag));
            }

            destination.Tags = destTags;

            if (source.Authors != null)
            {
                destination.Authors =
                    source.Authors.Select(a => new AuthorShortInfo { id = a.Id, name = a.FirstName + " " + a.MiddleName + " " + a.LastName })
                        .ToList();
            }

            return destination;
        }
    }
}