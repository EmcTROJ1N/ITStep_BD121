using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBFirstCRUDRazorPage.Models
{
    [Table("CarsTable")]
    public class Car
    {
        [Key]
        [Required]
        public int Car_id { get; set; }
    
        public int Owner_id { get; set; }
        
        public string Model { get; set; }
        public int MaxSpeed { get; set; }
    }
}
