namespace SchoolLibrary.DataAccess.Facades
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Entities;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.Mappers;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public class TagsFacade : ITagsFacade
    {
        private ILibraryUow libraryUow;

        public TagsFacade(ILibraryUow libraryUow)
        {
            this.libraryUow = libraryUow;
        }

        public TagBusinessModel GetTag(int id)
        {
            TagMapper tagMapper = new TagMapper();
            return tagMapper.Map(this.libraryUow.Tags.GetById(id));
        }

        public List<TagBusinessModel> GetTags(string filter)
        {
            var tags = this.libraryUow.Tags.GetAll().Where(t => t.Name.Contains(filter)).ToList();
            var tagsBusinessModels = new List<TagBusinessModel>();
            TagMapper mapper = new TagMapper();
            foreach (var tag in tags)
            {
                tagsBusinessModels.Add(mapper.Map(tag));
            }

            return tagsBusinessModels;
        }

        public void Create(string tagName)
        {
            this.libraryUow.Tags.Add(new Tag{Name = tagName});
            this.libraryUow.Commit();
        }

        public List<TagBusinessModel> GetReaderPreferences(int readerId)
        {
            var reader = this.libraryUow.Readers.GetById(readerId);
            TagMapper tagMapper = new TagMapper();
            List<TagBusinessModel> preferences = new List<TagBusinessModel>();
            foreach (var tag in reader.Preferences)
            {
                preferences.Add(tagMapper.Map(tag));
            }

            return preferences;
        }
    }
}