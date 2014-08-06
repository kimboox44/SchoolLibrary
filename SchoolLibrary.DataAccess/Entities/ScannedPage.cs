namespace SchoolLibrary.DataAccess.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ScannedPages")]
    public class ScannedPage
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string OrderText { get; set; }

        [Required]
        public DateTime? OrderDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public bool IsReady { get; set; }

        public bool IsLocked { get; set; }

        [StringLength(500)]
        public string Message { get; set; }

        [Required]
        public virtual Item Item { get; set; }

        [Required]
        public virtual Reader Reader { get; set; }
    }
}
