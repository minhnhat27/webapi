using MyWebAPI.Models;
using MyWebAPI.ViewModel;

namespace MyWebAPI.Services
{
    public interface IUserService
    {
        Task<GiangVien?> Register(GiangVien giangVien);
        Task<string> Login(Login login);
    }
}
