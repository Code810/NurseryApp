using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.Comment;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentCreateDto commentCreateDto)
        {
            var commentId = await _commentService.Create(commentCreateDto);

            return Ok(await _commentService.Get(commentId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _commentService.Get(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? blogId)
        {
            return Ok(await _commentService.GetAll(blogId));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CommentUpdateDto commentUpdateDto)
        {
            return Ok(await _commentService.Update(id, commentUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _commentService.Delete(id));
        }
    }
}
