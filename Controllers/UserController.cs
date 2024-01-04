using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webapi.Services;
using webapi.ViewModels;
using webapi.ViewModels.Request;

namespace webapi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous, HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _accountService.LoginAsync(model);
            if (result.success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous, HttpPost("externalLogin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExternalLogin([FromBody] LoginGoogleRequest googleRequest)
        {
            var result = await _accountService.ExternalLogin(googleRequest);
            if (result.success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous, HttpPost("sendToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendToken([FromBody] string id)
        {
            var result = await _accountService.sendToken(id);
            if (!result.success)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [AllowAnonymous, HttpPost("checkToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckToken([FromBody] ForgetPasswordModel forgetPassword)
        {
            var result = await _accountService.checkToken(forgetPassword);
            if (!result.success)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [AllowAnonymous, HttpPost("changePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] ForgetPasswordModel forgetPassword)
        {
            var result = await _accountService.changePassword(forgetPassword);
            if (!result.success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
