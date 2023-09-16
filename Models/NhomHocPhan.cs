using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class NhomHocPhan
    {
        [Key]
        [MaxLength(15)]
        public string MaNhomHP { get; set; }
        public int SoLuongSV { get; set; }
        public HocPhan? HocPhan { get; set; }
        public ICollection<GiangDay> GiangDays { get; } = new List<GiangDay>();
    }
}
