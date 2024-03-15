using Microsoft.AspNetCore.Identity;

namespace webapi.Models
{
    public class Roles : IdentityRole
    {
        public string? Description { get; set; }
    }
}
