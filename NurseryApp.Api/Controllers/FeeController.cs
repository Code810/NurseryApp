using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.FeeDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        private readonly IFeeService _feeService;

        public FeeController(IFeeService feeService)
        {
            _feeService = feeService;
        }

        [HttpPost("{groupId}")]
        public async Task<IActionResult> Create(int? groupId, FeeCreateDto feeCreateDto)
        {
            return Ok(await _feeService.CreateFeeAndAssignToStudent(groupId, feeCreateDto));
        }
        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _feeService.Get(id));
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _feeService.GetAll());
        }

        [HttpGet("{value}")]
        public async Task<IActionResult> GetByDateOrStudent(string value)
        {
            if (DateTime.TryParse(value, out DateTime date))
            {
                return Ok(await _feeService.GetAll(date));
            }
            else if (int.TryParse(value, out int studentId))
            {
                return Ok(await _feeService.GetAll(studentId));
            }
            else
            {
                return BadRequest("Invalid parameter");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FeeUpdateDto feeUpdateDto)
        {
            return Ok(await _feeService.Update(id, feeUpdateDto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _feeService.Delete(id));
        }
    }
}
