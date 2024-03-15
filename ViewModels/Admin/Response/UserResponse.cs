namespace webapi.ViewModels.Admin.Response
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        public string? LockoutEnd { get; set; }
        public List<string>? Roles { get; set; }
    }
}
