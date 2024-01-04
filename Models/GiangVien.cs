using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class GiangVien : IdentityUser
    {
        [MaxLength(20)]
        public string? HoTen { get; set; }
        [MaxLength(15)]
        public string? SoDienThoai { get; set; }

        public ICollection<GiangDay> GiangDays { get; } = new List<GiangDay>();
    }
}
