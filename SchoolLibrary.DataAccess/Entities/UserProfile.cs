using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.DataAccess.Entities
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email{ get; set; }
    }
}
