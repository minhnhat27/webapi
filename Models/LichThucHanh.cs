using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAPI.Models
{
    [PrimaryKey(nameof(NgayThucHanh), nameof(SoPhong), nameof(TenBuoi))]
    public class LichThucHanh
    {
        [Key]
        [MaxLength(20)]
        public string NgayThucHanh { set; get; }
        public int SoPhong { get; set; }
        public Phong? Phong { get; set; }
        public string TenBuoi { get; set; }
        public Buoi? Buoi { get; set; }
        public Tuan? Tuan { get; set; }
        public GiangDay? GiangDay { get; set; }
        [MaxLength(100)]
        public string? GhiChu { get; set; }
    }
}
