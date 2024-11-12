using ApiWithEventGrid.Models;
using ApiWithEventGrid.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserRegistrationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly EventGridService _eventGridService;

        public UserController(EventGridService eventGridService)
        {
            _eventGridService = eventGridService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            await _eventGridService.PublishUserEventAsync(user);
            return Ok("User registered and event sent to Event Grid.");
        }
    }
}
