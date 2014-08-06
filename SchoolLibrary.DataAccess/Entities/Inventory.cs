namespace SchoolLibrary.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Inventory")]
    public class Inventory
    {
        public int InventoryId { get; set; }

        /// <summary>
        /// Unique Inventory Number in a special format.
        /// </summary>
        [Required]
        [StringLength(15)]
        [RegularExpression("[0-9]{10}-[0-9]{4}")]
        public string Number { get;set;}

        /// <summary>
        /// Shows if inventory is available for reader
        /// </summary>
        public bool IsAvailable { get; set; }

        public DateTime? WriteOffDate { get; set; }

        public virtual Item Item { get; set; }

        public virtual Consignment Consignment { get; set; }

        public virtual ICollection<ReaderHistory> ReaderHistories { get; set; }

        public Inventory()
        {
            this.ReaderHistories = new HashSet<ReaderHistory>();
            this.IsAvailable = true;
            this.Number = String.Format("{0:D10}", 0) + "-" + "0000";
        }
    }
}
