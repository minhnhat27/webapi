using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class CPU
    {
        [Key]
        [MaxLength(15)]
        public string Ten { get; set; }
        public ICollection<CauHinh> CauHinhs { get; } = new List<CauHinh>();
    }
}
