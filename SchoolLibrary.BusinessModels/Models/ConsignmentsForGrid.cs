using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessModels.Models
{
    public class ConsignmentsForGrid
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public DateTime? ArrivalDate { get; set; }

        public DateTime? WriteOffDate { get; set; }

        public int InventoriesCount { get; set; }

        public ConsignmentsForGrid(ConsignmentBusinessModel consignmrntModel)
        {
            this.Id = consignmrntModel.Id;
            this.ArrivalDate = consignmrntModel.ArrivalDate;
            this.InventoriesCount = consignmrntModel.Inventories.Count;
            this.WriteOffDate = consignmrntModel.WriteOffDate;
            this.Number = consignmrntModel.Number;
        }
    }
}
