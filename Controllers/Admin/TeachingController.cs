using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Services.Admin;
using webapi.ViewModels.Admin.Request;

namespace webapi.Controllers.Admin
{
	[Route("api/admin/teaching")]
	[ApiController]
	//[Authorize(Roles = "Admin")]
	public class TeachingController : ControllerBase
	{
		private readonly ITeachingService _teachingService;
		private readonly ICourseService _courseService;
		public TeachingController(ITeachingService teachingService, ICourseService courseService)
		{
			_teachingService = teachingService;
			_courseService = courseService;
		}

		[HttpPost("getTeaching")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetTeaching(UserRequest userRequest)
		{
			var result = await _teachingService.GetTeaching(userRequest.Id);
			return Ok(result);
		}

		[HttpGet("getCourses")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCourses()
		{
			var result = await _courseService.GetCourses();
			return Ok(result);
		}

		[HttpPost("getCourseGroups")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCourseGroups(UserRequest userRequest)
		{
			var result = await _courseService.GetCourseGroups(userRequest.Id);
			return Ok(result);
		}

		[HttpPost("addTeaching")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> AddTeaching(AddTeachingRequest teachingRequest)
		{
			var result = await _teachingService.AddTeachingSchedule(teachingRequest);
			if(result.Success)
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost("removeTeaching")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> RemoveTeaching(UserRequest userRequest)
		{
			var result = await _teachingService.RemoveTeachingSchedule(userRequest.Id);
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
