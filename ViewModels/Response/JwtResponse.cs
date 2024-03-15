namespace webapi.ViewModels.Response
{
    public class JwtResponse
    {
        public bool Success { get; set; }
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public List<string>? Roles { get; set; }
        public string? AccessToken { get; set; }
    }
}
