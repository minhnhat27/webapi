using MyWebAPI.Data.ViewModels;

namespace MyWebAPI.Repository
{
    public interface IAccountRepository
    {
        Task<ApiResponse> LoginAsync(LoginModel login);
        Task<ApiResponse> ExternalLogin(string email);
        Task<ApiResponse> sendToken(string id);
        Task<ApiResponse> checkToken(ForgetPasswordModel forgetPassword);
        Task<ApiResponse> changePassword(ForgetPasswordModel forgetPassword);
    }
}
