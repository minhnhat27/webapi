using webapi.ViewModels;
using webapi.ViewModels.Request;
using webapi.ViewModels.Response;

namespace webapi.Services
{
    public interface IAccountService
    {
        Task<JwtResponse> LoginAsync(LoginModel login);
        Task<JwtResponse> ExternalLogin(LoginGoogleRequest googleRequest);
        Task<ApiResponse> sendToken(string id);
        Task<ApiResponse> checkToken(ForgetPasswordModel forgetPassword);
        Task<ApiResponse> changePassword(ForgetPasswordModel forgetPassword);
    }
}
