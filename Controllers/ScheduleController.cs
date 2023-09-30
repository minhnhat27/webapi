using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyWebAPI.Data.ViewModels;
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

        [HttpGet("GetTeachingofLecturer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeachingofLecturer() {
            var list = await _scheduleRepository.getAllCourseGroupofLecturer();            
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

        //[HttpPut("updateSchedule")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> updateSchedule([FromBody]LichThucHanhVM lichThucHanh)
        //{
        //    var result = await _scheduleRepository.updateSchedule(lichThucHanh);
        //    if (!result.success)
        //    {
        //        return BadRequest(result);
        //    }
        //    else
        //    {
        //        return Ok(result);
        //    }
        //}
    }
}
