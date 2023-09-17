using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public GiangVien GiangVien { get; set; }
        public string Token { get; set; }
        public bool IsUsed { get; set;}
        public bool CreateAt { get; set; }
        public DateTime ExpireAt { get; set;}
    }
}
