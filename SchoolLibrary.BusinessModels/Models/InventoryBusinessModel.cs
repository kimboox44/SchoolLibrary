namespace SchoolLibrary.BusinessModels.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InventoryBusinessModel
    {
        public int InventoryId { get; set; }

        /// <summary>
        /// Unique Inventory Number in a special format.
        /// </summary>
        [Required]
        [StringLength(15)]
        [RegularExpression("[0-9]{10}-[0-9]{4}")]
        public string Number { get; set; }
        
        /// <summary>
        /// Shows if inventory is available for reader
        /// </summary>
        public bool IsAvailable { get; set; }

        public DateTime? WriteOffDate { get; set; }

        public virtual ItemBusinessModel Item { get; set; }

        public virtual ConsignmentBusinessModel Consignment { get; set; }

        public virtual ICollection<ReaderHistoryBusinessModel> ReaderHistories { get; set; }

        public InventoryBusinessModel()
        {
            this.ReaderHistories = new HashSet<ReaderHistoryBusinessModel>();
            this.IsAvailable = true;
            this.Number = String.Format("{0:D10}", 0)+"-"+"0000";
        }
    }
}
