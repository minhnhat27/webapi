using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.Models
{
    [PrimaryKey(nameof(SoPhong), nameof(MaHP))]
    public class HocPhanPhuHop
    {
        public int SoPhong { get; set; }
        public Phong Phong { get; set; }
        public string MaHP { get; set; }
        public HocPhan HocPhan { get; set; }
    }
}
