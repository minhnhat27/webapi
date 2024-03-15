using webapi.Models;
using webapi.ViewModels.Admin.Request;
using webapi.ViewModels.Admin.Response;
using webapi.ViewModels.Response;

namespace webapi.Services.Admin
{
    public interface ISemesterService
    {
        HocKyNamHoc GetCurrentSemester();
		HocKyNamHoc? GetPreviousSemester();
		Task<List<SemesterResponse>> GetSemesters();
        Task<ApiResponse> setCurrentSemester(string id);
        Task<ApiResponse> setStartDate(DateTime dateTime);
        Task<List<NgayNghi>> GetDateOff();
		Task<ApiResponse> AddDateOff(DateTime dateTime);
		Task<ApiResponse> RemoveDateOff(DateTime dateTime);
	}
}
