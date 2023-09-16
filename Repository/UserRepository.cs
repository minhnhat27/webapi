using Microsoft.EntityFrameworkCore;
using MyWebAPI.Data;
using MyWebAPI.Models;
using MyWebAPI.ViewModel;

namespace MyWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _db;
        public UserRepository(MyDbContext db)
        {
            _db = db;
        }
        public async Task<GiangVien?> GetById(string id)
        {
            return await _db.GiangViens.SingleOrDefaultAsync(e => e.MSCB == id);
        }
        public async Task<string> Login(Login login)
        {
            var user = await _db.GiangDays.SingleOrDefaultAsync(e => e.MSCB == login.MSCB && e.Password == login.Password);
            if (user != null)
            {

            }
        }
    }
}
