using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class CauHinh
    {
        [Key]
        public int IdCauHinh { get; set; }
        public CPU? CPU { get; set; }
        public RAM? RAM { get; set; }
        public OCung? OCung { get; set; }
        public ICollection<CaiDatPhanMem> CaiDatPhanMems { get; } = new List<CaiDatPhanMem>();
        public ICollection<Phong> Phongs { get; } = new List<Phong>();
    }
}
