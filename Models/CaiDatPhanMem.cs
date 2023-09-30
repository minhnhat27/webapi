using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.Models
{
    [PrimaryKey(nameof(SoPhong), nameof(IdPhanMem))]
    public class CaiDatPhanMem
    {
        public int SoPhong { get; set; }
        public Phong? Phong { get; set; }
        public int IdPhanMem { get; set; }
        public PhanMem? PhanMem { get; set; }
    }
}
