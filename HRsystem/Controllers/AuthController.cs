using HR.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRsystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var res = await _authService.Login(username, password);

            return Ok(res);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string username, string password)
        {
            var res = await _authService.RegisterNewUser(username, password);

            return Ok(res);
        }
    }
}
