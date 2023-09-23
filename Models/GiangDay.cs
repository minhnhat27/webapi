using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.Models
{
    [PrimaryKey(nameof(HK_NH), nameof(GiangVienId), nameof(STT), nameof(MaNhomHP))]
    public class GiangDay
    {
        public string HK_NH { get; set; }
        public HocKyNamHoc? HocKyNamHoc { get; set; }
        public string GiangVienId { get; set; }
        public GiangVien GiangVien { get; set; }
        public int STT { get; set; }
        public BuoiThucHanh BuoiThucHanh { get; set; }
        public string? MaNhomHP { get; set; }
        public NhomHocPhan NhomHocPhan { get; set; }
        public ICollection<LichThucHanh> LichThucHanhs { get; } = new List<LichThucHanh>();
    }
}
