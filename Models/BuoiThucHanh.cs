using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class BuoiThucHanh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int STT { get; set; }
        public ICollection<GiangDay> GiangDays { get; } = new List<GiangDay>();
    }
}