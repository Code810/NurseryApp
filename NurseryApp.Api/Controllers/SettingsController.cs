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
        public async Task<IActionResult> Create(SettingCreateDto settingCreateDto)
        {
            return Ok(await _settingService.Create(settingCreateDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _settingService.Get(id));

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _settingService.GetAll());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SettingUpdateDto settingUpdateDto)
        {
            return Ok(await _settingService.Update(id, settingUpdateDto));
        }
    }
}
