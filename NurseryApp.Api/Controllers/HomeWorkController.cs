using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.HomeWork;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeWorkController : ControllerBase
    {
        private readonly IHomeWorkService _homeWorkService;

        public HomeWorkController(IHomeWorkService homeWorkService)
        {
            _homeWorkService = homeWorkService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeWorkCreateDto homeWorkCreateDto)
        {

            return Ok(await _homeWorkService.Create(homeWorkCreateDto));
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? id, [FromQuery] int? groupId)
        {
            if (id.HasValue)
            {
                return Ok(await _homeWorkService.Get(id));
            }
            else if (groupId.HasValue)
            {
                return Ok(await _homeWorkService.GetAll(groupId));
            }
            else
            {
                return Ok(await _homeWorkService.GetAll());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, HomeWorkUpdateDto homeWorkUpdateDto)
        {
            return Ok(await _homeWorkService.Update(id, homeWorkUpdateDto));
        }
    }
}
