using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi.ViewModels
{
    public class ForgetPasswordModel
    {
        [Required(ErrorMessage = "Vui lòng nhập MSCB hoặc Email")]
        public string id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã xác nhận")]
        public string? token { get; set; }
        public string? newPassword { get; set; }
    }
}
