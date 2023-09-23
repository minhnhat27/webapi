using MyWebAPI.Data.ViewModels.Schedule;

namespace MyWebAPI.Repository
{
    public interface IScheduleRepository
    {
        public Task<LichThucHanhVM> getAllCourseGroup();
    }
}
