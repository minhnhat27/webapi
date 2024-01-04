using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Data;
using webapi.Models;
using webapi.ViewModels;
using webapi.ViewModels.Request;
using webapi.ViewModels.Response;

namespace webapi.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<GiangVien> _signinManager;
        private readonly UserManager<GiangVien> _userManager;
        private readonly MyDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ISendMailService _mailService;
        public AccountService(SignInManager<GiangVien> signinManager, UserManager<GiangVien> userManager, MyDbContext dbContext, IConfiguration config, ISendMailService mailService)
        {
            _signinManager = signinManager;
            _dbContext = dbContext;
            _config = config;
            _userManager = userManager;
            _mailService = mailService;
        }
        private string GetEmailById(string id)
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
        private string GetIdByEmail(string email)
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

        public async Task<JwtResponse> LoginAsync(LoginModel login)
        {
            var result = await _signinManager.PasswordSignInAsync(login.username, login.password, false, false);
            GiangVien? user;
            if (!result.Succeeded)
            {
                var uid = GetIdByEmail(login.username);
                var resultEmail = await _signinManager.PasswordSignInAsync(uid, login.password, false, false);
                if (!resultEmail.Succeeded)
                {
                    return new JwtResponse
                    {
                        success = false
                    };
                }
                user = await _userManager.FindByIdAsync(uid);
            }
            else
            {
                user = await _userManager.FindByIdAsync(login.username);
            }

            var auth = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user!.Id),
                new Claim(ClaimTypes.Name, user.HoTen!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.MobilePhone, user.SoDienThoai!),
                new Claim(ClaimTypes.Role, "User")
            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SerectKey"]!));
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddDays(1),
                claims: auth,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtResponse
            {
                success = true,
                userId = user.Id,
                name = user.HoTen,
                accessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<JwtResponse> ExternalLogin(LoginGoogleRequest googleRequest)
        {
            try
            {
                var payload = GoogleJsonWebSignature.ValidateAsync(googleRequest.credential, new GoogleJsonWebSignature.ValidationSettings()).Result;
                var user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    return new JwtResponse
                    {
                        success = false
                    };
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user!.Id),
                    new Claim(ClaimTypes.Name, user.HoTen!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.MobilePhone, user.SoDienThoai!),
                    new Claim(ClaimTypes.Role, "User")
                };
                var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SerectKey"]!));
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.UtcNow.AddDays(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
                );
                return new JwtResponse
                {
                    success = true,
                    userId = user.Id,
                    name = user.HoTen,
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token)
                };

            }
            catch
            {
                return new JwtResponse
                {
                    success = false
                };
            }
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

            var email = GetEmailById(id);
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
            if (forgetPassword.token == null)
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
            if (!forgetPassword.token.Equals(_mailService.getToken().ToString()))
            {
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
