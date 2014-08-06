namespace SchoolLibrary.BusinessModels.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ReaderHistoryBusinessModel
    {
        [Required]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Return Date")]
        public DateTime? ReturnDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Finish Date")]
        public DateTime? FinishDate { get; set; }

        public int DeptorsDays { get; set; }

        public virtual ReaderBusinessModel ReaderBusiness { get; set; }
        public virtual InventoryBusinessModel InventoryBusiness { get; set; }
    }
}