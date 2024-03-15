namespace webapi.ViewModels.Admin.Request
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string?> Roles { get; set; }
    }
}
