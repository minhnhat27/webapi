using MyWebAPI.Data.ViewModels;

namespace MyWebAPI.Repository
{
    public interface IAccountRepository
    {
        public Task<ApiResponse> LoginAsync(LoginModel login);
        public string getEmailfromId(string id);
        public Task<ApiResponse> ExternalLogin(string email);
        public Task<ApiResponse> sendToken(string id);
        public Task<ApiResponse> checkToken(ForgetPasswordModel forgetPassword);
        public Task<ApiResponse> changePassword(ForgetPasswordModel forgetPassword);
    }
}
