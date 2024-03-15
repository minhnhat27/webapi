using webapi.ViewModels.Admin.Request;
using webapi.ViewModels.Admin.Response;
using webapi.ViewModels.Response;

namespace webapi.Services.Admin
{
	public interface ITeachingService
	{
		Task<ApiResponse> AddTeachingSchedule(AddTeachingRequest addTeachingRequest);
		Task<List<TeachingResponse>> GetTeaching(string id);
		Task<ApiResponse> RemoveTeachingSchedule(string id);
	}
}
