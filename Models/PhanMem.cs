using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class PhanMem
    {
        [Key]
        public int IdPhanMem { get; set; }
        [MaxLength(30)]
        public string TenPhanMem { get; set; }
        public ICollection<CaiDatPhanMem> CaiDatPhanMems { get; } = new List<CaiDatPhanMem>();

    }
}
