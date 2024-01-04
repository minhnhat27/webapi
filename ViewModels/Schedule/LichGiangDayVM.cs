namespace webapi.ViewModels.Schedule
{
    public class LichGiangDayVM
    {
        public List<GiangDayVM> giangDays { get; set; }
        public int sotuan { get; set; }
        public string hknh { get; set; }
        public DateTime ngaybatdauhk { get; set; }
        public List<LichThucHanhVM> lichThucHanhs { get; set; }
        public List<DateTime> ngaynghis { get; set; }
    }
}
