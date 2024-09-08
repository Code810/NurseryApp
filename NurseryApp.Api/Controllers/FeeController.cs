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

        [HttpPost]
        public async Task<IActionResult> Create(FeeCreateDto feeCreateDto)
        {
            //studentin grupu elave olunmalidir............................
            return Ok(await _feeService.Create(feeCreateDto));
        }
        [HttpGet("{date}/{studentId}")]
        public async Task<IActionResult> Get(DateTime date, int studentId)
        {
            return Ok(await _feeService.Get(date, studentId));
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _feeService.GetAll());
        }

        [HttpGet("{value}")]
        public async Task<IActionResult> Get(string value)
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
    }
}
