using MyWebAPI.Data.ViewModels;

namespace MyWebAPI.Repository
{
    public interface IAccountRepository
    {
        public Task<ApiResponse> LoginAsync(LoginModel login);
        //public Task LogoutAsync();
    }
}
