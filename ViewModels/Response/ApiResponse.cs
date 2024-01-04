namespace webapi.ViewModels.Response
{
    public class ApiResponse
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public string? data { get; set; }
    }
}
