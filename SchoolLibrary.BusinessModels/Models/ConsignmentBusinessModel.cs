using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessModels.Models
{
    public class ConsignmentBusinessModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Unique Consignment Number.
        /// </summary>
        public int Number { get; set; }

        public DateTime? ArrivalDate { get; set; }

        public DateTime? WriteOffDate { get; set; }

        public virtual ICollection<InventoryBusinessModel> Inventories { get; set; }

        public virtual ItemBusinessModel Item { get; set; }

        public ConsignmentBusinessModel()
        {
            this.Inventories = new HashSet<InventoryBusinessModel>();
        }
    }
}
