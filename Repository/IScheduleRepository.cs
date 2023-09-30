using MyWebAPI.Data.ViewModels;
using MyWebAPI.Data.ViewModels.Schedule;
using MyWebAPI.Models;

namespace MyWebAPI.Repository
{
    public interface IScheduleRepository
    {
        public Task<LichGiangDayVM> getAllCourseGroupofLecturer();
        public Task<ApiResponse> saveSchedule(LichThucHanhVM lichThucHanh);
        public Task<ApiResponse> updateOnSchedule(LichThucHanhVM lichThucHanh);
    }
}
