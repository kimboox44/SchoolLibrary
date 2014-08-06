namespace SchoolLibrary.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Readers")]
    public class Reader
    {
        [Key]
        [Required]
        public int ReaderId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public DateTime? Birthday { get; set; }

        [Required]
        [StringLength(30)]
        public string Phone { get; set; }

        [StringLength(51)]
        public string EMail { get; set; }

        public virtual ICollection<Tag> Preferences { get; set; }

        public virtual ICollection<ReaderHistory> ReaderHistories { get; set; }

        public virtual UserProfile UserProfile { get; set; }
        
        public virtual ICollection<ReservedItem> ReservedItems { get; set; }

        public Reader()
        {
            this.ReaderHistories = new HashSet<ReaderHistory>();
            this.ReservedItems = new HashSet<ReservedItem>();
        }
    }
}