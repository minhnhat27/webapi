using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using webapi.ViewModels.Admin.Request;
using webapi.ViewModels.Admin.Response;
using webapi.ViewModels.Response;
using webapi.ViewModels.Schedule;

namespace webapi.Services.Admin
{
	public class TeachingService : ITeachingService
	{
		private readonly MyDbContext _dbContext;
		private readonly ISemesterService _semesterService;
		private readonly IScheduleService _scheduleService;
		public TeachingService(MyDbContext dbContext, ISemesterService semesterService, IScheduleService scheduleService)
		{
			_dbContext = dbContext;
			_semesterService = semesterService;
			_scheduleService = scheduleService;
		}
		public async Task<ApiResponse> AddTeachingSchedule(AddTeachingRequest addTeachingRequest)
		{
			try
			{
				var semester = _semesterService.GetCurrentSemester();
				for(var i = 1; i <= addTeachingRequest.NumberOfSessions; i++)
				{
					var model = new GiangDay
					{
						HK_NH = semester.HK_NH,
						GiangVienId = addTeachingRequest.UserId,
						BuoiThucHanhSTT = i,
						MaNhomHP = addTeachingRequest.CourseGroups,
					};
					await _dbContext.AddAsync(model);
				}
				await _dbContext.SaveChangesAsync();

				var preSemester = _semesterService.GetPreviousSemester();
				if(preSemester != null)
				{
					var courseId = _dbContext.NhomHocPhans
							.First(e => e.MaNhomHP == addTeachingRequest.CourseGroups)
							.HocPhanMaHP;

					var preSchedule = await _dbContext.LichThucHanhs
							.Where(e => e.HK_NH == preSemester.HK_NH &&
										e.GiangVienId == addTeachingRequest.UserId &&
										e.GiangDay.NhomHocPhan.HocPhanMaHP == courseId &&
										e.GiangDay.onSchedule == true)
							.GroupBy(e => e.MaNhomHP).ToListAsync();

					var currentTeaching = await _dbContext.GiangDays
							.Where(e => e.HK_NH == semester.HK_NH &&
										e.GiangVienId == addTeachingRequest.UserId &&
										e.NhomHocPhan.HocPhanMaHP == courseId)
							.GroupBy(e => e.MaNhomHP).ToListAsync();
					//currentTeaching.Count == 1 do đã lưu ở dòng 39

					if (preSchedule.Count == 1 && currentTeaching.Count == 1)
					{
						var schedule = await _dbContext.LichThucHanhs
							.Where(e => e.HK_NH == preSemester.HK_NH &&
										e.GiangVienId == addTeachingRequest.UserId &&
										e.GiangDay.NhomHocPhan.HocPhanMaHP == courseId &&
										e.GiangDay.onSchedule == true).ToListAsync();
						var d = (semester.NgayBatDau - preSemester.NgayBatDau).TotalDays;
						foreach (var t in schedule)
						{
							var model = new LichThucHanhVM
							{
								hknk = semester.HK_NH,
								buoi = t.TenBuoi,
								manhomhp = addTeachingRequest.CourseGroups,
								ngaythuchanh = t.NgayThucHanh.AddDays(d),
								sotuan = t.TuanSoTuan,
								sttbuoithuchanh = t.BuoiThucHanhSTT
							};
							var result = await _scheduleService.saveScheduleToPreSemester(t.GiangVienId, model);
							if (!result.Success)
							{
								return new ApiResponse
								{
									Success = false,
								};
							}
						}
					}
				}

				return new ApiResponse
				{
					Success = true,
				};
			}
			catch
			{
				return new ApiResponse
				{
					Success = false,
				};
			}
		}

		public async Task<List<TeachingResponse>> GetTeaching(string id)
		{
			var teaching = await _dbContext.GiangDays
				.Where(e => e.GiangVienId == id && e.HocKyNamHoc!.HocKyHienTai == true)
				.GroupBy(e => e.NhomHocPhan)
				.Select(e => new TeachingResponse
				{
					CourseGroup = e.First().MaNhomHP!,
					CourseName = e.First().NhomHocPhan.HocPhan.TenHocPhan,
					NumberOfSessions = e.ToList().Count(),
				})
				.ToListAsync();

			return teaching;
		}

		public async Task<ApiResponse> RemoveTeachingSchedule(string id)
		{
			try
			{
				var semester = _semesterService.GetCurrentSemester();
				var teaching = await _dbContext.GiangDays
					.Where(e => e.MaNhomHP == id && e.HK_NH == semester.HK_NH)
					.ToArrayAsync();
				_dbContext.RemoveRange(teaching);
				await _dbContext.SaveChangesAsync();
				return new ApiResponse
				{
					Success = true,
				};
			}
			catch
			{
				return new ApiResponse
				{
					Success = false,
				};
			}
		}
	}
}
