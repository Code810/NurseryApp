using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? senderAppUserId, [FromQuery] string? ReceiverAppUserId)
        {
            var users = await _chatMessageService.GetAllAsync(senderAppUserId, ReceiverAppUserId);
            return Ok(users);
        }
    }
}
