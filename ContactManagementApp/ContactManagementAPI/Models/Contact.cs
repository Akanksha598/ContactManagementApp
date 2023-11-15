using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManagementAPI.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        public string? Email { get; set; }

        [Required]
        public long PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
