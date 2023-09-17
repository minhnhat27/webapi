using MyWebAPI.Data;
using MyWebAPI.Repository;
using MyWebAPI.ViewModel;

namespace MyWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<TokenModel> Login(UserModel login)
        {
            return await _userRepository.Login(login);
        }
    }
}
