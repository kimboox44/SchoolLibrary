using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SchoolLibrary.BusinessModels.XMLSearchModels
{
    [XmlInclude(typeof(AuthorModel))]
    [XmlType("Book")]
    public class BookModel
    {
        [XmlElement("BookId")]
        public int Id { get; set; }

        [XmlElement("BookName")]
        public string Name { get; set; }

        [XmlArray("Authors"), XmlArrayItem("Author")]
        public List<AuthorModel> Authors { get; set; }

        public string Publisher { get; set; }

        public int Year { get; set; }

        public int PageCount { get; set; }

        public BookModel()
        {
            Authors = new List<AuthorModel>();
        }
    }
}
