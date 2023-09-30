using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MyWebAPI.Data;
using MyWebAPI.Data.ViewModels;
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

        [Authorize]
        public async Task<LichGiangDayVM> getAllCourseGroupofLecturer()
        {
            var user = await _userManager.GetUserAsync(_signinManager.Context.User);
            var week = _Dbcontext.Tuans.Select(e => e.SoTuan);
            var currentSemester = _Dbcontext.HocKyNamHocs.Single(e => e.HocKyHienTai == true);
            
            var teaching = _Dbcontext.GiangDays
                .Where(e => e.GiangVienId == user!.Id && e.HK_NH == currentSemester.HK_NH)
                .Select(e => new GiangDayVM
                {
                    manhomhp = e.MaNhomHP!,
                    sttbuoithuchanh = e.BuoiThucHanhSTT,
                    onSchedule = e.onSchedule
                }).OrderBy(e => e.manhomhp)
                .ToList();

            //var thuchanh = from lichs in _Dbcontext.LichThucHanhs
            //         join giangdays in _Dbcontext.GiangDays
            //         on new { lichs.GiangVienId, lichs.HK_NH, lichs.MaNhomHP, lichs.BuoiThucHanhSTT }
            //         equals new { giangdays.GiangVienId, giangdays.HK_NH, giangdays.MaNhomHP, giangdays.BuoiThucHanhSTT }
            //         select new
            //         {
            //             ngaythuchanh = lichs.NgayThucHanh,
            //             buoi = lichs.TenBuoi,
            //             sotuan = lichs.TuanSoTuan,
            //             sttbuoithuchanh = lichs.BuoiThucHanhSTT,
            //             manhomhp = lichs.MaNhomHP,
            //             onSchedule = giangdays.onSchedule
            //         };

            var thuchanh = _Dbcontext.LichThucHanhs.Where(
                e => e.GiangVienId == user!.Id
                && e.HK_NH == currentSemester.HK_NH
            ).Select(e => new LichThucHanhVM
            {
                ngaythuchanh = e.NgayThucHanh,
                buoi = e.TenBuoi,
                sotuan = e.TuanSoTuan,
                sttbuoithuchanh = e.BuoiThucHanhSTT,
                manhomhp = e.MaNhomHP,
                onSchedule = e.GiangDay.onSchedule
            }).ToList();

            var practice = new LichGiangDayVM
            {
                mscb = user!.Id,
                hknh = currentSemester.HK_NH,
                giangDays = teaching!,
                ngaybatdauhk = currentSemester.NgayBatDau,
                sotuan = week.Count(),
                lichThucHanhs = thuchanh
            };

            return practice;
        }

        [Authorize]
        public async Task<ApiResponse> saveSchedule(LichThucHanhVM lichThucHanh)
        {
            var removeModel = _Dbcontext.LichThucHanhs.SingleOrDefault(
                    e => e.GiangVienId == lichThucHanh.mscb
                    && e.HK_NH == lichThucHanh.hknk
                    && e.BuoiThucHanhSTT == lichThucHanh.sttbuoithuchanh
                    && e.MaNhomHP == lichThucHanh.manhomhp);

            var model = new LichThucHanh
            {
                NgayThucHanh = lichThucHanh.ngaythuchanh,
                TenBuoi = lichThucHanh.buoi,
                TuanSoTuan = lichThucHanh.sotuan,
                HK_NH = lichThucHanh.hknk,
                GiangVienId = lichThucHanh.mscb,
                BuoiThucHanhSTT = lichThucHanh.sttbuoithuchanh,
                MaNhomHP = lichThucHanh.manhomhp
            };

            var updateGiangDay = _Dbcontext.GiangDays.SingleOrDefault(
                   e => e.GiangVienId == lichThucHanh.mscb
                   && e.HK_NH == lichThucHanh.hknk
                   && e.BuoiThucHanhSTT == lichThucHanh.sttbuoithuchanh
                   && e.MaNhomHP == lichThucHanh.manhomhp);

            if (removeModel != null )
            {
                _Dbcontext.LichThucHanhs.Remove(removeModel);
                await _Dbcontext.LichThucHanhs.AddAsync(model);
                updateGiangDay!.onSchedule = true;
                await _Dbcontext.SaveChangesAsync();
                return new ApiResponse
                {
                    success = true,
                    message = "Update LichThucHanh success!"
                };
            }

            try
            {
                await _Dbcontext.LichThucHanhs.AddAsync(model);
                updateGiangDay!.onSchedule = true;
                await _Dbcontext.SaveChangesAsync();
                return new ApiResponse
                {
                    success = true,
                    message = "Save LichThucHanh success!"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Có lỗi xảy ra! "+ ex.Message + ex.InnerException
                };
            }
        }

        [Authorize]
        public async Task<ApiResponse> updateOnSchedule(LichThucHanhVM lichThucHanh)
        {
            var updateGiangDay = _Dbcontext.GiangDays.SingleOrDefault(
                    e => e.GiangVienId == lichThucHanh.mscb
                    && e.HK_NH == lichThucHanh.hknk
                    && e.BuoiThucHanhSTT == lichThucHanh.sttbuoithuchanh
                    && e.MaNhomHP == lichThucHanh.manhomhp);

            if (updateGiangDay != null)
            {
                updateGiangDay.onSchedule = false;
                await _Dbcontext.SaveChangesAsync();
                return new ApiResponse
                {
                    success = true,
                    message = "Update GiangDay success!"
                };
            }
            else
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Update GiangDay fail!"
                };
            }
            
        }

        //[Authorize]
        //public async Task<ApiResponse> updateSchedule(LichThucHanhVM lichThucHanh, LichThucHanh updateLichThucHanh)
        //{
        //    try
        //    {
        //        updateLichThucHanh.NgayThucHanh = lichThucHanh.ngaythuchanh;
        //        //updateLichThucHanh.SoPhong = lichThucHanh.sophong;
        //        updateLichThucHanh.TenBuoi = lichThucHanh.buoi;
        //        updateLichThucHanh.TuanSoTuan = lichThucHanh.sotuan;
        //        await _Dbcontext.SaveChangesAsync();
        //        return new ApiResponse
        //        {
        //            success = true,
        //            message = "Update Success"
        //        };
        //    }
        //    catch(Exception ex)
        //    {
        //        return new ApiResponse
        //        {
        //            success = false,
        //            message = "Update Fail" + ex.Message + ex.InnerException
        //        };
        //    }
        //}
    }
}
