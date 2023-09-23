using Microsoft.AspNetCore.Identity;
using MyWebAPI.Data;
using MyWebAPI.Data.ViewModels.Schedule;
using MyWebAPI.Models;

namespace MyWebAPI.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly MyDbContext _Dbcontext;
        private readonly UserManager<GiangVien> _userManager;
        private readonly SignInManager<GiangVien> _signinManager;

        public ScheduleRepository(MyDbContext context, UserManager<GiangVien> userManager, SignInManager<GiangVien> signInManager)
        {
            _Dbcontext = context;
            _userManager = userManager;
            _signinManager = signInManager;
        }

        public async Task<LichThucHanhVM> getAllCourseGroup()
        {
            var user = await _userManager.GetUserAsync(_signinManager.Context.User);
            var semesterDay = _Dbcontext.HocKyNamHocs.Single(e => e.HocKyHienTai == true);
            var courseGroupId = _Dbcontext.GiangDays
                    .Where(e => e.GiangVienId == user!.Id && e.HocKyNamHoc!.HK_NH == semesterDay.HK_NH)
                    .Select(e => e.MaNhomHP).Distinct();

            var numberofSession = _Dbcontext.BuoiThucHanhs.Select(e => e);

            return new LichThucHanhVM
            {
                ngay = "s"
            };
        }
    }
}
