using MyWebAPI.ViewModel;

namespace MyWebAPI.Services
{
    public interface IUserService
    {
        Task<TokenModel> Login(UserModel login);
    }
}
