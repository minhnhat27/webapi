using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAPI.Models
{
    public class Buoi
    {
        [Key]
        [MaxLength(10)]
        public string TenBuoi { get; set; }
        public ICollection<LichThucHanh> LichThucHanhs { get; } = new List<LichThucHanh>();
    }
}
