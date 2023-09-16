using MyWebAPI.Data;
using MyWebAPI.Repository;
using MyWebAPI.ViewModel;

namespace MyWebAPI.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Login(Login login)
        {
            return await _userRepository.Login(login);
        }
    }
}
