using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.ParentDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ParentCreateDto parentCreateDto)
        {
            return Ok(await _parentService.Create(parentCreateDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _parentService.Get(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _parentService.GetAll());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ParentUpdateDto parentUpdateDto)
        {
            return Ok(await _parentService.Update(id, parentUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _parentService.Delete(id));
        }
    }
}
