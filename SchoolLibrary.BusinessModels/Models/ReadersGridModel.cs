using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessModels.Models
{
    public class ReadersGridModel
    {
        public int TotalRows { get; set; }

        public IEnumerable<ReaderBusinessModel> Readers { get; set; }
    }
}
