namespace SchoolLibrary.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using SchoolLibrary.BusinessModels.Models;

    public class ReaderSelfEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^[+38-]?[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}",
            ErrorMessage = "Wrong number format! Examples: +38096-780-78-78, 096-780-78-78")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid e-mail address!")]
        public string EMail { get; set; }

        public List<TagBusinessModel> Preferences { get; set; }

        [DisplayName("Preferences")]
        public string TagsId { get; set; }
    }
}