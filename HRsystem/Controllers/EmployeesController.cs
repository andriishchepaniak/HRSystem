using HR.DAL.Models;
using HR.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRsystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EmployeesController : ControllerBase
    {
        private readonly IUserService _userService;
        public EmployeesController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("GetAll")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.ShowAll());
        }

        [Route("getByPages")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int offset = 0, int count = 10)
        {
            return Ok(await _userService.ShowAll(offset, count));
        }

        [HttpGet("{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string email)
        {
            return Ok(await _userService.FindByEmail(email));
        }
    }
}
