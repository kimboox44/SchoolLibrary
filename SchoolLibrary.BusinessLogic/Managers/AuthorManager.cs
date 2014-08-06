namespace SchoolLibrary.BusinessLogic.Managers
{
    using System;
    using System.Linq;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using System.Collections.Generic;


    public class AuthorManager:IAuthorManager,IDisposable
    {
        private IAuthorFacade authorFacade;

        public AuthorManager(IAuthorFacade authorFacade)
        {
            this.authorFacade = authorFacade;
        }

        public List<AuthorShortInfo> SearchAuthorsShortInfo(string search)
        {
            var authors =
            this.GetAllAuthors().Select(x => new AuthorShortInfo { id = x.Id, name = x.FirstName + " " + x.MiddleName + " " + x.LastName }).Where(x => x.name.ToLower().Contains(search));
            return authors.ToList();
        }

        public void UpdateAuthor(AuthorBusinessModel author)
        {
            this.authorFacade.UpdateAuthor(author);
        }

        public void CreateAuthor(AuthorBusinessModel author)
        {
            this.authorFacade.CreateAuthor(author);
            
        }

        public List<AuthorBusinessModel> GetAllAuthors()
        {
            return this.authorFacade.GetAllAuthors();
        }

        public void RemoveAuthorById(int id)
        {
            this.authorFacade.RemoveAuthorById(id);
        }

        public void RemoveAuthor(AuthorBusinessModel author)
        {
            this.authorFacade.RemoveAuthorById(author.Id);
        }

        public AuthorBusinessModel GetAuthorById(int id)
        {
            return this.authorFacade.GetAuthorById(id);
        }

        public void Dispose()
        {
            if (this.authorFacade as IDisposable != null)
            {
                (this.authorFacade as IDisposable).Dispose();
            }
        }
    }
}
