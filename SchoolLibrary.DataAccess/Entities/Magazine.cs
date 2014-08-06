namespace SchoolLibrary.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("Magazines")]
    public class Magazine : Item
    {
        public Magazine()
        {
            this.Inventories = new HashSet<Inventory>();
            this.Tags = new HashSet<Tag>();
            this.Consignment = new HashSet<Consignment>();
        }

        [StringLength(50)]
        public string Publisher { get; set; }

        public int Issue { get; set; }

        public int PageCount { get; set; }
    }
}
