using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class BookWithAuthorsShortFacade : IBookWithAuthorsShortFacade, IDisposable
    {
        private ILibraryUow uow;

        public BookWithAuthorsShortFacade(ILibraryUow uow)
        {
            this.uow = uow;
            
        }

        public BookWithAuthorsShort GetBookById(int id)
        {
            var mapper = new BookWithAuthorsShortMapper();
            var item = this.uow.Items.GetById(id);
            return mapper.Map(item as Book);
        }

        public void UpdateBook(BookWithAuthorsShort book)
        {

            //book.Tags = new List<TagBusinessModel>();
            var mapper = new BookWithAuthorsShortMapper();

            //var autlist = book.AuthorsId.Split(',').Select(int.Parse).ToList();
            //var newTagsId = book.TagsId.Split(',').Select(int.Parse).ToList();

            var bookOld = this.uow.Items.GetById(book.Id) as Book;

            bookOld.Authors.Clear();
            bookOld.Tags.Clear();
            
            var bookMapped = mapper.Map(book);

            foreach (var aut in book.Authors)
            {
                var author = this.uow.Authors.GetById(aut.id);
                bookOld.Authors.Add(author);
            }

            foreach (var tag in book.Tags)
            {
                var t = this.uow.Tags.GetById(tag.id);
                bookOld.Tags.Add(t);
            }
            
            bookOld.Name = bookMapped.Name;
            bookOld.PageCount = bookMapped.PageCount;
            bookOld.Publisher = bookMapped.Publisher;
            bookOld.Year = bookMapped.Year;

            this.uow.Items.Update(bookOld);

            this.uow.Commit();
        }

        public void CreateBook(BookWithAuthorsShort book)
        {
            //book.Tags = new List<TagBusinessModel>();

            //var autlist = book.AuthorsId.Split(',').Select(int.Parse).ToList();
            //var newTagsId = book.TagsId.Split(',').Select(int.Parse).ToList();

            var mapper = new BookWithAuthorsShortMapper();
            var bookNew = mapper.Map(book);

            foreach (var aut in book.Authors)
            {
                var author = this.uow.Authors.GetById(aut.id);
                bookNew.Authors.Add(author);
            }

            foreach (var tag in book.Tags)
            {
                var t = this.uow.Tags.GetById(tag.id);
                bookNew.Tags.Add(t);
            }

            this.uow.Items.Add(bookNew);
            this.uow.Commit();
            book.Id = bookNew.Id; // updates the book.Id to Id value from DB
        }

        public List<BookWithAuthorsShort> GetAllBooks()
        {
            var mapper = new BookWithAuthorsShortMapper();
            return this.uow.Items.GetAll().OfType<Book>().Select(mapper.Map).ToList();
        }

        public void RemoveById(int id)
        {
            this.uow.Items.Delete(this.uow.Items.GetById(id));
            this.uow.Commit();
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
