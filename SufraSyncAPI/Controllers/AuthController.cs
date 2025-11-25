using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SufraSync.Controllers;
using SufraSyncAPI.Models.DTOs.AuthDtos;
using SufraSyncAPI.Services.Interfaces;

namespace SufraSyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var result =await _authService.RegisterAsync(request);
            if (!result.Success)
                return BadRequestError<AuthResponseDto>(result.Message);
            return Success(result,"User registerd Successfully");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result.Success)
                return BadRequestError<AuthResponseDto>(result.Message);

            return Success(result, "Login successful");
        }
    }
}