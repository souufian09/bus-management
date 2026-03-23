using AuthService.DTOs;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginDto loginDto)
        {
            try
            {

                var response = await _authService
                    .Login(loginDto);

                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterDto registerDto)
        {
            try
            {

                var response = await _authService
                    .Register(registerDto);

                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

    }
}
