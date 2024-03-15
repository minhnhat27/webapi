using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.ViewModels.Request;
using webapi.ViewModels.Response;

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
        public async Task<IActionResult> Login([FromBody]LoginRequest model)
        {
            var result = await _accountService.LoginAsync(model);
            if (result.Success)
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
        [ProducesResponseType(StatusCodes.Status203NonAuthoritative)]
        public async Task<IActionResult> ExternalLogin([FromBody]LoginGoogleRequest googleRequest)
        {
            var result = await _accountService.ExternalLogin(googleRequest);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status203NonAuthoritative);
            }
        }

        [AllowAnonymous, HttpPost("sendToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendToken([FromBody] SendCodeRequest sendCode)
        {
            var result = await _accountService.sendToken(sendCode.email);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous, HttpPost("checkToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckToken([FromBody] CheckTokenRequest checkTokenRequest)
        {
            var result = await _accountService.checkToken(checkTokenRequest);
            if (result.Success)
            { 
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous, HttpPost("changePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody]CheckTokenRequest forgetPassword)
        {
            var result = await _accountService.changePassword(forgetPassword);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
