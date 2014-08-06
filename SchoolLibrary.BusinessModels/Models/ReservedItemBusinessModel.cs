namespace SchoolLibrary.BusinessModels.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReservedItemBusinessModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public ItemBusinessModel Item { get; set; }

        [Required]
        public ReaderBusinessModel Reader { get; set; }

        public DateTime? Date { get; set; }

        [Required]
        public bool IsReady { get; set; }

        public DateTime? ReadyDate { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Name { get; set; }

    }
}