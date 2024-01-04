using webapi.ViewModels.Schedule;

namespace webapi.ViewModels.Response
{
    public class ScheduleResponse
    {
        public string? hknh { get; set; }
        public int sotuan { get; set; }
        public DateTime ngaybatdau { get; set; }
        public List<ViewLichThucHanhVM> lichThucHanhs { get; set; }
        public List<int> phong { get; set; }
    }
}
