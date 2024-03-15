namespace webapi.ViewModels.Admin.Response
{
    public class SemesterResponse
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public bool CurrentSemester { get; set; }
    }
}
