using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary.DataAccess.Entities
{
    [Table("Authors")]
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(35)]
        public string FirstName { get; set; }


        [StringLength(150)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(35)]
        public string LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            this.Books = new HashSet<Book>();
        }
    }
}
