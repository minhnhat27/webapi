using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Data.ViewModels.Schedule;
using MyWebAPI.Repository;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _scheduleRepository;
        public ScheduleController(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        [HttpPost("GetTeachingofLecturer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeachingofLecturer([FromBody] string MSCB) {
            var list = await _scheduleRepository.getAllCourseGroupofLecturer(MSCB);
            return Ok(list);
        }

        [HttpPost("saveSchedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> saveSchedule([FromBody]LichThucHanhVM lichThucHanh)
        {
            var result = await _scheduleRepository.saveSchedule(lichThucHanh);
            if (!result.success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPut("updateOnSchedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> updateOnSchedule([FromBody]LichThucHanhVM lichThucHanh)
        {
            var result = await _scheduleRepository.updateOnSchedule(lichThucHanh);
            if (!result.success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("getPraticeSchedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> getPraticeSchedule()
        {
            var result = await _scheduleRepository.getPracticeSchedule();
            return Ok(result);
        }
    }
}
