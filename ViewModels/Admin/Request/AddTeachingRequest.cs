namespace webapi.ViewModels.Admin.Request
{
	public class AddTeachingRequest
	{
		public string UserId { get; set; }
		public string CourseGroups { get; set; }
		public int NumberOfSessions { get; set; }
	}
}
