using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class CauHinh
    {
        [Key]
        public int IdCauHinh { get; set; }
        public CPU? CPU { get; set; }
        public RAM? RAM { get; set; }
        public OCung? OCung { get; set; }
        public ICollection<Phong> Phongs { get; } = new List<Phong>();
    }
}
