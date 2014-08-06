namespace SchoolLibrary.DataAccess.Mappers
{
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;

    public class TagMapper : IMapper<Tag, TagBusinessModel>
    {
        public Tag Map(TagBusinessModel source)
        {
            return new Tag { Items = null, Id = source.id, Name = source.name };
        }

        public TagBusinessModel Map(Tag source)
        {
            if (source == null) return null;

            return new TagBusinessModel { id = source.Id, name = source.Name };
        }
    }
}