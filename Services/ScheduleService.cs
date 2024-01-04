﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapi.ViewModels.Response;
using webapi.ViewModels.Schedule;
using webapi.ViewModels.Request;
using webapi.Data;
using webapi.Models;

namespace webapi.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly MyDbContext _Dbcontext;
        private readonly UserManager<GiangVien> _userManager;
        private readonly SignInManager<GiangVien> _signinManager;

        public ScheduleService(MyDbContext context,
            UserManager<GiangVien> userManager,
            SignInManager<GiangVien> signInManager)
        {
            _Dbcontext = context;
            _userManager = userManager;
            _signinManager = signInManager;
        }

        public async Task<LichGiangDayVM> getAllCourseGroupofLecturer(string MSCB)
        {
            var week = await _Dbcontext.Tuans.Select(e => e.SoTuan).ToListAsync();
            var currentSemester = await _Dbcontext.HocKyNamHocs.SingleAsync(e => e.HocKyHienTai == true);
            var dayoff = await _Dbcontext.NgayNghis.Select(e => e.ngayNghi).ToListAsync();

            var teaching = await _Dbcontext.GiangDays
                .Where(e => e.GiangVienId == MSCB && e.HK_NH == currentSemester.HK_NH && e.onSchedule == false)
                .Select(e => new GiangDayVM
                {
                    manhomhp = e.MaNhomHP!,
                    sttbuoithuchanh = e.BuoiThucHanhSTT
                }).OrderBy(e => e.manhomhp)
                .ToListAsync();

            var thuchanh = await _Dbcontext.LichThucHanhs.Where(
                e => e.GiangVienId == MSCB
                && e.HK_NH == currentSemester.HK_NH && e.GiangDay.onSchedule == true)
                .GroupBy(e => new
                {
                    e.GiangVienId,
                    e.HK_NH,
                    e.BuoiThucHanhSTT,
                    e.MaNhomHP
                })
                .Select(e => new LichThucHanhVM
                {
                    ngaythuchanh = e.First().NgayThucHanh,
                    buoi = e.First().TenBuoi,
                    sotuan = e.First().TuanSoTuan,
                    sttbuoithuchanh = e.First().BuoiThucHanhSTT,
                    manhomhp = e.First().MaNhomHP,
                    hknk = currentSemester.HK_NH
                }).ToListAsync();

            var practice = new LichGiangDayVM
            {
                hknh = currentSemester.HK_NH,
                giangDays = teaching!,
                ngaybatdauhk = currentSemester.NgayBatDau,
                sotuan = week.Count(),
                lichThucHanhs = thuchanh,
                ngaynghis = dayoff
            };

            return practice;
        }

        public async Task<ScheduleResponse> getSchedule(int w)
        {
            var week = await _Dbcontext.Tuans.Select(e => e.SoTuan).ToListAsync();
            var currentSemester = await _Dbcontext.HocKyNamHocs.SingleAsync(e => e.HocKyHienTai == true);
            var room = await _Dbcontext.Phongs.Select(e => e.SoPhong).ToListAsync();

            var thuchanh = await _Dbcontext.LichThucHanhs.Where(
                e => e.GiangDay.onSchedule == true
                && e.HK_NH == currentSemester.HK_NH && e.TuanSoTuan == w)
                .Select(e => new ViewLichThucHanhVM
                {
                    ngaythuchanh = e.NgayThucHanh,
                    buoi = e.TenBuoi,
                    tuan = e.TuanSoTuan,
                    sttbuoithuchanh = e.BuoiThucHanhSTT,
                    manhomhp = e.MaNhomHP,
                    phong = e.PhongSoPhong,
                    mscb = e.GiangVienId,
                    hoten = e.GiangDay.GiangVien.HoTen!,
                    tenhp = e.GiangDay.NhomHocPhan.HocPhan.TenHocPhan
                }).ToListAsync();

            var practice = new ScheduleResponse
            {
                ngaybatdau = currentSemester.NgayBatDau.AddDays((double)(w - 1) * 7),
                sotuan = week.Count(),
                lichThucHanhs = thuchanh,
                phong = room
            };

            return practice;
        }

        public async Task<ScheduleResponse> getSchedule()
        {
            var week = await _Dbcontext.Tuans.Select(e => e.SoTuan).ToListAsync();
            var currentSemester = await _Dbcontext.HocKyNamHocs.SingleAsync(e => e.HocKyHienTai == true);
            var room = await _Dbcontext.Phongs.Select(e => e.SoPhong).ToListAsync();

            var thuchanh = await _Dbcontext.LichThucHanhs.Where(
                e => e.GiangDay.onSchedule == true
                && e.HK_NH == currentSemester.HK_NH)
                .Select(e => new ViewLichThucHanhVM
                {
                    ngaythuchanh = e.NgayThucHanh,
                    buoi = e.TenBuoi,
                    tuan = e.TuanSoTuan,
                    sttbuoithuchanh = e.BuoiThucHanhSTT,
                    manhomhp = e.MaNhomHP,
                    phong = e.PhongSoPhong,
                    mscb = e.GiangVienId,
                    hoten = e.GiangDay.GiangVien.HoTen!,
                    tenhp = e.GiangDay.NhomHocPhan.HocPhan.TenHocPhan
                }).ToListAsync();

            var practice = new ScheduleResponse
            {
                hknh = currentSemester.HK_NH,
                ngaybatdau = currentSemester.NgayBatDau,
                sotuan = week.Count(),
                lichThucHanhs = thuchanh,
                phong = room
            };

            return practice;
        }

        private List<int> RoomArrange(LichThucHanhVM lichThucHanh)
        {
            try
            {
                var maHP = _Dbcontext.NhomHocPhans.Single(e => e.MaNhomHP == lichThucHanh.manhomhp).HocPhanMaHP;
                var soluongSV = _Dbcontext.NhomHocPhans.Single(e => e.MaNhomHP == lichThucHanh.manhomhp).SoLuongSV;
                var hocphanphuhop = _Dbcontext.HocPhanPhuHops.Where(e => e.MaHP == maHP).Select(e => new { e.MaHP, e.SoPhong, e.Phong.SoLuongMayTinh }).ToList();
                var LichThucHanh = _Dbcontext.LichThucHanhs.Where(
                    e => e.TenBuoi == lichThucHanh.buoi
                    && e.NgayThucHanh.Equals(lichThucHanh.ngaythuchanh)
                    && e.TuanSoTuan == lichThucHanh.sotuan
                    && e.GiangDay.onSchedule == true).Select(e => e.PhongSoPhong).ToList();

                int room = 0;
                int room1 = 0;
                int room2 = 0;
                bool isRoom = false;

                if (hocphanphuhop != null)
                {
                    foreach (var p in hocphanphuhop)
                    {
                        if (LichThucHanh.Contains(p.SoPhong))
                        {
                            isRoom = false;
                            continue;
                        }
                        else
                        {
                            if (p.SoLuongMayTinh >= soluongSV)
                            {
                                room = p.SoPhong;
                                isRoom = true;
                                break;
                            }
                            else
                            {
                                room = p.SoPhong;
                                if (room1 == 0)
                                {
                                    room1 = room;
                                    continue;
                                }
                                if (room2 == 0 && room1 != 0)
                                {
                                    room2 = room;
                                    isRoom = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (!isRoom)
                {
                    var phong = _Dbcontext.Phongs.Select(e => new { e.SoPhong, e.SoLuongMayTinh }).ToList();
                    foreach (var p in phong)
                    {
                        if (LichThucHanh.Contains(p.SoPhong))
                        {
                            isRoom = false;
                            continue;
                        }
                        else
                        {
                            if (p.SoLuongMayTinh >= soluongSV)
                            {
                                room = p.SoPhong;
                                isRoom = true;
                                break;
                            }
                        }
                    }
                    if (!isRoom)
                    {
                        foreach (var p in phong)
                        {
                            if (LichThucHanh.Contains(p.SoPhong))
                            {
                                isRoom = false;
                                continue;
                            }
                            else
                            {
                                if (p.SoLuongMayTinh >= soluongSV)
                                {
                                    room = p.SoPhong;
                                    isRoom = true;
                                    break;
                                }
                                else
                                {
                                    room = p.SoPhong;
                                    if (room1 == 0)
                                    {
                                        room1 = room;
                                        continue;
                                    }
                                    if (room2 == 0 && room1 != 0)
                                    {
                                        room2 = room;
                                        isRoom = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }
                if (room1 != 0 && room2 != 0)
                {
                    return new List<int> { room1, room2 };
                }
                else
                {
                    return new List<int> { room, room };
                }

            }
            catch
            {
                return new List<int>();
            }
        }

        public async Task<ApiResponse> saveSchedule(string mscb, LichThucHanhVM lichThucHanh)
        {
            try
            {
                var existedSchedule = await _Dbcontext.LichThucHanhs.Where(
                    e => e.GiangVienId == mscb
                    && e.HK_NH == lichThucHanh.hknk
                    && e.BuoiThucHanhSTT == lichThucHanh.sttbuoithuchanh
                    && e.MaNhomHP == lichThucHanh.manhomhp).ToListAsync();

                var updateGiangDay = await _Dbcontext.GiangDays.SingleOrDefaultAsync(
                       e => e.GiangVienId == mscb
                       && e.HK_NH == lichThucHanh.hknk
                       && e.BuoiThucHanhSTT == lichThucHanh.sttbuoithuchanh
                       && e.MaNhomHP == lichThucHanh.manhomhp);

                var room = RoomArrange(lichThucHanh);
                var model = new LichThucHanh
                {
                    NgayThucHanh = lichThucHanh.ngaythuchanh,
                    TenBuoi = lichThucHanh.buoi,
                    TuanSoTuan = lichThucHanh.sotuan,
                    HK_NH = lichThucHanh.hknk,
                    GiangVienId = mscb,
                    BuoiThucHanhSTT = lichThucHanh.sttbuoithuchanh,
                    MaNhomHP = lichThucHanh.manhomhp,
                    PhongSoPhong = room[0]
                };
                if (existedSchedule != null)
                {
                    foreach (var r in existedSchedule)
                    {
                        _Dbcontext.LichThucHanhs.Remove(r);
                    }
                }

                updateGiangDay!.onSchedule = true;
                if (room[0] == room[1])
                {
                    await _Dbcontext.LichThucHanhs.AddAsync(model);
                    await _Dbcontext.SaveChangesAsync();
                }
                else
                {
                    var model1 = new LichThucHanh
                    {
                        NgayThucHanh = lichThucHanh.ngaythuchanh,
                        TenBuoi = lichThucHanh.buoi,
                        TuanSoTuan = lichThucHanh.sotuan,
                        HK_NH = lichThucHanh.hknk,
                        GiangVienId = mscb,
                        BuoiThucHanhSTT = lichThucHanh.sttbuoithuchanh,
                        MaNhomHP = lichThucHanh.manhomhp,
                        PhongSoPhong = room[1]
                    };
                    await _Dbcontext.LichThucHanhs.AddAsync(model);
                    await _Dbcontext.LichThucHanhs.AddAsync(model1);
                    await _Dbcontext.SaveChangesAsync();
                }
                return new ApiResponse
                {
                    success = true,
                    message = "Saved Successfully"
                };
            }
            catch
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Save Failed"
                };
            }
        }

        public async Task<ApiResponse> updateOnSchedule(string mscb, OnScheduleRequest lichThucHanh)
        {
            try
            {
                var updateGiangDay = await _Dbcontext.GiangDays.Where(
                    e => e.GiangVienId == mscb
                    && e.HK_NH == lichThucHanh.hknk
                    && e.BuoiThucHanhSTT == lichThucHanh.sttbuoithuchanh
                    && e.MaNhomHP == lichThucHanh.manhomhp).ToListAsync();
                foreach (var r in updateGiangDay)
                {
                    r.onSchedule = false;
                }
                await _Dbcontext.SaveChangesAsync();
                return new ApiResponse
                {
                    success = true,
                    message = "Updated Successfully"
                };
            }
            catch
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Update Fsailed!"
                };
            }

        }
    }
}
