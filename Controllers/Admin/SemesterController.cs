using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Services.Admin;
using webapi.ViewModels.Admin.Request;

namespace webapi.Controllers.Admin
{
    [Route("api/admin/semester")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _semesterService;
        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [HttpGet("getSemesters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSemesters()
        {
            var result = await _semesterService.GetSemesters();
            return Ok(result);
        }

		[HttpGet("getDateOff")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetDateOff()
		{
			var result = await _semesterService.GetDateOff();
			return Ok(result);
		}


		[HttpPost("addDateOff")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> AddDateOff(DateRequest dateRequest)
		{
			var result = await _semesterService.AddDateOff(dateRequest.Date);
			if (result.Success)
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost("removeDateOff")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> RemoveDateOff(DateRequest dateRequest)
		{
			var result = await _semesterService.RemoveDateOff(dateRequest.Date);
			if (result.Success)
			{
				return Ok();
			}
			else
			{
				return BadRequest(result.Message);
			}
		}


		[HttpPost("setCurrentSemester")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes. Status400BadRequest)]
        public async Task<IActionResult> SetCurrentSemester(UserRequest userRequest)
        {
            var result = await _semesterService.setCurrentSemester(userRequest.Id);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("setStartDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetStartDate(DateRequest dateRequest)
        {
            var result = await _semesterService.setStartDate(dateRequest.Date);
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
