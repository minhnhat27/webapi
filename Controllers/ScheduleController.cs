using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Security.Claims;
using webapi.Services;
using webapi.ViewModels.Request;
using webapi.ViewModels.Schedule;

namespace webapi.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [Authorize, HttpGet("getTeachingofLecturer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTeachingofLecturer()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var list = await _scheduleService.getAllCourseGroupofLecturer(userId!);
                return Ok(list);
            }
            else return Unauthorized();
        }

        [Authorize, HttpPost("saveSchedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SaveSchedule([FromBody] LichThucHanhVM lichThucHanh)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _scheduleService.saveSchedule(userId, lichThucHanh);
            if (result.success)
            {
                return Ok(result.message);
            }
            else
            {
                return BadRequest(result.message);
            }
        }

        [Authorize, HttpPut("updateOnSchedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOnSchedule([FromBody] OnScheduleRequest lichThucHanh)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _scheduleService.updateOnSchedule(userId, lichThucHanh);
            if (result.success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous, HttpGet("getSchedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSchedule([FromQuery] int week = 1)
        {
            var result = await _scheduleService.getSchedule(week);
            return Ok(result);
        }

        [AllowAnonymous, HttpGet("download")]
        public async Task<IActionResult> downloadExcel()
        {
            var list = await _scheduleService.getSchedule();
            var stream = new MemoryStream();
            using (var excel = new ExcelPackage(stream))
                {
                    var toDay = list.ngaybatdau;
                    for (int i = 1; i <= list.sotuan; i++)
                    {
                        var wday = toDay;
                        var workSheet = excel.Workbook.Worksheets.Add("Tuần " + i);
                        workSheet.DefaultRowHeight = 15;
                        workSheet.DefaultColWidth = 20;
                        workSheet.Column(1).Width = 10;
                        workSheet.Column(2).Width = 10;
                        workSheet.Row(1).Height = 20;
                        workSheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        workSheet.Cells.Style.WrapText = true;

                        workSheet.Cells["A1:I1"].Merge = true;
                        workSheet.Cells["A1"].Value = "Lịch Thực Hành Trường CNTT & TT - " + list.hknh;
                        workSheet.Cells["A1"].Style.Font.Size = 14;
                        workSheet.Cells["A1"].Style.Font.Bold = true;
                        workSheet.Cells["A1"].Style.Font.Color.SetColor(ExcelIndexedColor.Indexed17);

                        workSheet.Cells["A2:A3"].Merge = true;
                        workSheet.Cells["A2"].Value = "Buổi";
                        workSheet.Cells["B2:B3"].Merge = true;
                        workSheet.Cells["B2"].Value = "Phòng";

                        workSheet.Cells["C2"].Value = "Thứ 2";
                        workSheet.Cells["D2"].Value = "Thứ 3";
                        workSheet.Cells["E2"].Value = "Thứ 4";
                        workSheet.Cells["F2"].Value = "Thứ 5";
                        workSheet.Cells["G2"].Value = "Thứ 6";
                        workSheet.Cells["H2"].Value = "Thứ 7";
                        workSheet.Cells["I2"].Value = "Chủ nhật";

                        workSheet.Cells["A4:A" + (list.phong.Count + 3)].Merge = true;
                        workSheet.Cells["A4:A" + (list.phong.Count + 3)].Value = "Sáng";
                        workSheet.Cells["A" + (list.phong.Count + 4) + ":A" + (list.phong.Count * 2 + 3)].Merge = true;
                        workSheet.Cells["A" + (list.phong.Count + 4) + ":A" + (list.phong.Count * 2 + 3)].Value = "Chiều";

                        for (int j = 1; j <= 7; j++)
                        {
                            workSheet.Cells[3, j + 2].Value = wday.ToString("dd/MM/yyyy");
                            wday = wday.AddDays(1);
                        }

                        int row = 4;
                        foreach (var item in list.phong)
                        {
                            workSheet.Cells["B" + row].Value = item;
                            wday = toDay;
                            for (int j = 1; j <= 7; j++)
                            {
                                foreach (var lich in list.lichThucHanhs)
                                {
                                    if (lich.phong == item && lich.ngaythuchanh.Equals(wday) && lich.buoi == "Morning")
                                    {
                                        workSheet.Cells[row, j + 2].Value = lich.manhomhp + Environment.NewLine + lich.tenhp + Environment.NewLine + lich.hoten;
                                        workSheet.Row(row).Height = 60;
                                    }
                                }
                                wday = wday.AddDays(1);
                            }
                            row++;
                        }
                        foreach (var item in list.phong)
                        {
                            workSheet.Cells["B" + row].Value = item;
                            wday = toDay;
                            for (int j = 1; j <= 7; j++)
                            {
                                foreach (var lich in list.lichThucHanhs)
                                {
                                    if (lich.phong == item && lich.ngaythuchanh.Equals(wday) && lich.buoi == "Afternoon")
                                    {
                                        workSheet.Cells[row, j + 2].Value = lich.manhomhp + Environment.NewLine + lich.tenhp + Environment.NewLine + lich.hoten;
                                    }
                                }
                                wday = wday.AddDays(1);
                            }
                            row++;
                        }
                        toDay = wday;
                    }
                    excel.Save();
                    stream.Position = 0;
                    return File(stream, "application/octet-stream");
                }
        }
    }
}
