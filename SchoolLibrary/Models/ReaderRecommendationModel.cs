namespace SchoolLibrary.Models
{
    using System.Collections.Generic;
    using SchoolLibrary.BusinessModels.Models;

    public class ReaderRecommendationModel
    {
        public int ReaderId { get; set; }

        public List<ItemBusinessModel> Items { get; set; }
    }
}