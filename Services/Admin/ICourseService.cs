using webapi.ViewModels.Admin.Request;
using webapi.ViewModels.Admin.Response;

namespace webapi.Services.Admin
{
	public interface ICourseService
	{
		Task<List<CourseResponse>> GetCourses();
		Task<List<CourseGroupsResponse>> GetCourseGroups(string CourseId);
		
	}
}
