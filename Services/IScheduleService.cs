using webapi.ViewModels.Request;
using webapi.ViewModels.Response;
using webapi.ViewModels.Schedule;

namespace webapi.Services
{
    public interface IScheduleService
    {
        Task<LichGiangDayVM> getAllCourseGroupofLecturer(string MSCB);
        Task<ApiResponse> saveSchedule(string mscb, LichThucHanhVM lichThucHanh);
        Task<ApiResponse> updateOnSchedule(string mscb, OnScheduleRequest lichThucHanh);
        Task<ScheduleResponse> getSchedule(int w);
        Task<ScheduleResponse> getSchedule();
    }
}
