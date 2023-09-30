using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.Models
{
    [PrimaryKey(nameof(HK_NH), nameof(GiangVienId), nameof(BuoiThucHanhSTT), nameof(MaNhomHP))]
    public class GiangDay
    {
        public string HK_NH { get; set; }
        public HocKyNamHoc? HocKyNamHoc { get; set; }
        public string GiangVienId { get; set; }
        public GiangVien GiangVien { get; set; }
        public int BuoiThucHanhSTT { get; set; }
        public BuoiThucHanh BuoiThucHanh { get; set; }
        public string? MaNhomHP { get; set; }
        public NhomHocPhan NhomHocPhan { get; set; }
        public bool caNgay { set; get; } = false;
        public bool onSchedule { set; get; } = false;
        public ICollection<LichThucHanh> LichThucHanhs { get; } = new List<LichThucHanh>();
    }
}
