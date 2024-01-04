using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class Tuan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoTuan { get; set; }
        public ICollection<LichThucHanh> LichThucHanhs { get; } = new List<LichThucHanh>();
    }
}
