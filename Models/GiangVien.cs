using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class GiangVien
    {
        [Key]
        [MaxLength(10)]
        public string MSCB { get; set; }
        [MaxLength(30)]
        public string HoTen { get; set; }
        public ICollection<GiangDay> GiangDays { get; } = new List<GiangDay>();
    }
}
