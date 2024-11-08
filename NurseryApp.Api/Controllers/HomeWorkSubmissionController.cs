﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.HomeWorkSubmission;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeWorkSubmissionController : ControllerBase
    {
        private readonly IHomeWorkSubmissionService _homeWorkSubmissionService;

        public HomeWorkSubmissionController(IHomeWorkSubmissionService homeWorkSubmissionService)
        {
            _homeWorkSubmissionService = homeWorkSubmissionService;
        }

        [HttpPost]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> Create(HomeWorkSubmissionCreateDto homeWorkSubmissionCreateDto)
        {
            return Ok(await _homeWorkSubmissionService.Create(homeWorkSubmissionCreateDto));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _homeWorkSubmissionService.Get(id));
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _homeWorkSubmissionService.GetAll());
        }

        [HttpGet("homeWork/{homeWorkId}")]
        public async Task<IActionResult> GetAllByHomeWorkId(int? homeWorkId, int? studentId)
        {
            return Ok(await _homeWorkSubmissionService.GetAll(homeWorkId, studentId));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> Update(int? id, HomeWorkSubmissionUpdateDto homeWorkSubmissionUpdateDto)
        {
            return Ok(await _homeWorkSubmissionService.Update(id, homeWorkSubmissionUpdateDto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _homeWorkSubmissionService.Delete(id));
        }
    }


}
