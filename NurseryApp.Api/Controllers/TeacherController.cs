using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.TeacherDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherCreateDto teacherCreateDto)
        {
            return Ok(await _teacherService.Create(teacherCreateDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _teacherService.Get(id));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _teacherService.Delete(id));
        }

        [HttpGet("all")]
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
        public async Task<IActionResult> Update(int id, TeacherUpdateDto teacherUpdateDto)
        {
            return Ok(await _teacherService.Update(id, teacherUpdateDto));
        }
    }
}
