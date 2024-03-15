using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace webapi.ViewModels.Request
{
    public class CheckTokenRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string? NewPassword { get; set; }
    }
}
