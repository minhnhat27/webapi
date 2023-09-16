using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class HocKyNamHoc
    {
        [Key]
        [MaxLength(15)]
        public string HK_NH { get; set; }
        [Required]
        [MaxLength(20)]
        public string NgayBatDau { get; set; }
        [Required]
        public bool HocKyHienTai { get; set; } = false;

        public ICollection<GiangDay> GiangDays { get; } = new List<GiangDay>();
    }
}
