using MyWebAPI.Models;
using MyWebAPI.ViewModel;

namespace MyWebAPI.Repository
{
    public interface IUserRepository
    {
        public Task<GiangVien?> GetById(string id);
        Task<string> Login(Login login);
    }
}
