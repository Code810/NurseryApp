using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.GroupDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _groupService.GetAll());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, GroupUpdateDto groupUpdateDto)
        {
            return Ok(await _groupService.Update(id, groupUpdateDto));
        }
    }
}
