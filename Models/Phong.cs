using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class Phong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoPhong { get; set; }
        [Required]
        public int SoLuongMayTinh { get; set; }
        public CauHinh? CauHinh { get; set; }
        public ICollection<CaiDatPhanMem> CaiDatPhanMems { get; } = new List<CaiDatPhanMem>();
        public ICollection<LichThucHanh> LichThucHanhs { get; } = new List<LichThucHanh>();
        public ICollection<HocPhanPhuHop> HocPhanPhuHops { get; } = new List<HocPhanPhuHop>();
    }
}
