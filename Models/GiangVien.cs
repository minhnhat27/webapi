using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class GiangVien : IdentityUser
    {
        [MaxLength(30)]
        public string? HoTen { get; set; }
        [MaxLength(15)]

        public int? ResetCode { get; set; }

        public DateTime? ResetCodeExpiresAt { get; set; }

        public ICollection<GiangDay> GiangDays { get; } = new List<GiangDay>();
    }
}
