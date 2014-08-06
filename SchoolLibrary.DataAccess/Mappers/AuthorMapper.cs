namespace SchoolLibrary.DataAccess.Mappers
{
    using System.Linq;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class AuthorMapper : IMapper<Author, AuthorBusinessModel>
    {
        public Author Map(AuthorBusinessModel source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = new Author 
            { 
                Id = source.Id,
                FirstName = source.FirstName,
                MiddleName = source.MiddleName,
                LastName = source.LastName
            };

            return destination;
        }

        public AuthorBusinessModel Map(Author source)
        {
            if (source == null)
            {
                return null;
            }
            
            var destination = new AuthorBusinessModel
            {
                Id = source.Id,
                FirstName = source.FirstName,
                MiddleName = source.MiddleName,
                LastName = source.LastName
            };
            
            return destination;
        }
    }
}
