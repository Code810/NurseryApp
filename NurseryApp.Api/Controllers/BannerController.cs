using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.Banner;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(BannerCreateDto bannerCreateDto)
        {
            return Ok(await _bannerService.Create(bannerCreateDto));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bannerService.Get());
        }

        [HttpPut]
        public async Task<IActionResult> Update(BannerUpdateDto bannerUpdateDto)
        {
            return Ok(await _bannerService.Update(bannerUpdateDto));
        }
    }
}
