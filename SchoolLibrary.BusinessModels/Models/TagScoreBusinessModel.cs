namespace SchoolLibrary.BusinessModels.Models
{
    public class TagScoreBusinessModel
    {
        public ReaderBusinessModel Reader { get; set; }

        public ItemBusinessModel Item { get; set; }

        public float Score { get; set; }
    }
}