using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAPI.Models
{
    [PrimaryKey(nameof(TenBuoi), nameof(HK_NH), nameof(GiangVienId), nameof(MaNhomHP), nameof(BuoiThucHanhSTT), nameof(PhongSoPhong))]
    public class LichThucHanh
    {
        [MaxLength(20)]
        public DateTime NgayThucHanh { set; get; }
        public int? PhongSoPhong { set; get; }
        public Phong? Phong { get; set; }
        public int TuanSoTuan { set; get; }
        public Tuan? Tuan { get; set; }

        public string TenBuoi { get; set; }
        public Buoi? Buoi { get; set; }

        public string HK_NH { get; set; }
        public string GiangVienId { get; set; }
        public int BuoiThucHanhSTT { get; set; }
        public string MaNhomHP { get; set; }
        public GiangDay GiangDay { get; set; }
        
        [MaxLength(100)]
        public string? GhiChu { get; set; }
    }
}
