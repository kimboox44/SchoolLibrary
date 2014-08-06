namespace SchoolLibrary.DataAccess.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TagScores")]
    public class TagScore
    {
        public int Id { get; set; }

        public Item Item { get; set; }

        public Reader Reader { get; set; }

        public float Score { get; set; }
    }
}