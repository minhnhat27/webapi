using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.ViewModel
{
    public class UserModel
    {
        [MaxLength(10)]
        public string MSCB { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
