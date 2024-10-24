using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.TeacherDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromForm] TeacherCreateDto teacherCreateDto)
        {
            var result = await _teacherService.Create(teacherCreateDto);

            return Ok(result);

        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            if (int.TryParse(id, out int intId))
            {
                return Ok(await _teacherService.Get(intId));
            }
            else
            {
                return Ok(await _teacherService.GetByAppUserId(id));
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _teacherService.Delete(id));
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] int? count, [FromQuery] string? text, [FromQuery] int page = 1)
        {
            if (!string.IsNullOrEmpty(text) || count == null)
            {
                return Ok(await _teacherService.GetAllWithSearch(text, page));
            }
            else
            {
                return Ok(await _teacherService.GetAll(count));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> Update(int id, TeacherUpdateDto teacherUpdateDto)
        {
            return Ok(await _teacherService.Update(id, teacherUpdateDto));
        }
    }
}
