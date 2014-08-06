namespace SchoolLibrary.BusinessLogic.Managers
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.Models;
    using SchoolLibrary.DataAccess.Facades.Interfaces;

    public class TagsManager: ITagsManager
    {
        private ITagsFacade tagsFacade;

        public TagsManager(ITagsFacade tagsFacade)
        {
            this.tagsFacade = tagsFacade;
        }

        public TagBusinessModel GetTag(int id)
        {
            return this.tagsFacade.GetTag(id);
        }

        public List<TagBusinessModel> GetTags(string token)
        {
            return this.tagsFacade.GetTags(token);
        }

        public void Create(string tagName)
        {
            this.tagsFacade.Create(tagName);
        }

        public List<TagBusinessModel> GetReaderPreferences(int readerId)
        {
            return this.tagsFacade.GetReaderPreferences(readerId);
        }
    }
}