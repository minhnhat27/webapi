using MyWebAPI.Models;
using MyWebAPI.ViewModel;

namespace MyWebAPI.Services
{
    public interface IUserService
    {
        Task<string> Login(Login login);
    }
}
