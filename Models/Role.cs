using Microsoft.AspNetCore.Identity;

namespace MyWebAPI.Models
{
    public class Role : IdentityRole
    {
        public string? Description { get; set; }
    }
}
