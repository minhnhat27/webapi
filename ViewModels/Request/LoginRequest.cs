using System.ComponentModel.DataAnnotations;

namespace webapi.ViewModels.Request
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
