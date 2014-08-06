namespace SchoolLibrary.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Books")]
    public class Book : Item
    {
        public Book()
        {
            this.Authors = new HashSet<Author>();
            this.Inventories = new HashSet<Inventory>();
            this.Tags = new HashSet<Tag>();
            this.Consignment = new HashSet<Consignment>();
            this.ReservedItems = new HashSet<ReservedItem>();
        }

        [Required]
        [StringLength(50)]
        public string Publisher { get; set; }

        public int PageCount { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
