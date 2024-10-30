using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.SettingDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromForm] SettingCreateDto settingCreateDto)
        {
            var result = await _settingService.Create(settingCreateDto);
            return Ok(result);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _settingService.Get(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("key/{key}")]
        public async Task<IActionResult> GetByKey(string key)
        {
            var result = await _settingService.Get(key);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _settingService.GetAll();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int id, [FromForm] SettingUpdateDto settingUpdateDto)
        {
            var result = await _settingService.Update(id, settingUpdateDto);
            return Ok(result);
        }
    }
}
