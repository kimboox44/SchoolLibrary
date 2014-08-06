using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SchoolLibrary.BusinessModels.MVCModels;

namespace SchoolLibrary.BusinessModels.Models
{
    using System.ComponentModel;

    [DataContract]
    [KnownType(typeof(BookBusinessModel))]
    [KnownType(typeof(DiskBusinessModel))]
    [KnownType(typeof(MagazineBusinessModel))]
    [KnownType(typeof(BookWithAuthorsShort))]
    public abstract class ItemBusinessModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Name")]
        [Required(ErrorMessage = "The Name is required")]
        [StringLength(150, ErrorMessage = "The Name must be less than {1}")]
        public string Name { get; set; }

        [DataMember]
        [DisplayName("Year")]
        [Required(ErrorMessage = "The Year is required")]
        public int Year { get; set; }

        [IgnoreDataMember]
        [DisplayName("Tags")]
        public virtual ICollection<TagBusinessModel> Tags { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<InventoryBusinessModel> Inventories { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<ReservedItemBusinessModel> ReservedItems { get; set; }

        [IgnoreDataMember]
        [DisplayName("ScannedPage")]
        public virtual ICollection<ScannedPageBusinessModel> ScannedPages { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<ConsignmentBusinessModel> Consignments { get; set; }
        

        public ItemBusinessModel()
        {
            Tags=new HashSet<TagBusinessModel>();
            Inventories=new HashSet<InventoryBusinessModel>();
            ReservedItems=new HashSet<ReservedItemBusinessModel>();
            ScannedPages = new HashSet<ScannedPageBusinessModel>();
            Consignments = new HashSet<ConsignmentBusinessModel>();
        }
    }
}
