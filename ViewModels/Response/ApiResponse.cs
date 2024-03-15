namespace webapi.ViewModels.Response
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Data { get; set; }
    }
}
