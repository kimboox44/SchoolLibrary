using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessModels.Models
{
    public class DiskBusinessModel:ItemBusinessModel
    {
        [Required(ErrorMessage = "The Producer is required")]
        [StringLength(50)]
        public string Producer { get; set; }

        [Required(ErrorMessage = "The Type is required")]
        public string Type { get; set; }

        public virtual ICollection<TagBusinessModel> Tags { get; set; }

        public DiskBusinessModel()
        {
            Tags=new HashSet<TagBusinessModel>();
        }
    }
}
