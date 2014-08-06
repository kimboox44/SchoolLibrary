using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using SchoolLibrary.BusinessModels.Models;

    public interface IAuthorFacade
    {
        AuthorBusinessModel GetAuthorById(int id);

        void UpdateAuthor(AuthorBusinessModel author);

        void CreateAuthor(AuthorBusinessModel author);

        List<AuthorBusinessModel> GetAllAuthors();

        void RemoveAuthorById(int id);
    }
}
