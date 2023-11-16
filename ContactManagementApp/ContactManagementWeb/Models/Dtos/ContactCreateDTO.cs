using System.ComponentModel.DataAnnotations;

namespace ContactManagementWeb.Models
{
    public class ContactCreateDTO
    {
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
