using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.GroupDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupCreateDto groupCreateDto)
        {

            return Ok(await _groupService.Create(groupCreateDto));
        }
        [HttpGet("{value}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string value)
        {
            if (int.TryParse(value, out int id))
            {
                return Ok(await _groupService.Get(id));
            }
            else
            {
                return Ok(await _groupService.Get(value));
            }
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] int? count, [FromQuery] string? text, [FromQuery] int page = 1)
        {
            if (!string.IsNullOrEmpty(text) || count == null)
            {
                return Ok(await _groupService.GetAllWithSearch(text, page));
            }
            else
            {
                return Ok(await _groupService.GetAll(count));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, GroupUpdateDto groupUpdateDto)
        {
            return Ok(await _groupService.Update(id, groupUpdateDto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _groupService.Delete(id));
        }
    }
}
