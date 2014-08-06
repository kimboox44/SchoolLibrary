namespace SchoolLibrary.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("Disks")]
    public class Disk : Item
    {
        public Disk()
        {
            this.Inventories = new HashSet<Inventory>();
            this.Tags = new HashSet<Tag>();
            this.Consignment = new HashSet<Consignment>();
        }

        [StringLength(50)]
        public string Producer { get; set; }

        public string Type { get; set; }
    }
}
