using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Data;
using webapi.Models;
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

        public AccountService(SignInManager<GiangVien> signinManager, UserManager<GiangVien> userManager, 
            MyDbContext dbContext, IConfiguration config, ISendMailService mailService)
        {
            _signinManager = signinManager;
            _dbContext = dbContext;
            _config = config;
            _userManager = userManager;
            _mailService = mailService;
        }

        public async Task<JwtResponse> LoginAsync(LoginRequest login)
        {
            var result = await _signinManager.PasswordSignInAsync(login.Username, login.Password, false, false);

            GiangVien? user = new GiangVien();
            if (!result.Succeeded)
            {
                var userTemp = await _userManager.FindByEmailAsync(login.Username);
                if (userTemp == null)
                {
                    return new JwtResponse
                    {
                        Success = false
                    };
                }
                var resultId = await _signinManager.PasswordSignInAsync(userTemp.UserName!, login.Password, false, false);
                if (!resultId.Succeeded)
                {
                    return new JwtResponse
                    {
                        Success = false
                    };
                }
                user = userTemp;
            }
            else
            {
                user = await _userManager.FindByIdAsync(login.Username);
            }
            if(user == null)
            {
                return new JwtResponse
                {
                    Success = false
                };
            }

            var userRole = await _userManager.GetRolesAsync(user);
            var auth = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
                new Claim(ClaimTypes.Name, user.HoTen!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!)
            };
            foreach (var role in userRole)
            {
                auth.Add(new Claim(ClaimTypes.Role, role));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SerectKey"]!));
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: auth,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtResponse
            {
                Success = true,
                UserId = user.UserName,
                Name = user.HoTen,
                Roles = userRole.ToList(),
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
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
                        Success = false
                    };
                }
                var provider = "GoogleProvider";

                var existed = await _userManager.FindByLoginAsync(provider, payload.Subject);
                if (existed == null)
                {
                    await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, payload.Subject, "Google"));
                }
                
                var result = await _signinManager.ExternalLoginSignInAsync(provider, payload.Subject, false);
                if(!result.Succeeded)
                {
                    return new JwtResponse
                    {
                        Success = false
                    };
                }


                var userRole = await _userManager.GetRolesAsync(user);
                var auth = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName!),
                    new Claim(ClaimTypes.Name, user.HoTen!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!)
                };
                foreach (var role in userRole)
                {
                    auth.Add(new Claim(ClaimTypes.Role, role));
                }

                var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SerectKey"]!));
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.UtcNow.AddHours(1),
                    claims: auth,
                    signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
                );
                return new JwtResponse
                {
                    Success = true,
                    UserId = user.UserName,
                    Name = user.HoTen,
                    Roles = userRole.ToList(),
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
                };

            }
            catch
            {
                return new JwtResponse
                {
                    Success = false
                };
            }
        }
        public async Task<ApiResponse> sendToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "User doesn't exist"
                };
            }

            Random rd = new Random();
            var token = rd.Next(100011, 999988);
            user.ResetCode = token;
            user.ResetCodeExpiresAt = DateTime.UtcNow.AddMinutes(30);
            await _dbContext.SaveChangesAsync();

            var sub = "Yêu cầu khôi phục mật khẩu";
            var body = "<table>" +
                        "<tr><td>Bạn nhận được email này vì chúng tôi đã nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn.</td></tr>" +
                        $"<tr><td><h2>{token}</h2></td></tr>" +
                        "<tr><td>Mã xác minh sẽ hết hạn sau 30 phút.</td></tr>" +
                        "<tr><td>Nếu bạn không yêu cầu mã, bạn có thể bỏ qua tin nhắn này.</td></tr>" +
                        "<tr><td style='padding-top:1rem'><i>Đây là email được tạo tự động. Vui lòng không trả lời thư này.</i></td></tr>" +
                       "</table>";

            await _mailService.SendEmailAsync(email, sub, body);
            return new ApiResponse
            {
                Success = true
            };
        }

        public async Task<ApiResponse> checkToken(CheckTokenRequest checkTokenRequest)
        {
            var user = await _userManager.FindByEmailAsync(checkTokenRequest.Email);
            if (user == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "User doesn't exist"
                };
            }
            if (checkTokenRequest.Token == null)
            {
                return new ApiResponse
                {
                    Success = false
                };
            }
            var result = checkTokenRequest.Email.Equals(user.Email) &&
                            checkTokenRequest.Token.Equals(user.ResetCode.ToString()) &&
                                user.ResetCodeExpiresAt > DateTime.UtcNow;
            if (result)
            {
                return new ApiResponse
                {
                    Success = true,
                };
            }
            else
            {
                return new ApiResponse
                {
                    Success = false
                };
            }
        }

        public async Task<ApiResponse> changePassword(CheckTokenRequest checkTokenRequest)
        {
            
            if (checkTokenRequest.NewPassword == null)
            {
                return new ApiResponse
                {
                    Success = false
                };
            }
            
            var user = await _userManager.FindByEmailAsync(checkTokenRequest.Email);
            if (user == null)
            {
                return new ApiResponse
                {
                    Success = false
                };
            }
            var result = checkTokenRequest.Email.Equals(user.Email) &&
                            checkTokenRequest.Token.Equals(user.ResetCode.ToString());
            if (!result)
            {
                return new ApiResponse
                {
                    Success = false,
                };
            }

            user.ResetCode = null;
            user.ResetCodeExpiresAt = null;
            //var token = _mailService.getToken().ToString();
            //await _userManager.RemovePasswordAsync(user);
            //await _userManager.AddPasswordAsync(user, token);
            //await _userManager.ChangePasswordAsync(user, token, forgetPassword.newPassword);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resultReset = await _userManager.ResetPasswordAsync(user, token, checkTokenRequest.NewPassword);
            await _userManager.UpdateSecurityStampAsync(user);

            await _dbContext.SaveChangesAsync();

            if (resultReset.Succeeded)
            {
                return new ApiResponse
                {
                    Success = true,
                    Message = "Success"
                };
            }
            else
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Fail"
                };
            }
        }
    }
}
