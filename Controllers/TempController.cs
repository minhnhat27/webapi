using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Data;
using MyWebAPI.Models;
using MyWebAPI.Repository;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {

        private readonly UserManager<GiangVien> _userManager;
        private readonly IPasswordHasher<GiangVien> _passwordHasher = new PasswordHasher<GiangVien>();

        private readonly MyDbContext _dbContext;

        public TempController(UserManager<GiangVien> userManager, MyDbContext dbContext)
        {
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
    }
}
