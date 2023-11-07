using DangKyPhongThucHanhTruongCNTT.Services;
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
        private readonly SendMailService _mailService;
        public AccountRepository(SignInManager<GiangVien> signinManager, UserManager<GiangVien> userManager, MyDbContext dbContext, IConfiguration config, SendMailService mailService)
        {
            _signinManager = signinManager;
            _dbContext = dbContext;
            _config = config;
            _userManager = userManager;
            _mailService = mailService;
        }
        private string getEmailById(string id)
        {
            var result = _dbContext.Users.SingleOrDefault(e => e.Id == id);
            if (result == null)
            {
                return string.Empty;
            }
            else
            {
                return result.Email!;
            }
        }
        private string getIdByEmail(string email)
        {
            var result = _dbContext.Users.SingleOrDefault(e => e.Email == email);
            if (result == null)
            {
                return string.Empty;
            }
            else
            {
                return result.Id;
            }
        }

        public async Task<ApiResponse> LoginAsync(LoginModel login)
        {
            var result = await _signinManager.PasswordSignInAsync(login.MSCB, login.Password, false, false);
            GiangVien? user;
            if (!result.Succeeded)
            {
                var uid = getIdByEmail(login.MSCB);
                var resultEmail = await _signinManager.PasswordSignInAsync(uid, login.Password, false, false);
                if (!resultEmail.Succeeded)
                {
                    return new ApiResponse
                    {
                        success = false,
                        message = "Username or Password incorrect."
                    };
                }
                user = await _userManager.FindByIdAsync(uid);
            }
            else
            {
                user = await _userManager.FindByIdAsync(login.MSCB);
            }
            
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
        public async Task<ApiResponse> sendToken(string id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(e => e.Id == id || e.Email == id);
            if (user == null)
            {
                return new ApiResponse
                {
                    success = false,
                    message = "User doesn't exist"
                };
            }

            var email = getEmailById(id);
            if (!string.IsNullOrEmpty(email))
            {
                id = email;
            }
            _mailService.setToken();
            var token = _mailService.getToken().ToString();
            var sub = "Yêu cầu khôi phục mật khẩu";
            await _mailService.SendEmailAsync(id, sub, token);
            
            return new ApiResponse
            {
                success = true,
                message = "Success",
                data = token
            };
        }

        public async Task<ApiResponse> checkToken(ForgetPasswordModel forgetPassword)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(e => e.Id == forgetPassword.id || e.Email == forgetPassword.id);
            if (user == null)
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Fail"
                };
            }
            if(forgetPassword.token == null)
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Fail"
                };
            }
            var result = forgetPassword.token.Equals(_mailService.getToken().ToString());
            if (result)
            {
                return new ApiResponse
                {
                    success = true,
                    message = "Success"
                };
            }
            else
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Fail"
                };
            }
        }
        public async Task<ApiResponse> changePassword(ForgetPasswordModel forgetPassword)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(e => e.Id == forgetPassword.id || e.Email == forgetPassword.id);
            if (forgetPassword.token == null || forgetPassword.newPassword == null)
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Fail"
                };
            }
            if (!forgetPassword.token.Equals(_mailService.getToken().ToString())){
                return new ApiResponse
                {
                    success = false,
                    message = "Fail"
                };
            }
            if (user == null)
            {
                return new ApiResponse
                {
                    success = false,
                    message = "Fail"
                };
            }
            else
            {
                //var token = _mailService.getToken().ToString();
                //await _userManager.RemovePasswordAsync(user);
                //await _userManager.AddPasswordAsync(user, token);
                //await _userManager.ChangePasswordAsync(user, token, forgetPassword.newPassword);

                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, forgetPassword.newPassword);

                if (result.Succeeded)
                {
                    return new ApiResponse
                    {
                        success = true,
                        message = "Success"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        success = false,
                        message = "Fail"
                    };
                }
            }
        }
    }
}
