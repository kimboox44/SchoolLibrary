using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessModels.MVCModels
{
    using System.ComponentModel.DataAnnotations;

    using SchoolLibrary.BusinessModels.Models;

    public class ReadingFromExcelModel
    {
        public  List<ReaderBusinessModel> Readers { get; set; }

        public List<ValidationResult> Errors { get; set; }

        public ReadingFromExcelModel()
        {
            this.Readers = new List<ReaderBusinessModel>();
            this.Errors = new List<ValidationResult>();
        }
    }
}
