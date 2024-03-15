using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Models;
using webapi.Services.Admin;
using webapi.ViewModels.Admin.Request;
using webapi.ViewModels.Response;
using webapi.ViewModels.Schedule;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {

        private readonly UserManager<GiangVien> _userManager;
        private readonly IPasswordHasher<GiangVien> _passwordHasher = new PasswordHasher<GiangVien>();
        private readonly ISemesterService _semesterService;

        private readonly MyDbContext _dbContext;

        public TempController(UserManager<GiangVien> userManager, MyDbContext dbContext, ISemesterService semesterService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _semesterService = semesterService;
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

        [HttpPost("Test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Test(AddTeachingRequest addTeachingRequest)
        {
			var preSemester = _semesterService.GetPreviousSemester();
			if (preSemester != null)
			{
				var courseId = _dbContext.NhomHocPhans
						.First(e => e.MaNhomHP == addTeachingRequest.CourseGroups)
						.HocPhanMaHP;

				var preTeaching = _dbContext.LichThucHanhs
						.Where(e => e.HK_NH == preSemester.HK_NH &&
									e.GiangVienId == addTeachingRequest.UserId &&
									e.GiangDay.onSchedule == true)
						.GroupBy(e => e.MaNhomHP)
						.Select(e => e.First().MaNhomHP);

				if (preTeaching.Count() == 1)
				{
					var schedule = _dbContext.LichThucHanhs
							.Where(e => e.HK_NH == preSemester.HK_NH &&
										e.GiangVienId == addTeachingRequest.UserId &&
										e.GiangDay.NhomHocPhan.HocPhanMaHP == courseId &&
										e.GiangDay.onSchedule == true);
					return Ok(schedule);
				}
			}
            return Ok();
		}
    }
}
