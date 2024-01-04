using System.ComponentModel.DataAnnotations;

namespace webapi.ViewModels
{
    public class ConfirmPasswordModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
        [MinLength(6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự.")]
        public string pass { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        public string newPassword { get; set; }
    }
}
