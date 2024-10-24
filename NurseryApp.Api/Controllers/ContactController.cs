using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.Contact;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(ContactCreateDto contactCreateDto)
        {
            return Ok(await _contactService.Create(contactCreateDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _contactService.Get(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _contactService.Delete(id));
        }

        [HttpDelete("bulk-delete")]
        public async Task DeleteMultiple(List<int> ids)
        {
            await _contactService.DeleteMultiple(ids);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] string? text, [FromQuery] int page = 1)
        {
            return Ok(await _contactService.GetAllWithSearch(text, page));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            return Ok(await _contactService.Update(id));
        }
    }
}
