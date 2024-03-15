using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.ViewModels.Admin.Request;
using webapi.ViewModels.Admin.Response;

namespace webapi.Services.Admin
{
	public class CourseService : ICourseService
	{
		private readonly MyDbContext _dbContext;
		public CourseService(MyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<CourseResponse>> GetCourses()
		{
			return await _dbContext.HocPhans
				.Select(e => new CourseResponse
				{
					Id = e.MaHP,
					CourseName = e.TenHocPhan
				}).ToListAsync();
		}

		public async Task<List<CourseGroupsResponse>> GetCourseGroups(string CourseId)
		{
			var teaching = await _dbContext.GiangDays
				.Where(e => e.HocKyNamHoc!.HocKyHienTai == true)
				.GroupBy(e => e.MaNhomHP)
				.Select(e => e.FirstOrDefault()!.MaNhomHP)
				.ToListAsync();

			var result = await _dbContext.NhomHocPhans
				.Where(e => e.HocPhan.MaHP == CourseId && !teaching.Contains(e.MaNhomHP))
				.Select(e => new CourseGroupsResponse
				{
					Id = e.MaNhomHP,
					NumberOfStudent = e.SoLuongSV
				})
				.ToListAsync();

			return result;
		}
	}
}
