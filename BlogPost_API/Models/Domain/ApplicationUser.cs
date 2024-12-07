using Microsoft.AspNetCore.Identity;

namespace BlogPost_API.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
