using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NurseryApp.Application.Dtos.Auth;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IParentService _parentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(IAuthService authService, IParentService parentService, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _parentService = parentService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterWithParentDto request)
        {


            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var user = await _authService.Register(request.RegisterDto);
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                if (request.ParentCreateDto != null)
                {
                    request.ParentCreateDto.AppUserId = user.Id;
                    await _parentService.Create(request.ParentCreateDto);
                }

                await _unitOfWork.CommitTransactionAsync();

                return Ok(new
                {
                    Message = "User registered successfully.",
                    Email = user.Email,
                    Token = token
                });
            }
            catch (CustomException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                {
                    return BadRequest(new { Message = "Invalid email or token." });
                }

                var result = await _authService.ConfirmEmail(email, token);
                return StatusCode(201, new { Message = "Email confirmed successfully" });
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

        [HttpGet("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromQuery] string email)
        {
            var token = await _authService.ForgetPassword(email);
            return Ok(new { Token = token });

        }



        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, [FromQuery] string token, [FromBody] ResetPasswordDto resetPasswordDto)
        {

            var result = await _authService.ResetPasswordAsync(email, token, resetPasswordDto);
            return Ok(new { Message = "Password reset successfully." });

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
