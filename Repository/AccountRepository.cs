using Microsoft.AspNetCore.Identity;
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
        private readonly MyDbContext _dbContext;
        private readonly IConfiguration _config;
        public AccountRepository(SignInManager<GiangVien> signinManager, MyDbContext dbContext, IConfiguration config)
        {
            _signinManager = signinManager;
            _dbContext = dbContext;
            _config = config;
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

            var auth = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, login.MSCB),
                new Claim(ClaimTypes.Name, login.MSCB),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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

        //public async Task LogoutAsync()
        //{
        //    await _signinManager.SignOutAsync();
        //}

    }
}
