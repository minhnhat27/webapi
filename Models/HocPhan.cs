using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class HocPhan
    {
        [Key]
        [MaxLength(10)]
        public string MaHP { get; set; }
        [Required]
        [MaxLength(30)]
        public string TenHocPhan { get; set; }
        [Required]
        public int SoTinChi { get; set; }
        [Required]
        public int SoTietThucHanh { get; set; }
        public ICollection<NhomHocPhan> NhomHocPhans { get; } = new List<NhomHocPhan>();
        public ICollection<HocPhanPhuHop> HocPhanPhuHops { get; } = new List<HocPhanPhuHop>();
    }
}
