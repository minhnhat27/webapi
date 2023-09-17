using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebAPI.Data;
using MyWebAPI.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _db;
        private readonly IConfiguration _config;
        public UserRepository(MyDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public UserModel? GetById(string id)
        {
            var user = _db.GiangViens.SingleOrDefault(e => e.MSCB == id);
            if (user != null)
            {
                return new UserModel
                {
                    MSCB = user.MSCB,
                    Password = user.Password
                };
            }
            return null;
        }

        public async Task Update(UserModel user)
        {
            var userUpdate = await _db.GiangViens.SingleOrDefaultAsync(e => e.MSCB == user.MSCB);
            userUpdate!.Password = user.Password;
            await _db.SaveChangesAsync();
        }

        private string GenerateRefeshToken()
        {
            return "RandomString";
        }

        public List<UserModel> GetAll()
        {
            return _db.GiangViens.Select(e => new UserModel { MSCB = e.MSCB, Password = e.Password}).ToList();
        }

        public TokenModel generateToken(UserModel login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JWT:SerectKey"]!);
            
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, login.MSCB)
                    //new Claim(ClaimTypes.Name, login.HoTen)
                    //new Claim("Id", login.MSCB)
                    //role...
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDesciptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return new TokenModel
            {
                AccessToken = jwtToken,
                RefreshToken = GenerateRefeshToken(),
            };
        }

        public async Task<TokenModel> Login(UserModel login)
        {
            var user = await _db.GiangViens.SingleOrDefaultAsync(e => e.MSCB == login.MSCB && e.Password == login.Password);
            if (user != null)
            {
                return generateToken(login);
            }
            else
            {
                throw new Exception("Email or Password incorrect.");
            }
        }
    }
}
