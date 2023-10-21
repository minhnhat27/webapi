using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Data.ViewModels;
using MyWebAPI.Repository;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public UserController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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

        [HttpPost("sendToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> sendToken([FromBody]string id)
        {
            var result = await _accountRepository.sendToken(id);
            if (!result.success)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost("checkToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> checkToken([FromBody] ForgetPasswordModel forgetPassword)
        {
            var result = await _accountRepository.checkToken(forgetPassword);
            if (!result.success)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost("changePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> changePassword([FromBody] ForgetPasswordModel forgetPassword)
        {
            var result = await _accountRepository.changePassword(forgetPassword);
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
