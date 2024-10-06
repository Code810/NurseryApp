using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.StudentDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = "parent")]
        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateDto studentCreateDto)
        {
            return Ok(await _studentService.Create(studentCreateDto));
        }

        [Authorize(Roles = "parent")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _studentService.Get(id));

        }

        [Authorize(Roles = "parent")]
        [HttpGet]
        public async Task<IActionResult> GetAll(int? parnetId)
        {
            return Ok(await _studentService.GetAll(parnetId));
        }


        [Authorize(Roles = "parent")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _studentService.Delete(id));
        }

        [Authorize(Roles = "parent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] StudentUpdateDto studentUpdateDto)
        {
            return Ok(await _studentService.Update(id, studentUpdateDto));
        }
    }
}
