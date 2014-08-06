using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessModels.Models
{
    public class MagazineBusinessModel:ItemBusinessModel
    {
        [Required(ErrorMessage = "The Publisher is required")]
        [StringLength(50)]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "The Issue is required")]
        public int Issue { get; set; }

        [Required(ErrorMessage = "The page count is required")]
        public int PageCount { get; set; }

        public virtual ICollection<TagBusinessModel> Tags { get; set; }

        public MagazineBusinessModel()
        {
            Tags=new HashSet<TagBusinessModel>();
        }
    }
}
