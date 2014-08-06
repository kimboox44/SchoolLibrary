namespace SchoolLibrary.BusinessModels.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class AuthorBusinessModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The First Name is required")]
        [DisplayName("First Name")]
        [StringLength(35, ErrorMessage = "The First Name must be less than {1}")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        [StringLength(150, ErrorMessage = "The Middle Name must be less than {1}")]
        public string MiddleName { get; set; }


        [Required(ErrorMessage = "The First Name is required")]
        [DisplayName("Last Name")]
        [StringLength(35, ErrorMessage = "The First Name must be less than {1}")]
        public string LastName { get; set; }

        public virtual ICollection<BookBusinessModel> Books { get; set; }

        public AuthorBusinessModel()
        {
            this.Books = new HashSet<BookBusinessModel>();
        }
    }
}
