namespace SchoolLibrary.BusinessModels.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReservedBookBusinessModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public BookBusinessModel Book { get; set; }

        [Required]
        public ReaderBusinessModel Reader { get; set; }

        public DateTime? Date { get; set; }

        public bool IsReady { get; set; }

        public DateTime? ReadyDate { get; set; }
    }
}