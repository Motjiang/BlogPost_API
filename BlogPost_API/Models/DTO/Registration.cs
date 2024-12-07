using System.ComponentModel.DataAnnotations;

namespace BlogPost_API.Models.DTO
{
    public class Registration
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
