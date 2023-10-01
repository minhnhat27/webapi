using MyWebAPI.Data.ViewModels;

namespace MyWebAPI.Repository
{
    public interface IAccountRepository
    {
        public Task<ApiResponse> LoginAsync(LoginModel login);
        public Task<ApiResponse> getIdfromEmail(string email);
        public Task<ApiResponse> ExternalLogin(string email);
    }
}
