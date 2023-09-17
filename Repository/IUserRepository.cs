using MyWebAPI.ViewModel;

namespace MyWebAPI.Repository
{
    public interface IUserRepository
    {
        public UserModel? GetById(string id);
        public List<UserModel> GetAll();
        public Task Update(UserModel user);
        Task<TokenModel> Login(UserModel login);
    }
}
