using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolLibrary.BusinessModels.MVCModels
{
    using System.ComponentModel;


    public class BookAuthorModel
    {
    [DisplayName("Назва книги")]
        public string BookName { get; set; }
        [DisplayName("Автор")]
        public string AuthorName { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}