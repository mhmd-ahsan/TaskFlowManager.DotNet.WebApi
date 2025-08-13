using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlowManager.Api.Models;
using TaskFlowManager.Api.Services.Interfaces;
using TaskFlowManager.Api.Dtos.Requests;

namespace TaskFlowManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAllUsersAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST: api/User/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Role = request.Role
            };

            var response = await _userService.RegisterUserAsync(user, request.Password);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // POST: api/User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _userService.LoginUserAsync(request.Email, request.Password);
            return response.Success ? Ok(response) : Unauthorized(response);
        }

        // PUT: api/User/activate/{id}?isActive=true
        [HttpPut("activate/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateDeactivate(int id, [FromQuery] bool isActive)
        {
            var response = await _userService.ActivateDeactivateUserAsync(id, isActive);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
