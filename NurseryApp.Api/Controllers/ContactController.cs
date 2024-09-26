using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.Contact;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] string? text, [FromQuery] int page = 1)
        {
            return Ok(await _contactService.GetAllWithSearch(text, page));
        }
    }
}
