using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary.DataAccess.Entities
{
    [Table("ReaderHistories")]
    public class ReaderHistory
    {
        [Key]
        [Required]
        public  int ReaderHistoryId { get; set; }
        
        public  DateTime? StartDate { get; set; }

        public  DateTime? ReturnDate { get; set; }

        public  DateTime? FinishDate { get; set; }
        
        public virtual Reader Reader { get; set; }

        public virtual Inventory Inventory { get; set; }
        
    }
}