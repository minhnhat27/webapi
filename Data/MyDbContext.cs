using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class MyDbContext : IdentityDbContext<GiangVien, Roles, string>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<CPU> CPUs { get; set; }
        public DbSet<RAM> RAMs { get; set; }
        public DbSet<OCung> OCungs { get; set; }
        public DbSet<CauHinh> CauHinhs { get; set; }
        public DbSet<PhanMem> PhanMems { get; set; }
        public DbSet<CaiDatPhanMem> CaiDatPhanMems { get; set; }
        public DbSet<HocPhan> HocPhans { get; set; }
        public DbSet<NhomHocPhan> NhomHocPhans { get; set; }
        public DbSet<BuoiThucHanh> BuoiThucHanhs { get; set; }
        public DbSet<HocKyNamHoc> HocKyNamHocs { get; set; }
        public DbSet<GiangDay> GiangDays { get; set; }
        public DbSet<Tuan> Tuans { get; set; }
        public DbSet<Buoi> Buois { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<LichThucHanh> LichThucHanhs { get; set; }
        public DbSet<HocPhanPhuHop> HocPhanPhuHops { get; set; }
        public DbSet<NgayNghi> NgayNghis { get; set; }
    }
}
