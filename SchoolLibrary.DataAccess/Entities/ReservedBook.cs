namespace SchoolLibrary.DataAccess.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ReservedBooks")]
    public class ReservedBook
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public virtual Book Book { get; set; }

        [Required]
        public virtual Reader Reader { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public bool IsReady { get; set; }

        public DateTime? ReadyDate { get; set; }
    }
}