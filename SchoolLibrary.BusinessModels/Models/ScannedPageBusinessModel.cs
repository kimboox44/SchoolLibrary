namespace SchoolLibrary.BusinessModels.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ScannedPageBusinessModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string OrderText { get; set; }

        [Required]
        public DateTime? OrderDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public bool IsReady { get; set; }

        public bool IsLocked { get; set; }

        [StringLength(500)]
        public string Message { get; set; }

        [Required]
        public ItemBusinessModel Item { get; set; }

        [Required]
        public ReaderBusinessModel Reader { get; set; }

    }
}