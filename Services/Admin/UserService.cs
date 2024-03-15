using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System.Data;
using webapi.Data;
using webapi.Models;
using webapi.ViewModels.Admin.Request;
using webapi.ViewModels.Admin.Response;
using webapi.ViewModels.Response;

namespace webapi.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<GiangVien> _userManager;
        private readonly ISendMailService _sendMailService;

        public UserService(MyDbContext dbContext, UserManager<GiangVien> userManager, ISendMailService sendMailService) { 
            _dbContext = dbContext;
            _userManager = userManager;
            _sendMailService = sendMailService;
        }

        private string GenerateRandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            string randomString = "";
            for (int i = 0; i <= length; i++)
            {
                int randomIndex = random.Next(0, chars.Length);
                randomString += chars[randomIndex];

            }
            return randomString;
        }
        public async Task<List<UserResponse>> GetAllUser()
        {
            var user = await _userManager.Users.ToListAsync();
            var userRes = new List<UserResponse>();
            foreach (var e in user)
            {
                var roles = (await _userManager.GetRolesAsync(e)).ToList();
                string dateEnd = "";
                if (e.LockoutEnd.HasValue)
                {
                    if(e.LockoutEnd.Value.ToLocalTime().Year == 9999)
                    {
                        dateEnd = "Vô hạn";
                    }
                    else
                    {
                        dateEnd = e.LockoutEnd.Value.ToString("dd/MM/yyyy");
                    }
                }
                var u = new UserResponse
                {
                    Id = e.UserName!,
                    Email = e.Email!,
                    EmailConfirmed = e.EmailConfirmed,
                    FullName = e.HoTen!,
                    PhoneNumber = e.PhoneNumber!,
                    LockoutEnabled = e.LockoutEnd > DateTime.UtcNow,
                    LockoutEnd = dateEnd,
                    Roles = roles
                };
                userRes.Add(u);
            }

            return userRes;
        }

        public async Task<List<string?>> GetRoles()
        {
            return await _dbContext.Roles.Select(e => e.Name).ToListAsync();
        }
        public async Task<ApiResponse> AddUser(AddUserRequest addUserRequest)
        {
            var checkEmail = await _userManager.FindByEmailAsync(addUserRequest.Email);
            if (checkEmail != null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Data = "Email đã tồn tại"
                };
            }
            var u = await _dbContext.Users.OrderBy(e => e.Id).LastOrDefaultAsync();
            string newId;
            if (u == null)
                newId = "1000";
            else newId = (int.Parse(u.Id) + 1).ToString();

            var user = new GiangVien()
            {
                Id = newId,
                Email = addUserRequest.Email,
                NormalizedEmail = addUserRequest.Email,
                UserName = newId,
                HoTen = addUserRequest.FullName,
                PhoneNumber = addUserRequest.PhoneNumber,
                EmailConfirmed = false,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                LockoutEnd = DateTime.UtcNow,
                LockoutEnabled = false,
            };
            user.SecurityStamp = Guid.NewGuid().ToString();
            string passwordRandom = GenerateRandomString(8);
            var result = await _userManager.CreateAsync(user, passwordRandom);

            try
            {
                if (addUserRequest.Roles.IsNullOrEmpty())
                {
                    await _userManager.AddToRolesAsync(user, new List<string> { "User" });
                }
                else await _userManager.AddToRolesAsync(user, addUserRequest.Roles!);
            }
            catch
            {
                return new ApiResponse
                {
                    Success = false
                };
            }

            string roles = "";
            if (addUserRequest.Roles.IsNullOrEmpty())
            {

                roles += "User";
            }
            else
            {

                foreach (var role in addUserRequest.Roles)
                {
                    roles += role + ", ";
                }
            }

            var sub = "Tài khoản của bạn";
            var body = "<table>" +
                        "<tr><td>Bạn nhận được email này vì bạn đã được cấp tài khoản tại ...</td></tr>" +
                        "<tr><td></td></tr>" +
                        "<tr><td>Thông tin tài khoản:</td></tr>" +
                        $"<tr><td>Họ và tên: <b>{user.HoTen}</b></td></tr>" +
                        $"<tr><td>Mã cán bộ: <b>{user.UserName}</b></td></tr>" +
                        $"<tr><td>Địa chỉ Email: <b>{user.Email}</b></td></tr>" +
                        $"<tr><td>Mật khẩu: <b>{passwordRandom}</b></td></tr>" +
                        $"<tr><td>Quyền truy cập: <b>{roles.Substring(0, roles.LastIndexOf(','))}</b></td></tr>" +
                        "<tr><td></td></tr>" +
                        "<tr><td><b>Vui lòng sử dụng Mã cán bộ, địa chỉ email hoặc tài khoản Google để đăng nhập.</b></td></tr>" +
                        "<tr><td style='padding-top:1rem'><i>Đây là email được tạo tự động. Vui lòng không trả lời thư này.</i></td></tr>" +
                       "</table>";
            await _sendMailService.SendEmailAsync(user.Email, sub, body);

            if (result.Succeeded)
            {
                return new ApiResponse
                {
                    Success = true
                };
            }
            else
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = result.ToString(),
                };
            }
        }
        public async Task<ApiResponse> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            var user = await _userManager.FindByIdAsync(updateUserRequest.Id);
            if (user == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Không tìm thấy người dùng"
                };
            }
            try
            {
                user.HoTen = updateUserRequest.FullName;
                user.PhoneNumber = updateUserRequest.PhoneNumber;
                if (!user.EmailConfirmed)
                {
                    user.Email = updateUserRequest.Email;
                }

				await _userManager.UpdateAsync(user);

                var roles = await _userManager.GetRolesAsync(user);
                var resulte = await _userManager.RemoveFromRolesAsync(user, roles);
                if (!updateUserRequest.Roles.IsNullOrEmpty())
                {
                    await _userManager.AddToRolesAsync(user, updateUserRequest.Roles!);
                }

                return new ApiResponse
                {
                    Success = true
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = ex.InnerException!.Message
                };
            }
        }

        public async Task<ApiResponse> BlockUser(BlockUserRequest blockUserRequest)
        {
            var user = await _userManager.FindByIdAsync(blockUserRequest.Id);
            if (user == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Không tìm thấy người dùng"
                };
            }
            try
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                var result = await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(blockUserRequest.Time));

                if (result.Succeeded)
                {
                    return new ApiResponse
                    {
                        Success = true
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = result.Errors.ToString()
                    };
                }
            }
            catch
            {
                return new ApiResponse
                {
                    Success = false
                };
            }
        }

        public async Task<ApiResponse> UnblockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Không tìm thấy người dùng"
                };
            }
            try
            {
                var result = await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.UtcNow));
                await _userManager.SetLockoutEnabledAsync(user, false);

                if (result.Succeeded)
                {
                    return new ApiResponse
                    {
                        Success = true
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = result.Errors.ToString()
                    };
                }
            }
            catch
            {
                return new ApiResponse
                {
                    Success = false
                };
            }
        }

		public Task<List<UserResponse>> GetActiveUsers()
		{
			return _userManager.Users
                .Where(e => e.LockoutEnabled == false)
                .Select(e => new UserResponse
			    {
				    Id = e.UserName!,
				    Email = e.Email!,
				    EmailConfirmed = e.EmailConfirmed,
				    FullName = e.HoTen!,
				    PhoneNumber = e.PhoneNumber!,
			    }).ToListAsync();
		}
	}
}
