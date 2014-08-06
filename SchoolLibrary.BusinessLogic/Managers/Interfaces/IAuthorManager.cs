using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;

    public interface IAuthorManager
    {
        AuthorBusinessModel GetAuthorById(int id);

        List<AuthorShortInfo> SearchAuthorsShortInfo(string search);

        void UpdateAuthor(AuthorBusinessModel author);

        void CreateAuthor(AuthorBusinessModel author);

        List<AuthorBusinessModel> GetAllAuthors();

        void RemoveAuthorById(int id);

        void RemoveAuthor(AuthorBusinessModel author);
    }
}
