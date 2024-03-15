using webapi.ViewModels.Request;
using webapi.ViewModels.Response;

namespace webapi.Services
{
    public interface IAccountService
    {
        Task<JwtResponse> LoginAsync(LoginRequest login);
        Task<JwtResponse> ExternalLogin(LoginGoogleRequest googleRequest);
        Task<ApiResponse> sendToken(string id);
        Task<ApiResponse> checkToken(CheckTokenRequest checkTokenRequest);
        Task<ApiResponse> changePassword(CheckTokenRequest checkTokenRequest);
    }
}
