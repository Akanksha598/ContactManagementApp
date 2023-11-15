using System.ComponentModel.DataAnnotations;

namespace ContactManagementAPI.Models.Dtos
{
    public class ContactDTO
    {
        public int ID { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Email { get; set; }

        [Required]
        public long PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
