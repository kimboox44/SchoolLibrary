namespace SchoolLibrary.BusinessModels.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class BookBusinessModel : ItemBusinessModel
    {
        

        [DisplayName("Authors")]
        public virtual ICollection<AuthorBusinessModel> Authors { get; set; }

        [DisplayName("Publisher")]
        [Required(ErrorMessage = "The Publisher is required")]
        [StringLength(50, ErrorMessage = "The Publisher must be less than {1}")]
        public string Publisher { get; set; }

        [DisplayName("Pages")]
        public int PageCount { get; set; }

        public BookBusinessModel()
        {
            this.Authors = new HashSet<AuthorBusinessModel>();
           // this.Tags = new HashSet<TagBusinessModel>();
        }

    }
}
