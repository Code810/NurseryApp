using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.Auth;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            string scheme = Request.Scheme;
            string host = Request.Host.ToString();

            try
            {
                await _authService.Register(registerDto, scheme, host);
                return Ok(new { Message = "User registered successfully. Please check your email to confirm your account." });
            }
            catch (CustomException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            try
            {
                var result = await _authService.ConfirmEmail(email, token);
                return StatusCode(201);// burda redirect url edecem yada basqa bir yol
            }
            catch (CustomException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            try
            {

                string token = await _authService.Login(loginDto);

                return Ok(new { Token = token, Message = "Login successful" });
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Code, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during login", Error = ex.Message });
            }
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] string email)
        {
            var token = await _authService.ForgetPassword(email);
            return Ok(token);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string email, string token, [FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = await _authService.ResetPasswordAsync(email, token, resetPasswordDto);
            return Ok(result);
        }











        //[HttpPost("create-roles")]
        //public async Task<IActionResult> CreateRoles()
        //{
        //    try
        //    {
        //        await _authService.CreateRoles();
        //        return Ok(new { Message = "Roles created successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred while creating roles.", Error = ex.Message });
        //    }
        //}
    }
}
