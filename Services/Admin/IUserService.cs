using webapi.ViewModels.Admin.Request;
using webapi.ViewModels.Admin.Response;
using webapi.ViewModels.Response;

namespace webapi.Services.Admin
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUser();
		Task<List<UserResponse>> GetActiveUsers();
		Task<List<string?>> GetRoles();
        Task<ApiResponse> AddUser(AddUserRequest addUserRequest);
        Task<ApiResponse> UpdateUser(UpdateUserRequest updateUserRequest);
        Task<ApiResponse> BlockUser(BlockUserRequest blockUserRequest);
        Task<ApiResponse> UnblockUser(string id);
    }
}
