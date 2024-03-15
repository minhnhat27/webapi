using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using webapi.ViewModels.Admin.Response;
using webapi.ViewModels.Response;

namespace webapi.Services.Admin
{
    public class SemesterService : ISemesterService
    {
        private readonly MyDbContext _dbContext;
        public SemesterService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

		public async Task<ApiResponse> AddDateOff(DateTime dateTime)
		{
            try
            {
                var model = new NgayNghi
                {
                    ngayNghi = dateTime
                };
                await _dbContext.NgayNghis.AddAsync(model);
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

		public async Task<ApiResponse> RemoveDateOff(DateTime dateTime)
		{
			try
			{
                var model = await _dbContext.NgayNghis.FindAsync(dateTime);
                if(model == null)
                {
					return new ApiResponse
					{
						Success = false,
					};
				}
                _dbContext.NgayNghis.Remove(model);
				await _dbContext.SaveChangesAsync();
				return new ApiResponse
				{
					Success = true,
				};
			}
			catch(Exception ex)
			{
				return new ApiResponse
				{
					Success = false,
                    Message = ex.Message
				};
			}
		}

		public HocKyNamHoc GetCurrentSemester()
		{
            return _dbContext.HocKyNamHocs.Single(e => e.HocKyHienTai == true);
		}

		public async Task<List<NgayNghi>> GetDateOff()
		{
            return await _dbContext.NgayNghis.ToListAsync();
		}

		public HocKyNamHoc? GetPreviousSemester()
		{
			var current = _dbContext.HocKyNamHocs.Single(e => e.HocKyHienTai == true);
			var semesters = _dbContext.HocKyNamHocs.ToList();
			var indexCurrent = semesters.IndexOf(current);
            if(indexCurrent > 0)
            {
                return semesters[indexCurrent - 1];
            }
			return null;
		}

		public async Task<List<SemesterResponse>> GetSemesters()
        {
            //Lấy 2 học kỳ trước và sau học kỳ hiện tại
            var current = await _dbContext.HocKyNamHocs.SingleAsync(e => e.HocKyHienTai == true);
            var semesters = await _dbContext.HocKyNamHocs.ToListAsync();
            var indexCurrent = semesters.IndexOf(current);

            int index = 0, length = 5;
            if (semesters.Count < 5)
            {
                length = semesters.Count;
            }
            if (indexCurrent == 1)
            {
                index = indexCurrent - 1;
            }
            if (indexCurrent >= 2)
            {
                index = indexCurrent - 2;
            }

            return semesters.GetRange(index, length)
                .Select(e => new SemesterResponse
                {
                    Id = e.HK_NH,
                    StartDate = e.NgayBatDau,
                    CurrentSemester = e.HocKyHienTai
                }).ToList();
        }

        public async Task<ApiResponse> setCurrentSemester(string id)
        {
            var setSemester = await _dbContext.HocKyNamHocs.SingleOrDefaultAsync(e => e.HK_NH == id);
            if (setSemester != null)
            {
                var current = await _dbContext.HocKyNamHocs.SingleAsync(e => e.HocKyHienTai == true);
                current.HocKyHienTai = false;
                setSemester.HocKyHienTai = true;

                var semesters = await _dbContext.HocKyNamHocs.ToListAsync();
                var indexCurrent = semesters.IndexOf(setSemester);
                if (indexCurrent == (semesters.Count - 2) || indexCurrent == (semesters.Count - 1))
                {
                    var lastRecord = semesters.Last().HK_NH;
                    int year = int.Parse(lastRecord.Substring(0, 4)) + 101;
                    var newRecord1 = new HocKyNamHoc
                    {
                        HK_NH = year + "_HK1",
                        NgayBatDau = new DateTime(2000, 1, 1),
                        HocKyHienTai = false,
                    };
                    var newRecord2 = new HocKyNamHoc
                    {
                        HK_NH = year + "_HK2",
                        NgayBatDau = new DateTime(2000, 1, 1),
                        HocKyHienTai = false,
                    };
                    await _dbContext.AddAsync(newRecord1);
                    await _dbContext.AddAsync(newRecord2);
                    await _dbContext.SaveChangesAsync();
                }

                await _dbContext.SaveChangesAsync();
                return new ApiResponse
                {
                    Success = true,
                };
            }
            else
            {
                return new ApiResponse
                {
                    Success = false,
                };
            }
        }

        public async Task<ApiResponse> setStartDate(DateTime dateTime)
        {
            try
            {
                var current = await _dbContext.HocKyNamHocs.SingleAsync(e => e.HocKyHienTai == true);
                current.NgayBatDau = dateTime;
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
