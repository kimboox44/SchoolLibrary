namespace SchoolLibrary.BusinessModels.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ReaderBusinessModel
    {
        public int ReaderId { get; set; }

        [Required( ErrorMessage = "First name is required")]
        [RegularExpression("[A-Za-zА-Яа-я]{2,}", ErrorMessage = "Incorrect first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression("[A-Za-zА-Яа-я]{2,}", ErrorMessage = "Incorrect last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^[+38-]?[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}", 
            ErrorMessage = "Wrong number format! Examples: +38096-780-78-78, 096-780-78-78")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter valid e-mail address!")]
        public string EMail { get; set; }

        public UserProfileBusinessModel UserProfileBusiness { get; set; }
        
        public List<TagBusinessModel> Preferences { get; set; }
    }
}