using MyWebAPI.Data.ViewModels;
using MyWebAPI.Data.ViewModels.Schedule;
using MyWebAPI.Models;

namespace MyWebAPI.Repository
{
    public interface IScheduleRepository
    {
        Task<LichGiangDayVM> getAllCourseGroupofLecturer(string MSCB);
        Task<ApiResponse> saveSchedule(LichThucHanhVM lichThucHanh);
        Task<ApiResponse> updateOnSchedule(LichThucHanhVM lichThucHanh);
        Task<ViewSchedule> getPracticeSchedule();
    }
}
