namespace SchoolLibrary.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class Item
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

        public virtual ICollection<ReservedItem> ReservedItems { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Consignment> Consignment { get; set; }

        public virtual ICollection<ScannedPage> ScannedPage { get; set; }
    }
}
