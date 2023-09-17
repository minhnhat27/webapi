using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Services;
using MyWebAPI.ViewModel;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(UserModel login)
        {
            try
            {
                TokenModel jwtToken = await _userService.Login(login);
                return Ok(new {jwtToken});
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Massage = ex.Message });
            }
        }
    }
}
