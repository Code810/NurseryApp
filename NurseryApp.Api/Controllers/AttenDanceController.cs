using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.AttenDanceDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttenDanceController : ControllerBase
    {
        private readonly IAttenDanceService _attenDanceService;

        public AttenDanceController(IAttenDanceService attenDanceService)
        {
            _attenDanceService = attenDanceService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(AttenDanceCreateDto attenDanceCreateDto)
        {

            return Ok(await _attenDanceService.CreateOrUpdate(attenDanceCreateDto));
        }
        [HttpGet("{date}/{studentId}")]
        public async Task<IActionResult> Get(DateTime date, int? studentId)
        {
            return Ok(await _attenDanceService.Get(date, studentId));
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _attenDanceService.GetAll());
        }

        [HttpGet("{value}")]
        public async Task<IActionResult> Get(string value)
        {
            if (DateTime.TryParse(value, out DateTime date))
            {
                return Ok(await _attenDanceService.GetAll(date));
            }
            else if (int.TryParse(value, out int studentId))
            {
                return Ok(await _attenDanceService.GetAll(studentId));
            }
            else
            {
                return BadRequest("Invalid parameter");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _attenDanceService.Delete(id));
        }
    }
}
