using MyWebAPI.Data.ViewModels;
using MyWebAPI.Data.ViewModels.Schedule;
using MyWebAPI.Models;

namespace MyWebAPI.Repository
{
    public interface IScheduleRepository
    {
        public Task<LichGiangDayVM> getAllCourseGroupofLecturer(string MSCB);
        public Task<ApiResponse> saveSchedule(LichThucHanhVM lichThucHanh);
        public Task<ApiResponse> updateOnSchedule(LichThucHanhVM lichThucHanh);
        public List<int> roomArrange(LichThucHanhVM lichThucHanh);
        public Task<ViewSchedule> getPracticeSchedule();
    }
}
