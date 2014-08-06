using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary.DataAccess.Entities
{
    [Table("Consignment")]
    public class Consignment
    {
        public int Id { get; set; }

        /// <summary>
        /// Unique Consignment Number.
        /// </summary>
        public int Number { get; set; }

        public DateTime? ArrivalDate { get; set; }

        public DateTime? WriteOffDate { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

        public virtual Item Item { get; set; }

        public Consignment()
        {
            this.Inventories = new HashSet<Inventory>();
        }
    }
}
