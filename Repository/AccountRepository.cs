using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebAPI.Data;
using MyWebAPI.Data.ViewModels;
using MyWebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWebAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SignInManager<GiangVien> _signinManager;
        private readonly UserManager<GiangVien> _userManager;
        private readonly MyDbContext _dbContext;
        private readonly IConfiguration _config;
        public AccountRepository(SignInManager<GiangVien> signinManager, UserManager<GiangVien> userManager, MyDbContext dbContext, IConfiguration config)
        {
            _signinManager = signinManager;
            _dbContext = dbContext;
            _config = config;
            _userManager = userManager;
        }

        public async Task<ApiResponse> LoginAsync(LoginModel login)
        {
            var result = await _signinManager.PasswordSignInAsync(login.MSCB, login.Password, false, false);
            if(!result.Succeeded)
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Username or Password incorrect."
                };
            }
            var user = await _userManager.FindByIdAsync(login.MSCB);
            var auth = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user!.Id),
                new Claim(ClaimTypes.Name, user.HoTen!),
                new Claim(ClaimTypes.Role, "Admin")
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SerectKey"]!));
            
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddDays(1),
                claims: auth,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
            );

            return new ApiResponse
            {
                success = true,
                message = "Login Success!",
                data = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<ApiResponse> ExternalLogin(string email)
        {
            if(email == null)
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Haven't External login"
                };
            }
            var user = await _dbContext.Users.SingleOrDefaultAsync(e => e.Email == email);
            if (user == null)
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Email of user does not exist."
                };
            }
            await _signinManager.SignInAsync(user, false);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user!.Id),
                new Claim(ClaimTypes.Name, user.HoTen!),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SerectKey"]!));
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddDays(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
            );
            return new ApiResponse
            {
                success = true,
                message = "External login success",
                data = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<ApiResponse> getIdfromEmail(string email)
        {
            var result = await _dbContext.Users.SingleOrDefaultAsync(e => e.Email == email);
            if (result == null)
            {
                return new ApiResponse
                {
                    success = false
                };
            }
            else
            {
                return new ApiResponse
                {
                    success = true,
                    data = result.Id
                };
            }
        }
    }
}
