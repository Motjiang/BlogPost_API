using Microsoft.AspNetCore.Identity;

namespace BlogPost_API.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        string? Name { get; set; }
    }
}
