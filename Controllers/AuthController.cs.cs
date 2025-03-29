using Microsoft.AspNetCore.Mvc;
using N_Tier.BLL.Services;

namespace N_Tier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password, string role)
        {
            var result = await _authService.RegisterUserAsync(email, password, role);
            if (result)
                return Ok("User registered successfully.");
            return BadRequest("User registration failed.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var token = await _authService.LoginUserAsync(email, password);
            if (token == null)
                return Unauthorized();
            return Ok(new { Token = token });
        }
    }
}
