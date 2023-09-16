using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.Models
{
    [PrimaryKey(nameof(IdCauHinh), nameof(IdPhanMem))]
    public class CaiDatPhanMem
    {
        public int IdCauHinh { get; set; }
        public CauHinh? CauHinh { get; set; }
        public int IdPhanMem { get; set; }
        public PhanMem? PhanMem { get; set; }
    }
}
