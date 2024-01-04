namespace webapi.ViewModels.Response
{
    public class JwtResponse
    {
        public bool success { get; set; }
        public string? userId { get; set; }
        public string? name { get; set; }
        public string? accessToken { get; set; }
    }
}
