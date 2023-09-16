using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAPI.Models
{
    public class Phong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoPhong { get; set; }
        [Required]
        public int SoLuongMayTinh { get; set; }
        public CauHinh? CauHinh { get; set; }
        public ICollection<LichThucHanh> LichThucHanhs { get; } = new List<LichThucHanh>();
        public ICollection<HocPhanPhuHop> HocPhanPhuHops { get; } = new List<HocPhanPhuHop>();
    }
}
