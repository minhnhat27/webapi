using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class OCung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DungLuong { get; set; }
        public ICollection<CauHinh> CauHinhs { get; } = new List<CauHinh>();
    }
}
