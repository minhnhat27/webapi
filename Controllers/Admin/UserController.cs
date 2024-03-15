using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Services.Admin;
using webapi.ViewModels.Admin.Request;

namespace webapi.Controllers.Admin
{
    [Route("api/admin/user")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getAllUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllUser();
            return Ok(users);
        }

		[HttpGet("getActiveUsers")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetActiveUsers()
		{
			var users = await _userService.GetActiveUsers();
			return Ok(users);
		}

		[HttpGet("getRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _userService.GetRoles();
            return Ok(roles);
        }

        [HttpPost("addUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest addUserRequest)
        {
            var result = await _userService.AddUser(addUserRequest);

            if(result.Success)
            {
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("updateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest updateUserRequest)
        {
            var result = await _userService.UpdateUser(updateUserRequest);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("blockUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserRequest blockUserRequest)
        {
            var result = await _userService.BlockUser(blockUserRequest);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("unblockUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UnblockUser([FromBody] UserRequest userRequest)
        {
            var result = await _userService.UnblockUser(userRequest.Id);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
