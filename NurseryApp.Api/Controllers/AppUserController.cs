using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.AppUser;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _appUserService.Get(id);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? searchText)
        {
            var users = await _appUserService.GetAll(searchText);
            return Ok(users);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] AppUserUpdateDto appUserUpdateDto)
        {
            var result = await _appUserService.Update(id, appUserUpdateDto);
            return Ok(new { Message = "User updated successfully", Result = result });
        }

        [HttpPost("change-status/{id}")]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            var result = await _appUserService.ChangeStatus(id);
            var statusMessage = result == 1 ? "User blocked" : "User unblocked";
            return Ok(new { Message = statusMessage });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _appUserService.Delete(id);
            return Ok(new { Message = "User deleted successfully", Result = result });
        }
    }
}
