using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyWebAPI.Data;
using MyWebAPI.Data.ViewModels;
using MyWebAPI.Models;
using MyWebAPI.Repository;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<GiangVien> _userManager;
        private readonly IPasswordHasher<GiangVien> _passwordHasher = new PasswordHasher<GiangVien>();

        private readonly MyDbContext _dbContext;
        public UserController(IAccountRepository accountRepository, UserManager<GiangVien> userManager, MyDbContext dbContext)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet("GetAllCourse")]
        public IActionResult GetAllCourse()
        {
            var list = _dbContext.HocPhans.ToList();
            return Ok(list);
        }

        [HttpPut("UpdatePassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin123");
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.NormalizedEmail = user.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var result = await _accountRepository.LoginAsync(model);
            if(!result.success)
            {
                return Unauthorized(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [AllowAnonymous]
        [HttpPost("ExternalLogin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExternalLogin([FromBody]string email)
        {
            var result = await _accountRepository.ExternalLogin(email);
            if (!result.success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost("getIdfromEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getIdfromEmail([FromBody]string email)
        {
            var result = await _accountRepository.getIdfromEmail(email);
            if (!result.success)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
        }

        //[Authorize]
        //[HttpGet("SignOut")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> Logout()
        //{   
        //    await _accountRepository.LogoutAsync();
        //    return Ok();
        //}
    }
}
