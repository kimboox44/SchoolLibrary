using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessModels.Models
{
    public class ItemsForGrid
    {
        public int Id;

        public string Name { get; set; }

        public int Year { get; set; }

        public string Publisher { get; set; }

        public string Type { get; set; }

        public int InventoriesCount { get; set; }
    }
}
