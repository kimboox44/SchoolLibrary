namespace SchoolLibrary.WcfServices
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using SchoolLibrary.BusinessModels.Models;

    [ServiceContract]
    public interface ISearchService
    {
        [OperationContract]
        List<ItemBusinessModel> SearchItems(string searchString);
    }
}
