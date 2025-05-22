using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Services;
using ProjectEmploee.Service;


namespace ProjectEmploee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SystemAdministrator, Admin, Emploee")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendanceService attendanceService, IMapper mapper )
        {
            _attendanceService = attendanceService;
            _mapper = mapper;
        }

   
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var AttendanceAll = await _attendanceService.GetAllAsync();
            return Ok(AttendanceAll);
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var attendanceById = await _attendanceService.GetByIdAsync(id);
            return Ok(attendanceById);
        }

    
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] AttendanceDTO a)
           {var attendance = _mapper.Map<Attendance>(a);
            var newAttendance = await _attendanceService.PostAsync(attendance);
            return Ok(newAttendance);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] AttendanceDTO a)
        {
            var attendance = _mapper.Map<Attendance>(a);
            var attendancePut = await _attendanceService.PutAsync(id, attendance);
            return Ok(attendancePut);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _attendanceService.DeleteAsync(id);
            return Ok();
        }
    }
}
