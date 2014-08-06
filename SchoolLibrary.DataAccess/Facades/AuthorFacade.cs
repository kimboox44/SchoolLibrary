using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class AuthorFacade:IAuthorFacade,IDisposable
    {
        private ILibraryUow uow;

        public AuthorFacade(ILibraryUow uow)
        {
            this.uow = uow;
        }

        public AuthorBusinessModel GetAuthorById(int id)
        {
            var mapper = new AuthorMapper();
            var author = this.uow.Authors.GetById(id);
            return mapper.Map(author);
        }

        public void UpdateAuthor(AuthorBusinessModel author)
        {
            var mapper = new AuthorMapper();
            this.uow.Authors.Update(mapper.Map(author));
            this.uow.Commit();
        }

        public void CreateAuthor(AuthorBusinessModel author)
        {
            var mapper = new AuthorMapper();
            var authorNew = mapper.Map(author);
            this.uow.Authors.Add(authorNew);
            this.uow.Commit();
            author.Id = authorNew.Id;// updates the author.Id to Id value from DB
        }

        public List<AuthorBusinessModel> GetAllAuthors()
        {
            var mapper = new AuthorMapper();
            return this.uow.Authors.GetAll().Select(mapper.Map).ToList();
        }

        public void RemoveAuthorById(int id)
        {
            this.uow.Authors.Delete(this.uow.Authors.GetById(id));
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
