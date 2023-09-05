using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBFirstCRUDRazorPage.Models
{
    [Table("OwnersTable")]
    public class Owner
    {
        [Required]
        [Key]
        public int Owner_Id { get; set; }

        public String FullName { get; set; }
    }
}
