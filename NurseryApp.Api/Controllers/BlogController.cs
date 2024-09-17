using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.Blog;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateDto blogCreateDto)
        {
            return Ok(await _blogService.Create(blogCreateDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _blogService.Get(id));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _blogService.Delete(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _blogService.GetAll());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BlogUpdateDto blogUpdateDto)
        {
            return Ok(await _blogService.Update(id, blogUpdateDto));
        }
    }
}
