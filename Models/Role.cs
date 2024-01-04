using Microsoft.AspNetCore.Identity;

namespace webapi.Models
{
    public class Role : IdentityRole
    {
        public string? Description { get; set; }
    }
}
