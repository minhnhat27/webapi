using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAPI.Models
{
    public class RAM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DungLuong { get; set; }
        public ICollection<CauHinh> CauHinhs { get; } = new List<CauHinh>();
    }
}
