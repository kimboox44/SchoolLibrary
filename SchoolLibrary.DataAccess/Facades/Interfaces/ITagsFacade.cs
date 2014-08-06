namespace SchoolLibrary.DataAccess.Facades.Interfaces
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public interface ITagsFacade
    {
        TagBusinessModel GetTag(int id);

        List<TagBusinessModel> GetTags(string filter);

        void Create(string tagName);

        List<TagBusinessModel> GetReaderPreferences(int readerId);
    }
}