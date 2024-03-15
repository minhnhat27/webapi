namespace webapi.ViewModels.Admin.Request
{
    public class AddUserRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string?> Roles { get; set; }
    }
}
