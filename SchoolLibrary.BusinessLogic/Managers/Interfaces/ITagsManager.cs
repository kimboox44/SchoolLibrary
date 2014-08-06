namespace SchoolLibrary.BusinessLogic.Managers.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface ITagsManager
    {
        TagBusinessModel GetTag(int id);

        List<TagBusinessModel> GetTags(string token);

        void Create(string tagName);

        List<TagBusinessModel> GetReaderPreferences(int readerId);
    }
}