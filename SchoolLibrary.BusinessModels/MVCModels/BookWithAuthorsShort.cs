using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessModels.MVCModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using SchoolLibrary.BusinessModels.Models;

    public class BookWithAuthorsShort : ItemBusinessModel
    {

        [DisplayName("Authors")]
        public virtual ICollection<AuthorShortInfo> Authors { get; set; }

        [DisplayName("Publisher")]
        [Required(ErrorMessage = "The Publisher is required")]
        [StringLength(50, ErrorMessage = "The Publisher must be less than {1}")]
        public string Publisher { get; set; }


        [DisplayName("Page count")]
        public int PageCount { get; set; }


        public BookWithAuthorsShort()
        {
            this.Authors = new HashSet<AuthorShortInfo>();

        }
    }
}
