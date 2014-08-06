using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary.DataAccess.Entities
{
    using System.Data.SqlClient;

    [Table("Party")]
    public class Party
    {

        public int Id { get; set; }

        //[Required]
        public DateTime? ArrivalDate { get; set; }

        //[Required]
        public DateTime? WriteOffDate { get; set; }

        public int PartyNumber { get; private set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

        public Party()
        {
            this.Inventories = new HashSet<Inventory>();
        }
    }
}
